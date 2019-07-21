using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTHTown : MonoBehaviour {

	public List<GameObject> building;

	private void Awake()
	{
		building = new List<GameObject>();
	}
	private void Start()
	{
		for(int i = 0; i < transform.childCount; ++i)
		{
			building.Add(transform.GetChild(i).gameObject);
		}
	}
	public void SetBuilding(int health)
	{

	}

	public void Dameged()
	{
		GameObject b = building[0];
		building.Remove(b);
		Destroy(b);
	}
	public int Health()
	{
		return building.Count;
	}

}
