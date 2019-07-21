using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDHStar : MonoBehaviour {

    public GameObject GameController, destroysound;
    public GameObject[] star;
    int point, buttonpoint = 0, iconpoint;
    float timer;

    public void Addpoint(int pluspoint)
    {
        point += pluspoint;
    }

	// Use this for initialization
	void Start ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        point++;
         if (point < 3)
        {
            Instantiate(destroysound, transform.position, transform.rotation);
        }
        

    }

    // Update is called once per frame
    void Update () {
        GameObject tutorialCanvas = GameObject.Find("TutorialCanvas");
        if (tutorialCanvas != null) // 튜토리얼
        {
            if (timer >= 14)
            {
                Destroy(gameObject);
            }
            if (point == 0)
            {
                if (timer >= 14)
                {
                    GameController.GetComponent<KDHTutorialManagement>().GameOver(3);
                }
            }
            else if (point == 1)
            {
                Destroy(star[0]);
                if (timer >= 14)
                {
                    GameController.GetComponent<KDHTutorialManagement>().GameOver(2);
                }
            }
            else if (point == 2)
            {
                Destroy(star[1]);
                if (timer >= 14)
                {
                    GameController.GetComponent<KDHTutorialManagement>().GameOver(2);
                }
            }
            else if (point == 3)
            {
                Destroy(star[2]);
                GameController.GetComponent<KDHTutorialManagement>().GameOver(1);
            }
            timer += Time.deltaTime;
        }
        else {
            if (timer >= 14)
            {
                Destroy(gameObject);
            }
            if (point == 0)
            {
                if (timer >= 14)
                {
                    GameController.GetComponent<KDHGameController>().GameOver(3);
                }
            }
            else if (point == 1)
            {
                Destroy(star[0]);
                if (timer >= 14)
                {
                    GameController.GetComponent<KDHGameController>().GameOver(2);
                }
            }
            else if (point == 2)
            {
                Destroy(star[1]);
                if (timer >= 14)
                {
                    GameController.GetComponent<KDHGameController>().GameOver(2);
                }
            }
            else if (point == 3)
            {
                Destroy(star[2]);
                GameController.GetComponent<KDHGameController>().GameOver(1);
            }
            timer += Time.deltaTime;
        }
        
    }
}
