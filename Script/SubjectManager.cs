using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubjectManager : MonoBehaviour {
    // 현재항목 뭔지, 게임 단계 표시, 남은 횟수 보여주기, 게임 시작 기능
    // 삼각응원단 관리(애니터메이터 관리), 현재 항목 게임 끝났는지도 확인, 중간 종료 화면으로 전환 기능

    public float UIAppearTime;
    public float UISpeedRate;
    private float[] UIPositionY;
    public int endCoroutineCount;
    public GameObject slotImage;
    public float slotRotateCount;
    public float slotRotateSpeed;
    public float slotRotateSpeedLimit;
    public int[] gameWeight;
    [SerializeField]
    private float alphaReduce;
    private int uiCount;

    private AudioSource audioSource;
	[SerializeField]
	private SamgackManager samgackManager;
    [SerializeField]
    private Sprite endGameIcon;

    [SerializeField]
    private List<AudioClip> slotMachineSound;

    private GameObject PushImage;
    private GameObject StartButton;

    // Use this for initialization
    void Start () {
        PushImage = GetPushIcon();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private GameObject GetPushIcon()
    {
        List<Transform> MainCameraChilds = new List<Transform>();
        GameObject pushIcon = null;
        foreach (Transform child in GameObject.Find("Main Camera").GetComponentInChildren<Transform>(true))
        {
            if (child.gameObject.name == "Push")
            {
                pushIcon = child.gameObject;
            }
        }
        return pushIcon;
    }
    private void AppearUI(bool isAnimate) // UI 나타나기, 부드럽게 내려와야함, (부가설명 있음)
    {
        List<Transform> childList = GetChilds(isAnimate);
        StartCoroutine(UIAppear(childList, isAnimate));
        StartCoroutine(CheckDifficulty());
    }

    private List<Transform> GetChilds(bool isAnimate)
    {
        List<Transform> UIList = new List<Transform>();

        Transform startPosition = GameObject.Find("StartPosition").transform;
        foreach (Transform childTransform in GetComponentsInChildren<Transform>(true))
        {
            UIList.Add(childTransform);
        }
        if (UIList.Contains(this.transform))
        {
            UIList.Remove(this.transform);
        }
        UIList.Remove(startPosition);
        for (int i = UIList.Count-1; i >= 0; i--)
        {
            if (UIList.Contains(UIList[i].parent))
                UIList.Remove(UIList[i]);
        }
        UIList.Sort((a, b) => a.localPosition.y < b.localPosition.y ? -1 : 1);
        
        if (isAnimate)
        {
            UIPositionY = new float[UIList.Count];
            for (int i = 0; i < UIList.Count; i++)
            {
                UIPositionY[i] = UIList[i].localPosition.y;
                UIList[i].localPosition = new Vector3(UIList[i].localPosition.x, startPosition.localPosition.y);
            }
        }

        uiCount = UIList.Count;
        return UIList;
    }

    private void InitializeUI(bool isChange)
    {
        GameObject subject = GameObject.Find("Subject");
        GameObject subjectIcon = GameObject.Find("SubjectIcon");
        GameObject difficultLevel = GameObject.Find("DifficultLevel");
        GameObject evalation = GameObject.Find("Evalation");
        
        if(isChange)
        {
            int nowEvaluationValue = NewGameManager.Instance.GetMaxEvalationTime() - NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject()).Count - 1;
            if (nowEvaluationValue < 0)
                nowEvaluationValue = 0;
            evalation.GetComponent<UnityEngine.UI.Text>().text =
                 nowEvaluationValue + "/" + NewGameManager.Instance.GetMaxEvalationTime().ToString();
        }
        else
            evalation.GetComponent<UnityEngine.UI.Text>().text = NewGameManager.Instance.GetMaxEvalationTime() - NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject()).Count +
            "/" + NewGameManager.Instance.GetMaxEvalationTime().ToString();
        difficultLevel.GetComponent<UnityEngine.UI.Text>().text = NewGameManager.Instance.GetNowDifficultyLevel(NewGameManager.Instance.GetNowSubject()).ToString() + " 단계";
        subject.GetComponent<UnityEngine.UI.Image>().sprite = NewGameManager.Instance.GetSubjectIcon(NewGameManager.Instance.GetNowSubject());
    }

    IEnumerator UIAppear(List<Transform> list, bool isAnimate)
    {
        if (isAnimate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].gameObject.SetActive(true);
                if (list[i].gameObject.name == "StartGame")
                {
                    StartButton = GameObject.Find("StartButton");
                    StartButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
                }
            }
            InitializeUI(false);
            for (int i = 0; i < list.Count; i++)
            {
                StartCoroutine(DownUI(list[i].gameObject, UIPositionY[i]));
                yield return new WaitForSeconds(UIAppearTime);
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
                list[i].gameObject.SetActive(true);
            endCoroutineCount = list.Count;
            GetPushIcon().SetActive(true);
            InitializeUI(false);
        }
    }

    IEnumerator DownUI(GameObject ui, float yPosition)
    {
        float speed = (ui.transform.localPosition.y - yPosition) * UISpeedRate;
        while (ui.transform.localPosition.y > yPosition)
        {
            if(speed > 1)
                speed = (ui.transform.localPosition.y - yPosition) * UISpeedRate;
            ui.transform.localPosition = new Vector3(ui.transform.localPosition.x, ui.transform.localPosition.y - speed * Time.deltaTime * 60);
            yield return null;
        }
        PushImage.SetActive(true);
        if (ui.gameObject.name == "StartGame")
            StartButton.GetComponent<UnityEngine.UI.Button>().enabled = true;
        endCoroutineCount++;
    }

    public void NextGameShow() // 다음 게임을 정하는것을 슬롯머신처럼 보여주기, 해당항목이 평가가 끝났다면 항목 종료 글 띄우기
    {
        List<GameObject> slotImageSet = new List<GameObject>();
        GameObject startButton = GameObject.Find("StartButton");
        int selectGame;
        if (startButton.GetComponent<UnityEngine.UI.Button>().enabled)
        {
            PushImage.SetActive(false);
            GameObject.Find("StartGame").SetActive(false);

            SceneField[] _tempFields = CustomSceneManager.Instance.GetSceneNames(NewGameManager.Instance.GetNowSubject());// 이 함수를 이용 미니게임 목록 구하기
                                                                                                                          //List<GameObject> slotImageSet = new List<GameObject>();
            
            if (_tempFields != null)
            {
                for (int i = 0; i < _tempFields.Length; i++)
                {
                    slotImageSet.Add(MakeSlotImage(_tempFields[i], i));
                }
                slotImageSet.Add(MakeSlotImage("EndSequence", _tempFields.Length));
                audioSource = GameObject.Find("Scroll").GetComponent<AudioSource>();
                GameObject.Find("Scroll").GetComponent<UnityEngine.UI.Image>().color = Color.white;
                GameObject.Find("Border").GetComponent<UnityEngine.UI.Image>().color = Color.white;

                audioSource.clip = slotMachineSound[0];
                audioSource.loop = false;
                //slotMachine시작음 설정(1번)
                
                if (NewGameManager.Instance.GetMaxEvalationTime() - NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject()).Count > 0)
                {
                    InitializeUI(true);
                    //selectGame = Random.Range(0, slotImageSet.Count - 1);
                    selectGame = SelectGame(_tempFields);
                    //각각의 게임에 대한 가중치를 두고 플레이 하면 가중치 초기화, 안한 게임은 가중치를 증가시켜서 더 잘 나오도록 설정
                    //ex) 각각의 게임에 가중치 1씩 둠, 플레이 안한 게임들 가중치 증가+플레이 한 게임은 1로 설정
                }
                else
                    selectGame = slotImageSet.Count-1;

                if (selectGame < _tempFields.Length)
                    NewGameManager.Instance.NowMiniGame = _tempFields[selectGame];

                startButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
                StartCoroutine(StartSlotImage(slotImageSet, selectGame));
            }
        }
    }

    private GameObject MakeSlotImage(string sceneName, int index)
    {
        GameObject image = Instantiate(slotImage, GameObject.Find("GameIcon").transform);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        image.transform.localPosition = new Vector3(GameObject.Find("GameIcon").transform.localPosition.x, GameObject.Find("GameIcon").transform.localPosition.y + index * 200);
        if (sceneName != "EndSequence")
            image.GetComponent<UnityEngine.UI.Image>().sprite = NewGameManager.Instance.GetMiniGameIcon(sceneName);
        else
            image.GetComponent<UnityEngine.UI.Image>().sprite = endGameIcon;
        return image;
    }

    IEnumerator StartSlotImage(List<GameObject> list, int index)
    {
        while (endCoroutineCount != uiCount)
            yield return null;
        if (audioSource != null)
        {
            audioSource.Play();
        }
            
        float startYPosition = list[0].transform.localPosition.y;
        float endYPosition = list[index].transform.localPosition.y;
        float targetYPosition = GameObject.Find("Scroll").transform.position.y;

        GameObject gameIcon = GameObject.Find("GameIcon");
        float speed = slotRotateSpeed;
        float rotateCount = 0;
        while(slotRotateCount > rotateCount)
        {
            if (speed > slotRotateSpeedLimit)
                speed *= 0.95f;

            if (targetYPosition < list[list.Count - 1].transform.position.y)
            {
                gameIcon.transform.localPosition = new Vector3(gameIcon.transform.localPosition.x, gameIcon.transform.localPosition.y - speed * Time.deltaTime * 60);

            }
            else
                gameIcon.transform.localPosition = new Vector3(gameIcon.transform.localPosition.x, startYPosition);
            rotateCount += Time.deltaTime;

            if (audioSource.isPlaying == false)
            {
                audioSource.clip = slotMachineSound[1];
                audioSource.loop = true;
                audioSource.Play();
            }
            yield return null;
        }
        while (targetYPosition < list[index].transform.position.y)
        {
            gameIcon.transform.localPosition = new Vector3(gameIcon.transform.localPosition.x, gameIcon.transform.localPosition.y - speed * Time.deltaTime * 60);
            yield return null;
        }
        audioSource.clip = slotMachineSound[2];
        audioSource.loop = false;
        audioSource.Play();
        //슬롯머신 끝나는 음 한번, 반복 해제
        gameIcon.transform.localPosition = new Vector3(gameIcon.transform.localPosition.x, startYPosition - (endYPosition - startYPosition));
        //씬 전환 이후 목록 초기화 필요 할 수 있음
        //UI비활성화
        /*
        List<Transform> uiList = GetChilds(false);
        for (int i = 0; i < uiList.Count; i++)
            uiList[i].gameObject.SetActive(false);
        */
        if (index < list.Count-1)
        {
            yield return new WaitForSeconds(1f);
            GameStart("ExplanationScene");
        }
        else
        {
            GameStart("SubjectResult");
        }
        
    }

    IEnumerator CheckDifficulty()
    {
        while (endCoroutineCount != uiCount)
            yield return null;
        //난이도가 변경되면 이펙트를 실행하는 부분
        //난이도 1 변화당 이펙트를 한번 실행
        //
        List<GameResult> results = NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject());
        if (results.Count == 0)
            yield break;
        GameObject arrow = GameObject.Find("DifficultyArrow");
        switch(results[results.Count-1].resultValue)
        {
            case Result.BigSuccessful:
                arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Result.Successful:
                arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Result.Fail:
                arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
        arrow.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        float alpha = 1f;
        yield return new WaitForSeconds(0.5f);
        while(alpha > 0)
        {
            alpha -= alphaReduce;
            arrow.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
    public void GameStart(string sceneName) // 셔터매니저 ShutterSequence 함수 호출 예정
    {
        ShutterManager _shutterManager = GameObject.Find("ShutterCanvas").GetComponent<ShutterManager>();
        if (_shutterManager != null)
        {
            if(sceneName == "ExplanationScene")
            {
                if (NewGameManager.Instance.IsPlayedMiniGame(NewGameManager.Instance.GetNowSubject(),NewGameManager.Instance.NowMiniGame)) // 튜토리얼 스킵
                {
                    _shutterManager.ShutterSequence(Result.BigSuccessful, NewGameManager.Instance.NowMiniGame, false); // 게임 호출
                }
                else
                {
                    _shutterManager.ShutterSequence(Result.BigSuccessful, sceneName, false); // 게임 호출
                }
            }
            else
            {
                _shutterManager.ShutterSequence(Result.BigSuccessful, sceneName, false); // 게임 호출
            }
            
        }
    }

    public void EvaluationSetting(bool isAnimate)
    {
        AppearUI(isAnimate);
        List<GameResult> results = NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject());
        if (results.Count > 0)
            samgackManager.SetSamgackStatus(results[results.Count - 1].resultValue);
        samgackManager.AppearSamgack(isAnimate);
    }

    private int SelectGame(SceneField[] gameList)
    {
        int selectNumber = 0;
        gameWeight = new int[gameList.Length];
        int weightSum = 0;

		foreach (GameResult result in NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject()))
		{
			int count = 0;
			foreach (SceneField scene in gameList)
			{
				if (result.name == scene.SceneName)
				{
					gameWeight[count]--;
				}
				count++;
			}
		}

		for (int i = 0; i < gameWeight.Length; i++)
		{
			gameWeight[i] = gameWeight[i] + 8;
			weightSum += gameWeight[i];
		}
		//CalcGameWeigth(out gameWeight, NewGameManager.Instance.GetGameResults(NewGameManager.Instance.GetNowSubject()), gameList);

		selectNumber = RandomWithWeight(gameWeight);

        return selectNumber;
    }

	private void CalcGameWeigth(out int[] gameWeight, List<GameResult> src, SceneField[] gameList)
	{
		gameWeight = new int[gameList.Length];
		for (int i = 0; i < gameWeight.Length; ++i)
			gameWeight[i] = 1;

		foreach(GameResult item in src)
		{
			for(int i = 0; i < gameWeight.Length; ++i)
			{
				gameWeight[i] *= 2;

				if (gameList[i].SceneName == item.name)
				{
					gameWeight[i] = 1;
				}
			}
		}
	}

    private int RandomWithWeight(int[] weight)
    {
        int answer = 0;

        int tmp = Random.Range(0, weight.Sum());
		print(tmp);
        for (; ; ++answer)
        {
            tmp -= weight[answer];
            if (tmp < 0)
                break;
        }

        return answer;
    }

}
