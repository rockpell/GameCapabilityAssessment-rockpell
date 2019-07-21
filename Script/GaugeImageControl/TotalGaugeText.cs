using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGaugeText : MonoBehaviour {

	public TextMesh target;
	[SerializeField]
	private int orderInLayer;
	[SerializeField]
	private float moveSpeedRate;
	private float moveTime;
	private bool isOuter;

	private bool isRunning;

	// Use this for initialization
	void Awake ()
	{
		gameObject.GetComponent<MeshRenderer>().sortingOrder = orderInLayer;
	}
	private void Start()
	{
		moveTime = GetComponentInParent<TotalGaugeImageController>().GetDuration() * moveSpeedRate;
		isOuter = true;
		isRunning = false;
	}
	// Update is called once per frame
	void Update ()
	{
		if(float.Parse(target.text) >= 9.1f && isOuter && !isRunning)
		{
			StartCoroutine(TextMove());
			isOuter = false;
		}
		else if(float.Parse(target.text) <= 8.9f && !isOuter && !isRunning)
		{
			StartCoroutine(TextMove());
			isOuter = true;
		}
	}
	IEnumerator TextMove()
	{
		isRunning = true;
		float timer = 0;
		Vector3 startPos = transform.localPosition;
		while(timer < moveTime)
		{
			timer += Time.deltaTime;

			float rate = timer / moveTime;
			if (rate > 1)
				rate = 1;
			transform.localPosition = Vector3.Lerp(startPos,-startPos, Mathf.SmoothStep(0, 1, rate));
			yield return null;
		}

		yield return null;
		isRunning = false;
	}
}
