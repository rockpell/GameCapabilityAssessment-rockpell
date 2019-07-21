using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhyGaugeImageController : GaugeImageController
{
    private GameObject mask;

    private float destination;

    private float speed;

    public override IEnumerator ImageActivate(int value)
    {
        mask = GameObject.Find("RhyGuageMask");

        destination = value / 15.0f * 10.0f;

        speed = 0.1f;

        while (mask.transform.position.x < destination)
        {
            mask.transform.position = new Vector3(mask.transform.position.x + speed - ( speed * (float)(mask.transform.position.x * 10 / destination * 0.095)), mask.transform.position.y, mask.transform.position.z);
          
            nowValue = mask.transform.position.x / 10.0f * 15.0f;

            yield return null;
        }

        if (value == 15)
        {
            maxScore();
        }

        Debug.Log("done");

        yield return null;
    }

    public void startGuage(int value)
    {
        StartCoroutine(ImageActivate(value));
    }

    private void maxScore()
    {

    }

    // Use this for initialization
    void Start ()
    {
        nowValue = 0;
	}

	// Update is called once per frame
	void Update ()
    {
		
	}
}
