using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGaugeMesh : MonoBehaviour {

	public MeshRenderer target;
	public int orderInLayer;

	void Awake()
	{
		target.sortingOrder = orderInLayer;
	}
	private void Update()
	{
		target.sortingOrder = orderInLayer;
	}
}
