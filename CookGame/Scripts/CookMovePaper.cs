using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookMovePaper : MonoBehaviour {
    private float speed;
	// Use this for initialization
	void Start () {
        speed = GameObject.Find("LevelSet").GetComponent<CookLevelSet>().GetPaperSpeed();
    }
	
	// Update is called once per frame
	void Update () {
        if (CookUIManager.instance.currentTime > 0)
        {
            transform.Translate(-Time.deltaTime * speed, 0, 0);
            if (transform.position.x < -16.5f)
            {
                CookUIManager.instance.SetBarImage(-0.1f);
                gameObject.SetActive(false);
            }
        }
	}
}
