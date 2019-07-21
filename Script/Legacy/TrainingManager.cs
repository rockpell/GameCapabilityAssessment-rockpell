using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour {

    [SerializeField] private Image minigameImage;
    [SerializeField] private Image tutorialImage;

    [SerializeField] private Sprite[] aimingImages;
    [SerializeField] private Sprite[] concentrationImages;
    [SerializeField] private Sprite[] quicknessImages;
    [SerializeField] private Sprite[] rhythmicSenseImages;
    [SerializeField] private Sprite[] thinkingImages;

    //[SerializeField] private Sprite[] aimingTutorialImages;
    //[SerializeField] private Sprite[] concentrationTutorialImages;
    //[SerializeField] private Sprite[] quicknessTutorialImages;
    //[SerializeField] private Sprite[] rhythmicSenseTutorialImages;
    //[SerializeField] private Sprite[] thinkingTutorialImages;

    [SerializeField] private Transform rollingTextObject;
    [SerializeField] private Transform lifeImages;
    [SerializeField] private Transform rankingText;

    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject RankPanel;

    private Sprite[] nowSubjectImages;

    private bool isTrainingClear = false;

    private int subjectIndex; // 항목 번호
    private int minigameIndex; // 항목 내 미니게임 번호
    
    void Start () {
        GameManager.Instance.IsTrainingStart = true;
        subjectIndex = 0;
        minigameIndex = 0;
        RefreshMinigameImage();
        RefreshRankingText();
        if (GameManager.Instance.CheckTraining == null)
        {
            MoveCameraToSelectCanvas();
        }
        else
        {
            subjectIndex = GameManager.Instance.CheckTraining.SubjectIndex;
            minigameIndex = GameManager.Instance.CheckTraining.MinigameIndex;

            LifeImageSetting();
            CheckTrainingLife();
            CheckTrainingClear();
            RollingNumberSetting();

            if (isTrainingClear)
            {
                Invoke("ShowEndPanel", 0.7f);
            } else
            {
                if (GameManager.Instance.TrainingLife > 0)
                {
                    RollingNumberStart();
                    if (!GameManager.Instance.IsTutorial)
                    {
                        Invoke("LoadGame", 1f);
                    } else
                    {
                        Invoke("StartLoadGame", 1f);
                    }
                }
                else
                {
                    Invoke("ShowEndPanel", 0.7f);
                    //Invoke("EndTraining", 1f);
                }
            }
        }
    }

    public int SubjectIndex {
        get { return subjectIndex; }
    }

    public int MinigameIndex {
        get { return minigameIndex; }
    }

    public void EndTraining()
    {
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.TrainingScene);
        GameManager.Instance.EndTraining();
    }

    public void ControlIndex(int value)
    {
        if(value == 1 || value == -1)
        {
            minigameIndex += value;
            if(minigameIndex >= GetNowSubjectImages().Length)
            {
                subjectIndex += 1;
                minigameIndex = 0;
                RectifySubjectIndex();
            } else if(minigameIndex < 0)
            {
                subjectIndex -= 1;
                RectifySubjectIndex();
                minigameIndex = GetNowSubjectImages().Length - 1;
            }
        }
        RefreshMinigameImage();
        RefreshRankingText();
    }

    private void RectifySubjectIndex()
    {
        if (subjectIndex > 4) subjectIndex = 0;
        else if (subjectIndex < 0) subjectIndex = 4;
    }

    private Sprite[] GetNowSubjectImages()
    {
        if (subjectIndex == 0) return aimingImages;
        else if (subjectIndex == 1) return concentrationImages;
        else if (subjectIndex == 2) return quicknessImages;
        else if (subjectIndex == 3) return rhythmicSenseImages;
        else if (subjectIndex == 4) return thinkingImages;

        return null;
    }

    private void RefreshMinigameImage()
    {
        minigameImage.sprite = GetNowSubjectImages()[minigameIndex];
    }

    private void MoveCameraToSelectCanvas()
    {
        Camera.main.transform.position = new Vector3(1000, 0, -10);
    }

    private void MoveCameraToTutorialCanvas()
    {
        Camera.main.transform.position = new Vector3(2000, 0, -10);
    }

    public void LoadGame()
    {
        //tutorialImage.sprite = GetNowTutorialImage()[minigameIndex];
        //MoveCameraToTutorialCanvas();
        GameManager.Instance.IsTutorial = true;
        GameManager.Instance.StartTraining(subjectIndex, minigameIndex);
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.Tutorial);
    }

    public void StartLoadGame()
    {
        GameManager.Instance.StartTraining(subjectIndex, minigameIndex);
        CustomSceneManager.Instance.LoadGame((Subject)subjectIndex, minigameIndex);
    }

    private void RollingNumberSetting()
    {
        int _previousLevel = GameManager.Instance.PreviousDifficultyLevel;
        int _nowLevel = GameManager.Instance.DifficultyLevel;

        rollingTextObject.GetChild(2).GetComponent<Text>().text = _previousLevel.ToString();

        if (_previousLevel > _nowLevel) // 현재 난이도가 낮아지는 경우
        {
            if (_previousLevel - _nowLevel == 1)
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
        switch (GameManager.Instance.DifficultyLevel - GameManager.Instance.PreviousDifficultyLevel)
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

    private void CheckTrainingLife(){
        
        if (GameManager.Instance.PreviousDifficultyLevel > GameManager.Instance.DifficultyLevel) // 현재 난이도가 낮아지는 경우
        {
            GameManager.Instance.TrainingLife -= 1;
            GameManager.Instance.DifficultyLevel = GameManager.Instance.PreviousDifficultyLevel; 
            Invoke("HideLifeImage", 0.4f);
        }
    }

    private void LifeImageSetting()
    {
        for(int i = GameManager.Instance.TrainingLife; i < lifeImages.childCount; i++)
        {
            lifeImages.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }
    }

    private void HideLifeImage()
    {
        lifeImages.GetChild(GameManager.Instance.TrainingLife).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
    }

    private void ShowEndPanel()
    {
        EndPanel.SetActive(true);
    }

    public void CloseEndPanel()
    {
        EndPanel.SetActive(false);
    }

    public void ShowRankPanel()
    {
        RankPanel.SetActive(true);
    }

    private void CheckTrainingClear()
    {
        if(GameManager.Instance.PreviousDifficultyLevel == 15 && GameManager.Instance.DifficultyLevel == 15)
        {
            isTrainingClear = true;
        }
    }

    //private void RefreshRankingText()
    //{
    //    string _gameName = subjectIndex.ToString() + minigameIndex.ToString();
    //    SetRankingText(GameManager.Instance.gameObject.GetComponent<LocalRankingManager>().GetRanking(_gameName));
    //}

    //private void SetRankingText(List<LocalRecord> records)
    //{
    //    int _recordsCount = records.Count;
    //    int _limit;
    //    Debug.Log("records : " + records.Count);
    //    if (_recordsCount > 10) _limit = 10;
    //    else _limit = _recordsCount;

    //    for (int i = 0; i < _limit; i++)
    //    {
    //        rankingText.GetChild(i).GetComponent<Text>().text = (i + 1).ToString() + ". " + records[i].name + " " + records[i].score.ToString();
    //        //rankingText.GetComponent<Text>().text = (i + 1).ToString() + ". " + records[i].name + " " + records[i].score.ToString();
    //    }
    //    if (_limit < 10)
    //    {
    //        for (int i = _limit; i < 10; ++i)
    //        {
    //            rankingText.GetChild(i).GetComponent<Text>().text = "";
    //        }
    //    }
    //}

    private void RefreshRankingText()
    {
        //Debug.Log("is Work;");
        SetRankingText(new List<Record>());
        string _gameName = subjectIndex.ToString() + minigameIndex.ToString();
        GameManager.Instance.gameObject.GetComponent<RankingManager>().GetRanking(_gameName, SetRankingText);
    }

    public void SetRankingText(List<Record> records)
    {
        int _recordsCount = records.Count;
        int _limit;
        if (_recordsCount > 10) _limit = 10;
        else _limit = _recordsCount;

        for (int i = 0; i < _limit; i++)
        {
            rankingText.GetChild(i).GetComponent<Text>().text = (i + 1).ToString() + ". " + records[i].name + " " + records[i].score.ToString();
            //rankingText.GetComponent<Text>().text = (i + 1).ToString() + ". " + records[i].name + " " + records[i].score.ToString();
        }
        if (_limit < 10)
        {
            for (int i = _limit; i < 10; ++i)
            {
                rankingText.GetChild(i).GetComponent<Text>().text = "";
            }
        }
    }

    public void SetMyRankingText()
    {
        int num;
        GameManager.Instance.gameObject.GetComponent<RankingManager>().GetMyRank(() =>
        {
            num = GameManager.Instance.gameObject.GetComponent<RankingManager>().TempMyRanking;
            RankPanel.transform.Find("RankText").GetComponent<Text>().text = num.ToString();
        });
        
    }
}