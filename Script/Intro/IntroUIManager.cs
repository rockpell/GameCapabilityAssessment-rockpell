using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUIManager : MonoBehaviour {

    [SerializeField] private IntroManager introManager;
    [SerializeField] private MenuTutorial menuTutorial;
    [SerializeField] private SelectSubjectTutorial selectSubjectTutorial;

    [SerializeField] Canvas yukgackCanvas;
    [SerializeField] GameObject messageWindow;
    [SerializeField] Text messageText;
    [SerializeField] GameObject skipButton;
    [SerializeField] GameObject subjectButtons;
    [SerializeField] GameObject evalationButton;
    [SerializeField] GameObject touchPanel;
    [SerializeField] GameObject blockPanel;

    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip btnClickSound;
    private AudioSource audioSource;

    private IntroObject introObject;

    private int originYukgackCanvasOrderLayer;

    private bool isTutorial = false;

    // Use this for initialization
    void Start () {
        if (introManager != null)
            introObject = introManager;
        else if (menuTutorial != null)
            introObject = menuTutorial;
        else if(selectSubjectTutorial != null)
            introObject = selectSubjectTutorial;

        originYukgackCanvasOrderLayer = yukgackCanvas.sortingOrder;

        audioSource = NewGameManager.Instance.EffectSouce;

        if(audioSource != null)
            audioSource.loop = false;
    }

    public void TouchScreenSend()
    {
        introObject.TouchScreen();
    }

    public void ActiveMessageWindow()
    {
        messageText.text = "튜토리얼을 스킵하시겠습니까?";
        messageWindow.SetActive(true);
        blockPanel.SetActive(true);
        ClickSound();
    }

    public void InactiveMessageWindow()
    {
        messageWindow.SetActive(false);
        blockPanel.SetActive(false);
        ButtonClickSound();
    }

    public void SkipIntroScene()
    {
        InactiveMessageWindow();
        introObject.SkipIntroScene();
        isTutorial = false;
        ButtonClickSound();
    }

    public void ToggleSkipButton(bool value)
    {
        skipButton.SetActive(value);
    }

    public void ToggleSubjectButtons(bool value)
    {
        subjectButtons.SetActive(value);
    }

    public void ToggleEvalationButton(bool value)
    {
        evalationButton.SetActive(value);
    }

    public void ToggleTouchPanel(bool value)
    {
        touchPanel.SetActive(value);
    }

    public void StartEvalationButton()
    {
        selectSubjectTutorial.StartEvalation();
        ButtonClickSound();
    }

    public void SelectAiming()
    {
        selectSubjectTutorial.SelectSubject(Subject.Aiming);
        ClickSound();
    }

    public void SelectConcentration()
    {
        selectSubjectTutorial.SelectSubject(Subject.Concentration);
        ClickSound();
    }

    public void SelectRhythmicSense()
    {
        selectSubjectTutorial.SelectSubject(Subject.RhythmicSense);
        ClickSound();
    }

    public void SelectQuickness()
    {
        selectSubjectTutorial.SelectSubject(Subject.Quickness);
        ClickSound();
    }

    public void SelectThinking()
    {
        selectSubjectTutorial.SelectSubject(Subject.Thinking);
        ClickSound();
    }

    public void ToggleYukgack(bool value)
    {
        yukgackCanvas.transform.GetChild(0).gameObject.SetActive(value);
    }

    public void UpYukgackCanvasLayerOrder(int nowValue)
    {
        ChangeCanvasLayerOrder(yukgackCanvas, nowValue + 1);
    }

    public void RecoverYukgackCanvasLayerOrder()
    {
        ChangeCanvasLayerOrder(yukgackCanvas, originYukgackCanvasOrderLayer);
    }

    private void ChangeCanvasLayerOrder(Canvas target, int value)
    {
        target.sortingOrder = value;
    }

    private void ClickSound()
    {
        if (audioSource != null && clickSound != null)
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

    public bool IsTutorial
    {
        get { return isTutorial; }
        set { isTutorial = value; }
    }
}
