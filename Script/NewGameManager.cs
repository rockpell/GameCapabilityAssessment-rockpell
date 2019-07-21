using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] Sprite[] miniGameIcons;
    [SerializeField] Sprite[] subjectIcons;
    [SerializeField] Sprite[] resultStampIcons;
    [SerializeField] Sprite clearStampIcon;

    /*전체 평가 값*/
    private List<GameResult>[] subjectResult = new List<GameResult>[5]; // 항목별 게임 결과들
    List<Subject> endSubjectList;
    [SerializeField] private int maxEvalationTime = 8; // 항목별 최대 평가 횟수

	/* 화면 터치 방지용 패널 */
	[Header("Panel Object")]
	[SerializeField]
	private GameObject worldPanel;
	[SerializeField]
	private GameObject canvasPanel;

    [SerializeField] private AudioSource effectAudio;
    [SerializeField] private AudioSource bgmAudio;

    /*현재 평가 값*/
    //private List<int> evaluationGameList; // 미니게임 순서 저장하는 용도, CustomSceneManager에 연결되어 있는 미니게임 인덱스값을 저장
    private Subject nowSubject;
    private int nowEvalationTime;
    private int startDifficultyLevel;
    private int trainingLevel = 5;

    public const int maxDifficultyLevel = 15;
    public const int minDifficultyLevel = 1;
    //private int nowDifficultyLevel;

    private string nowMiniGame;

    private bool isGameStart = false;
    private bool isSkipTutorial = false;
    private bool isSkipTouchToStart = false;
    private bool isTraining = false;
    private bool isExistMiddleData = false;

    private IntroTrigger startIntroTrigger;

    private static NewGameManager instance;

    public static NewGameManager Instance {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            Screen.SetResolution(1920, 1080, false);
            Application.targetFrameRate = 30;

            instance = GetComponent<NewGameManager>();
            DontDestroyOnLoad(this.gameObject);

            nowSubject = Subject.None;
            startDifficultyLevel = 5;
            nowEvalationTime = 0;
            nowMiniGame = "";

			InitPanel();

			for (int i = 0; i < subjectResult.Length; i++) subjectResult[i] = new List<GameResult>();
            endSubjectList = new List<Subject>();
            //evaluationGameList = new List<int>();

            ApplyMiddleData(); // 게임을 실행시켰을 때 중간데이터가 남아있다면 적용시키는 함수
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //DeleteAllRecord();
        //CreateTestGameReulst(8); // 테스트 케이스 자동생성
        //SaveGameRecord(); // 결과 저장
        //Debug.Log("저장된 결과 개수 : " + LoadGameRecord().Count);

        //List<GameRecord> _list = LoadGameRecord();

        //for (int i = 0; i < _list.Count; i++)
        //{
        //    Debug.Log("시간 : " + _list[i].time);
        //    for(int p = 0; p < _list[i].gameResults.Length; p++)
        //    {
        //        List<GameResult> _resultList = _list[i].gameResults[p];

        //        for(int q = 0; q < _resultList.Count; q++)
        //        {
        //            Debug.Log(_resultList[q].name + "  :  " + _resultList[q].resultValue);
        //        }
        //    }
        //}
        startIntroTrigger = LoadIntroTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                ClearGame(Result.BigSuccessful);
            }
            if (Input.GetKey(KeyCode.W))
            {
                ClearGame(Result.Successful);
            }
            if (Input.GetKey(KeyCode.E))
            {
                ClearGame(Result.Fail);
            }
        }
	}

	public int StartGame() // 각 미니게임이 시작될때 실행되어야하는 함수
    {
        if (isTraining)
        {
			isGameStart = true;
            return trainingLevel;
        }
        else if (nowSubject != Subject.None)
        {
            isGameStart = true;
            return GetNowDifficultyLevel(nowSubject); // 현재 게임 난이도를 계산한 값이 필요
        }
        else
        {
            return 1;
        }
    }

    public void ClearGame(Result score) // 각 미니게임이 끝났을때 실행되어야 하는 함수
    {
        GameObject tutorialCanvas = GameObject.Find("TutorialCanvas");
        
        if (isGameStart)
        {
            if (tutorialCanvas != null) // 튜토리얼 씬일 경우 튜토리얼 종료
            {
                tutorialCanvas.GetComponent<NewTutorialManager>().EndTutorial();
            }
            else if (isTraining)
            {
                isTraining = false;
                isGameStart = false;

                SaveTrainingRecord(score);

                if (ShutterManager.Instance != null)
                {
                    ShutterManager.Instance.ShutterSequence(score, "TestTraining", true);
                }
            }
            else
            {
                isGameStart = false;
                subjectResult[GetSubjectIndex(nowSubject)].Add(new GameResult(CustomSceneManager.Instance.GetNowSceneName(), score)); // 게임의 결과를 저장

                SaveMiddleData(); // 게임 중간 저장

                // 셔터매니저 호출로 씬전환
                if (ShutterManager.Instance != null)
                {
                    ShutterManager.Instance.ShutterSequence(score, "NewEvaluation", true);
                }
            }
        }
    }

    public int GetNowDifficultyLevel(Subject subject)
    {
        int _result = 0;
        _result = startDifficultyLevel;

        if(subject != Subject.None)
        {
            if (subjectResult[GetSubjectIndex(subject)] != null)
            {
                if (subjectResult[GetSubjectIndex(subject)].Count > 0)
                {
                    for (int i = 0; i < subjectResult[GetSubjectIndex(subject)].Count; i++)
                    {
                        _result += (int)subjectResult[GetSubjectIndex(subject)][i].resultValue;
                        if (_result < minDifficultyLevel) _result = 1;
                        else if (_result > maxDifficultyLevel) _result = 15;
                    }
                }
            }
        }
        return _result;
    }

    public int CalculateResultValue(List<GameResult> list)
    {
        int _result = startDifficultyLevel;
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].resultValue == Result.BigSuccessful)
            {
                _result += 2;
            }
            else if(list[i].resultValue == Result.Successful)
            {
                _result += 1;
            }
            else if(list[i].resultValue == Result.Fail)
            {
                _result += -1;
            }

            if (_result > 15) _result = 15;
            else if (_result < 1) _result = 1;
        }
        return _result;
    }

    public void SetNowSubjet(Subject subject)
    {
        nowSubject = subject;
    }

    public Subject GetNowSubject()
    {
        return nowSubject;
    }

    public bool AddEndSubjectList(Subject subject)
    {
        if(subject != Subject.None)
        {
            if (subjectResult[GetSubjectIndex(subject)].Count == maxEvalationTime)
            {
                endSubjectList.Add(subject);
                return true;
            }
            else if(subjectResult[GetSubjectIndex(subject)].Count > maxEvalationTime)
            {
                Debug.LogError("GetEndSubjectList Error!");
            }
        }
        return false;
    }

    public List<Subject> GetEndSubjectList()
    {
        return endSubjectList;
    }

    public List<GameResult> GetGameResults(Subject subject)
    {
        return subjectResult[GetSubjectIndex(subject)];
    }

    public List<GameResult>[] GetGameReuslts()
    {
        return subjectResult;
    }

    public int GetMaxEvalationTime() // 항목별 최대 평가 횟수 반환
    {
        return maxEvalationTime;
    }

    public int GetLeftEvalationTime() // 현재 항목 남은 평가 횟수 반환
    {
        return maxEvalationTime - nowEvalationTime;
    }

    public int NowEvalationTime {
        get { return nowEvalationTime; }
        set { nowEvalationTime = value; }
    }
    
    public string NowMiniGame {
        get { return nowMiniGame; }
        set { nowMiniGame = value; }
    }

    public int StartDifficultyLevel {
        get { return startDifficultyLevel; }
        set { startDifficultyLevel = value; }
    }

    public bool IsSkipTutorial
    {
        get { return isSkipTutorial; }
        set { isSkipTutorial = value; }
    }

    public bool IsSkipTouchToStart
    {
        get { return isSkipTouchToStart; }
        set { isSkipTouchToStart = value; }
    }

    public bool IsTraining
    {
        get { return isTraining; }
        set { isTraining = value; }
    }

    public int TrainingLevel
    {
        get { return trainingLevel; }
        set
        {
            if (value > maxDifficultyLevel) trainingLevel = maxDifficultyLevel;
            else if (value < minDifficultyLevel) trainingLevel = minDifficultyLevel;
            else trainingLevel = value;
        }
    }

    public bool IsPlayedMiniGame(Subject subject, string gameName)
    {
        int _index = GetSubjectIndex(subject);
        bool _result = false;
        for(int i = 0; i < subjectResult[_index].Count; i++)
        {
            if(subjectResult[_index][i].name == gameName)
            {
                _result = true;
                break;
            }
        }
        return _result;
    }

    public bool IsExistMiddleData
    {
        get { return isExistMiddleData; }
        set { isExistMiddleData = value; }
    }

    public bool IsNeedIntro()
    {
        if (isExistMiddleData) return false;
        else if (IsExistGameRecord()) return false;

        return true;
    }

    public AudioSource EffectSouce
    {
        get { return effectAudio; }
    }

    public void PlayBGM(float delayTime)
    {
        GetComponent<BGMManager>().StartBGM(delayTime);
    }

    public void StopBGM()
    {
        GetComponent<BGMManager>().StopBGM(1);
    }

    public void PauseBGM()
    {
        GetComponent<BGMManager>().PauseBGM(1);
    }

    public void SetNowEvalationTime(int value)
    {
        nowEvalationTime = value;
    }

    public bool IsEvalationEnd()
    {
        if(endSubjectList.Count == 5)
            return true;
        return false;
    }

    public IntroTrigger StartIntroTrigger
    {
        get { return startIntroTrigger; }
    }

    public Sprite GetMiniGameIcon(string sceneName)
    {
        Sprite _result = null;

        switch (sceneName)
        {
            case "BoxingGame":
                _result = miniGameIcons[0];
                break;
            case "Chicken":
                _result = miniGameIcons[1];
                break;
            case "CoffeeGrinder":
                _result = miniGameIcons[2];
                break;
            case "Cooking":
                _result = miniGameIcons[3];
                break;
            case "MyPresent":
                _result = miniGameIcons[4];
                break;
            case "NEmain":
                _result = miniGameIcons[5];
                break;
            case "SomeBodyHelpMe":
                _result = miniGameIcons[6];
                break;
            case "VegetableSlayer":
                _result = miniGameIcons[7];
                break;
            case "WhiteFlag":
                _result = miniGameIcons[8];
                break;
            case "MagicStudent":
                _result = miniGameIcons[9];
                break;
            case "Convini":
                _result = miniGameIcons[10];
                break;
            case "Ticketing":
                _result = miniGameIcons[11];
                break;
        }

        return _result;
    }

    //public Sprite GetSubjectIcon(Subject subject)
    //{
    //    string _path = "Images/SubjectIcon/" + subject.ToString() + "Icon";
    //    return Resources.Load<Sprite>(_path);
    //}

    public Sprite GetSubjectIcon(Subject subject)
    {
        Sprite _result = null;
        switch (subject)
        {
            case Subject.Aiming:
                _result = subjectIcons[0];
                break;
            case Subject.Concentration:
                _result = subjectIcons[1];
                break;
            case Subject.Quickness:
                _result = subjectIcons[2];
                break;
            case Subject.RhythmicSense:
                _result = subjectIcons[3];
                break;
            case Subject.Thinking:
                _result = subjectIcons[4];
                break;
        }
        return _result;
    }

    //public Sprite GetResultStampIcon(Result result)
    //{
    //    string _path = "Images/StampIcon/" + result.ToString() + "Stamp";
    //    Sprite _result = Resources.Load<Sprite>(_path);
    //    if(_result == null) Resources.Load<Sprite>("Images/StampIcon/nothing");
    //    return _result;
    //}

    public Sprite GetResultStampIcon(Result result)
    {
        Sprite _result = null;

        switch (result)
        {
            case Result.BigSuccessful:
                _result = resultStampIcons[0];
                break;
            case Result.Successful:
                _result = resultStampIcons[1];
                break;
            case Result.Fail:
                _result = resultStampIcons[2];
                break;
        }
        
        return _result;
    }

    //public Sprite GetClearStampIcon()
    //{
    //    string _path = "Images/StampIcon/ClearStamp";
    //    Sprite _result = Resources.Load<Sprite>(_path);
    //    if (_result == null) Resources.Load<Sprite>("Images/StampIcon/nothing");
    //    return _result;
    //}

    public Sprite GetClearStampIcon()
    {
        return clearStampIcon;
    }

    public int GetSubjectIndex(Subject subject)
	{
		int index = -1;
		switch (subject)
		{
			case Subject.Aiming:
				index = 0;
				break;
			case Subject.Concentration:
				index = 1;
				break;
            case Subject.Quickness:
                index = 2;
				break;
            case Subject.RhythmicSense:
                index = 3;
				break;
			case Subject.Thinking:
				index = 4;
				break;
		}
		return index;
	}

    private Subject GetSubejctToIndex(int index)
    {
        Subject _result = Subject.None;
        switch (index)
        {
            case 0:
                _result = Subject.Aiming;
                break;
            case 1:
                _result = Subject.Concentration;
                break;
            case 2:
                _result = Subject.Quickness;
                break;
            case 3:
                _result = Subject.RhythmicSense;
                break;
            case 4:
                _result = Subject.Thinking;
                break;
        }

        return _result;
    }

    public void InitialClearData()
    {
        nowSubject = Subject.None;
        nowMiniGame = "";
        endSubjectList.Clear();
        for (int i = 0; i < subjectResult.Length; i++)
            subjectResult[i].Clear();
            
        DeleteMiddleData(); // 중간 저장 데이터 삭제
    }

    public void SaveGameRecord()
    {
        List<GameRecord> _gameRecordList = new List<GameRecord>();
        List<GameRecord> _gameRecordHistory = LoadGameRecord();
        GameRecord _gameRecord = new GameRecord(CreateNowTimeData(), subjectResult);

        if(_gameRecordHistory.Count > 0) // 과거 기록들을 저장할 리스트에 넣기
        {
            _gameRecordList = _gameRecordHistory;
        }

        _gameRecordList.Add(_gameRecord);

        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream();

        _binaryFormatter.Serialize(_memoryStream, _gameRecordList);
        PlayerPrefs.SetString("GameRecords", Convert.ToBase64String(_memoryStream.GetBuffer()));
    }

    private string CreateNowTimeData()
    {
        DateTime _nowData = DateTime.Now;
        int _year = _nowData.Year;
        int _month = _nowData.Month;
        int _day = _nowData.Day;
        int _hour = _nowData.Hour;
        int _minute = _nowData.Minute;
        int _second = _nowData.Second;

        string _result = _year.ToString("0000") + _month.ToString("00") + _day.ToString("00") + _hour.ToString("00") + _minute.ToString("00") + _second.ToString("00");

        return _result;
    }

    public void SaveTrainingRecord(List<MiniGameRecord> list)
    {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream();

        _binaryFormatter.Serialize(_memoryStream, list);
        PlayerPrefs.SetString("TrainingRecords", Convert.ToBase64String(_memoryStream.GetBuffer()));
    }

    private void SaveTrainingRecord(Result score)
    {
        List<MiniGameRecord> _trainingRecordList = new List<MiniGameRecord>();
        List<MiniGameRecord> _trainingRecordHistory = LoadTrainingRecord();
        MiniGameRecord _trainingRecord = new MiniGameRecord(CreateNowTimeDataDotFormat(), CustomSceneManager.Instance.GetNowSceneName(),
            trainingLevel, score);

        if (_trainingRecordHistory.Count > 0)
        {
            _trainingRecordList = _trainingRecordHistory;
        }
        _trainingRecordList.Add(_trainingRecord);

        SaveTrainingRecord(_trainingRecordList);
    }

    public List<MiniGameRecord> LoadTrainingRecord()
    {
        string _trainingRecordsString = PlayerPrefs.GetString("TrainingRecords");
        List<MiniGameRecord> _trainingRecordList = new List<MiniGameRecord>();

        if (!string.IsNullOrEmpty(_trainingRecordsString))
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream(Convert.FromBase64String(_trainingRecordsString));

            _trainingRecordList = (List<MiniGameRecord>)_binaryFormatter.Deserialize(_memoryStream);
        }

        return _trainingRecordList;
    }

    private string CreateNowTimeDataDotFormat()
    {
        DateTime _nowData = DateTime.Now;
        int _year = _nowData.Year;
        int _month = _nowData.Month;
        int _day = _nowData.Day;
        int _hour = _nowData.Hour;
        int _minute = _nowData.Minute;
        int _second = _nowData.Second;

        string _result = _year.ToString("0000") + "." + _month.ToString("00") + "." + _day.ToString("00") + "." + _hour.ToString("00") + "."
            + _minute.ToString("00") + "." + _second.ToString("00");

        return _result;
    }

    public List<GameRecord> LoadGameRecord()
    {
        string _gameRecordsString = PlayerPrefs.GetString("GameRecords");
        List<GameRecord> _gameRecordList = new List<GameRecord>();

        if (!string.IsNullOrEmpty(_gameRecordsString))
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream(Convert.FromBase64String(_gameRecordsString));

            _gameRecordList = (List<GameRecord>)_binaryFormatter.Deserialize(_memoryStream);
        }

        return _gameRecordList;
    }

    private bool IsExistGameRecord()
    {
        string _gameRecordsString = PlayerPrefs.GetString("GameRecords");
        if (!string.IsNullOrEmpty(_gameRecordsString))
        {
            return true;
        }
        return false;
    }

    [ContextMenu("DeleteAllRecord")]
    public void DeleteAllRecord()
    {
        PlayerPrefs.DeleteAll();
    }

    [ContextMenu("DeleteIntroTrigger")]
    public void DeleteIntroTrigger()
    {
        PlayerPrefs.DeleteKey("IntroTrigger");
    }

    public List<MiniGameRecord> LoadMiniGameRecord(string gameName)
    {
        List<MiniGameRecord> _miniGameRecord = new List<MiniGameRecord>();
        List<GameRecord> _gameRecordList = LoadGameRecord();
        int _subjectIndex = FindSubjectIndex(gameName);
        if (_subjectIndex != -1)
        {
            for (int i = 0; i < _gameRecordList.Count; i++)
            {
                List<GameResult> _gameResultList = _gameRecordList[i].gameResults[_subjectIndex];
                int _difficultyLevel = startDifficultyLevel;
                for (int p = 0; p < _gameResultList.Count; p++)
                {
                    if(_gameResultList[p].name == gameName)
                    {
                        _miniGameRecord.Add(new MiniGameRecord(gameName, _difficultyLevel, _gameResultList[p].resultValue));
                    }

                    _difficultyLevel += (int)_gameResultList[p].resultValue;
                    if (_difficultyLevel > maxDifficultyLevel) _difficultyLevel = maxDifficultyLevel;
                    else if (_difficultyLevel < minDifficultyLevel) _difficultyLevel = minDifficultyLevel;
                }
            }
        }
        return _miniGameRecord;
    }

    private int FindSubjectIndex(string miniGameName)
    {
        int _result = -1;
        for(int i = 0; i < 5; i++)
        {
            SceneField[] _names = CustomSceneManager.Instance.GetSceneNames(GetSubejctToIndex(i));

            for(int p = 0; p < _names.Length; p++)
            {
                if (_names[p] == miniGameName)
                {
                    _result = i;
                    break;
                }
            }
            if (_result != -1) break;
        }

        return _result;
    }

    [ContextMenu("CreateTestGameReulst")]
    public void CreateTestGameReulst()
    {
        CreateTestGameReulst(8);
        for(int i = 0; i < 5; i++)
        {
            AddEndSubjectList(GetSubejctToIndex(i));
        }
    }

    private void CreateTestGameReulst(int evalationTime)
    {
        for(int i = 0; i < subjectResult.Length; i++)
        {
            for(int p = 0; p < evalationTime; p++)
            {
                subjectResult[i].Add(new GameResult(RandomMinigame(GetSubejctToIndex(i)), RandomResult()));
            }
        }
    }

    private string RandomMinigame(Subject subject)
    {
        SceneField[] _names = CustomSceneManager.Instance.GetSceneNames(subject);
        int _index = UnityEngine.Random.Range(0, _names.Length);

        return _names[_index];
    }

    private Result RandomResult()
    {
        int _index = UnityEngine.Random.Range(0,3);
        Result _result = Result.BigSuccessful;

        if(_index == 0)
        {
            _result = Result.Fail;
        }
        else if (_index == 1)
        {
            _result = Result.Successful;
        }
        else
        {
            _result = Result.BigSuccessful;
        }

        return _result;
    }

    public void TestRandomRecordSave(int evalationTime)
    {
        CreateTestGameReulst(evalationTime); // 테스트 케이스 자동생성
        SaveGameRecord(); // 결과 저장
        InitialClearData();
    }

    private void SaveMiddleData()
    {
        GameRecord _gameRecord = new GameRecord(CreateNowTimeData(), subjectResult);

        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream();

        _binaryFormatter.Serialize(_memoryStream, _gameRecord);
        PlayerPrefs.SetString("MiddleData", Convert.ToBase64String(_memoryStream.GetBuffer()));
    }

    private GameRecord LoadMiddleData()
    {
        GameRecord _gameRecord = null;

        string _gameRecordsString = PlayerPrefs.GetString("MiddleData");
        if (!string.IsNullOrEmpty(_gameRecordsString))
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream(Convert.FromBase64String(_gameRecordsString));

            _gameRecord = (GameRecord)_binaryFormatter.Deserialize(_memoryStream);
        }

        return _gameRecord;
    }

    private void ApplyMiddleData()
    {
        GameRecord _middleData = LoadMiddleData();

        if(_middleData != null)
        {
            subjectResult = _middleData.gameResults;

            for(int i = 0; i < subjectResult.Length; i++)
            {
                if(subjectResult[i].Count == maxEvalationTime)
                {
                    AddEndSubjectList(GetSubejctToIndex(i));
                }
            }
            isExistMiddleData = true;
        }
    }

    private void DeleteMiddleData()
    {
        PlayerPrefs.DeleteKey("MiddleData");
    }

    public void ResetMiddleData()
    {
        nowSubject = Subject.None;
        nowMiniGame = "";
        endSubjectList.Clear();

        for (int i = 0; i < subjectResult.Length; i++)
            subjectResult[i].Clear();

        DeleteMiddleData(); // 중간 저장 데이터 삭제
    }

    public bool IsMiddleData()
    {
        string _gameRecordsString = PlayerPrefs.GetString("MiddleData");

        if (!string.IsNullOrEmpty(_gameRecordsString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	public void SetTouchDisablePanel(bool isActivate, int worldPanelLayer = 5, int canvasPanelLayer = 1)
	{
		if(isActivate)
		{
			InitPanel(worldPanelLayer, canvasPanelLayer);
			worldPanel.SetActive(true);
			canvasPanel.SetActive(true);
		}
		else
		{
            if (worldPanel.activeSelf)
            {
                StartCoroutine(DelayOnOff(worldPanel, false));
            }
            if (canvasPanel.activeSelf)
            {
                StartCoroutine(DelayOnOff(canvasPanel, false));
            }
        }
	}

    private IEnumerator DelayOnOff(GameObject target, bool isActive)
    {
        yield return null;
        target.SetActive(isActive);
    }

	private void InitPanel(int worldPanelLayer = 5, int canvasPanelLayer = 1)
	{
		//worldPanel
		worldPanel.transform.position = Camera.main.transform.position * 0.9f;
		Vector2 size = (Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Vector3.zero)) * 2;
		worldPanel.transform.GetComponentInChildren<BoxCollider>().size = new Vector3(size.x, size.y, 0.1f);
		worldPanel.transform.GetComponentInChildren<BoxCollider2D>().size = size;
		worldPanel.transform.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(size.x, size.y, 0.1f);

		worldPanel.transform.GetComponentInChildren<SpriteRenderer>().sortingOrder = worldPanelLayer;

		//canvasPanel
		Vector2 imageSize = canvasPanel.GetComponent<CanvasScaler>().referenceResolution;
		canvasPanel.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = imageSize;

		canvasPanel.GetComponent<Canvas>().sortingOrder = canvasPanelLayer;
	}

    public void SaveIntroTrigger(IntroTrigger introTrigger)
    {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream();

        _binaryFormatter.Serialize(_memoryStream, introTrigger);
        PlayerPrefs.SetString("IntroTrigger", Convert.ToBase64String(_memoryStream.GetBuffer()));
    }

    public IntroTrigger LoadIntroTrigger()
    {
        IntroTrigger _introTrigger = null;

        string _introTriggerString = PlayerPrefs.GetString("IntroTrigger");
        if (!string.IsNullOrEmpty(_introTriggerString))
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream(Convert.FromBase64String(_introTriggerString));

            _introTrigger = (IntroTrigger)_binaryFormatter.Deserialize(_memoryStream);
        }

        return _introTrigger;
    }
}