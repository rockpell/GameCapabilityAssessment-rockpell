using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSDongSil : MonoBehaviour {

    public float speed = 1;
    public float range = 1;

    float timer;
    float y;
    Vector3 pivot;

	void Start ()
    {
        timer = 0;
        pivot = transform.position;
	}
	
	void Update ()
    {
        timer += Time.deltaTime * speed;
        y = Mathf.Sin(timer);

        transform.position = pivot + new Vector3(0, y * range, 0);
	}
}
