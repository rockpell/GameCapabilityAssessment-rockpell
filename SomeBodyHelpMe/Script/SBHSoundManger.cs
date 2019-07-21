using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHSoundManger : MonoBehaviour {

    public static SBHSoundManger Instance;

    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip screamAudio;
    [SerializeField] AudioClip glassAudio;

    private AudioSource myAudioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start () {
        myAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayShootAudio()
    {
        myAudioSource.PlayOneShot(shootAudio);
    }

    public void PlayScreamAudio()
    {
        myAudioSource.PlayOneShot(screamAudio);
    }

    public void PlayGlassAudio()
    {
        myAudioSource.PlayOneShot(glassAudio);
    }
}
