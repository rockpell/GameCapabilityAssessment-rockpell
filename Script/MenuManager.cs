using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] private BackgroundImageController bgController;
    [SerializeField] private GameObject titleLogo;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject recordButton;
    [SerializeField] private float outPositionY;
    [SerializeField] private float returnPositionY;
    [SerializeField] private float targetScale;
    [SerializeField] private float speed;
    [SerializeField] private float[] buttonPositionY;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip btnClickSound;

    [SerializeField] private GameObject triggerImage;

    private float deltaSpeed = 10f;
    private float deltaPositionY = 200;
    private float appearPositionY = 60;
    private float appearTextPositionY = -415;
    private bool firstTouch;
    private bool introEnd;
    private bool isAppearButtonEnd = false;

    private bool isCreateTestTrigger = false;
    private int removeRecordTriggerCount = 0;

    // Use this for initialization
    void Start () {
        introEnd = false;
        firstTouch = true;
        bgController.Active();

        InactiveText();

        //if(NewGameManager.Instance.IsEvalationEnd())
        //    NewGameManager.Instance.InitialClearData();

        audioSource.loop = false;

        if (NewGameManager.Instance.IsSkipTouchToStart) // 터치하면 게임 시작되는 화면 스킵
        {
            firstTouch = false;
            StartCoroutine(SkipMenuSequence());
        }
        else
        {
            StartCoroutine(AppearLogo(titleLogo, appearPositionY));
        }

        if (!NewGameManager.Instance.IsMiddleData() || NewGameManager.Instance.IsEvalationEnd())
        {
            continueButton.GetComponent<Button>().interactable = false;
        }

        NewGameManager.Instance.PlayBGM(0.5f);
    }

    public void TouchToStart()
    {
        if (firstTouch && introEnd)
        {
            ClickSound();
            firstTouch = false;
            StartCoroutine(MenuSequence());
            NewGameManager.Instance.IsSkipTouchToStart = true;
        }
    }

    private void GameStart()
    {
        if (NewGameManager.Instance.IsEvalationEnd())
            NewGameManager.Instance.InitialClearData();
        ShutterManager.Instance.ShutterSequence(Result.Fail, "NewEvaluation", false);
    }

    public void NewGameStart()
    {
        ButtonClickSound();
        NewGameManager.Instance.ResetMiddleData();
        GameStart();
    }

    public void ContinueGame()
    {
        ButtonClickSound();
        GameStart();
    }

    public void OpenOption()
    {

    }

    public void CloseOption()
    {

    }

    public void ViewRecord()
    {
        ButtonClickSound();
        CustomSceneManager.Instance.ChangeScene("RecordScene");
    }

    public void Exit()
    {
        ButtonClickSound();
        Application.Quit();
    }

    public void TestTriggerOn()
    {
        removeRecordTriggerCount += 1;
        Debug.Log("TestTriggerOn");
        if (removeRecordTriggerCount >= 5)
        {
            triggerImage.SetActive(true);
            isCreateTestTrigger = true;
        }
    }

    public void TestRandomRecord()
    {
        if (isCreateTestTrigger)
        {
            triggerImage.SetActive(true);
            triggerImage.GetComponent<Image>().color = new Color(255, 0, 0, 1);
            NewGameManager.Instance.TestRandomRecordSave(8);
        }
    }

    public void TestRemoveRecord()
    {
        if(isCreateTestTrigger)
        {
            NewGameManager.Instance.DeleteAllRecord();
            triggerImage.SetActive(true);
            triggerImage.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }

    public void TestRemoveMiddleData()
    {
        NewGameManager.Instance.ResetMiddleData();
        triggerImage.SetActive(true);
        triggerImage.GetComponent<Image>().color = new Color(0, 255, 0, 1);
    }

    public void GoToTraining()
    {
        CustomSceneManager.Instance.ChangeScene("TestTraining");
    }

    private void ClickSound()
    {
        if(audioSource != null && clickSound != null)
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
        else
        {
            Debug.Log("audioSource null or clickSound null");
        }
    }

    private void ButtonClickSound()
    {
        if (audioSource != null && btnClickSound != null)
        {
            audioSource.clip = btnClickSound;
            audioSource.Play();
        }
        else
        {
            Debug.Log("audioSource null or clickSound null");
        }
    }

    // 타이틀 로고가 올라가고 작아진 로고가 내려오고 버튼이 나타나는 과정
    private IEnumerator MenuSequence() 
    {
        InactiveText();
        yield return StartCoroutine(UpingDownScaleDowning(titleLogo));
        StartCoroutine(DownLogo(titleLogo));
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < buttons.Length; i++)
        {
            StartCoroutine(AppearButton(buttons[i], buttonPositionY[i]));
            yield return new WaitForSeconds(0.2f);
        }

        isAppearButtonEnd = true;
        recordButton.SetActive(true);
    }

    // 작아진 타이틀 로고가 내려오고 버튼이 나타나는 과정
    private IEnumerator SkipMenuSequence()
    {
        InactiveText();
        StartCoroutine(DownScaleDowning(titleLogo));
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < buttons.Length; i++)
        {
            StartCoroutine(AppearButton(buttons[i], buttonPositionY[i]));
            yield return new WaitForSeconds(0.2f);
        }

        isAppearButtonEnd = true;
        recordButton.SetActive(true);
    }

    private IEnumerator UpingDownScaleDowning(GameObject target) // 로고가 올라갔다 내려오는 함수
    {
        while (target.transform.localPosition.y < outPositionY)
        {
            target.transform.localPosition += new Vector3(0, speed, 0);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.5f);

        yield return null;
    }

    private IEnumerator DownLogo(GameObject target)
    {
        titleLogo.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

        float _speed = speed;

        while (target.transform.localPosition.y > returnPositionY)
        {
            if (target.transform.localPosition.y - returnPositionY < deltaPositionY && _speed > 10)
            {
                _speed -= deltaSpeed;
            }
            target.transform.localPosition -= new Vector3(0, _speed, 0);
            yield return new WaitForSeconds(0.02f);
        }

        yield return null;
    }

    private IEnumerator DownScaleDowning(GameObject target) // 로고가 내려오는 함수
    {
        titleLogo.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

        float _speed = speed;

        while (target.transform.localPosition.y > returnPositionY)
        {
            if (target.transform.localPosition.y - returnPositionY < deltaPositionY && _speed > 10)
            {
                _speed -= deltaSpeed;
            }
            target.transform.localPosition -= new Vector3(0, _speed, 0);
            yield return new WaitForSeconds(0.02f);
        }

        yield return null;
    }

    //터치 글자 끄기
    private void InactiveText()
    {
        textObject.SetActive(false);
    }

    //버튼 가져오기
    private IEnumerator AppearButton(GameObject target, float buttonPosition)
    {
        float _speed = speed;
        while (target.transform.localPosition.y < buttonPosition)
        {
            if (buttonPosition - target.transform.localPosition.y < deltaPositionY && _speed > 10)
            {
                _speed -= deltaSpeed;
            }
            target.transform.localPosition += new Vector3(0, _speed, 0);
            yield return new WaitForSeconds(0.02f);
        }
        
        yield return null;
    }

    //로고 나타나는 함수
    private IEnumerator AppearLogo(GameObject target,float appearPositionY)
    {
        float _speed = speed;
        while(target.transform.localPosition.y > appearPositionY)
        {
            if(target.transform.localPosition.y - appearPositionY < deltaPositionY && _speed > 10)
            {
                _speed -= deltaSpeed;
            }
            target.transform.localPosition -= new Vector3(0, _speed, 0);
            yield return new WaitForSeconds(0.02f);
        }

        textObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(AppearButton(textObject, appearTextPositionY));

        introEnd = true;
        StartCoroutine(Flickr(textObject));

        yield return null;
    }

    //깜빡거리기 위함
    private IEnumerator Flickr(GameObject target)
    {
        while(target.activeSelf == true)
        {
            yield return new WaitForSeconds(0.5f);
            while (target.transform.localScale.x > 0.95f)
            {
                target.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
                yield return new WaitForSeconds(0.02f);
            }
            while (target.transform.localScale.x < 1.0f)
            {
                target.transform.localScale += new Vector3(0.01f, 0.01f, 0);
                yield return new WaitForSeconds(0.02f);
            }
        }
        
         yield return null;
    }

    public bool IntroEnd
    {
        get { return introEnd; }
    }

    public bool IsAppearButtonEnd
    {
        get { return isAppearButtonEnd; }
    }
}
