using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTrainingManager : MonoBehaviour {

    [SerializeField] private GameObject selectCanvas;
    [SerializeField] private GameObject statisticCanvas;

    [SerializeField] private Text selectGameNameText;
    [SerializeField] private Text statisticGameNameText;
    [SerializeField] private Text difficultText;
    [SerializeField] private GameObject originalRecordText;
	[SerializeField] private GameObject originalStatisticText;
	[SerializeField] private GameObject recordTextParent;
    [SerializeField] private GameObject statisticTextParent;
    [SerializeField] private GameObject deleteButton;

    private string selectGameName = null;

    // Use this for initialization
    void Start () {
        if(difficultText != null)
        {
            difficultText.text = NewGameManager.Instance.TrainingLevel.ToString();
            LoadRecords();
        }
    }

    public void DifficultUpDown(int value)
    {
        NewGameManager.Instance.TrainingLevel += value;
        difficultText.text = NewGameManager.Instance.TrainingLevel.ToString();
    }

    public void TrainingGameStart()
    {
        if(selectGameName != null)
        {
            ShutterManager.Instance.ShutterSequence(Result.Fail, selectGameName, false);
        }
    }

    public void TrainingGameSelect(int index)
    {
        StartTraining();
        selectGameName = SelectStartGame(index);
        RefreshSelectGameNameText(selectGameNameText);
    }

    public void BackMainMenu()
    {
        EndTraining();
        CustomSceneManager.Instance.ChangeScene("MenuScene");
    }

    public void BackSelectCanvas()
    {
        for(int i = 0; i < statisticTextParent.transform.childCount; i++)
        {
            Destroy(statisticTextParent.transform.GetChild(i).gameObject);
        }
        
        selectCanvas.SetActive(true);
        statisticCanvas.SetActive(false);
    }

    public void ActiveStatistic()
    {
        if(selectGameName != null)
        {
            selectCanvas.SetActive(false);
            statisticCanvas.SetActive(true);
            RefreshSelectGameNameText(statisticGameNameText);
            LoadStatistic();
        }
    }

    public void ToggleDeleteButton()
    {
        deleteButton.SetActive(!deleteButton.activeSelf);
    }

    public void DeleteSelectedGameRecord()
    {
        if(selectGameName != null)
            DeleteSelectedGameRecord(selectGameName);
        ToggleDeleteButton();
    }

    private void DeleteSelectedGameRecord(string gameName) // 선택된 게임의 전적을 삭제하는 함수
    {
        List<MiniGameRecord> _trainingRecords = NewGameManager.Instance.LoadTrainingRecord();

        for(int i = _trainingRecords.Count - 1; i >= 0; i--)
        {
            if(_trainingRecords[i].gameName == gameName)
                _trainingRecords.RemoveAt(i);
        }
        NewGameManager.Instance.SaveTrainingRecord(_trainingRecords);
        DeleteAllChild(recordTextParent);
        LoadRecords(); // 화면상에 다른 것은 그대로 보여주기 위해 재생성
        Debug.Log("DeleteSelectedGameRecord");
    }

    private void StartTraining()
    {
        NewGameManager.Instance.IsTraining = true;
    }

    private void EndTraining()
    {
        NewGameManager.Instance.IsTraining = false;
    }

    private string SelectStartGame(int index)
    {
        string _sceneName = null;
        _sceneName = GetGameName(index);
        return _sceneName;
    }

    private void RefreshSelectGameNameText(Text target)
    {
        target.text = GetChaneGameName(selectGameName);
    }

    private string GetGameName(int index)
    {
        string _sceneName = null;
        switch (index)
        {
            case 1:
                _sceneName = "SomeBodyHelpMe";
                break;
            case 2:
                _sceneName = "WhiteFlag";
                break;
            case 3:
                _sceneName = "Cooking";
                break;
            case 4:
                _sceneName = "BoxingGame";
                break;
            case 5:
                _sceneName = "VegetableSlayer";
                break;
            case 6:
                _sceneName = "MyPresent";
                break;
            case 7:
                _sceneName = "NEmain";
                break;
            case 8:
                _sceneName = "Chicken";
                break;
            case 9:
                _sceneName = "MagicStudent";
                break;
            case 10:
                _sceneName = "Convini";
                break;
            case 11:
                _sceneName = "CoffeeGrinder";
                break;
            case 12:
                _sceneName = "LittelTownHero";
                break;
            case 13:
                _sceneName = "Ticketing";
                break;
        }
        return _sceneName;
    }

    private string GetChaneGameName(string name)
    {
        string _sceneName = null;
        switch (name)
        {
            case "SomeBodyHelpMe":
                _sceneName = "총게임";
                break;
            case "WhiteFlag":
                _sceneName = "대포게임";
                break;
            case "Cooking":
                _sceneName = "요리게임";
                break;
            case "BoxingGame":
                _sceneName = "복싱게임";
                break;
            case "VegetableSlayer":
                _sceneName = "야채게임";
                break;
            case "MyPresent":
                _sceneName = "산타게임";
                break;
            case "NEmain":
                _sceneName = "닌자게임";
                break;
            case "Chicken":
                _sceneName = "치킨게임";
                break;
            case "MagicStudent":
                _sceneName = "카드게임";
                break;
            case "Convini":
                _sceneName = "편의점게임";
                break;
            case "CoffeeGrinder":
                _sceneName = "커피게임";
                break;
            case "LittelTownHero":
                _sceneName = "작마용";
                break;
            case "Ticketing":
                _sceneName = "티켓팅";
                break;
        }
        return _sceneName;
    }

    private string ChangeToResultName(Result result)
    {
        if(result == Result.BigSuccessful)
        {
            return "대성공";
        }
        else if(result == Result.Successful)
        {
            return "성공";
        }
        else
        {
            return "실패";
        }
    }

    private void CreateRecordText(GameObject parent, GameObject target, string value)
    {
        GameObject _child = Instantiate(target, parent.transform);
        _child.GetComponent<Text>().text = value;
        _child.SetActive(true);
    }

    private void DeleteAllChild(GameObject parent)
    {
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

	private void SettingRecordProto(GameObject target, Result result)
	{
		Color bigSucColor = new Color(0, 0, 1, 0.2f);
		Color sucColor = new Color(0, 1, 0, 0.2f);
		Color failColor = new Color(1, 0, 0, 0.2f);

		Image img = target.transform.GetChild(0).GetComponent<Image>();

		switch (result)
		{
			case Result.BigSuccessful:
				img.color = bigSucColor;
				break;
			case Result.Successful:
				img.color = sucColor;
				break;
			case Result.Fail:
				img.color = failColor;
				break;
			default:
				/* Error!! */
				Debug.Break();
				break;
		}
	}
	private void SettingStatisticProto(GameObject target, int bigSuc, int suc, int fail)
	{
		const int childCount = 3;
		Rect rect = target.GetComponent<RectTransform>().rect;
		int total = bigSuc + suc + fail;
		int[] values = { bigSuc, suc, fail };
		/* RectTransform 구하기 */
		RectTransform[] images = new RectTransform[childCount];
		for (int i = 0; i < childCount; ++i)
		{

			images[i] = target.transform.GetChild(i).GetComponent<RectTransform>();
            Debug.Log(images[i].ToString());
        }
        /* 각 Rect 정보 구하기 */
        Vector2[] rects = new Vector2[childCount];
		for(int i = 0; i < childCount; ++i)
		{
			rects[i] = new Vector2((float)values[i] / total * rect.width, rect.height);
		}

		/* 각 크기 및 위치 세팅하기 */
		for (int i = 0; i < childCount; ++i)
		{
			images[i].sizeDelta = rects[i];
			float xPos = 0;
			for(int j = 0; j < i; ++j)
			{
				xPos += rects[j].x;
			}
			xPos += (rects[i].x / 2);
			images[i].anchoredPosition = new Vector2(xPos, 0);
		}
	}

    private void LoadRecords()
    {
        List<MiniGameRecord> trainingRecords = NewGameManager.Instance.LoadTrainingRecord();
        string _value;

        if(trainingRecords != null)
        {
            for (int i = trainingRecords.Count - 1; i >= 0; i--)
            {
                _value = trainingRecords[i].time + "   " + GetChaneGameName(trainingRecords[i].gameName) + "   " +
                    trainingRecords[i].difficult.ToString() + "    " + ChangeToResultName(trainingRecords[i].result) + "    ";
				SettingRecordProto(originalRecordText, trainingRecords[i].result);
				CreateRecordText(recordTextParent, originalRecordText, _value);
            }
        }
        else
        {
            Debug.Log("trainingRecords is null");
        }
    }

    private void LoadStatistic()
    {
        List<MiniGameRecord> trainingRecords = NewGameManager.Instance.LoadTrainingRecord();
        LoadStatistic(trainingRecords, selectGameName);
    }

    public void LoadStatistic(List<MiniGameRecord> gameRecords, string selectGameName)
    {
        List<SubStatisticRecord> selectStatisticRecord = new List<SubStatisticRecord>();
        string _value;

        if (gameRecords != null)
        {
            for(int i = 0; i < gameRecords.Count; i++)
            {
                if(gameRecords[i].gameName == selectGameName)
                {
                    AddStatisticRecordList(selectStatisticRecord, gameRecords[i]);
                }
            }
            for(int i = 0; i < selectStatisticRecord.Count; i++)
            {
                int _allCount = selectStatisticRecord[i].AllCount();
                int _bigSuc = selectStatisticRecord[i].bigSuccessfulCount;
                int _suc = selectStatisticRecord[i].successfulCount;
                int _fail = selectStatisticRecord[i].failCount;
                _value = "  난이도: " + selectStatisticRecord[i].difficult + "   대성공: " + _bigSuc +
                    "(" + TwoIntToPercent(_bigSuc, _allCount) + ")   성공: " + _suc + "(" + TwoIntToPercent(_suc, _allCount)
                    + ")   실패: " + _fail + "(" + TwoIntToPercent(_fail, _allCount) + ")   총 횟수: "
                    + _allCount;

                SettingStatisticProto(originalStatisticText, _bigSuc, _suc, _fail);
                CreateRecordText(statisticTextParent, originalStatisticText, _value);
            }
        }
        else
        {
            Debug.Log("trainingRecords is null");
        }
    }

    private string TwoIntToPercent(float numerator, float denominator)
    {
        float _result = numerator / denominator;
        _result *= 100; 
        return ((int)_result).ToString() + "%";
    }

    private void AddStatisticRecordList(List<SubStatisticRecord> list, MiniGameRecord target)
    {
        bool _isContain = false;
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].difficult == target.difficult)
            {
                list[i].AddResultRecord(target.result);
                _isContain = true;
            }
        }
        if(!_isContain) list.Add(new SubStatisticRecord(target.difficult, target.result));

        list.Sort(delegate (SubStatisticRecord a, SubStatisticRecord b)
        {
            if (a.difficult > b.difficult) return 1;
            else if (a.difficult < b.difficult) return -1;
            return 0;
        });
    }

    public class SubStatisticRecord
    {
        public int difficult;
        public int bigSuccessfulCount = 0;
        public int successfulCount = 0;
        public int failCount = 0;

        public SubStatisticRecord(int difficult, Result result)
        {
            this.difficult = difficult;
            AddResultRecord(result);
        }

        public void AddResultRecord(Result result)
        {
            if (result == Result.BigSuccessful) bigSuccessfulCount++;
            else if (result == Result.Successful) successfulCount++;
            else if (result == Result.Fail) failCount++;
        }

        public int AllCount()
        {
            return bigSuccessfulCount + successfulCount + failCount;
        }
    }
    public GameObject GetStatisticTextParent()
    {
        return statisticTextParent;
    }
}

[Serializable]
public class MiniGameRecord
{
    public string time;
    public string gameName;
    public int difficult;
    public Result result;

    public MiniGameRecord(string time, string gameName, int difficult, Result result)
    {
        this.time = time;
        this.gameName = gameName;
        this.difficult = difficult;
        this.result = result;
    }

    public MiniGameRecord(string gameName, int difficult, Result result)
    {
        this.gameName = gameName;
        this.difficult = difficult;
        this.result = result;
    }
}