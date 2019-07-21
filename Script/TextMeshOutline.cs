using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshOutline : MonoBehaviour {

	public float pixelSize = 0.05f;
	public Color outlineColor = Color.black;
	//public bool resolutionDependant = false;
	//public int doubleResolution = 1024;

	private TextMesh textMesh;
	private MeshRenderer meshRenderer;
	private GameObject[] outlines;

	void Start()
	{
		outlines = new GameObject[8];
		textMesh = GetComponent<TextMesh>();
		meshRenderer = GetComponent<MeshRenderer>();

		for (int i = 0; i < 8; i++)
		{
			GameObject outline = new GameObject("outline", typeof(TextMesh));
			outlines[i] = outline;
			outline.transform.parent = transform;
			outline.transform.localScale = new Vector3(1, 1, 1);

			MeshRenderer otherMeshRenderer = outline.GetComponent<MeshRenderer>();
			otherMeshRenderer.material = new Material(meshRenderer.material);
			//otherMeshRenderer.castShadows = false;
			otherMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			otherMeshRenderer.receiveShadows = false;
			otherMeshRenderer.sortingLayerID = meshRenderer.sortingLayerID;
			otherMeshRenderer.sortingLayerName = meshRenderer.sortingLayerName;
			otherMeshRenderer.sortingOrder = meshRenderer.sortingOrder;
		}
	}

	void LateUpdate()
	{
		//Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

		outlineColor.a = textMesh.color.a * textMesh.color.a;

		// copy attributes
		for (int i = 0; i < outlines.Length; i++)
		{

			TextMesh other = outlines[i].GetComponent<TextMesh>();
			other.color = outlineColor;
			other.text = textMesh.text;
			other.alignment = textMesh.alignment;
			other.anchor = textMesh.anchor;
			other.characterSize = textMesh.characterSize;
			other.font = textMesh.font;
			other.fontSize = textMesh.fontSize;
			other.fontStyle = textMesh.fontStyle;
			other.richText = textMesh.richText;
			other.tabSize = textMesh.tabSize;
			other.lineSpacing = textMesh.lineSpacing;
			other.offsetZ = textMesh.offsetZ;

			//bool doublePixel = resolutionDependant && (Screen.width > doubleResolution || Screen.height > doubleResolution);
			//Vector3 pixelOffset = GetOffset(i) * (doublePixel ? 2.0f * pixelSize : pixelSize);
			//Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint + pixelOffset);
			other.transform.localPosition = (GetOffset(i) * pixelSize) + (Vector3.forward * 0.01f * pixelSize);

			MeshRenderer otherMeshRenderer = outlines[i].GetComponent<MeshRenderer>();
			otherMeshRenderer.sortingLayerID = meshRenderer.sortingLayerID;
			otherMeshRenderer.sortingLayerName = meshRenderer.sortingLayerName;
			otherMeshRenderer.sortingOrder = meshRenderer.sortingOrder;
		}
	}

	Vector3 GetOffset(int i)
	{
		switch (i % 8)
		{
			case 0: return new Vector3(0, Mathf.Sqrt(2), 0);
			case 1: return new Vector3(1, 1, 0);
			case 2: return new Vector3(Mathf.Sqrt(2), 0, 0);
			case 3: return new Vector3(1, -1, 0);
			case 4: return new Vector3(0, -Mathf.Sqrt(2), 0);
			case 5: return new Vector3(-1, -1, 0);
			case 6: return new Vector3(-Mathf.Sqrt(2), 0, 0);
			case 7: return new Vector3(-1, 1, 0);
			default: return Vector3.zero;
		}
	}
	private void OnDisable()
	{
		if (outlines != null)
		{
			foreach (GameObject g in outlines)
			{
				if (g != null)
					g.SetActive(false);
			}
		}
	}
	private void OnEnable()
	{
		if (outlines != null)
		{
			foreach (GameObject g in outlines)
			{
				if (g != null)
					g.SetActive(true);
			}
		}
	}
}
