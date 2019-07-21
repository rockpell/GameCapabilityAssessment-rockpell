using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NELevel
{
	public int row;
	public int colum;
	[Range (0f,1f)]
	public float color;
	public float delay;
	public float power;
	public int[] amount;
}

public class NEGameManager : MonoBehaviour {

	public NELevel[] levels;

	public int gameLevel;
	public GameObject[] target;
	public GameObject[] obstacle;
	public GameObject[] spawnerLine;
	public GameObject stageUI;
	public GameObject targetPool;
	public NEDeadLine deadLine;
	public NEPlayerController playerController;

	public GameObject resultUI;
	public Sprite success;
	public Sprite fail;

	[SerializeField]
	private bool[] gameResult;
	public int nowPhase;
	public int maxPhase;
	public float distance;


	private bool isShowStageOver;
	private bool isStageDone;
	void Start()
	{
        gameLevel = NewGameManager.Instance.StartGame() - 1;

		if (GameObject.Find("TutorialCanvas"))
			gameLevel = 0;

		isShowStageOver = false;
		isStageDone = false;

		nowPhase = 0;
		if (maxPhase > 0)
			gameResult = new bool[maxPhase];
		for (int i = 0; i < gameResult.Length; ++i)
			gameResult[i] = true;
		StartCoroutine(GameProgress());



	}
	void Spawn(NELevel lv)
	{
		//곰 양 정하기
		int _amount = lv.amount[Random.Range(0,lv.amount.Length)];

		//타겟 인덱스 정하기
		int[] targetIndex = new int[lv.row * lv.colum];
		for (int i = 0; i < lv.row * lv.colum; ++i)
			targetIndex[i] = i;
		Shuffle(targetIndex);

		//열 정하기
		int[] _row = new int[lv.row];
		int[] tmp = Enumerable.Range(0, spawnerLine.Length).ToArray();
		Shuffle(tmp);
		for (int i = 0; i < _row.Length; ++i)
			_row[i] = tmp[i];

		//오브젝트 생성
		int length = targetIndex.Length;
		for (int i = 0; i < length; ++i)
		{
			Vector3 sp = spawnerLine[_row[targetIndex[i] / lv.colum]].transform.GetChild(0).position;
			Vector3 ep = spawnerLine[_row[targetIndex[i] / lv.colum]].transform.GetChild(1).position;
			Vector3 point = sp + ((ep - sp) / (float)lv.colum) * ((targetIndex[i] % lv.colum) + 0.5f);
			if (i < _amount)
			{
				//곰 만들기
				StartCoroutine(SpawnObject(lv, target[lv.color > Random.Range(0f, 1f) ? 1 : 0], point));
			}
			else
			{
				//보록화시 만들기
				StartCoroutine(SpawnObject(lv, obstacle[lv.color > Random.Range(0f, 1f) ? 1 : 0], point));
			}
		}
		
	}

	IEnumerator SpawnObject(NELevel lv, GameObject ob, Vector3 point)
	{
		yield return new WaitForSeconds(Random.Range(0,lv.delay));
		GameObject g = Instantiate(ob, point, Quaternion.identity, targetPool.transform);
		g.transform.eulerAngles += (Vector3.forward * Random.Range(0, 360));
		g.GetComponent<Rigidbody2D>().velocity = Vector2.up * lv.power;
		g.GetComponent<Rigidbody2D>().gravityScale = lv.power * lv.power / distance;
		g.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-20f, 20f);
	}

	IEnumerator GameProgress()
	{
		yield return new WaitForSeconds(0.7f);

		for (; nowPhase < maxPhase; ++nowPhase)
		{
			//Stage UI 보여주기
			StartCoroutine(ShowStageUI());
			yield return new WaitUntil(() => isShowStageOver);
			isShowStageOver = false;

			//표적 만들어 던지기
			yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
			Spawn(levels[gameLevel]);
			yield return new WaitForSeconds(1f);

			//떨어질때까지 기다림
			yield return new WaitUntil(() => targetPool.transform.childCount == 0);

			//결과 계산
			//터치의 결과 확인
			int[] result = playerController.Result();
			playerController.Clear();
			if (result[1] > 0)
				gameResult[nowPhase] = false;
			Debug.Log("P Target : " + result[0]);
			Debug.Log("P Obstacle : " + result[1]);
			//데드라인의 결과 확인
			result = deadLine.Result();
			deadLine.Clear();
			if (result[0] > 0)
				gameResult[nowPhase] = false;
			Debug.Log("D Target : " + result[0]);
			Debug.Log("D Obstacle : " + result[1]);

			resultUI.transform.GetChild(nowPhase).GetComponent<SpriteRenderer>().sprite = gameResult[nowPhase] ? success : fail;

		}

        //결과 보여주기
        int count = 0;
        foreach (bool b in gameResult)
            if (b) ++count;

        Result r = count > 1 ? (Result)count - 1 : Result.Fail;
        NewGameManager.Instance.ClearGame(r);
    }
	IEnumerator ShowStageUI()
	{
		//스테이지 보이기
		stageUI.GetComponent<NEUIVisualizer>().ShowUI(nowPhase);
		yield return new WaitForSeconds(0.7f);
		//스테이지 사라지기
		stageUI.GetComponent<NEUIVisualizer>().HideUI();
		yield return new WaitForSeconds(0.7f);
		isShowStageOver = true;
	}

	public static void Shuffle (int[] list)
	{
		int n = list.Length;
		while (n > 1)
		{
			n--;
			int k = Random.Range(0,n);
			int value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
