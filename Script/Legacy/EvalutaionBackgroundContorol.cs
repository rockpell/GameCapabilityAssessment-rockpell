using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalutaionBackgroundContorol : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(1, 0, 0);
        if(transform.localPosition.x >= 1071.5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 1071.5f*2, 0, 0);
        }
	}
}
