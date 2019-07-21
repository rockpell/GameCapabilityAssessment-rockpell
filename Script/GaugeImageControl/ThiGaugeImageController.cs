using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiGaugeImageController : GaugeImageController
{
    public Transform target;
    public GameObject[] spark;
    private float speed = 0.05f;
    private Vector3 targetPoint = new Vector3(0, -0.29f, 0);
    private Vector3 startPoint = new Vector3(0, -5.5f, 0);
    private float stepValue;
    private int maxStep = 15;

    // Use this for initialization
    private void Awake()
    {
        nowValue = 0;
        stepValue = (targetPoint.y - startPoint.y) / 15;
    }
    void Start ()
    {
        //StartCoroutine(ImageActivate(15));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override IEnumerator ImageActivate(int value)
    {
        int step = 1;
        for (int i = 0; i < spark.Length; i++)
        {
            spark[i].SetActive(false);
        }

        target.localPosition = startPoint;
        while(true)
        {
            target.localPosition = Vector3.MoveTowards(target.localPosition, targetPoint, speed);
            if (nowValue >= value - 2)
            {
                speed = 0.02f;
            }
            if (nowValue >= value - 1)
            {
                speed = 0.01f;
            }
            if (nowValue >= (maxStep/spark.Length)*step)
            {
                spark[step - 1].SetActive(true);
                step++;
            }
            if(nowValue >= value)
            {
                break;
            }
            nowValue = 15 * (target.localPosition.y - startPoint.y) / (targetPoint.y - startPoint.y);
            yield return null;
        }
    }
    public float GetNowValue()
    {
        return nowValue;
    }
}
