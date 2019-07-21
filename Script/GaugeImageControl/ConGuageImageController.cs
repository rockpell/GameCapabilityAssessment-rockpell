using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConGuageImageController : GaugeImageController
{
    public Transform niddlePos;
    public GameObject thread;
    public GameObject[] pikas;
    private float level = 0;
    private Vector3 goDisplacement;
    private Vector3 firstposition;
    private float destination = 0;
    public override IEnumerator ImageActivate(int value)
    {
        //value = 15; //for Test
        level = 1.0f / 15.0f * value;
        float leveldown = 1.0f / 15.0f * (value - 4);
        float speed = 0.01f;
        
        firstposition = thread.transform.position;
        for (; ; )
        {
            if (value <= 4)
            {
                if (destination >= level)     // 탈출
                {
                    break;
                }
                else if (speed <= 0.0005f)
                {
                    speed = 0.0005f;
                }
                else
                {
                    speed -= 0.00018f;
                }
                destination += speed;
            }
            else
            {
                if (destination >= level)     // 탈출
                {
                    break;
                }
                else if (destination >= leveldown)
                {
                    if (speed <= 0.0005f)
                    {
                        speed = 0.0005f;
                    }
                    else
                    {
                        speed -= 0.00018f;
                    }
                    destination += speed;
                }
                else
                {
                    destination += speed;
                }
            }
            nowValue = destination *15.0f ;               //value 값 
            Debug.Log(nowValue);
            ShineON(nowValue);
            goDisplacement = Vector3.Lerp(firstposition, niddlePos.position, destination);
            thread.transform.position = new Vector3(goDisplacement.x, firstposition.y, firstposition.z);
            yield return new WaitForSeconds(0.001f);
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void ShineON(float nowlevel)
    {
        if(nowlevel >= 15.0f)
        {
            pikas[4].SetActive(true);
        }
        else if(nowlevel >= 12.0f)
        {
            pikas[3].SetActive(true);
        }
        else if (nowlevel >= 9.0f)
        {
            pikas[2].SetActive(true);
        }
        else if (nowlevel >= 6.0f)
        {
            pikas[1].SetActive(true);
        }
        else if (nowlevel >= 3.0f)
        {
            pikas[0].SetActive(true);
        }
    }

}

