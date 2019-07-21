using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TotalGaugeImageController : GaugeImageController
{
	public MeshFilter ogackMesh;
	public LineRenderer ogackLine;
	public Transform[] ogackPoints;
	public TextMesh[] scoreTexts;

	[SerializeField]
	[Tooltip("오각형이 자라는 시간을 나타낸다. 항목 최대 점수에 따라 duration의 30%에서 100%사이의 시간이 걸린다. 최대점수가 작으면 결과가 빨리 끝이난다.")]
	private float duration = 3f;
	[SerializeField]
	[Tooltip("오각형의 사이즈를 나타냅니다.")]
	private float size = 1;

	

	//큰수는 넣으면 안되는데 이걸 우째 말해줄 방법이 없네
	static private int IntPow(int num, int p)
	{
		int result = 1;
		for (int i = 0; i < p; ++i)
			result *= num;
		return result;
	}
	private int[] SeparateValue(int value)
	{
		int[] values = new int[5];
		for (int i = 0; i < values.Length; ++i)
		{
			values[values.Length - 1 - i] = (value / IntPow(10, i * 2)) % 100;
		}
		return values;
	}
	private int ZipValue(int[] values)
	{
		int value = 0;
		for (int i = 0; i < values.Length; ++i)
		{
			value *= 100;
			value += values[i];
		}
		return value;
	}

	public float GetDuration() { return duration; }

	private void Awake()
	{
	
	}

	[ContextMenu("Test")]
	private void Test()
	{
		StartCoroutine(ImageActivate(1512100911));
	}
	

	//값은 조준,집중,순발,리듬,사고를 자리수를 기준으로 나누어서 사용
	//ex) 조준 8, 집중, 12, 순발 10, 리듬 7, 사고 9일때 값은 812100709
	public override IEnumerator ImageActivate(int value)
	{
		//0:조준, 1:집중, 2:순발, 3:리듬, 4:사고
		float[] nowValues = { 0, 0, 0, 0, 0 };
		Vector3[] nowPositions = new Vector3[5];
		int[] targets = SeparateValue(value);


		//항목 최대 점수에 따라 duration의 30%에서 100%사이의 값을 가진다.
		//최대점수가 작으면 결과가 빨리 나온다.
		float dur = (duration * 0.3f) + (duration * 0.7f * (targets.Max() / 15f));
		
		Mesh m = new Mesh();
		m.vertices = new Vector3[5];
		m.triangles = new int[]{ 0, 1, 2, 0, 2, 3, 0, 3, 4 };

		float startTime = Time.time;
		while(true)
		{
			float t = (Time.time - startTime) / dur;
			if (t > 1)
				t = 1;
			
			//Set nowValue
			for(int i = 0; i < targets.Length; ++i)
			{
				nowValues[i] = Mathf.SmoothStep(0, targets.Max(), t);
				if (nowValues[i] > targets[i])
					nowValues[i] = targets[i];
			}
			nowValue = nowValues.Sum();

			//SetPosition
			for (int i = 0; i < targets.Length; ++i)
			{
				float rad = Mathf.Deg2Rad * (-i * 72 + 90);
				float v = (nowValues[i] / 15) * size;
				nowPositions[i] = new Vector3(Mathf.Cos(rad) * v, Mathf.Sin(rad) * v, 0);
			}
			//point
			for (int i = 0; i < targets.Length; ++i)
			{
				ogackPoints[i].localPosition = nowPositions[i];
			}
			//mesh
			m.vertices = nowPositions;
			ogackMesh.mesh = m;
			//line
			for (int i = 0; i < targets.Length; ++i)
			{
				ogackLine.SetPosition(i, nowPositions[i]);
			}
			//text
			for( int i =0; i < scoreTexts.Length; ++i)
			{
				//scoreTexts[i].text = string.Format("{0:F1}", Mathf.Floor(nowValues[i] * 10) / 10);
				scoreTexts[i].text = Mathf.Floor(nowValues[i]).ToString();
			}


			if (t == 1)
				break;
			yield return null;
		}
	}
}