using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTH;

public class LTHEnemy : MonoBehaviour, LTHBeat {

	public Vector3 startPoint;
	public Vector3 endPoint;
	public int health;

	public Action state;
	public LTHPlayer opponent;
	[SerializeField]
	public LTHGameManager gameManager;
	[SerializeField]
	public LTHBeatManager beatManager;
	[SerializeField]
	private LTHActionListController listController;
	[SerializeField]
	private TextMesh hp;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private ActionBeat[][] actions;
	public ActionBeat[][] Actions { get { return actions; } set { actions = value; } }

	private int curPattern;
	private int curIndex;
	private int beatCount;
	private int readyMax;
	private int readyCount;
	private bool isMove;
	private bool isReady;
	private bool isDead;
	private void Awake()
	{
		curPattern = 0;
		curIndex = 0;
		beatCount = 0;
		readyMax = 999999;
		readyCount = 0;
		isMove = false;
		isReady = false;
		isDead = false;
		state = Action.None;
	}
	private void Start()
	{
		SetHPText();
		listController.SetImages(Actions[curPattern]);
	}
	
	public void Appear(int beat)
	{
		isReady = false;
		readyMax = beat;
		readyCount = 0;

		MoveToPlace();
	}
	public void Dameged()
	{
		
		--health;
		SetHPText();
		if(health <= 0)
		{
			Die();
		}
		else
		{
			anim.Play("Dameged");
		}
	}
	public bool InterAction(Action pAction)
	{
		bool isSucceed = false;

		if (pAction == Action.None || pAction == Action.End)
			return false;

		if (state == pAction)
		{
			isSucceed = true;
			//공격이면 막히고, 방어이면 체력담, 상태도 None으로
			//작성해야함
			switch (pAction)
			{
				case Action.Up:
				case Action.Down:
				case Action.TwoSide:
					//공격 막힘
					opponent.Block();
					break;
				case Action.Attack:
					//공격 들어옴
					Dameged();
					break;
			}
			state = Action.None;
		}
		return isSucceed;
	}
	public void Die()
	{
		isDead = true;
		//GameManager에 알림
		gameManager.MonsterDown(this);
		anim.Play("Die");
		//애니메이션 시간 알아내기
		float animTime = 0.2f;
		//몬스터 제거
		Destroy(gameObject, animTime);
	}
	public void Attack()
	{
		gameManager.TownAttacked();
	}
	[ContextMenu("Beat")]
	public void Beat()
	{
		if (isDead)
			return;
		if (isReady)
		{
			++beatCount;
			if (beatCount == 1)
			{
				if (isMove)
					Act();
				else
					Warn();
			}
			if (beatCount >= Actions[curPattern][curIndex].beat)
			{
				++curIndex;
				beatCount = 0;
			}
			if (curIndex >= Actions[curPattern].Length)
			{
				if (isMove)
				{
					++curPattern;
					if (curPattern >= Actions.Length)
					{
						curPattern = 0;
					}
					listController.Clear();
					listController.SetImages(Actions[curPattern]);
					isMove = false;
				}
				else
				{
					isMove = true;
				}
				curIndex = 0;
			}
		}
		else
		{
			++readyCount;

			MoveToPlace();
			
			if(readyCount >= readyMax)
			{
				isReady = true;
			}
		}
	}
	private void MoveToPlace()
	{
		float rate = (float)readyCount / readyMax;
		if (rate > 1)
			rate = 1;
		transform.position = Vector3.Lerp(startPoint, endPoint, rate);
	}
	private void Warn()
	{
		listController.Open(curIndex);
	}
	private void SetHPText()
	{
		hp.text = "HP : " + health;
	}
	private void Act()
	{
		bool isWork = false;
		switch (actions[curPattern][curIndex].action)
		{

			case Action.Up:
				anim.Play("UpAttack");
				isWork = true;
				break;
			case Action.Down:
				anim.Play("DownAttack");
				isWork = true;
				break;
			case Action.TwoSide:
				anim.Play("TwoSideAttack");
				isWork = true;
				break;
			case Action.Attack:
				isWork = true;
				break;
		}
		if (isWork)
		{
			if(opponent != null)
			{
				if(!opponent.InterAction(actions[curPattern][curIndex].action))
				{
					state = actions[curPattern][curIndex].action;
					StartCoroutine(ValidTime(beatManager.OneBeatTime / 2));
				}
			}
		}
	}
	private IEnumerator ValidTime(float time)
	{
		float timer = 0;
		yield return null;

		while (true)
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
	}
	private void TimeOut(Action pAction)
	{
		switch(pAction)
		{
			case Action.Up:
			case Action.Down:
			case Action.TwoSide:
				Attack();
				break;
		}
	}
}
