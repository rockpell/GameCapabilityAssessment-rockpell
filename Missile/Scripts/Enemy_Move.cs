using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {
    public float speed = 0.1f; //Enemy 의 전진속도
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Alarm")  //Alarm 을 만나면 알람을 지우고 전진
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(this.gameObject);  //자폭
        }
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        this.gameObject.transform.Translate(speed, 0, 0);  //생성되고 speed 만큼 전진함.
	}
}
