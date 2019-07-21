using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVSetter : MonoBehaviour {

	public float density;
	public bool xFilp;
	public bool yFilp;

	private void Awake()
	{
		Mesh m = GetComponent<MeshFilter>().mesh;

		float maxX, minX, maxY, minY;

		//버텍스 최고 최소값알기
		maxX = m.vertices[0].x;
		minX = m.vertices[0].x;
		maxY = m.vertices[0].y;
		minY = m.vertices[0].y;
		foreach (Vector3 v in m.vertices)
		{
			maxX = Mathf.Max(v.x, maxX);
			minX = Mathf.Min(v.x, minX);
			maxY = Mathf.Max(v.y, maxY);
			minY = Mathf.Min(v.y, minY);
		}
		Vector2[] newUV = new Vector2[m.vertices.Length];

		//UV값 설정하기
		for (int i = 0; i < m.vertices.Length; ++i)
		{
			//Debug.Log(m.vertices[i]);
			float ratio = (maxY - minY) / (maxX - minX);
			newUV[i] = new Vector2((m.vertices[i].x - minX) / (maxX - minX), (m.vertices[i].y - minY) * ratio / (maxY - minY));
			if (xFilp) newUV[i] = new Vector2(newUV[i].x * -1, newUV[i].y);
			if (yFilp) newUV[i] = new Vector2(newUV[i].x, newUV[i].y * -1);
			//Debug.Log(newUV[i]);
		}
		m.uv = newUV;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
