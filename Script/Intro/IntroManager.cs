using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : IntroObject
{
    [SerializeField] IntroUIManager introUIManager;
    [SerializeField] RectTransform spotlightObject;
    [SerializeField] YukgackAct yukgack;

    [SerializeField] float offSpotlightSpeed;

    private IntroTrigger introTrigger;
    private RoutineStream routineStream;

    private void Start()
    {
        introTrigger = NewGameManager.Instance.LoadIntroTrigger();
        yukgack.gameObject.SetActive(false);
        spotlightObject.gameObject.SetActive(false);
        StartCoroutine(Sequence());

        NewGameManager.Instance.PauseBGM();
    }

    public override void SkipIntroScene()
    {
        StartCoroutine(SkipIntro());
    }

    private IEnumerator SkipIntro()
    {
        yield return StartCoroutine(yukgack.SetDialogActive(false));
        SetParent(spotlightObject, yukgack.GetComponent<RectTransform>());
        StartCoroutine(EndSequence());
        introTrigger.isNeedIntro = false;
        NewGameManager.Instance.SaveIntroTrigger(introTrigger);
    }

    public override void TouchScreen()
    {
        if (routineStream != null)
            routineStream.flag = true;
    }

    private IEnumerator Sequence()
    {
        introUIManager.ToggleSkipButton(true);
        yield return new WaitForSeconds(2f);
        OnSpotlight();
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(yukgack.SetDialogActive(true));

        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("게임능력평가에 오신 것을 환영합니다.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("저는 당신의 평가를 돕기 위한 초고지능 AI 육각이입니다.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("저희 게임능력평가는 여러가지 미니게임을 통해 당신을 총 5가지 항목으로 평가할 것입니다.", routineStream));
        routineStream = new RoutineStream();
        yield return StartCoroutine(yukgack.Talk("그럼 평가를 하러 가볼까요?", routineStream));

        yield return StartCoroutine(yukgack.SetDialogActive(false));

        SetParent(spotlightObject, yukgack.GetComponent<RectTransform>());

        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        yield return StartCoroutine(OffSpotlight());

        IntroTrigger _introTrigger = NewGameManager.Instance.LoadIntroTrigger();
        if(_introTrigger != null)
        {
            _introTrigger.isNeedIntro = false;
            NewGameManager.Instance.SaveIntroTrigger(_introTrigger);
        }
        NextScene();
    }

    private void OnSpotlight() // 스포트라이트가 켜지면서 하는 일들(육각이 활성화, 스킵버튼 활성화)
    {
        spotlightObject.gameObject.SetActive(true);
        yukgack.gameObject.SetActive(true);
    }

    private IEnumerator OffSpotlight()
    {
        Vector2 _size = spotlightObject.sizeDelta;
        while (_size.x > 0)
        {
            _size.x -= offSpotlightSpeed * Time.deltaTime;
            if (_size.x < 0) _size.x = 0;
            spotlightObject.sizeDelta = _size;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void SetParent(RectTransform parent, RectTransform child)
    {
        child.parent = parent;
    }

    private void NextScene()
    {
        CustomSceneManager.Instance.ChangeScene("MenuScene");
    }
}
