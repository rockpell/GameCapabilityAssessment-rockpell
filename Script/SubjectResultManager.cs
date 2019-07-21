using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubjectResultManager : MonoBehaviour {

	[SerializeField]
	private Material[] bgMat;
    //0.통합 1. 조준, 2. 집중, 3. 순발, 4. 리듬, 5. 사고
	[SerializeField]
	private GaugeImageController[] gaugeImage;

	[SerializeField]
	private BackgroundImageController bgImage;
	[SerializeField]
	private YukgackManager yukgack;
    [SerializeField] private YukgackAct yukgackAct;

    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private TextMesh scoreText;

    [SerializeField] private Vector3 yukgackAppearPosition = Vector3.zero;
    private float yukgackAppearTime = 0.5f;

    private RoutineStream routineStream;

    private GaugeImageController selectedGauge;
    private Subject subject;
    private int currentSubject;
    private float moveSpeed = 0.1f;

    private void Start()
    {
        ShowResult();
    }
    public void ShowResult()
	{
		Setting();
	}

	private void Setting()
	{
        int nowLevel = 0;
        //항목은 무엇인가
        subject = NewGameManager.Instance.GetNowSubject();
        currentSubject = (int)subject;
        //배경화면 설정
        bgImage.SetMaterial(bgMat[currentSubject]); //None은 0이기 때문에 임시로 주석
        bgImage.Active();
        //게이지 이미지 설정
        gaugeImage[currentSubject].gameObject.SetActive(true);
        selectedGauge = gaugeImage[currentSubject];
        //레벨값 설정
        nowLevel = NewGameManager.Instance.GetNowDifficultyLevel(subject);
        StartCoroutine(ShowResultRoutine(nowLevel));
    }
    
	private IEnumerator ShowResultRoutine(int nowLevel)
	{
        int totalResult = CaculateTotalGaugeInput();
        //코루틴의 기본적인 내용들
        //http://theeye.pe.kr/archives/tag/waituntil
        //gauge image 루틴 호출
        if (currentSubject != 0) yield return StartCoroutine(selectedGauge.ImageActivate(nowLevel));
        else
        {
            yield return StartCoroutine(selectedGauge.ImageActivate(totalResult));
        }
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(MoveResult());
        yield return new WaitForSeconds(0.5f);

        //육각이 등장
        //if (nowLevel <= selectedGauge.GetValue())
        //{
        //    //yukgack.Act(subject, NewGameManager.Instance.GetGameReuslts());
        //}
        string[] _ments = yukgack.EvalationMente(subject, NewGameManager.Instance.GetGameReuslts());

        yukgackAct.gameObject.SetActive(true);

        yield return StartCoroutine(yukgackAct.MoveTo(yukgackAppearPosition, yukgackAppearTime));
        yield return StartCoroutine(yukgackAct.SetDialogActive(true));

        if (_ments != null)
        {
            for(int i = 0; i < _ments.Length; i++)
            {
                routineStream = new RoutineStream();
                yield return StartCoroutine(yukgackAct.Talk(_ments[i], routineStream));
            }
        }

        //육각이 대사
        nextButton.gameObject.SetActive(true);  //육각이 대사 끝나고 나서 호출하는것으로 바꿔야함
    }
    private void Update()
    {
        scoreText.text = selectedGauge.GetValue().ToString("N1");
    }
    private IEnumerator MoveResult()
    {
        float moveDistance = 0.0f;
        if (currentSubject == 1 || currentSubject == 2) moveDistance = -3.0f;
        else moveDistance = -4.8f;
        while(true)
        {
            if (scoreText.transform.position.x >= moveDistance)
            {
                selectedGauge.transform.Translate(-moveSpeed, 0.025f, 0);
                if (scoreText.transform.localScale.x > 0.8f && scoreText.transform.position.x <= moveDistance / 2)
                {
                    scoreText.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                    selectedGauge.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                    scoreText.transform.Translate(-moveSpeed, 0.07f, 0);
                }
                else
                {
                    scoreText.transform.Translate(-moveSpeed, 0.025f, 0);
                }
            }
            else break;
            
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void NextScene()
    {
        if(ShutterManager.Instance != null)
        {
            string _tempName = "";
            if (NewGameManager.Instance != null)
            {
                NewGameManager.Instance.AddEndSubjectList(NewGameManager.Instance.GetNowSubject());
                List<Subject> _tempList = NewGameManager.Instance.GetEndSubjectList(); // 평가가 끝난 항목들
                if (_tempList.Count == 5 && NewGameManager.Instance.GetNowSubject() == Subject.None)
                {
                    _tempName = "MenuScene";
                    NewGameManager.Instance.SaveGameRecord();
                }
                else if (_tempList.Count == 5)
                {
                    _tempName = "SubjectResult";
                }
                else
                {
                    _tempName = "NewEvaluation";
                }
                NewGameManager.Instance.SetNowSubjet(Subject.None);
            }
            ShutterManager.Instance.ShutterSequence(Result.Fail, _tempName, false); // 화면 전환 함수 호출
        }
    }
    private int CaculateTotalGaugeInput()
    {
        List<GameResult>[] record= NewGameManager.Instance.GetGameReuslts();
        int sum = 0;
        int result = 0;
        for (int i = 0; i < record.Length; i++)
        {
            result = NewGameManager.Instance.CalculateResultValue(record[i]);
            sum = sum + result * (1000000000 / (int)Mathf.Pow(10, (i+1) * 2 -1));
        }
        return sum;
    }

    public void TouchScreen()
    {
        if (routineStream != null)
            routineStream.flag = true;
    }
}
