using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph : MonoBehaviour {

	[Header("Value Setting")]
	[SerializeField]
	private float[] values;

	[Header("Graph Shape Settings")]
	public float lineLength = 10;
	public float minPos = 1;
	public float maxPos = 15;
	public float minValue = 1;
	public float maxValue = 15;
	[Header("Animate Settings")]
	public float time = 3;

	[Header("Dependency Settings")]
	[SerializeField]
	private Transform dotParent;
	[SerializeField]
	private GameObject dot;
	[SerializeField]
	public Transform headImage;

	private LineRenderer line;
	private Vector3[] valuePos;
	private Coroutine routine;

	private void Awake()
	{
		line = GetComponentInChildren<LineRenderer>();
		valuePos = new Vector3[line.GetPositions(new Vector3[line.positionCount])];
	}

	public void SetValues(float[] pValues)
	{
		values = pValues;
	}

	public void DrawGraph(bool isAnimated = true, float[] pValues = null)
	{
		if(pValues != null)
		{
			SetValues(pValues);
		}

		if (routine != null)
			StopCoroutine(routine);

		if(isAnimated)
		{
			routine = StartCoroutine(DrawGraphAnimate());
		}
		else
		{
            DrawLineGraph(values.Length - 1);
		}
	}
	private IEnumerator DrawGraphAnimate()
	{
		float timer = 0;
		
		while(timer < time)
		{
			float rate = Mathf.SmoothStep(0, values.Length - 1, timer / time);

			DrawLineGraph(rate);

			timer += Time.deltaTime;
			if (timer > time)
				timer = time;
			yield return null;
		}

		DrawLineGraph(values.Length - 1);
	}
	private void DrawLineGraph(float rate) //0에서 values.Length -1까지
	{
		//vector3 value setting
		SetValuePositions(rate);

		//Draw Lint and Dot
		DrawLine(rate);
		SetDot(rate);
		SetHeadImage(rate);
	}

	private void DrawLine(float rate)  //0에서 values.Length -1 까지
	{

		//apply valuePosition to LineRenderer
		line.positionCount = valuePos.Length;
		line.SetPositions(valuePos);
	}
	private void SetValuePositions(float pRate) //0에서 values.Length -1 까지
	{
		Vector3[] newPos;

		newPos = new Vector3[Mathf.CeilToInt(pRate) + 1];

		for(int i = 0; i < newPos.Length; ++i)
		{
			if(pRate - i + 1 > 1)
				newPos[i] = CalculatePosition(i, 1);
			else
				newPos[i] = CalculatePosition(i, pRate - i + 1);
		}

		valuePos = newPos;
	}
	private Vector3 CalculatePosition(int index, float rate)
	{
		float y = Mathf.Lerp(minPos,maxPos,CalcPercent(minValue,maxValue,values[index]));
		if (index == 0)
		{
			return new Vector3(0, y);
		}
		float prevY = Mathf.Lerp(minPos, maxPos, CalcPercent(minValue, maxValue, values[index - 1]));

		//Debug.Log(lineLength * ((index + rate - 1) / (values.Length - 1f)));

		return new Vector3((lineLength * (index + rate - 1) / (values.Length - 1f)), Mathf.Lerp(prevY, y, rate));
	}
	private float CalcPercent(float min, float max, float value)
	{
		if (max - min == 0)
			return 1;

		return (value - min) / (max - min);
	}
	private void SetDot(float pRate)
	{
		if (dotParent == null)
			return;

		if(Mathf.CeilToInt(pRate) + 1 > dotParent.childCount)
		{
			int amount = Mathf.CeilToInt(pRate) + 1 - dotParent.childCount;
			for(int i = 0; i < amount; ++i)
			{
				GameObject tmp = Instantiate(dot, dotParent);
				tmp.SetActive(false);
			}
		}
		for(int i = 0; i < dotParent.childCount; ++i)
		{
			dotParent.GetChild(i).gameObject.SetActive(false);
			dotParent.GetChild(i).gameObject.GetComponent<DotSetter>().SetScore(values[i].ToString());
		}
		for(int i = 0; i < Mathf.FloorToInt(pRate) + 1; ++i)
		{
			dotParent.GetChild(i).localPosition = valuePos[i];
			dotParent.GetChild(i).gameObject.SetActive(true);
		}
	}
	private void SetHeadImage(float pRate)
	{
		if (headImage == null)
			return;

		headImage.localPosition = valuePos[valuePos.Length - 1];
	}

	[ContextMenu("Test")]
	private void Test()
	{
		//TEST CODE
		values = new float[9] { 5, 4, 5, 7, 8, 9, 8, 7, 8 };
		DrawGraph();
	}
}
