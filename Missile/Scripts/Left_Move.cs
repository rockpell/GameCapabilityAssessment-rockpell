using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Move : MonoBehaviour {
    public GameObject Tank;  //조종할 Tank 오브젝트
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Left_Moving()
    {
        //Debug.Log("LEFT_rotation");
        //Debug.Log(Tank.transform.rotation.z);
        if (Tank.transform.rotation.z <= 0.6)  //너무 많이 돌아가지않게 조절
        {
            Tank.transform.Rotate(0, 0, 15);  //15'씩 돌림
        }
    }
}
