using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTHSoundManager : MonoBehaviour , LTHBeat{

	public int soundBeat;

	[SerializeField]
	private LTHBeatManager beatManager;
	[SerializeField]
	private AudioSource sound;


	private int beatCount;
	
	private void Awake()
	{
		beatCount = 0;
	}
	private void Start()
	{
		beatManager.AddToList(GetComponent<LTHSoundManager>());
	}

	public void Beat()
	{
		if(beatCount == 0)
		{
			sound.Play();
		}
		++beatCount;
		if (beatCount >= soundBeat)
			beatCount = 0;
	}
}
