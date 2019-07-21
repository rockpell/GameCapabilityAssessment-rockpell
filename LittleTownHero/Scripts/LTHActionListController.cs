using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTH;
public class LTHActionListController : MonoBehaviour {

	public GameObject[] actionProto; 
	public ActionBeat[] actionList;
	public float distance;

	private List<Transform> childList;

	private void Awake()
	{
		childList = new List<Transform>();
	}

	public void SetImages(ActionBeat[] list = null)
	{
		if(list != null)
		{
			actionList = list;
		}
		/* 이미지 생성 */
		foreach(ActionBeat item in actionList)
		{
			GameObject tmp = null;
			switch(item.action)
			{
				case Action.Up:
					tmp = Instantiate(actionProto[0],transform);
					break;
				case Action.Down:
					tmp = Instantiate(actionProto[1], transform);
					break;
				case Action.TwoSide:
					tmp = Instantiate(actionProto[2], transform);
					break;
				case Action.Attack:
					tmp = Instantiate(actionProto[3], transform);
					break;
				case Action.None:
					tmp = Instantiate(actionProto[4], transform);
					break;
			}
			if (tmp != null)
			{
				tmp.transform.localPosition = Vector3.zero;
				childList.Add(tmp.transform);
			}
		}
		/* 자리 정렬 */
		int childCount = childList.Count;
		if(childCount > 9)
		{
			for (int i = 0; i < 8; ++i)
			{
				childList[i].localPosition = new Vector3((i - ((8 - 1.0f) / 2f)) * distance, 0, 0);
			}
			for(int i = 8; i < childCount; ++i)
			{
				childList[i].localPosition = new Vector3(((i-8) - (((childCount - 8) - 1.0f) / 2f)) * distance, -distance, 0);
			}
		}
		else
		{
			for (int i = 0; i < childCount; ++i)
			{
				childList[i].localPosition = new Vector3((i - ((childCount - 1.0f) / 2f)) * distance, 0, 0);
			}
		}
		/* child disable */
		ChildOff();
	}
	public void Open(int pIndex = -1)
	{
		int index = pIndex;
		if(index < 0)
		{
			for(int i = 0; i < transform.childCount; ++i)
			{
				if(!transform.GetChild(i).gameObject.activeSelf)
				{
					index = i;
					break;
				}
			}
		}
		if (index < transform.childCount && index >= 0)
			transform.GetChild(index).gameObject.SetActive(true);
	}
	public void Clear()
	{
		//child 제거
		for(int i = childList.Count - 1; i >= 0; --i)
		{
			Destroy(childList[i].gameObject);
		}
		childList.Clear();

	}

	private void ChildOff()
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}


	[ContextMenu("Test")]
	private void Test()
	{
		ActionBeat[] test = new ActionBeat[8];
		for(int i = 0; i < 8; ++i)
		{
			test[i] = new ActionBeat(1, (Action)((i % 4) + 1));
		}
		SetImages(test);
	}
	[ContextMenu("OpenTest")]
	private void OpenTest()
	{
		Open();
	}
}
