using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTH;

public class LTHPlayer : MonoBehaviour, LTHBeat {

	public Action state;
	public LTHEnemy opponent;

	[SerializeField]
	private GameObject blockProto;
	[SerializeField]
	private Transform body;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private LTHBeatManager beatManager;

	private bool isActable;

	private void Awake()
	{
		isActable = true;
		state = Action.None;
	}
	void Update ()
	{
		
	}
	public bool InterAction(Action pAction)
	{
		bool isSucceed = false;

		if (pAction == Action.None || pAction == Action.End)
			return false;

		if (state == pAction)
		{
			isSucceed = true;
			//적이 공격이면 막고, 방어이면 데미지줌, 상태도 None으로
			//작성해야함
			switch(pAction)
			{
				case Action.Up:
				case Action.Down:
				case Action.TwoSide:
					//막기 성공
					Block();
					break;
				case Action.Attack:
					//공격 성공
					opponent.Dameged();
					break;
			}
			state = Action.None;
		}
		return isSucceed;
	}
	public void Block()
	{
		GameObject tmp = Instantiate(blockProto, body.position, Quaternion.identity);
		Destroy(tmp, 0.7f);
	}
	public void Act(Action pAction)
	{
		if (!isActable)
			return;

		bool isWork = false;
		switch(pAction)
		{
			case Action.Up:
				anim.Play("UpDefence");
				isWork = true;
				break;
			case Action.Down:
				anim.Play("DownDefence");
				isWork = true;
				break;
			case Action.TwoSide:
				anim.Play("TwoSideDefence");
				isWork = true;
				break;
			case Action.Attack:
				anim.Play("Attack");
				isWork = true;
				break;
		}
		if(isWork)
		{
			if(opponent != null)
			{
				if(!opponent.InterAction(pAction))
				{
					state = pAction;
					StartCoroutine(ValidTime(beatManager.OneBeatTime / 2));
				}
			}
		}
	}
	private IEnumerator ValidTime(float time)
	{
		float timer = 0;
		isActable = false;
		yield return null;

		while(true)
		{
			timer += Time.deltaTime;
			if (timer >= time)
			{
				TimeOut(state);
				state = Action.None;
				break;
			}
			if (state == Action.None)
				break;
			yield return null;
		}
		isActable = true;
	}
	private void TimeOut(Action pAction)
	{
		//필요시 작성
	}
	public void Beat()
	{
		//필요시 작성
	}

}
