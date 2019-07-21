using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Move : MonoBehaviour {

    public float power = 400;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * power);  //로켓에 힘을가해서 이동하게함
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet")  //총알 말고 다른걸 만나면 자폭
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity);
    }
}
