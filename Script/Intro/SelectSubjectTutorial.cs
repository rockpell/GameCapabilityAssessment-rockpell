using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSubjectTutorial : IntroObject {

    [SerializeField] private HighlightController highLight;
    [SerializeField] private IntroUIManager introUIManager;
    [SerializeField] private YukgackAct yukgack;
    [SerializeField] private SubjectSelectManager subjectSelectManager;

    [SerializeField] private Vector3 yukgackAppearPosition;
    [SerializeField] private Vector3 originYukgackPosition;
    [SerializeField] private Vector3 yukgacRotateAngle;
    [SerializeField] private Vector3 yukgacRotateBeforePosition;
    [SerializeField] private Vector3 yukgacRotateAppearPosition;

    [SerializeField] private GameObject highLightStartButton;
    [SerializeField] private GameObject[] highLightSamgacks;
    [SerializeField] private GameObject highLightStatus;
    [SerializeField] private GameObject highLightSlotMachine;

    private float yukgackAppearTime = 0.5f;

    private bool isSkipNeedSelectSubject = false;
    private bool isSelectSubject = false;

    private IntroTrigger introTrigger;
    private RoutineStream routineStream;

    // Use this for initialization
    void Start () {

        introTrigger = NewGameManager.Instance.LoadIntroTrigger();

        if (introTrigger == null)
        {
            introTrigger = new IntroTrigger();
            NewGameManager.Instance.SaveIntroTrigger(introTrigger);
        }

        if (introTrigger.isNeedSelectSubject)
        {
            ActiveTutorial();
            StartCoroutine(StartSelectTutorial());
        }
        else if (introTrigger.isNeedSubjectResultAfter && NewGameManager.Instance.GetEndSubjectList().Count > 0)
        {
            ActiveTutorial();
            StartCoroutine(SubjectResultAfterTutorial());
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (introTrigger.isNeedDetailSubejct)
        {
            if (!isSkipNeedSelectSubject)
            {
                if (subjectSelectManager.IsGameStart())
                {
                    isSkipNeedSelectSubject = true;
                    StartCoroutine(StartDetailTutorial());
                }
            }
        }
    }

    public override void SkipIntroScene()
    {
        StopAllCoroutines();
        StartCoroutine(SkipIntro());
    }

    private IEnumerator SkipIntro()
    {
        if (yukgack.gameObject.activeSelf)
        {
            yield return StartCoroutine(yukgack.SetDialogActive(false));
            yield return StartCoroutine(yukgack.MoveTo(originYukgackPosition, yukgackAppearTime));
        }

        introUIManager.ToggleYukgack(false);
        introUIManager.ToggleSkipButton(false);
        introUIManager.ToggleTouchPanel(false);
        highLight.SetActive(false);
        NewGameManager.Instance.SetTouchDisablePanel(false);

        if (introTrigger.isNeedSelectSubject)
        {
            introTrigger.isNeedSelectSubject = false;
        }
        else if (introTrigger.isNeedDetailSubejct)
        {
            introTrigger.isNeedDetailSubejct = false;
        }
        else if (introTrigger.isNeedSubjectResultAfter)
        {
            introTrigger.isNeedSubjectResultAfter = false;
        }
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    public override void TouchScreen()
    {
        if(routineStream != null)
            routineStream.flag = true;
    }

    [ContextMenu("EndSelectExplan")]
    private void AllowSelectSubejct() // 항목 선택을 유도하게 만드는 과정이 끝나면 실행 되야 하는 함수
    {
        //NewGameManager.Instance.SetTouchDisablePanel(false);
        introUIManager.ToggleSubjectButtons(true); // 항목 선택이 가능하게 해주는 버튼 활성화
    }

    public void StartEvalation() // 슬롯머신 화면으로 전환
    {
        StartCoroutine(StartEvalationRoutine());
    }

    private IEnumerator StartEvalationRoutine()
    {
        subjectSelectManager.StartEvaluation();
        introUIManager.ToggleEvalationButton(false);
        highLight.SetActive(false);

        yield return null;

        //yield return new WaitForSeconds(4f);

        //ActiveTutorial();
        StartCoroutine(StartDetailTutorial());

        introTrigger.isNeedSelectSubject = false;
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    private IEnumerator StartSelectTutorial() // 항목 선택에 대해서 설명해주는 함수
    {
        highLight.SetActive(true);

        yield return StartCoroutine(yukgack.MoveTo(yukgackAppearPosition, yukgackAppearTime));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("방금 셔터가 내려오면서 사각이를 보셨나요?", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("저희 평가를 뒤에서 도와주는 아주 좋은 친구죠!", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("셔터가 내려올 때마다 보일테니 그의 노고를 알아주세요.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("이제 저희는 평가를 위해 하나의 항목을 선택할 거에요.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("제가 잠시 사라질테니 원하시는 항목을 하나 선택해주세요!", routineStream));

        yield return StartCoroutine(yukgack.SetDialogActive(false));
        yield return StartCoroutine(yukgack.MoveTo(originYukgackPosition, yukgackAppearTime));

        highLight.SetActive(false);

        //introUIManager.ToggleYukgack(false);
        //AllowSelectSubejct(); // 항목 선택이 가능하게 하는 함수

        //yield return new WaitUntil(() => isSelectSubject); //항목을 누를때 까지 기다리게 하기
        yield return StartCoroutine(WaitSelectSubject());

        introUIManager.ToggleYukgack(true);
        StartCoroutine(ConfirmSelectSubject());
    }

    private IEnumerator WaitSelectSubject()
    {
        introUIManager.ToggleYukgack(false);
        AllowSelectSubejct(); // 항목 선택이 가능하게 하는 함수

        yield return new WaitUntil(() => isSelectSubject); //항목을 누를때 까지 기다리게 하기
        isSelectSubject = false;
    }

    private IEnumerator ConfirmSelectSubject() // 항목 선택한게 확실한지 다시 물어보는 함수
    {
        routineStream = new RoutineStream();
        string[] _chioce = { "응", "아니" };

        //introUIManager.UpYukgackCanvasLayerOrder(10);
        introUIManager.ToggleTouchPanel(false);

        highLight.SetActive(true);

        yield return StartCoroutine(yukgack.MultipleTask(yukgack.RotateTo(yukgacRotateAngle, 0.1f),
            yukgack.MoveTo(yukgacRotateAppearPosition, 0.1f)));
        //yield return StartCoroutine(yukgack.MoveTo(yukgacRotateBeforePosition, 0));
        //yield return StartCoroutine(yukgack.MultipleTask(yukgack.RotateTo(yukgacRotateAngle, 0),
        //    yukgack.MoveTo(yukgacRotateAppearPosition, 0.3f)));

        yield return StartCoroutine(yukgack.Ask("원하시는 항목을 고르신건가요?", _chioce, routineStream, 0));

        //introUIManager.RecoverYukgackCanvasLayerOrder();
        introUIManager.ToggleTouchPanel(true);
        Debug.Log("touchpanel");
        if (routineStream.result as string == _chioce[0])
        {
            routineStream = new RoutineStream();
            yield return StartCoroutine(yukgack.Talk("고르셨으니 이제 항목으로 넘어가기 위해 시작버튼을 눌러주세요!", routineStream));
            yield return StartCoroutine(yukgack.SetDialogActive(false));

            yield return StartCoroutine(yukgack.MultipleTask(yukgack.RotateTo(Vector3.zero, 0.3f),
            yukgack.MoveTo(originYukgackPosition, 0.3f)));

            introUIManager.ToggleYukgack(false);
            ActiveEvalationButton(); // 시작 버튼 활성화
            highLight.StartTracing(highLightStartButton);
        }
        else
        {
            routineStream = new RoutineStream();
            yield return StartCoroutine(yukgack.Talk("이런, 죄송해요. 다시 고르실 때 까지 기다릴게요.", routineStream));

            yield return StartCoroutine(yukgack.SetDialogActive(false));
            //yield return StartCoroutine(yukgack.MoveTo(yukgacRotateBeforePosition, 0.3f));

            yield return StartCoroutine(yukgack.MultipleTask(yukgack.RotateTo(Vector3.zero, 0.3f),
            yukgack.MoveTo(originYukgackPosition, 0.3f)));

            highLight.SetActive(false);

            yield return StartCoroutine(WaitSelectSubject());

            introUIManager.ToggleYukgack(true);
            StartCoroutine(ConfirmSelectSubject());
        }
        
    }

    private IEnumerator StartDetailTutorial() // 항목 평가에 대해서 설명해수는 함수(슬롯 머신 화면)
    {
        ActiveTutorial();
        yield return new WaitForSeconds(4f);
        introUIManager.ToggleYukgack(true);
        highLight.SetActive(true);

        yield return StartCoroutine(yukgack.MoveTo(yukgackAppearPosition, yukgackAppearTime));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("좋아요. 무사히 여기까지 왔군요! 이제 이곳에 대해 설명드릴거에요.", routineStream));

        highLight.StartTracing(highLightSamgacks);

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("일단 양쪽에 있는 이 친구들은 “삼각응원단”이에요. 당신의 평가를 응원해주는 친구들이죠!", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("당신의 평가가 어떻게 진행되는지에 따라, 이 친구들의 반응이 달라질거에요.", routineStream));

        highLight.StopTracing(highLightSamgacks);
        highLight.StartTracing(highLightStatus);

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("단계는 당신이 진행할 미니게임의 난이도를 결정합니다. 당신이 게임클리어를 성공하면 한단계 높아지고, 실패하면 한단계 낮아지죠.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("그리고 게임을 “완벽하게” 클리어하면 두단계가 올라가요!", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("높은 점수를 얻고싶다면 완벽하게 클리어하는 것이 좋겠죠?", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("8/8은 현재 남은 게임의 횟수를 뜻해요. 왼쪽의 8은 현재 남은횟수, 오른쪽의 8은 전체 횟수를 뜻하죠.", routineStream));

        highLight.StopTracing(highLightStatus);
        highLight.StartTracing(highLightSlotMachine);

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("가운데 GAME SELECT 버튼은 누르게되면 당신이 진행하게될 게임이 선택돼요.", routineStream));

        highLight.StopTracing(highLightSlotMachine);

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("자 이제 한번 해볼까요? 8번의 테스트 이후에 다시 보도록해요!", routineStream));

        yield return StartCoroutine(yukgack.SetDialogActive(false));
        yield return StartCoroutine(yukgack.MoveTo(originYukgackPosition, yukgackAppearTime));

        introUIManager.ToggleYukgack(false);
        introUIManager.ToggleSkipButton(false);
        introUIManager.ToggleTouchPanel(false);
        introUIManager.IsTutorial = false;
        highLight.SetActive(false);

        EndDetailExplanYukgack(); // 육각이가 사라지고 나서 실행되어야하는 함수

        NewGameManager.Instance.SetTouchDisablePanel(false);
    }

    private IEnumerator SubjectResultAfterTutorial() // 하나의 항목이 끝난 후에 실행되야 하는 함수
    {
        yield return StartCoroutine(yukgack.MoveTo(yukgackAppearPosition, yukgackAppearTime));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("한 항목에 대한 테스트를 끝내셨군요 이제 감이 잡히시나요?", routineStream));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("남은 4가지 항목도 제가 설명드린대로, 끝내보자구요.", routineStream));

        yield return StartCoroutine(yukgack.SetDialogActive(false));
        yield return StartCoroutine(yukgack.MoveTo(originYukgackPosition, yukgackAppearTime));

        introUIManager.ToggleYukgack(false);
        introUIManager.ToggleTouchPanel(false);
        introUIManager.ToggleSkipButton(false);
        introUIManager.IsTutorial = false;
        NewGameManager.Instance.SetTouchDisablePanel(false);

        EndResultAfter(); // 육각이가 사라지고 나서 실행되어야하는 함수
    }

    public void SelectSubject(Subject subject) // 항목 선택을 했을 때 실행되는 함수
    {
        subjectSelectManager.subjectSelected(subject);
        introUIManager.ToggleSubjectButtons(false);
        isSelectSubject = true;
    }

    [ContextMenu("ActiveEvalationButton")]
    private void ActiveEvalationButton()
    {
        introUIManager.ToggleEvalationButton(true);
    }

    [ContextMenu("EndDetailExplan")]
    private void EndDetailExplanYukgack() // 항목 세부 사항에 대해 설명이 끝나면 실행 되야 하는 함수(슬롯머신 화면)
    {
        introTrigger.isNeedDetailSubejct = false;
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    private void EndResultAfter()
    {
        introTrigger.isNeedSubjectResultAfter = false;
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    private void ActiveTutorial()
    {
        introUIManager.ToggleYukgack(true);
        introUIManager.ToggleSkipButton(true);
        introUIManager.ToggleTouchPanel(true);
        NewGameManager.Instance.SetTouchDisablePanel(true, 5, 3);
        introUIManager.IsTutorial = true;
        //highLight.SetActive(true);
    }
}
