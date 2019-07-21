using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATParticleControl : MonoBehaviour {

    public AudioClip _sound;
    private void Start()
    {
    }
    void Update ()
    {
		if(GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(this.gameObject);
        }
	}
}
