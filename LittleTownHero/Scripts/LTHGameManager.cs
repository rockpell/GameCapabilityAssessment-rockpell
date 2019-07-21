using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTH;

public class LTHGameManager : MonoBehaviour, LTHBeat {

	public Transform startPoint;
	public Transform endPoint;
	public GameObject monsterProto;
	public LTHPlayer player;
	public LTHEnemy monster;
	public int monsterAppearBeat;
	[SerializeField]
	private LTHTown town;
	[SerializeField]
	private LTHBeatManager beatManager;
	private int beatCount;

	private ActionBeat[][][] actionBeatsList;
	private int[] monsterHealth;
	private int monsterCount;
	private void Awake()
	{
		monsterCount = 0;
		monsterHealth = new int[6] { 2,2,2,2,4,10 };
		actionBeatsList = new ActionBeat[6][][];
		
		//0번째 몬스터 체력 2
		actionBeatsList[0] = new ActionBeat[2][];
		actionBeatsList[0][0] = new ActionBeat[4];
		actionBeatsList[0][0][0] = new ActionBeat(4, Action.Up);
		actionBeatsList[0][0][1] = new ActionBeat(4, Action.Up);
		actionBeatsList[0][0][2] = new ActionBeat(4, Action.Up);
		actionBeatsList[0][0][3] = new ActionBeat(4, Action.Attack);
		actionBeatsList[0][1] = new ActionBeat[4];
		actionBeatsList[0][1][0] = new ActionBeat(4, Action.Down);
		actionBeatsList[0][1][1] = new ActionBeat(4, Action.Down);
		actionBeatsList[0][1][2] = new ActionBeat(4, Action.Down);
		actionBeatsList[0][1][3] = new ActionBeat(4, Action.Attack);

		//1번째 몬스터 체력 2
		actionBeatsList[1] = new ActionBeat[2][];
		actionBeatsList[1][0] = new ActionBeat[4];
		actionBeatsList[1][0][0] = new ActionBeat(4, Action.Up);
		actionBeatsList[1][0][1] = new ActionBeat(4, Action.Down);
		actionBeatsList[1][0][2] = new ActionBeat(4, Action.Up);
		actionBeatsList[1][0][3] = new ActionBeat(4, Action.Attack);
		actionBeatsList[1][1] = new ActionBeat[4];
		actionBeatsList[1][1][0] = new ActionBeat(4, Action.Down);
		actionBeatsList[1][1][1] = new ActionBeat(4, Action.Up);
		actionBeatsList[1][1][2] = new ActionBeat(4, Action.Down);
		actionBeatsList[1][1][3] = new ActionBeat(4, Action.Attack);

		//2번째 몬스터 체력 2
		actionBeatsList[2] = new ActionBeat[2][];
		actionBeatsList[2][0] = new ActionBeat[5];
		actionBeatsList[2][0][0] = new ActionBeat(4, Action.Up);
		actionBeatsList[2][0][1] = new ActionBeat(4, Action.Down);
		actionBeatsList[2][0][2] = new ActionBeat(4, Action.Up);
		actionBeatsList[2][0][3] = new ActionBeat(2, Action.TwoSide);
		actionBeatsList[2][0][4] = new ActionBeat(2, Action.Attack);
		actionBeatsList[2][1] = new ActionBeat[5];
		actionBeatsList[2][1][0] = new ActionBeat(4, Action.Down);
		actionBeatsList[2][1][1] = new ActionBeat(4, Action.Up);
		actionBeatsList[2][1][2] = new ActionBeat(4, Action.Down);
		actionBeatsList[2][1][3] = new ActionBeat(2, Action.TwoSide);
		actionBeatsList[2][1][4] = new ActionBeat(2, Action.Attack);

		//3번째 몬스터 체력 2
		actionBeatsList[3] = new ActionBeat[2][];
		actionBeatsList[3][0] = new ActionBeat[6];
		actionBeatsList[3][0][0] = new ActionBeat(2, Action.Up);
		actionBeatsList[3][0][1] = new ActionBeat(2, Action.Down);
		actionBeatsList[3][0][2] = new ActionBeat(2, Action.Up);
		actionBeatsList[3][0][3] = new ActionBeat(2, Action.Down);
		actionBeatsList[3][0][4] = new ActionBeat(4, Action.TwoSide);
		actionBeatsList[3][0][5] = new ActionBeat(4, Action.Attack);
		actionBeatsList[3][1] = new ActionBeat[5];
		actionBeatsList[3][1][0] = new ActionBeat(4, Action.TwoSide);
		actionBeatsList[3][1][1] = new ActionBeat(4, Action.TwoSide);
		actionBeatsList[3][1][2] = new ActionBeat(2, Action.Down);
		actionBeatsList[3][1][3] = new ActionBeat(4, Action.Up);
		actionBeatsList[3][1][4] = new ActionBeat(2, Action.Attack);

		//4번째 몬스터 체력 4
		actionBeatsList[4] = new ActionBeat[2][];
		actionBeatsList[4][0] = new ActionBeat[6];
		actionBeatsList[4][0][0] = new ActionBeat(2, Action.Up);
		actionBeatsList[4][0][1] = new ActionBeat(2, Action.Up);
		actionBeatsList[4][0][2] = new ActionBeat(2, Action.Up);
		actionBeatsList[4][0][3] = new ActionBeat(4, Action.Up);
		actionBeatsList[4][0][4] = new ActionBeat(4, Action.TwoSide);
		actionBeatsList[4][0][5] = new ActionBeat(2, Action.Attack);
		actionBeatsList[4][1] = new ActionBeat[6];
		actionBeatsList[4][1][0] = new ActionBeat(2, Action.None);
		actionBeatsList[4][1][1] = new ActionBeat(4, Action.TwoSide);
		actionBeatsList[4][1][2] = new ActionBeat(4, Action.TwoSide);
		actionBeatsList[4][1][3] = new ActionBeat(2, Action.Up);
		actionBeatsList[4][1][4] = new ActionBeat(2, Action.Down);
		actionBeatsList[4][1][5] = new ActionBeat(2, Action.Attack);

		//5번째 몬스터 체력 10
		actionBeatsList[5] = new ActionBeat[1][];
		actionBeatsList[5][0] = new ActionBeat[12];
		actionBeatsList[5][0][0] = new ActionBeat(1, Action.Up);
		actionBeatsList[5][0][1] = new ActionBeat(1, Action.Attack);
		actionBeatsList[5][0][2] = new ActionBeat(1, Action.Down);
		actionBeatsList[5][0][3] = new ActionBeat(1, Action.Attack);
		actionBeatsList[5][0][4] = new ActionBeat(1, Action.Up);
		actionBeatsList[5][0][5] = new ActionBeat(1, Action.Attack);
		actionBeatsList[5][0][6] = new ActionBeat(1, Action.Down);
		actionBeatsList[5][0][7] = new ActionBeat(1, Action.Attack);
		actionBeatsList[5][0][8] = new ActionBeat(2, Action.Up);
		actionBeatsList[5][0][9] = new ActionBeat(2, Action.TwoSide);
		actionBeatsList[5][0][10]= new ActionBeat(2, Action.Down);
		actionBeatsList[5][0][11]= new ActionBeat(2, Action.TwoSide);

		beatCount = 0;
	}
	private void Start()
	{
        //게임 시작 코드
        NewGameManager.Instance.StartGame();

		beatManager.AddToList(GetComponent<LTHGameManager>());
	}

	public void Beat()
	{
		beatCount++;
		if(beatCount == 1 && monster == null)
		{
			CreateMonster(actionBeatsList[monsterCount], monsterHealth[monsterCount]);
			monsterCount++;
            //몬스터 배열이 끝났는지 확인
			if(monsterCount == monsterHealth.Length)
			{
                monsterCount = 0;
			}
		}
		if(beatCount == monsterAppearBeat)
		{
			beatCount = 0;
		}
	}

	public void CreateMonster(ActionBeat[][] actions, int health)
	{
		GameObject tmp = Instantiate(monsterProto);
		monster = tmp.GetComponent<LTHEnemy>();
		// 위치 잡아주기
		tmp.transform.position = startPoint.position;
		monster.startPoint = startPoint.position ;
		monster.endPoint = endPoint.position ;
		// 패턴 정해주기
		monster.Actions = actions;
		// 체력 설정
		monster.health = health;
		// 상호인식
		player.opponent = monster;
		monster.opponent = player;
		monster.beatManager = beatManager;
		monster.gameManager = this;
		// 등록 및 등장
		beatManager.AddToList(monster);
		monster.Appear(7);

	}
	public void MonsterDown(LTHEnemy target)
	{
		beatManager.RemoveToList(monster);
		player.opponent = null;
		monster = null;
		//게임 종료
		if(monsterCount == 0)
		{
			GameOver(Result.BigSuccessful);
		}
	}
	public void TownAttacked()
	{
		if (town.Health() <= 0)
		{
			return;
		}
		town.Dameged();
		if(town.Health() <= 0)
		{
            //게임 종료!!
            GameOver(Result.Fail);

        }
	}
    public void GameOver(Result result)
    {
        NewGameManager.Instance.ClearGame(result);
    }
}

namespace LTH
{
	public enum Action
	{
		None, Up, Down, TwoSide, Attack, End
	}
	[System.Serializable]
	public class ActionBeat
	{
		public int beat;
		public Action action;
		public ActionBeat(int pBeat, Action pAction)
		{
			if (pBeat > 0)
				beat = pBeat;
			else
				beat = 1;
			action = pAction;
		}
	}
}