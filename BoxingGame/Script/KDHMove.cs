using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDHMove : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
	}
	
  public void Speed(double newspeed)
    {
        speed = (float)newspeed;
    }

	// Update is called once per frame
	public void Update () {
        transform.Translate(translation: Vector2.left * Time.deltaTime * speed);
    }
   
}
