using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHParticleContorl : MonoBehaviour {

    private ParticleSystem myParticle;

	// Use this for initialization
	void Start () {
        myParticle = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (myParticle.isStopped)
        {
            Destroy(this.gameObject);
        }
	}
}
