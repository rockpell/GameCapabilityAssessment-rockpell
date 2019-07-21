using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MazeUIManager : MonoBehaviour {
    public static MazeUIManager instance;
    public Text currentTime;
    public Text currentGift;
    [SerializeField] private Text bonusTime;
    [SerializeField] private Image arrow;
    [SerializeField] private Text startTime;
    [SerializeField] private Text tutorialClearText;
    [SerializeField] private NewTutorialManager tutorialCanvas;
    private float time = 10.0f;
    private float bonus = 0;
    private Transform bonusTimeTrans;
    public int giftNum = 0;
    public int totalGiftNum = 0;
    public int gameLevel = 0;
    public bool isFinish = false;
    public bool startSignalEnd;
    public bool isTutorial;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
    }
    private void Start()
    {
        if (tutorialCanvas != null)
            isTutorial = true;
        else isTutorial = false;
        if (!isTutorial)
        {
            gameLevel = NewGameManager.Instance.StartGame();
            if (gameLevel % 3 == 0)
            {
                totalGiftNum = 5;
            }
            else if (gameLevel % 3 == 1)
            {
                totalGiftNum = 3;
            }
            else if (gameLevel % 3 == 2)
            {
                totalGiftNum = 4;
            }
            GiveBonusTime();
            bonusTime.gameObject.SetActive(false);
            bonusTimeTrans = bonusTime.transform;
        }
        else
        {
            totalGiftNum = 3;
            gameLevel = 1;
            bonusTime.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (!isFinish)
        {
            currentGift.text = "선물 : " + giftNum + " / " + totalGiftNum + "개";
            //총선물이랑 얻은 선물이 같을때
            if (giftNum == totalGiftNum)
            {
                if (isTutorial)
                {
                    tutorialClearText.gameObject.SetActive(true);
                    tutorialClearText.text = "게임 성공";
                    tutorialCanvas.EndTutorial();
                }
                else NewGameManager.Instance.ClearGame(Result.BigSuccessful);
                isFinish = true;
                return;
            }
            //카메라 이동, 산타 움직일때, 처음 산타위치 알려줄때 시간이 지나지 않음
            if (!MazeSanta.instance.isCamMove && !MazeSanta.instance.isOn && startSignalEnd) time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                if (isTutorial)
                {
                    tutorialClearText.gameObject.SetActive(true);
                    tutorialClearText.text = "게임 실패";
                    tutorialCanvas.EndTutorial();
                }
                else GameResult();
                isFinish = true;
                return;
            }
            currentTime.text = "Time : " + time.ToString("N1") + "초";
        }
    }
    //게임 결과, 총 선물 개수에따라 설정
    void GameResult()
    {
        switch (totalGiftNum)
        {
            case 3:
                if (giftNum == 0)
                {
                    NewGameManager.Instance.ClearGame(Result.Fail);
                }
                else if (giftNum == 2 || giftNum == 1)
                {
                    NewGameManager.Instance.ClearGame(Result.Successful);
                }
                break;
            case 4:
                if (giftNum < 2)
                {
                    NewGameManager.Instance.ClearGame(Result.Fail);
                }
                else if (giftNum < totalGiftNum  && giftNum > 1)
                {
                    NewGameManager.Instance.ClearGame(Result.Successful);
                }
                break;
            case 5:
                if (giftNum < 2)
                {
                    NewGameManager.Instance.ClearGame(Result.Fail);
                }
                else if (giftNum < totalGiftNum && giftNum > 1)
                {
                    NewGameManager.Instance.ClearGame(Result.Successful);
                }
                break;
        }
    }
    //보너스 타임추가, 7단계 이전은 추가되지 않음
    public IEnumerator BonusTime()
    {
        if (gameLevel < 7) yield return null;
        else
        {
            if (time + bonus < 10.0f) time += bonus;
            else time = 10.0f;

            bonusTime.gameObject.SetActive(true);
            for (float i = 1f; i > 0; i -= 0.03f)
            {
                bonusTime.text = "+" + bonus + "초";
                bonusTime.transform.Translate(0, 0.5f, 0);
                Color color = new Vector4(0, 0, 0, i);
                bonusTime.color = color;
                yield return 0;
            }
            ResetBonusTimeText();
        }
    }
    //보너스 텍스트 위치 초기화
    private void ResetBonusTimeText()
    {
        bonusTime.transform.position = bonusTimeTrans.position;
        bonusTime.gameObject.SetActive(false);
    }
    //플레이어 위치를 알려주는 화살표를 움직여줌
    public IEnumerator PlayerPointArrow()
    {
        startSignalEnd = false;
        arrow.gameObject.SetActive(true);
        Vector3 santaPosTmp = MazeSanta.instance.transform.position;
        santaPosTmp.x -= 1;
        Vector3 arrowPos = Camera.main.WorldToScreenPoint(santaPosTmp);
        startTime.gameObject.SetActive(true);
        arrow.transform.position = arrowPos;
        for (float i = 0; i < 2.0f; i+=Time.deltaTime)
        {
            arrow.transform.localRotation *= Quaternion.Euler(8, 0, 0);
            startTime.text = ((int)(3 - i)).ToString();
            if((i + 1) * 2 <= 4) startTime.transform.localScale = new Vector3((i + 1) * 2, (i + 1) * 2, (i + 1) * 2);
            yield return null;
        }
        startTime.text = "Start";
        startSignalEnd = true;
        arrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        startTime.gameObject.SetActive(false);
    }
    //보너스 추가량
    void GiveBonusTime()
    {
        if (totalGiftNum == 3)
        {
            bonus = 0.3f;
        }
        else if (totalGiftNum == 4)
        {
            bonus = 0.7f;
        }
        else if (totalGiftNum == 5)
        {
            bonus = 1.2f;
        }
    }
    public NewTutorialManager GetTutorialManager()
    {
        return tutorialCanvas;
    }
}
