using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHP : MonoBehaviour {
    public Sprite HP100;
    public Sprite HP90;
    public Sprite HP80;
    public Sprite HP70;
    public Sprite HP60;
    public Sprite HP50;
    public Sprite HP40;
    public Sprite HP30;
    public Sprite HP20;
    public Sprite HP10;
    public Sprite HP0;
	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = HP100;
	}
	
	// Update is called once per frame
	void Update () {
		if(ChickenGameController.Score <= 90)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP90;
        }
        if (ChickenGameController.Score <= 80)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP80;
        }
        if (ChickenGameController.Score <= 70)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP70;
        }
        if (ChickenGameController.Score <= 60)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP60;
        }
        if (ChickenGameController.Score <= 50)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP50;
        }
        if (ChickenGameController.Score <= 40)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP40;
        }
        if (ChickenGameController.Score <= 30)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP30;
        }
        if (ChickenGameController.Score <= 20)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP20;
        }
        if (ChickenGameController.Score <= 10)
        {
            this.GetComponent<SpriteRenderer>().sprite = HP10;
        }
		if (ChickenGameController.Score <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = HP0;
		}
	}
}
