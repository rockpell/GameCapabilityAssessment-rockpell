using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour {

	[SerializeField]
	private GaugeImageController gic;

	private TextMesh textmesh;

	private void Start()
	{
		textmesh = transform.GetChild(0).GetComponent<TextMesh>();
	}
	// Update is called once per frame
	void Update ()
	{
		string tmp = string.Format("{0:F1}", Mathf.Floor(gic.GetValue() * 10) / 10);
		textmesh.text = tmp;
	}
}
