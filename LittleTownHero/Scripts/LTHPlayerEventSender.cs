using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTHPlayerEventSender : MonoBehaviour {

	public LTHPlayer target;

	private float counter;

	private void Start()
	{
		counter = 0;
	}
	void Update ()
	{

		if (Input.GetKeyDown(KeyCode.W))
			UpDefence();
		if (Input.GetKeyDown(KeyCode.F))
			DownDefence();
		if (Input.GetKeyDown(KeyCode.E))
			TwoSideDefence();
		if (Input.GetKeyDown(KeyCode.Space))
			Attack();
	}
	public void UpDefence()
	{
		target.Act(LTH.Action.Up);
	}
	public void DownDefence()
	{
		target.Act(LTH.Action.Down);
	}
	public void TwoSideDefence()
	{
		target.Act(LTH.Action.TwoSide);
	}
	public void Attack()
	{
		target.Act(LTH.Action.Attack);
	}
}
