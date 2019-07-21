using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightController : MonoBehaviour {

	public GameObject[] testList;

	[SerializeField]
	private GameObject uMask;
	[SerializeField]
	private RectTransform canvas;
	[SerializeField]
	private GameObject mask;
	[SerializeField]
	private Dictionary<GameObject, GameObject> tracingObject;

	public void SetActive(bool isActivate)
	{
		mask.SetActive(isActivate);
	}
	public void StartTracing(params GameObject[] target)
	{
		foreach(GameObject item in target)
		{
			if(!tracingObject.ContainsKey(item))
			{
				GameObject tmp = Instantiate(uMask, mask.transform);
				tmp.transform.SetAsFirstSibling();
				tracingObject.Add(item, tmp);
			}
		}
	}
	public void StopTracing(params GameObject[] target)
	{
		foreach(GameObject item in target)
		{
			if(tracingObject.ContainsKey(item))
			{
				Destroy(tracingObject[item]);
				tracingObject.Remove(item);
			}
		}
	}
	public void StopAllTracing()
	{
		tracingObject.Clear();
	}

	private void Awake()
	{
		tracingObject = new Dictionary<GameObject, GameObject>();
	}
	private void LateUpdate()
	{
		foreach(KeyValuePair<GameObject, GameObject> item in tracingObject)
		{
			if(item.Key.GetComponent<RectTransform>())
			{
				TraceUI(item);
			}
			else
			{
				TraceWorldObj(item);
			}
		}
	}

	private void TraceUI(KeyValuePair<GameObject,GameObject> target)
	{
		RectTransform trace = target.Value.GetComponent<RectTransform>();
		RectTransform origin = target.Key.GetComponent<RectTransform>();

		//활성상태 통일
		target.Value.SetActive(target.Key.activeInHierarchy);

		//이미지 통일
		trace.GetComponent<Image>().sprite = origin.GetComponent<Image>().sprite;
		//위치 및 크기 통일
		trace.transform.position = origin.transform.position;
		trace.sizeDelta = origin.sizeDelta;
		trace.rotation = origin.rotation;
		trace.localScale = origin.localScale;
	}
	private void TraceWorldObj(KeyValuePair<GameObject, GameObject> target)
	{
		RectTransform trace = target.Value.GetComponent<RectTransform>();
		Transform origin = target.Key.transform;

		//활성상태 통일
		target.Value.SetActive(target.Key.activeInHierarchy);

		//이미지 통일
		trace.GetComponent<Image>().sprite = origin.GetComponent<SpriteRenderer>().sprite;

		//위치 및 크기 통일
		trace.anchoredPosition = ConvertWorldToCanvas(origin.position);
		Vector2 size = ConvertWorldToCanvas( origin.GetComponent<SpriteRenderer>().bounds.size + Camera.main.transform.position);
		trace.sizeDelta = size;

	}
	private Vector2 ConvertWorldToCanvas(Vector3 pos)
	{
		Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(pos);
		return new Vector2(
		((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
		((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));
	}

	[ContextMenu("TestON")]
	private void TestON()
	{
		StartTracing(testList);
	}
	[ContextMenu("TestOFF")]
	private void TestOFF()
	{
		StopTracing(testList);
	}
}
