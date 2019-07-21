using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationManager : MonoBehaviour {

    [SerializeField] private Text subjectText;
    [SerializeField] private GameObject difficultyText;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject endText;
    [SerializeField] private Transform rollingTextObject;
    [SerializeField] private Transform rollingImageObject;
    [SerializeField] private Transform positions;
    [SerializeField] private Transform[] capsules;
    [SerializeField] private Transform subjectImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backgroundImage2;
    [SerializeField] private CapsuleRolling[] capsuleRollings;
    [SerializeField] private Color[] capsuleColors;
    [SerializeField] private GameObject midResult;

    private Sprite[] icons;
    private Sprite iconEdge;
    private Transform overlayCanvas;

    //private int autoCount = 3;
    private int evaluationCount = 8; // 각 항목당 평가 횟수(항목당 미니게임 실행되는 횟수)

    private float cameraOriginSize = 205;
    //private float cameraZoomInSize = 90;

    //private bool isFadeIn = false;
    private bool isSubjectEvaluationing = false;
    private bool isClickableCapsule = false;
    private bool isSettingProcedure = false; // Start 함수가 끝났는지 확인하기 위함
    private bool isMovingProcedure = false; // 화면상에 자동으로 움직이는 것이 끝났는지 확인하기 위함

    // Use this for initialization
    void Start () {
        overlayCanvas = GameObject.Find("OverlayCanvas").transform;
        if (GameManager.Instance.AccumulateGame == 0)
        {
            CapsuleColorSetting();
            MoveCameraToSelectCanvas();
        }
        else if (GameManager.Instance.AccumulateGame % evaluationCount == 0)
        {
            GameManager.Instance.EndSubject();
            if (GameManager.Instance.EvaluationSubject.Count == 5) // 모든 평가가 끝났을때
            {
                rollingImageObject.gameObject.SetActive(false);
                EvaluationEnd();
            }
            else // 평가항목이 전환된 직후
            {
                CapsuleColorSetting();
                CapsulesActive();
                ProduceChangeSubject();
                ShowMIdResult();
            }
        }
        else // 각 항목의 평가가 진행중일 때
        {
            LoadMiniGameIcon();
            //RefreshSubjectCanvasMember(true);
            ChangeMinigameIconEdge(GameManager.Instance.NowSubject);
            RollingGameSetting();
            RollingGameStart(true);
            StartMiniGameTest(1);
            isSubjectEvaluationing = true;
        }

        RefreshSubjectCanvasMember(isSubjectEvaluationing);
        RollingNumberSetting();
        RollingNumberStart();

        isSettingProcedure = true;
    }

    public bool IsSettingProcedure {
        get { return isSettingProcedure; }
        set { isSettingProcedure = value; }
    }

    public bool IsMovingProcedure {
        get { return isMovingProcedure; }
        set { isMovingProcedure = value; }
    }

    private void RefreshSubjectCanvasMember(bool isNow)
    {
        if (isNow)
        {
            ChangeBackgroundImage(GameManager.Instance.NowSubject);
            //LoadMinigameIconEdge(GameManager.Instance.NowSubject);
            subjectText.text = GameManager.Instance.NowSubject.ToString();
        }
        else
        {
            if(GameManager.Instance.PreviousSubject == Subject.None)
            {
                RefreshSubjectCanvasMember(true);
            } else
            {
                ChangeBackgroundImage(GameManager.Instance.PreviousSubject);
                //LoadMinigameIconEdge(GameManager.Instance.NowSubject);
                subjectText.text = GameManager.Instance.PreviousSubject.ToString();
            }
        }
    }

    private void ProduceChangeSubject()
    {
        //int _capsuleIndex = GameManager.Instance.EvaluationSubjectIndex - 1;
        int _capsuleIndex = (int)GameManager.Instance.NowSubject;

        MoveCameraToSelectCanvas();
        CapsuleMove(_capsuleIndex, 5);
        CapsuleOpen(_capsuleIndex);
        //SubjectImageUp();
        ChangeDark();
        //StartCoroutine(ReverseFadeIn(CapsuleClosing(_capsuleIndex, CapsuleMoving(_capsuleIndex, _capsuleIndex,
        //    RefreshSubjectCanvasMemberTrue(CapsuleMoving(_capsuleIndex + 1, 5,
        //    CapsuleOpeing(_capsuleIndex + 1, ReverseFadeOut(CameraInit(FadeIn(null)) ))) ) ))));
        StartCoroutine(ReverseFadeIn(CapsuleClosing(_capsuleIndex, CapsuleMoving(_capsuleIndex, _capsuleIndex,
            RefreshSubjectCanvasMemberTrue(EndMovingProcedure())))));
    }

    private IEnumerator EndMovingProcedure()
    {
        isMovingProcedure = true;
        yield return null;
    }

    public void StartMiniGameTest()
    {
        //GameManager.Subject tempSubject = GameManager.Instance.NowSubject;
        //GameManager.Instance.GetChoicedMiniGame();
        //CustomSceneManager.Instance.LoadGame(tempSubject, tempIndex);
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.Tutorial);
    }

    public void StartMiniGameTest(float delay)
    {
        Invoke("StartMiniGameTest", delay);
        RollingGameStart(false);
    }

    private void EvaluationEnd()
    {
        difficultyText.SetActive(false);
        subjectText.gameObject.SetActive(false);
        endText.SetActive(true);
        Invoke("StartShowResult", 1.5f);
    }

    private IEnumerator RefreshSubjectCanvasMemberTrue(IEnumerator callback)
    {
        RefreshSubjectCanvasMember(true);
        yield return null;
        if (callback != null) StartCoroutine(callback);
    }

    public void StartShowResult()
    {
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.ResultScene);
    }

    private void RollingNumberSetting()
    {
        int _previousLevel = GameManager.Instance.PreviousDifficultyLevel;
        int _nowLevel = GameManager.Instance.DifficultyLevel;

        rollingTextObject.GetChild(2).GetComponent<Text>().text = _previousLevel.ToString();

        if(_previousLevel > _nowLevel) // 현재 난이도가 낮아지는 경우
        {
            if(_previousLevel - _nowLevel == 1)
            {
                rollingTextObject.GetChild(3).GetComponent<Text>().text = _nowLevel.ToString();
            }
            else
            {
                rollingTextObject.GetChild(3).GetComponent<Text>().text = (_nowLevel + 1).ToString();
                rollingTextObject.GetChild(4).GetComponent<Text>().text = _nowLevel.ToString();
            }
        }
        else if (_previousLevel < _nowLevel) // 현재 난이도가 높아지는 경우
        {
            if (_nowLevel - _previousLevel == 1)
            {
                rollingTextObject.GetChild(1).GetComponent<Text>().text = _nowLevel.ToString();
            }
            else
            {
                rollingTextObject.GetChild(1).GetComponent<Text>().text = (_nowLevel - 1).ToString();
                rollingTextObject.GetChild(0).GetComponent<Text>().text = _nowLevel.ToString();
            }
        }
    }

    private void RollingNumberStart()
    {
        //0, 110, 220, 330, 440
        switch(GameManager.Instance.DifficultyLevel - GameManager.Instance.PreviousDifficultyLevel)
        {
            case -2:
                StartCoroutine(RollingNumber(440));
                break;
            case -1:
                StartCoroutine(RollingNumber(330));
                break;
            case 1:
                StartCoroutine(RollingNumber(110));
                break;
            case 2:
                StartCoroutine(RollingNumber(0));
                break;
        }
    }

    private IEnumerator RollingNumber(int posY)
    {
        Vector3 _originPosition = rollingTextObject.localPosition;
        Vector3 _targetPosition = new Vector3(_originPosition.x, posY, _originPosition.z);
        float speed = 0.1f;
        float fracJourney = 0;
        while (fracJourney != 1)
        {
            fracJourney += speed;
            if (fracJourney > 1) fracJourney = 1;
            rollingTextObject.localPosition = Vector3.Lerp(_originPosition, _targetPosition, fracJourney);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private Sprite LoadIcon(string target)
    {
        return Resources.Load<Sprite>("MiniGameIcon/" + target);
    }

    private void LoadMiniGameIcon()
    {
        string[] _gameList = GameManager.Instance.GetEvaluationGameListString();
        //SceneField[] _sceneFields = CustomSceneManager.Instance.GetSceneNames(GameManager.Instance.NowSubject);
        int _tempLength = _gameList.Length;
        icons = new Sprite[_tempLength];

        for (int i = 0; i < _tempLength; i++)
        {
            icons[i] = LoadIcon(_gameList[i]);
        }
    }

    private void RollingGameSetting()
    {
        for (int i = 0; i < rollingImageObject.childCount - 1; i++)
        {
            if (icons.Length > i)
                rollingImageObject.GetChild(i + 1).GetComponent<Image>().sprite = icons[i];
        }
    }

    private void RollingGameStart(bool isSetting) // 미니 게임 아이콘 보여주는 scrollview 세팅
    {
        if (isSetting)
        {
            for(int i = 1; i < GameManager.Instance.EvaluationGameIndex + 1; i++)
            {
                rollingImageObject.GetChild(i).GetChild(0).gameObject.SetActive(true);
                rollingImageObject.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = GameManager.Instance.PreviousResult[i - 1].ToString();
            }

            if(GameManager.Instance.EvaluationGameIndex == 0)
            {
                rollingImageObject.localPosition = new Vector3(25, rollingImageObject.localPosition.y, rollingImageObject.localPosition.z);
            }
            else
            {
                int _tempNum = (GameManager.Instance.EvaluationGameIndex - 1) * -140;
                rollingImageObject.localPosition = new Vector3(-300 + _tempNum, rollingImageObject.localPosition.y, rollingImageObject.localPosition.z);
            }
        }
        else
        {
            if(GameManager.Instance.EvaluationGameIndex == 0)
            {
                StartCoroutine(RollingGame(-300, false));
            } else
            {
                StartCoroutine(RollingGame(-300 + (-140 * GameManager.Instance.EvaluationGameIndex), true));
            }
        }
    }

    private IEnumerator RollingGame(int posX, bool isDelay)
    {
        Vector3 _originPosition = rollingImageObject.localPosition;
        Vector3 _targetPosition = new Vector3(posX, _originPosition.y, _originPosition.z);
        float speed = 0.1f;
        float fracJourney = 0;

        if (isDelay) yield return new WaitForSeconds(0.5f);

        while (fracJourney != 1)
        {
            fracJourney += speed;
            if (fracJourney > 1) fracJourney = 1;
            rollingImageObject.localPosition = Vector3.Lerp(_originPosition, _targetPosition, fracJourney);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void MoveCameraToSelectCanvas()
    {
        Camera.main.transform.position = new Vector3(1000, 0, -10);
    }

    private void MoveCameraInitPosition()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    //private void CameraInit()
    //{
    //    Camera.main.transform.position = new Vector3(0, 0, -10);
    //    Camera.main.orthographicSize = cameraOriginSize;
    //}
    private IEnumerator CameraInit(IEnumerator callback)
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = cameraOriginSize;
        if (callback != null) StartCoroutine(callback);
        yield return null;
    }

    private void CapsuleOpen(int index)
    {
        capsules[index].GetChild(0).transform.Translate(new Vector3(-25, 0, 0));
        capsules[index].GetChild(0).transform.Rotate(new Vector3(0, 0, 50));

        capsules[index].GetChild(1).transform.Translate(new Vector3(25, 0, 0));
        capsules[index].GetChild(1).transform.Rotate(new Vector3(0, 0, -50));
    }

    private IEnumerator CapsuleOpeing(int index, IEnumerator callback)
    {
        Vector3 _position1 = capsules[index].GetChild(0).transform.position;
        capsules[index].GetChild(0).transform.position = new Vector3(_position1.x - 25, _position1.y, _position1.z);
        capsules[index].GetChild(0).transform.Rotate(new Vector3(0, 0, 50));

        Vector3 _position2 = capsules[index].GetChild(1).transform.position;
        capsules[index].GetChild(1).transform.position = new Vector3(_position2.x + 25, _position2.y, _position2.z);
        capsules[index].GetChild(1).transform.Rotate(new Vector3(0, 0, -50));

        yield return new WaitForSeconds(0.1f);

        if (callback != null) StartCoroutine(callback);
    }

    private IEnumerator CapsuleClosing(int index, IEnumerator callback)
    {
        Vector3 _position1 = capsules[index].GetChild(0).transform.position;
        capsules[index].GetChild(0).transform.position = new Vector3(_position1.x + 25, _position1.y, _position1.z);
        capsules[index].GetChild(0).transform.Rotate(new Vector3(0, 0, -50));

        Vector3 _position2 = capsules[index].GetChild(1).transform.position;
        capsules[index].GetChild(1).transform.position = new Vector3(_position2.x - 25, _position2.y, _position2.z);
        capsules[index].GetChild(1).transform.Rotate(new Vector3(0, 0, 50));

        yield return new WaitForSeconds(0.1f);

        if (callback != null) StartCoroutine(callback);
    }

    private void CapsuleMove(int index, int targetIndex) // 0 ~ 5
    {
        capsules[index].position = positions.GetChild(targetIndex).position;
    }

    private IEnumerator CapsuleMoving(int index, int targetIndex, IEnumerator callback)
    {
        Vector3 _originPosition = capsules[index].position;
        Vector3 _targetPosition = positions.GetChild(targetIndex).position;
        float fracJourney = 0;
        //float speed = 0.05f;
        float speed = 0.25f;
        while (fracJourney < 1)
        {
            fracJourney += speed;
            if (fracJourney > 1) fracJourney = 1;
            capsules[index].position = Vector3.Lerp(_originPosition, _targetPosition, fracJourney);
            yield return new WaitForSeconds(0.3f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    private IEnumerator SubjectImageUping(IEnumerator callback)
    {
        float _scale = subjectImage.localScale.x;

        if (!subjectImage.gameObject.activeSelf) subjectImage.gameObject.SetActive(true);

        while (_scale < 1)
        {
            _scale += 0.1f;
            if (_scale > 1) _scale = 1;
            subjectImage.localScale = new Vector3(_scale, _scale, 1);
            subjectImage.Translate(new Vector3(0, 12.5f, 0));
            yield return new WaitForSeconds(0.04f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    private void SubjectImageUp()
    {
        if (!subjectImage.gameObject.activeSelf) subjectImage.gameObject.SetActive(true);
        subjectImage.localScale = new Vector3(1, 1, 1);
        subjectImage.localPosition = Vector3.zero;
    }

    private IEnumerator SubjectImageDowning(IEnumerator callback)
    {
        float _scale = subjectImage.localScale.x;

        if (!subjectImage.gameObject.activeSelf) subjectImage.gameObject.SetActive(true);

        while (_scale > 0.2f)
        {
            _scale -= 0.1f;
            if (_scale < 0.2f) _scale = 0.2f;
            subjectImage.localScale = new Vector3(_scale, _scale, 1);
            subjectImage.Translate(new Vector3(0, -12.5f, 0));
            yield return new WaitForSeconds(0.04f);
        }
        subjectImage.gameObject.SetActive(false);
        if (callback != null) StartCoroutine(callback);
    }

    //private IEnumerator CameraZoomIn(IEnumerator callback)
    //{
    //    float _cameraSize = Camera.main.orthographicSize;

    //    while(_cameraSize > cameraZoomInSize)
    //    {
    //        _cameraSize -= 6f;
    //        if (_cameraSize < cameraZoomInSize) _cameraSize = cameraZoomInSize;
    //        Camera.main.orthographicSize = _cameraSize;
    //        yield return new WaitForSeconds(0.04f);
    //    }
    //    if (callback != null) StartCoroutine(callback);
    //    CameraInit();
    //}

    private void ChangeBackgroundImage(Subject standard)
    {
        string target = null;
        switch (standard)
        {
            case Subject.Aiming:
                target = "red";
                break;
            case Subject.Concentration:
                target = "blue";
                break;
            case Subject.Quickness:
                target = "yellow";
                break;
            case Subject.RhythmicSense:
                target = "violet";
                break;
            case Subject.Thinking:
                target = "green";
                break;
        }
        Sprite _tempSprite = Resources.Load<Sprite>("Images/using/" + target);
        backgroundImage.sprite = _tempSprite;
        backgroundImage2.sprite = _tempSprite;
    }

    private void LoadMinigameIconEdge(Subject standard)
    {
        string target = null;
        switch (standard)
        {
            case Subject.Aiming:
                target = "red";
                break;
            case Subject.Concentration:
                target = "blue";
                break;
            case Subject.Quickness:
                target = "yellow";
                break;
            case Subject.RhythmicSense:
                target = "violet";
                break;
            case Subject.Thinking:
                target = "green";
                break;
        }
        iconEdge = Resources.Load<Sprite>("Images/Training/" + target + "_edge");
    }

    private void ChangeMinigameIconEdge(Subject standard)
    {
        LoadMinigameIconEdge(standard);
        for(int i = 1; i < rollingImageObject.childCount; i++)
        {
            rollingImageObject.GetChild(i).GetChild(1).gameObject.GetComponent<Image>().sprite = iconEdge; // 아이콘 테두리 색깔 변경
        }
    }

    private void CapsuleRollingSetting() // scroll view 내부 캡슐 색깔 지정 ,캡슐 회전시 필요한 함수
    {
        for(int i = 0; i < capsuleRollings.Length; i++)
        {
            capsuleRollings[i].ColorSetting(capsuleColors, i);
            //capsuleRollings[i].IsActive = true;
        }
    }

    private IEnumerator StopCapsuleRolling(IEnumerator callback)
    {
        for(int i = 0; i < capsuleRollings.Length; i++)
        {
            capsuleRollings[i].gameObject.SetActive(false);
            capsules[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    private IEnumerator DelayInterval(float delayTime, IEnumerator callback)
    {
        yield return new WaitForSeconds(delayTime);
        if (callback != null) StartCoroutine(callback);
    }

    private void CapsulesActive()
    {
        for(int i = 0; i < capsules.Length; i++)
            capsules[i].gameObject.SetActive(true);
    }

    private void CapsuleColorSetting()
    {
        List<int> _evaluationSubject = GameManager.Instance.EvaluationSubject;
        for (int i = 0; i < capsules.Length; i++)
        {
            //capsules[i].GetChild(0).GetComponent<Image>().color = capsuleColors[_evaluationSubject[i]];
            //capsules[i].GetChild(1).GetComponent<Image>().color = capsuleColors[_evaluationSubject[i]];
            capsules[i].GetChild(0).GetComponent<Image>().color = capsuleColors[i];
            capsules[i].GetChild(1).GetComponent<Image>().color = capsuleColors[i];
        }
    }

    private void ChangeDark()
    {
        for (int i = 0; i < overlayCanvas.childCount; i++)
        {
            overlayCanvas.GetChild(i).gameObject.SetActive(true);
        }
    }

    private IEnumerator FadeIn(IEnumerator callback)
    {
        for (int i = 0; i < overlayCanvas.childCount; i++)
        {
            overlayCanvas.GetChild(i).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    private IEnumerator FadeOut(IEnumerator callback)
    {
        int count = overlayCanvas.childCount - 1;
        for(int i = count; i >= 0; i--)
        {
            overlayCanvas.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    private IEnumerator ReverseFadeIn(IEnumerator callback)
    {
        int count = overlayCanvas.childCount - 1;
        for (int i = count; i >= 0; i--)
        {
            overlayCanvas.GetChild(i).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    private IEnumerator ReverseFadeOut(IEnumerator callback)
    {
        for (int i = 0; i < overlayCanvas.childCount; i++)
        {
            overlayCanvas.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        if (callback != null) StartCoroutine(callback);
    }

    public void ClickCapsule(int index)
    {
        if (!isClickableCapsule && GameManager.Instance.SelectSubject(index))
        {
            isClickableCapsule = true;
            GameManager.Instance.StartSubject();
            LoadMiniGameIcon();
            RefreshSubjectCanvasMember(true);
            ChangeMinigameIconEdge(GameManager.Instance.NowSubject);
            RollingGameSetting();
            RollingGameStart(true);
            StartCoroutine(CapsuleMoving(index, 5, CapsuleOpeing(index, ReverseFadeOut(CameraInit(FadeIn(null))))));
        }
    }

    public void ShowMIdResult()
    {
        RefreshMidResult();
        midResult.SetActive(true);
    }

    private void RefreshMidResult()
    {
        Subject _subject = GameManager.Instance.NowSubject;
        midResult.transform.Find("MidResultSubject").GetComponent<Text>().text = _subject.ToString();
        midResult.transform.Find("MidResultValue").GetComponent<Text>().text = GameManager.Instance.SubjectScore[_subject].ToString();
    }

    public void HideMidResult()
    {
        midResult.SetActive(false);
    }
}