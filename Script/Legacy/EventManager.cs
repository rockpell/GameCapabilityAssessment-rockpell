using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    [SerializeField] private CustomEvent[] normal;
    [SerializeField] private GameObject speechBalloon;
    [SerializeField] private GameObject speechNext;
    [SerializeField] private GameObject branchBox;

    private CustomEvent nowEvent;
    private Text speech;
    private Dictionary<string, int> labelActionDic;

    private bool isSkip = false;
    private bool isNextAction = false;
    private bool isSpeechClick = false;
    private bool isSelectBranch = false;
    private bool isChoiseBranch = false;
    private bool isJumpIndex = false;
    private bool isEventStart = false;

    private string answerTrue, answerFalse;

    private int jumpIndex;

    // Use this for initialization
    void Start () {
        //nowEvent = normal[1];
        speech = speechBalloon.transform.GetChild(0).GetComponent<Text>();
        labelActionDic = new Dictionary<string, int>();
        StartRandomNormalEvent();
    }

    private void StartRandomNormalEvent()
    {
        isEventStart = true;
        RandomNormalEvent();
        StartCoroutine(EventProcessor());
    }

    private void LabelEventPaser()
    {
        if (labelActionDic.Count > 0) labelActionDic.Clear();
        for (int i = 0; i < nowEvent.actionList.Length; i++)
        {
            if (nowEvent.actionList[i].aType == ActionType.LABEL)
            {
                labelActionDic.Add(nowEvent.actionList[i].value, i);
            }
        }
    }

    private IEnumerator EventProcessor()
    {
        isSkip = false;

        LabelEventPaser();

        for (int i = 0; i < nowEvent.actionList.Length; i++)
        {
            if (isJumpIndex)
            {
                i = jumpIndex;
                isJumpIndex = false;
            }

            if (nowEvent.actionList[i].aType == ActionType.NONSTOPTALK)
            {
                StartCoroutine(NonStopTalk(nowEvent.actionList[i].value));
            }
            else if (nowEvent.actionList[i].aType == ActionType.TALK)
            {
                StartCoroutine(Talk(nowEvent.actionList[i].value));
            }
            else if (nowEvent.actionList[i].aType == ActionType.WAITCLICK)
            {
                StartCoroutine(WaitClick());
            }
            else if (nowEvent.actionList[i].aType == ActionType.WAIT)
            {
                float temp = 0;
                float.TryParse(nowEvent.actionList[i].value, out temp);
                StartCoroutine(Wait(temp));
            }
            else if (nowEvent.actionList[i].aType == ActionType.CLOSEDIALOG)
            {
                CloseDialog();
            }
            else if (nowEvent.actionList[i].aType == ActionType.ACT)
            {
                Act(nowEvent.actionList[i].value);
            }
            else if (nowEvent.actionList[i].aType == ActionType.BRANCH)
            {
                StartCoroutine(Branch(i));
            }
            else if (nowEvent.actionList[i].aType == ActionType.JUMP)
            {
                Jump(nowEvent.actionList[i].value);
            }

            yield return new WaitUntil(() => isNextAction);
        }
    }

    private IEnumerator NonStopTalk(string text)
    {
        isNextAction = false;
        speech.text = "";
        if (!speechBalloon.activeSelf)
        {
            speechBalloon.SetActive(true);
        }

        text = StringVariablePaser(text);

        foreach (char letter in text.ToCharArray())
        {
            speech.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.2f);
        isNextAction = true;
    }

    private IEnumerator Talk(string text)
    {
        isNextAction = false;
        speech.text = "";
        if (!speechBalloon.activeSelf)
        {
            speechBalloon.SetActive(true);
        }
        isSpeechClick = false;

        text = StringVariablePaser(text);

        foreach (char letter in text.ToCharArray())
        {
            if (isSpeechClick)
            {
                speech.text = text;
                break;
            }
            speech.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        isSpeechClick = false;

        speechNext.SetActive(true);
        yield return new WaitUntil(() => isSpeechClick);
        speechNext.SetActive(false);

        isSpeechClick = false;
        isNextAction = true;
    }

    private IEnumerator Wait(float time)
    {
        isNextAction = false;
        yield return new WaitForSeconds(time);
        isNextAction = true;
    }

    private IEnumerator WaitClick()
    {
        isNextAction = false;
        isSpeechClick = false;

        speechNext.SetActive(true);
        yield return new WaitUntil(() => isSpeechClick);
        speechNext.SetActive(false);

        isSpeechClick = false;
        isNextAction = true;
    }

    private IEnumerator Branch(int nowIndex)
    {
        isNextAction = false;
        isSelectBranch = false;
        branchBox.SetActive(true);

        string[] values = nowEvent.actionList[nowIndex].value.Split(',');
        answerTrue = values[0];
        answerFalse = values[1];

        yield return new WaitUntil(() => isSelectBranch);

        if (isChoiseBranch) Jump(answerTrue);
        else Jump(answerFalse);

        isNextAction = true;
    }

    private void Act(string name)
    {
        Invoke(name, 0);
    }

    private void Jump(string label)
    {
        if (labelActionDic.ContainsKey(label))
        {
            isJumpIndex = true;
            jumpIndex = labelActionDic[label];
        }
    }

    public void ActiveSpeechClick()
    {
        isSpeechClick = true;
    }

    public void CloseDialog()
    {
        speechBalloon.SetActive(false);
        isEventStart = false;
    }

    public void SelectBranch(bool choice)
    {
        isSelectBranch = true;
        isChoiseBranch = choice;
        branchBox.SetActive(false);
    }

    public void EventSkip()
    {
        isSkip = true;
    }

    public void ToggleSpeechBalloon()
    {
        if(speechBalloon.activeSelf) speechBalloon.SetActive(false);
        else speechBalloon.SetActive(true);
    }

    public void HideSpeechBalloon()
    {
        speechBalloon.SetActive(false);
    }

    private string StringVariablePaser(string text)
    {
        //string txt = "%%test%%?잠이 %%test2%% 오네요 이런 %%test3%% 젠장";
        string txt = text;
        Regex rgPattern = new Regex(@"\%%(\s*\w+\s*\w*)\%%");
        MatchCollection result = rgPattern.Matches(txt);
        int resizeNum = 0;

        foreach (Match mm in result)
        {
            Group g = mm.Groups[1];
            txt = txt.Remove(g.Index - 2 - resizeNum, g.Value.Length + 4);
            resizeNum -= InsertVariable(ref txt, g.Value, g.Index - 2 - resizeNum);
            resizeNum += g.Value.Length + 4;
        }
        Debug.Log(txt);

        return txt;
    }

    private int InsertVariable(ref string text, string variableText, int startIndex)
    {
        string target = "";

        switch (variableText)
        {
            case "test":
                target = "테스트";
                break;
            case "test2":
                target = "테스트2";
                break;
            case "test3":
                target = "테스트3";
                break;
        }

        text = text.Insert(startIndex, target);

        return target.Length;
    }

    private void RandomNormalEvent()
    {
        int _result = Random.Range(0, normal.Length);
        nowEvent = normal[_result];
    }

    public void RestartNomralEvent()
    {
        if(!isEventStart)
            StartRandomNormalEvent();
    }
}