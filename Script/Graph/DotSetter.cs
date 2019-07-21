using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSetter : MonoBehaviour {

	[SerializeField]
	private TextMesh text;

	public Vector3 posToPivot;
	public float fontSize;
	public float outlineSize;
	public Color textColor;
	public Color outlineColor;

	public void SetScore(string value)
	{
		Setting();
		text.text = value;
	}

	public void Setting()
	{
		text.transform.localPosition = posToPivot;
		text.characterSize = fontSize;
		text.color = textColor;
		text.GetComponent<TextMeshOutline>().pixelSize = outlineSize;
		text.GetComponent<TextMeshOutline>().outlineColor = outlineColor;

	}
}
