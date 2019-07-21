using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //public enum Subject { Aiming, Concentration, Quickness, RhythmicSense, Thinking, None };
    public enum Result { D = -2, C = -1, B = 1, A = 2 };
    // 조준 : 빨강, 집중 : 파랑, 순발 : 노랑, 리듬 : 보라, 사고 : 초록
    [SerializeField] private GameObject miniGameEndUI;

    private Dictionary<Subject, int> subjectScore;

    private List<int> evaluationSubject; // 실행한 항목 저장
    private List<int> evaluationGameList; // 미니게임 순서 저장하는 용도

    private CustomEvent nowEvent; // 현재 진행중인 이벤트

    //private Ads ads;
    //private int leftCountPlay = 0;
    //private const int playCountForAd = 6; // 광고 출력을 위한 기준
    private int difficultyLevel = 5; // difficultyLevel 1 ~ 10, ~15
    private int previousDifficultyLevel = 5; // 예전 difficultyLevel
    private int accumulateGame = 0; // play 한 게임 횟수
    private int evaluationGameIndex = 0; // 평가 게임 리스트 index
    private int evaluationSubjectIndex = -1; // 평가 항목 리스트 index
    private int trainingLife = 3; // 트레이닝 남은 실패 기회

    private bool isHiddenStroy = true;
    private bool isGameStart = false;
    private bool isGameOver = false;
    private bool isGameClear = false;
    private bool isEvaluationStart = false;
    private bool isTrainingStart = false;
    private bool isTutorial = false;

    private CheckTraining checkTraining; // 트레이닝 실행 유무

    private Subject nowSubject; // 현재 진행중인 평가의 항목
    private Subject previousSubject; // 한 차례 전에 진행한 항목

    private List<Result> previousResult;

    private static GameManager instance;

    public static GameManager Instance {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<GameManager>();
            DontDestroyOnLoad(this.gameObject);
            InitTrigger();
            subjectScore = new Dictionary<Subject, int>();
            evaluationSubject = new List<int>();
            evaluationGameList = new List<int>();
            previousResult = new List<Result>();
            //ads = GetComponent<Ads>();
        }
        else
        {
            Destroy(this.gameObject);
        }
        Screen.SetResolution(1920, 1080, false);
    }

    void Start ()
	{
        
    }
	
	// Update is called once per frame
	void Update () {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (isTrainingStart)
                {
                    CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.TrainingScene);
                }
                else
                {
                    CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.SeletScene);
                }
                
                //checkTraining = null;
                InitTrigger();
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            ClearGame(Result.A);
        } else if (Input.GetKey(KeyCode.W))
        {
            ClearGame(Result.B);
        } else if (Input.GetKey(KeyCode.E))
        {
            ClearGame(Result.C);
        } else if (Input.GetKey(KeyCode.R))
        {
            ClearGame(Result.D);
        }
    }

    public void InitTrigger()
    {
        IsGameStart = false;
        IsGameOver = false;
        IsGameClear = false;
        IsEvaluationStart = false;
        isTrainingStart = false;
        isHiddenStroy = true;
        isTutorial = false;
        difficultyLevel = 5;
        accumulateGame = 0;
        trainingLife = 3;
        nowSubject = Subject.None;
        previousSubject = Subject.None;
        evaluationGameList = new List<int>();
        evaluationSubject = new List<int>();
        evaluationSubjectIndex = -1;
        checkTraining = null;
    }

    public bool IsGameStart {
        get { return isGameStart; }
        set { isGameStart = value; }
    }

    public bool IsGameOver {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public bool IsGameClear {
        get { return isGameClear; }
        set { isGameClear = value; }
    }

    public bool IsEvaluationStart {
        get { return isEvaluationStart; }
        set { isEvaluationStart = value; }
    }

    public bool IsTrainingStart {
        get { return isTrainingStart; }
        set { isTrainingStart = value; }
    }

    public bool IsHiddenStroy {
        get { return isHiddenStroy; }
        set { isHiddenStroy = value; }
    }

    public bool IsTutorial {
        get { return isTutorial; }
        set { isTutorial = value; }
    }

    public int DifficultyLevel {
        get { return difficultyLevel; }
        set { difficultyLevel = value; }
    }

    public int PreviousDifficultyLevel {
        get { return previousDifficultyLevel; }
        set { previousDifficultyLevel = value; }
    }

    public int AccumulateGame {
        get { return accumulateGame; }
        set { accumulateGame = value; }
    }

    public int EvaluationSubjectIndex {
        get { return evaluationSubjectIndex; }
        set { evaluationSubjectIndex = value; }
    }

    public int EvaluationGameIndex {
        get { return evaluationGameIndex; }
    }

    public int TrainingLife {
        get { return trainingLife; }
        set { trainingLife = value; }
    }

    public List<int> EvaluationSubject {
        get { return evaluationSubject; }
    }

    public Subject NowSubject {
        get { return nowSubject; }
        set { nowSubject = value; }
    }

    public Subject PreviousSubject {
        get { return previousSubject; }
        set { previousSubject = value; }
    }

    public List<Result> PreviousResult {
        get { return previousResult; }
    }

    public CheckTraining CheckTraining {
        get { return checkTraining; }
    }

    public Dictionary<Subject, int> SubjectScore {
        get { return subjectScore; }
        set { subjectScore = value; }
    }

    public CustomEvent NowEvent {
        get { return nowEvent; }
        set { nowEvent = value; }
    }

    public int StartGame() // 각 미니게임이 시작될때 실행되어야하는 함수
    {
        isGameStart = true;
        return difficultyLevel;
    }

    public void ClearGame(Result score) // 각 미니게임이 끝났을때 실행되어야 하는 함수
	{
        if (isGameStart)
        {
            isGameStart = false;
            previousResult.Add(score);
            accumulateGame += 1;

            previousDifficultyLevel = difficultyLevel;
            difficultyLevel += (int)score;

            if (isHiddenStroy)
            {
                if (difficultyLevel > 15) difficultyLevel = 15;
            }
            else
            {
                if (difficultyLevel > 10) difficultyLevel = 10;
            }
            if (difficultyLevel < 1) difficultyLevel = 1;

            string scoreText = "";
            if (score == Result.A) scoreText = "대성공";
            else if (score == Result.B) scoreText = "성공";
            else if (score == Result.C) scoreText = "실패";
            else if (score == Result.D) scoreText = "대실패";

            EndUI _endUI = CreateEndUI();
            _endUI.GameOverPanelOn(scoreText);
            _endUI.WhiteNoiseOn(1f);

            if (IsEvaluationStart)
                CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.Evaluation, 1.5f);
            else
                CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.TrainingScene, 1.5f);

        }
    }

    public EndUI CreateEndUI()
    {
        EndUI _endUI = Instantiate(miniGameEndUI).GetComponent<EndUI>();
        return _endUI;
    }

    private void SaveSubjectScore() // 각 항목의 평가 점수를 저장
    {
        if (nowSubject != Subject.None)
        {
            if (subjectScore.ContainsKey(nowSubject))
            {
                subjectScore[nowSubject] = difficultyLevel;
            } else
            {
                subjectScore.Add(nowSubject, difficultyLevel);
            }
        }
    }

    public void StartSubject() // 각 항목이 시작될때 실행되어야 하는 함수
    {
        IsEvaluationStart = true;
        previousResult.Clear();
        ChoiceMiniGame();
    }

    //public void NextSubject()
    //{
    //    evaluationSubjectIndex += 1;
    //    //previousSubject = nowSubject;
    //    nowSubject = (Subject)evaluationSubject[evaluationSubjectIndex];
    //}

    private void ChoiceMiniGame() // 각 항목에서 평가되는 미니게임 선정 하는 함수
    {
        SceneField[] _tempFields =  CustomSceneManager.Instance.GetSceneNames(nowSubject);
        if (evaluationGameList.Count != 0) evaluationGameList.Clear();

        //if(_tempFields.Length < 3) // 등록된 미니게임이 3개보다 적을 경우 중복 실행 가능
        //{
        //    while (evaluationGameList.Count < 3)
        //    {
        //        int _randNum = Random.Range(0, _tempFields.Length);
        //        evaluationGameList.Add(_randNum);
        //    }
        //} else
        //{
        //    while (evaluationGameList.Count < 3)
        //    {
        //        int _randNum = Random.Range(0, _tempFields.Length);
        //        if (!evaluationGameList.Contains(_randNum))
        //        {
        //            evaluationGameList.Add(_randNum);
        //        }
        //    }
        //}
        while (evaluationGameList.Count < 11)
        {
            int _randNum = Random.Range(0, _tempFields.Length);
            evaluationGameList.Add(_randNum);
        }
    }

    public bool SelectSubject(int index)
    {
        bool _result = false;
        if (!evaluationSubject.Contains(index))
        {
            //evaluationSubject.Add(index);
            nowSubject = (Subject)index;
            _result = true;
        }
        return _result;
    }

    //public void ChoiceSubject() // 평가 항목 순서 정하는 함수
    //{
    //    if (evaluationSubject.Count != 0) evaluationSubject.Clear();

    //    while(evaluationSubject.Count < 5)
    //    {
    //        int randNum = Random.Range(0, 5);
    //        if (!evaluationSubject.Contains(randNum))
    //        {
    //            evaluationSubject.Add(randNum);
    //        }
    //    }
    //    evaluationSubject.Add(5); // None 추가
    //}

    //public void ChoiceSubject(int index) // 평가 항목 우선적으로 하나 넣고 시작하는 함수
    //{
    //    if (evaluationSubject.Count != 0) evaluationSubject.Clear();
    //    evaluationSubject.Add(index);
    //    while (evaluationSubject.Count < 5)
    //    {
    //        int randNum = Random.Range(0, 5);
    //        if (!evaluationSubject.Contains(randNum))
    //        {
    //            evaluationSubject.Add(randNum);
    //        }
    //    }
    //    evaluationSubject.Add(5); // None 추가
    //}

    public void EndSubject() // 각 항목이 끝날때 실행되어야 하는 함수
    {
        SaveSubjectScore();
        previousSubject = nowSubject;
        evaluationSubject.Add((int)nowSubject);
        difficultyLevel = 5;
        previousDifficultyLevel = 5;
        evaluationGameIndex = 0;
    }

    public int GetChoicedMiniGame()
    {
        if (evaluationGameIndex > evaluationGameList.Count - 1)
        {
            evaluationGameIndex = 0;
        }

        return evaluationGameList[evaluationGameIndex++];
    }

    public int GetNowMiniGame()
    {
        return evaluationGameList[evaluationGameIndex];
    }

    public void StartTraining(int subjectIndex, int minigameIndex)
    {
        checkTraining = new CheckTraining(subjectIndex, minigameIndex);
        isHiddenStroy = true;
    }

    public void EndTraining()
    {
        checkTraining = null;
        trainingLife = 3;
        isTutorial = false;
    }

    public string[] GetEvaluationGameListString()
    {
        SceneField[] _sceneFields = CustomSceneManager.Instance.GetSceneNames(GameManager.Instance.NowSubject);
        string[] result = new string[evaluationGameList.Count];
        for (int i = 0; i < evaluationGameList.Count; i++)
        {

            result[i] = _sceneFields[evaluationGameList[i]];
        }
        return result;
    }
}
