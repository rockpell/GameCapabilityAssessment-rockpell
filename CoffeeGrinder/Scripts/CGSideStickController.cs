using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGSideStickController : MonoBehaviour {

    private GameObject _spinStick;

	void Start () {
        _spinStick = GameObject.Find("UpGrinder_Stick");
	}
	
	void Update () {
        Quaternion quaternion = _spinStick.transform.rotation;
        quaternion.y = quaternion.z;
        quaternion.z = quaternion.x;
        this.transform.rotation = quaternion;
	}
}
