using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTHBeatManager : MonoBehaviour {

	[SerializeField]
	private float oneBeatTime;
	public float OneBeatTime { get { return oneBeatTime;  } set { if(value > 0) oneBeatTime = value; } }

	private float timer;
	private List<LTHBeat> beatList;
	private List<LTHBeat> tmpList;
	private List<LTHBeat> removeList;

	public void AddToList(LTHBeat target)
	{
		tmpList.Add(target);
	}

	public void RemoveToList(LTHBeat target)
	{
		removeList.Add(target);
	}

	private void Awake()
	{
		timer = 0;
		beatList = new List<LTHBeat>();
		tmpList = new List<LTHBeat>();
		removeList = new List<LTHBeat>();
	}

	private float superTimer;

	private void Update()
	{
		if(superTimer < 1)
		{
			superTimer += Time.deltaTime;
			return;
		}

		timer += Time.deltaTime;
		if(timer >= oneBeatTime)
		{
			timer -= oneBeatTime;
			BeatLoop();
		}
	}

	private void BeatLoop()
	{
		foreach( LTHBeat item in beatList)
		{
			item.Beat();
		}
		//루프 도중에 리스트 내용이 변경되는 것을 막기 위해
		//루프가 종료되면 미뤄뒀던 작업을 한다.
		beatList.AddRange(tmpList);
		tmpList.Clear();
		foreach(LTHBeat item in removeList)
		{
			beatList.Remove(item);
		}
		removeList.Clear();
		
	}
}
