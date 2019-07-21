using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSoundController : MonoBehaviour {

    public AudioClip SoundMouse;
    public AudioClip SoundEat;
    AudioSource myAudio;
    public static ChickenSoundController instance;
    private void Awake()
    {
        if (ChickenSoundController.instance == null)
        {
            ChickenSoundController.instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        myAudio = this.gameObject.GetComponent<AudioSource>();
	}
	public void playMouseSound()
    {
        myAudio.PlayOneShot(SoundMouse);
    }

    public void playEatSound()
    {
        myAudio.PlayOneShot(SoundEat);
    }
	// Update is called once per frame
	void Update () {
        
	}
}
