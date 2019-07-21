using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTutorial : IntroObject
{
    [SerializeField] private HighlightController highLight;
    [SerializeField] private IntroUIManager introUIManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private YukgackAct yukgack;
    [SerializeField] private Image blackPanel;
    [SerializeField] private float fadeValue;
    [SerializeField] private Vector3 yukgackAppearPosition;
    [SerializeField] private Vector3 originYukgackPosition;
    [SerializeField] private Vector3 yukgacLeftPosition;
    [SerializeField] private Vector3 dialogRightPosition;
    [SerializeField] private Vector3 yukgackDisappearPosition;

    [SerializeField] private GameObject[] highLightMenuButtons;
    [SerializeField] private GameObject highLightRecordButton;

    private bool isNeedIntro = false;
    private bool compareTouchToStart;
    private bool isTutorialStarted = false;

    private float yukgackAppearTime = 0.5f;

    private IntroTrigger introTrigger;
    private RoutineStream routineStream;

    // Use this for initialization
    void Start () {
        compareTouchToStart = NewGameManager.Instance.IsSkipTouchToStart;
        introTrigger = NewGameManager.Instance.LoadIntroTrigger();

        if (introTrigger == null)
        {
            introTrigger = new IntroTrigger();
            NewGameManager.Instance.SaveIntroTrigger(introTrigger);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (compareTouchToStart && !isTutorialStarted)
        {
            if (introTrigger.isNeedIntro)
            {
                isTutorialStarted = true;
                StartCoroutine(IntroSequence());
            }
            else if (introTrigger.isNeedMenu)
            {
                isTutorialStarted = true;
                StartCoroutine(MenuSequence());
            }
            else if (introTrigger.isNeedEvalationEnd && NewGameManager.Instance.IsEvalationEnd())
            {
                isTutorialStarted = true;
                StartCoroutine(EvalationEndSequence());
            }
        }

        compareTouchToStart = NewGameManager.Instance.IsSkipTouchToStart;
    }

    public override void SkipIntroScene() // 메뉴 설명만을 스킵, 인트로는 다른 장면에서 하기 때문에
    {
        introUIManager.ToggleYukgack(false);
        introUIManager.ToggleSkipButton(false);
        introUIManager.ToggleTouchPanel(false);
        introTrigger.isNeedMenu = false;
        highLight.SetActive(false);
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    public override void TouchScreen()
    {
        if (routineStream != null)
            routineStream.flag = true;
    }

    private IEnumerator IntroSequence() // 육각이 스포트라이트 장면으로 넘어가기 위한 연출
    {
        yield return StartCoroutine(FadeOut());
        CustomSceneManager.Instance.ChangeScene("IntroScene");
    }

    private IEnumerator MenuSequence() // 메뉴 설명을 위한 연출
    {
        introUIManager.ToggleTouchPanel(true);
        introUIManager.ToggleSkipButton(true);
        highLight.SetActive(true);

        if (IsFirstStartIntroScene())
        {
            yield return StartCoroutine(FadeIn());
        }
        
        yield return new WaitUntil(() => menuManager.IsAppearButtonEnd);

        introUIManager.ToggleYukgack(true);

        yield return StartCoroutine(yukgack.MoveTo(yukgackAppearPosition, yukgackAppearTime));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("좋아요! 게임을 시작하기에 앞서, 버튼들에 대한 설명을 해드리죠.", routineStream));

        highLight.StartTracing(highLightMenuButtons);
        StartCoroutine(yukgack.SetEmotion(Face.LookLeft));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("우선 이 버튼들은 게임을 위한 버튼이에요!", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("새로하기를 누르면 평가를 시작합니다.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("평가를 하던 도중에 나왔을때 이어하기 버튼을 누르면 하던 평가를 이어서 할 수 있어요.", routineStream));

        highLight.StopTracing(highLightMenuButtons);
        StartCoroutine(yukgack.SetEmotion(Face.Idle));

        yield return  StartCoroutine(yukgack.SetDialogActive(false));
        StartCoroutine(yukgack.DialogMoveTo(dialogRightPosition, 0));

        yield return StartCoroutine(yukgack.MoveTo(yukgacLeftPosition, 1f));

        highLight.StartTracing(highLightRecordButton);
        StartCoroutine(yukgack.SetEmotion(Face.LookRight));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("이곳은 당신의 전적을 볼 수 있는 곳이에요!", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("나중에 평가가 끝난 뒤에 보시면, 평가에 대한 분석을 보실 수 있어요.", routineStream));

        highLight.StopTracing(highLightRecordButton);
        StartCoroutine(yukgack.SetEmotion(Face.Idle));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("여기서 알려드릴건 다 알려드린 것 같군요! 그럼 새로하기를 눌러 평가를 시작해 볼까요?", routineStream));

        yield return StartCoroutine(yukgack.SetDialogActive(false));
        yield return StartCoroutine(yukgack.MoveTo(yukgackDisappearPosition, yukgackAppearTime));

        introUIManager.ToggleYukgack(false);
        introUIManager.ToggleTouchPanel(false);
        introUIManager.ToggleSkipButton(false);
        highLight.SetActive(false);

        introTrigger.isNeedMenu = false;
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    private void ToggleBlackOut(bool value)
    {
        blackPanel.gameObject.SetActive(value);
    }

    private IEnumerator FadeOut() // 점차 어두워진다
    {
        float _value = 0;
        blackPanel.color = new Color(0, 0, 0, _value);
        ToggleBlackOut(true);

        while (_value < 1)
        {
            _value += fadeValue * Time.deltaTime;
            if (_value > 1) _value = 1;
            blackPanel.color = new Color(0, 0, 0, _value);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator FadeIn() // 점차 밝아 진다
    {
        float _value = 1;
        blackPanel.color = new Color(0, 0, 0, _value);
        ToggleBlackOut(true);

        while(_value > 0)
        {
            _value -= fadeValue * Time.deltaTime;
            if (_value < 0) _value = 0;
            blackPanel.color = new Color(0, 0, 0, _value);
            yield return new WaitForSeconds(0.05f);
        }
        ToggleBlackOut(false);
    }

    private IEnumerator EvalationEndSequence()
    {
        yield return new WaitUntil(() => menuManager.IsAppearButtonEnd);

        introUIManager.ToggleYukgack(true);
        introUIManager.ToggleTouchPanel(true);
        introUIManager.ToggleSkipButton(true);

        Debug.Log("EvalationEndSequence");

        yield return StartCoroutine(yukgack.MoveTo(yukgackAppearPosition, yukgackAppearTime));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("드디어 평가를 끝내셨군요! 좋은 성적을 받으셨다고 생각하나요?", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("전적을 누르게 되시면 자신의 평가에 대한 결과들을 다시 둘러볼 수 있어요.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("시간 날때 전적을 보고 자신의 능력을 파악하는것도 중요해요.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("그럼 앞으로의 활약을 기대할게요!", routineStream));

        yield return StartCoroutine(yukgack.SetDialogActive(false));
        yield return StartCoroutine(yukgack.MoveTo(originYukgackPosition, yukgackAppearTime));

        introUIManager.ToggleYukgack(false);
        introUIManager.ToggleTouchPanel(false);
        introUIManager.ToggleSkipButton(false);

        introTrigger.isNeedEvalationEnd = false;
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    private bool IsFirstStartIntroScene() // 처음 시작이 인트로씬이 맞는지
    {
        if(NewGameManager.Instance.StartIntroTrigger != null)
            return NewGameManager.Instance.StartIntroTrigger.isNeedIntro;
        return true;
    }
}