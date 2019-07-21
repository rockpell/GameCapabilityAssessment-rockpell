using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHBulletControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.transform.name);
            if (collision.transform.name == "Head")
            {
                SBHEnemyControl enemyControl = collision.transform.parent.GetComponent<SBHEnemyControl>();
                enemyControl.HeadShot();
            }
            else if (collision.transform.name.Contains("Enemy"))
            {
                SBHEnemyControl enemyControl = collision.transform.GetComponent<SBHEnemyControl>();
                enemyControl.GunShot();
            }
        } else if(collision.gameObject.tag == "Unit")
        {
            SBHCivilianControl civilianControl = collision.transform.GetComponent<SBHCivilianControl>();
            civilianControl.GunShot();
        }
        else if(collision.gameObject.tag == "Obstacle")
        {

        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Glass")
        {
            other.gameObject.SetActive(false);
            SBHSoundManger.Instance.PlayGlassAudio();
        }
    }
}
