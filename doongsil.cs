using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doongsil : MonoBehaviour {

	public Transform pivot;	//중심
	[Range(0.01f,60f)]
	public float cycle;		//주기
	public float amplitude;	//진폭

	private float timer;

	// Use this for initialization
	void Start ()
	{
		timer = 0;
		if (cycle <= 0) cycle = 1;

	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		transform.position = pivot.position + Vector3.up * Mathf.Sin((timer/cycle) * (Mathf.PI*2)) * amplitude;
	}
}
