using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEDeadLine : MonoBehaviour {

	[SerializeField]
	private int target;
	[SerializeField]
	private int obstacle;

	private void Start()
	{
		Clear();
	}

	public void Clear()
	{
		target = 0;
		obstacle = 0;
	}
	public int[] Result()
	{
		return new int[] {target, obstacle };
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(LayerMask.LayerToName(collision.gameObject.layer) == "Target")
		{
			//Debug.Log("Target +1");
			++target;
		}
		else if(LayerMask.LayerToName(collision.gameObject.layer) == "Obstacle")
		{
			//Debug.Log("Obstacle +1");
			++obstacle;
		}
		Destroy(collision.gameObject);
	}
}
