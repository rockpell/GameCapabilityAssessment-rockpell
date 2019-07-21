using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBtn : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject returnMenuField;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject shutterForm;
    [SerializeField]
    private IntroUIManager UIManager;
    [SerializeField]
    private AudioClip btnClick;
    [SerializeField]
    private AudioClip menuClick;


    void Start () {
	}
	
	void Update ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && (UIManager.IsTutorial == false))
            {
                FieldControl();
            }
        }
	}
    
    public void FieldControl()
    {
        shutterForm = ShutterManager.Instance.FindChild(ShutterManager.Instance.gameObject, "ShutterForm");
        
        if (shutterForm.activeInHierarchy == false)
        {
            if (returnMenuField.activeInHierarchy)
            {
                returnMenuField.SetActive(false);
                NewGameManager.Instance.SetTouchDisablePanel(false);
            }
            else
            {
                NewGameManager.Instance.EffectSouce.clip = menuClick;
                NewGameManager.Instance.EffectSouce.Play();
                returnMenuField.SetActive(true);
                NewGameManager.Instance.SetTouchDisablePanel(true);
                if (NewGameManager.Instance.GetNowSubject() != Subject.None)
                {
                    text.GetComponent<UnityEngine.UI.Text>().text = "항목 선택으로 돌아가시겠습니까?";
                }
                else
                {
                    text.GetComponent<UnityEngine.UI.Text>().text = "메인메뉴로 돌아가시겠습니까?";
                }
            }
        }
    }

    public void FieldBtnCheck(bool check)
    {
        shutterForm = ShutterManager.Instance.FindChild(ShutterManager.Instance.gameObject, "ShutterForm");

        if (shutterForm.activeInHierarchy == false)
        {
            NewGameManager.Instance.EffectSouce.clip = btnClick;
            NewGameManager.Instance.EffectSouce.Play();
            if (check && (NewGameManager.Instance.GetNowSubject() == Subject.None))
            {
                CustomSceneManager.Instance.ChangeScene("MenuScene");
                NewGameManager.Instance.SetTouchDisablePanel(false);
            }
            else if (check && (NewGameManager.Instance.GetNowSubject() != Subject.None))
            {
                NewGameManager.Instance.SetNowSubjet(Subject.None);
                CustomSceneManager.Instance.Replay();
                NewGameManager.Instance.SetTouchDisablePanel(false);
            }
            else
            {
                FieldControl();
                NewGameManager.Instance.SetTouchDisablePanel(false);
            }
        }

    }
}
