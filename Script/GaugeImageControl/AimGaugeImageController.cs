using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGaugeImageController : GaugeImageController
{
    [SerializeField]
    private GameObject bullet;
	Vector2 aftermaskVector;
	public float speed = 10;
	public float step =10.0f;
	private float per = 0.8609752f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override IEnumerator ImageActivate(int value)
    {
        aftermaskVector = new Vector2(bullet.transform.position.x + per * (float)value, bullet.transform.position.y);
        while (bullet.transform.position.x - aftermaskVector.x <= 0.00001f)
        {
            bullet.transform.Translate(Vector2.right * Time.deltaTime * speed * (-(bullet.transform.position.x - aftermaskVector.x) * 0.1f + 0.03f));
            nowValue = (float)value + (bullet.transform.position.x - aftermaskVector.x) / per;
            Debug.Log("step =  " + nowValue);
            yield return null;
        }
        bullet.transform.position = aftermaskVector;
        nowValue = (float)value + (bullet.transform.position.x - aftermaskVector.x) / per;
    }
}
