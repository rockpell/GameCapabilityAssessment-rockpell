using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewTutorialManager : MonoBehaviour {
    [SerializeField] private Canvas tutoCanvas;
    private bool isShowExplantion;          //설명창 보여주는가?
    private GameObject explantion;          //설명창
    private GameObject replayButton;        //리플레이 버튼
    private Text progressText;              //"계속하기"로 바꾸기 위한것
    
    // Use this for initialization
    void Start () {
        isShowExplantion = false;
        explantion = tutoCanvas.transform.GetChild(3).gameObject;
        replayButton = tutoCanvas.transform.GetChild(2).gameObject;
        progressText = explantion.transform.GetChild(0).GetComponentInChildren<Text>();
        replayButton.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Replay() // 튜토리얼을 다시하는 함수
    {
        CustomSceneManager.Instance.Replay();
    }

    public void SkipTutorial() // 튜토리얼을 종료하고 본게임 시작하는 함수
    {
        ShutterManager _shutterManager = GameObject.Find("ShutterCanvas").GetComponent<ShutterManager>();
        if (_shutterManager != null)
            _shutterManager.ShutterSequence(Result.BigSuccessful, NewGameManager.Instance.NowMiniGame, false);
        else
            Debug.Log("ShutterManager를 찾을 수 없음");
    }

    public void ShowExplanation()
    {
        CustomSceneManager.Instance.ChangeScene("ExplanationScene");
    }

    public void ToggleExplanation() // 설명창을 다시 띄워주고 끄는 함수
    {
        //설명창을
        isShowExplantion = true;
        explantion.SetActive(true);
        progressText.text = "계속하기";
    }
    //튜토리얼 스타트
    //설명창 가리기
    public void TutorialStart()
    {
        isShowExplantion = false;
        explantion.SetActive(false);
        replayButton.SetActive(false);
    }
    //현재 설명창 상태
    public bool GetIsShowExplantion()
    {
        return isShowExplantion;
    }

    //튜토리얼이 끝난 상태, 리플레이 버튼을 보여줌
    public void EndTutorial()
    {
        replayButton.SetActive(true);
    }
    
}
