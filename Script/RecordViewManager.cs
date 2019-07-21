using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecordViewManager : MonoBehaviour {

    [SerializeField]
    BackgroundImageController samgak;
    //최고점수, 총점수의 평균 점수,항목 평균
    //1. 조준, 2. 집중, 3. 리듬, 4. 순발, 5. 사고
    [SerializeField] private GameObject scrollText;
    [SerializeField] private Canvas summaryCanvas; //요약 캔버스
    [SerializeField] private Canvas latestCanvas; //최근기록 캔버스
    [SerializeField] private Canvas statisticsCanvas; //통계 캔버스
    [SerializeField] private Image StatisticsSelectImage; //통계에서 선택된 미니게임 아이콘이미지
    [SerializeField] private GameObject LatestScrollView; // 최근기록 스크롤 뷰
    [SerializeField] private GameObject LatestResultView; //최근 기록 결과
    [SerializeField] private GameObject LatestContent; //최근기록 스크롤에 있는 content
    [SerializeField] private GameObject LatestRecords; //최근기록들
    [SerializeField] private Image LatestSelectImage; //최근기록에서 선택된 미니게임 아이콘 이미지
    [SerializeField] private GaugeImageController highTotalGaugeImage; //최고기록 토탈 게이지
    [SerializeField] private GaugeImageController averageTotalGaugeImage; //평균기록 토탈 게이지
    [SerializeField] private LineGraph line; //통계 라인 그래프
    [SerializeField] private Text lastestDate; //최근 기록 눌렀을때 날짜
    [SerializeField] private Image[] tabButtonSprites;
    [SerializeField] private Image selectSubjectImage;
    [SerializeField] private Sprite[] subjectSprites;
    [SerializeField] private AudioClip clickBtn;
    [SerializeField] private AudioSource audioSource;
    private int totalHighScore = 0;
    private int totalAverScore = 0;
    private int totalScoreSum = 0;
    private int[] subjectHighScore;
    private int[] subjectAverScore;
    private int[] subjectScoreSum;
    private int playCount = 0;
    private string[] dateArr; //저장될 날짜 값
    private int latestIndex = 0;
    private List<GameRecord> gameRecord;
    private TestTrainingManager trainingManager;
    //[5개 항목] 1개 결과(총 점수) , 날짜?-> 가진 리스트
    //마지막에 결과 반영
    //현재 기록 반영
    // Use this for initialization
    void Start() {
        audioSource.clip = clickBtn;
        gameRecord = NewGameManager.Instance.LoadGameRecord();
        trainingManager = GetComponent<TestTrainingManager>();
        tabButtonSprites[1].color = Color.gray;
        tabButtonSprites[2].color = Color.gray;
        samgak.Active();
        subjectAverScore = new int[5];
        subjectHighScore = new int[5];
        subjectScoreSum = new int[5];
        for (int i = 0; i < subjectAverScore.Length; i++)
        {
            subjectAverScore[i] = 0;
            subjectHighScore[i] = 0;
            subjectScoreSum[i] = 0;
        }
        SortCurrentResult();
        CaculateTotalGaugeInput();
        SettingResultOnClick();
    }

    //최고 점수 총 점수 비교
    private void HighScoreCompare(int score)
    {
        if (totalHighScore < score) totalHighScore = score;
    }
    //평균 점수 계산
    private void AverTotalScoreCaculate(int score)
    {
        HighScoreCompare(score);
        totalScoreSum += score;
        if (playCount != 0) totalAverScore = totalScoreSum / playCount;
    }
    //항목 최고점비교 
    private void SubjectHighScoreCompare(int score, int index)
    {
        if (subjectHighScore[index] < score) subjectHighScore[index] = score;
    }
    //항목 평균 계산
    private void SubjectAverScoreCaculate(int score, int index)
    {
        SubjectHighScoreCompare(score, index);
        subjectScoreSum[index] += score;
        if (playCount != 0) subjectAverScore[index] = subjectScoreSum[index] / playCount;
    }

    private void SortCurrentResult()
    {
        int subjectResult = 0;
        gameRecord.Reverse();
        dateArr = new string[gameRecord.Count];
        for (int i = 0; i < gameRecord.Count; i++)
        {
            GameObject tmpRecord = Instantiate(LatestRecords);

            int totalScore = 0;
            Text dateText = tmpRecord.transform.GetChild(0).GetComponent<Text>();
            Text resultText = tmpRecord.transform.GetChild(1).GetComponent<Text>();
            Text[] scoreText = new Text[5];
            for (int j = 0; j < scoreText.Length; j++)
            {
                scoreText[j] = tmpRecord.transform.GetChild(j + 2).GetComponent<Text>();
            }
            playCount++;
            int[] subjectResultArr = new int[gameRecord[i].gameResults.Length + 1];
            for (int j = 0; j < gameRecord[i].gameResults.Length; j++)
            {
                subjectResult = NewGameManager.Instance.CalculateResultValue(gameRecord[i].gameResults[j]);
                totalScore += subjectResult;
                scoreText[j].text = subjectResult.ToString();
                SubjectAverScoreCaculate(subjectResult, j);
                subjectResultArr[j] = subjectResult;
            }
            subjectResultArr[gameRecord[i].gameResults.Length] = totalScore;
            AverTotalScoreCaculate(totalScore);
            dateText.text = DateCaculate(gameRecord[i].time);
            dateArr[i] = gameRecord[i].time.Substring(0, 4) + ". " + gameRecord[i].time.Substring(4, 2) + ". " + gameRecord[i].time.Substring(6, 2);
            resultText.text = "결과 점수 : " + totalScore.ToString();
            tmpRecord.transform.SetParent(LatestContent.transform);
            tmpRecord.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private string DateCaculate(string currentTimeString)
    {
        string currentString = "";
        string recordTimeString = "";
        string resultString = "";
        currentString = System.DateTime.Now.Year.ToString() + "/" + 
            System.DateTime.Now.Month.ToString()+ "/" + 
            System.DateTime.Now.Day.ToString()+ " " + 
            System.DateTime.Now.Hour.ToString()+ ":" + 
            System.DateTime.Now.Minute.ToString() + ":" +
            System.DateTime.Now.Second.ToString();
        recordTimeString = currentTimeString.Substring(0, 4) + "/" + 
            currentTimeString.Substring(4, 2) + "/" + 
            currentTimeString.Substring(6, 2) + " " + 
            currentTimeString.Substring(8, 2) + ":" + 
            currentTimeString.Substring(10, 2) + ":" +
            currentTimeString.Substring(12, 2);
        System.DateTime recordTime = System.Convert.ToDateTime(recordTimeString);
        System.DateTime currentTime = System.Convert.ToDateTime(currentString);
        System.TimeSpan timeSpan = currentTime - recordTime;
        if(timeSpan.Days <1)
        {
            if(timeSpan.Hours < 1)
            {
                if(timeSpan.Minutes < 1)
                {
                    resultString = "몇초전";
                }
                else resultString = timeSpan.Minutes + "분전";
            }
            else resultString = timeSpan.Hours + "시간전";
        }
        else if(timeSpan.Days <30)
        {
            resultString = timeSpan.Days + "일전";
        }
        else
        {
            int result = timeSpan.Days / 30;
            if(result < 12)
            {
                resultString = result.ToString() + "달전";
            }
            else resultString = (result - 11).ToString() + "년전";
        }
        return resultString;
    }
    public void ClickSummaryButton()
    {
        summaryCanvas.gameObject.SetActive(true);
        latestCanvas.gameObject.SetActive(false);
        statisticsCanvas.gameObject.SetActive(false);
        line.gameObject.SetActive(false);
        highTotalGaugeImage.gameObject.SetActive(true);
        averageTotalGaugeImage.gameObject.SetActive(true);
        tabButtonSprites[0].color = Color.white;
        tabButtonSprites[1].color = Color.gray;
        tabButtonSprites[2].color = Color.gray;
        audioSource.Play();
    }
    public void ClickLatestButton()
    {
        summaryCanvas.gameObject.SetActive(false);
        latestCanvas.gameObject.SetActive(true);
        statisticsCanvas.gameObject.SetActive(false);
        LatestScrollView.SetActive(true);
        LatestResultView.SetActive(false);
        line.gameObject.SetActive(false);
        highTotalGaugeImage.gameObject.SetActive(false);
        averageTotalGaugeImage.gameObject.SetActive(false);
        tabButtonSprites[0].color = Color.gray;
        tabButtonSprites[1].color = Color.white;
        tabButtonSprites[2].color = Color.gray;
        audioSource.Play();
    }
    public void ClickStatisticsButton()
    {
        summaryCanvas.gameObject.SetActive(false);
        latestCanvas.gameObject.SetActive(false);
        statisticsCanvas.gameObject.SetActive(true);
        line.gameObject.SetActive(false);
        highTotalGaugeImage.gameObject.SetActive(false);
        averageTotalGaugeImage.gameObject.SetActive(false);
        tabButtonSprites[0].color = Color.gray;
        tabButtonSprites[1].color = Color.gray;
        tabButtonSprites[2].color = Color.white;
        audioSource.Play();
        GameObject oldObject = trainingManager.GetStatisticTextParent();
        if (oldObject.transform.childCount == 0) trainingManager.LoadStatistic(NewGameManager.Instance.LoadMiniGameRecord("BoxingGame"), "BoxingGame");
    }

    public void ClickStatisticsSubjectIcon(Sprite image)
    {
        StatisticsSelectImage.sprite = image;
        GameObject oldObject = trainingManager.GetStatisticTextParent();
        Subject tmpSub = CustomSceneManager.Instance.GetGameSubject(image.name);
        selectSubjectImage.sprite = subjectSprites[NewGameManager.Instance.GetSubjectIndex(tmpSub)];
        if (oldObject.transform.childCount != 0)
        {
            for (int i = 0; i < oldObject.transform.childCount; i++)
            {
                Destroy(oldObject.transform.GetChild(i).gameObject);
            }
        }
        audioSource.Play();
        trainingManager.LoadStatistic(NewGameManager.Instance.LoadMiniGameRecord(image.name),image.name);
    }
    public void ClickLatestSubjectIcon(Sprite image)
    {
        LatestSelectImage.sprite = image;
        SpriteRenderer lineSprite = line.headImage.GetChild(0).GetComponent<SpriteRenderer>();
        lineSprite.sprite = image;
        audioSource.Play();
    }
    private void SettingResultOnClick()
    {
        Button[] results = new Button[LatestContent.transform.childCount];
        
        for (int i = 0; i < results.Length; i++)
        {
            int num = i;
            results[i] = LatestContent.transform.GetChild(i).GetComponent<Button>();
            results[i].onClick.AddListener(()=>ClickResultButton(num));
        }
    }
    public void ClickResultButton(int num)
    {
        LatestScrollView.SetActive(false);
        LatestResultView.SetActive(true);
        lastestDate.text = dateArr[num];
        latestIndex = num;
        line.gameObject.SetActive(true);
        DrawLineGraph(0);
        audioSource.Play();

        //Init sprite
        SpriteRenderer lineSprite = line.headImage.GetChild(0).GetComponent<SpriteRenderer>();
        lineSprite.sprite = subjectSprites[0];
        LatestSelectImage.sprite = subjectSprites[0];
    }

    private void CaculateTotalGaugeInput()
    {
        string highSum = "";
        string averageSum = "";
        for (int i = 0; i < subjectHighScore.Length; i++)
        {
            if (subjectHighScore[i] < 10)
                highSum += ("0" + subjectHighScore[i].ToString());
            else highSum += subjectHighScore[i].ToString();
        }
        for (int i = 0; i < subjectAverScore.Length; i++)
        {
            if(subjectAverScore[i] < 10)
                averageSum += ("0" + subjectAverScore[i].ToString());
            else averageSum += subjectAverScore[i].ToString();
        }
        StartCoroutine(highTotalGaugeImage.ImageActivate(int.Parse(highSum)));
        StartCoroutine(averageTotalGaugeImage.ImageActivate(int.Parse(averageSum)));
    }
    public void DrawLineGraph(int index)
    { 
        float[] scores = new float[9];
        float score = NewGameManager.Instance.StartDifficultyLevel;
        scores[0] = score;
        for (int i = 0; i < gameRecord[latestIndex].gameResults[index].Count; i++)
        {
            if (gameRecord[latestIndex].gameResults[index][i].resultValue == Result.BigSuccessful)
            {
                score += 2.0f;
                if (score > 15) score = 15;
                scores[i + 1] = score;
            }
            else if (gameRecord[latestIndex].gameResults[index][i].resultValue == Result.Successful)
            {
                score += 1.0f;
                if (score > 15) score = 15;
                scores[i + 1] = score;
            }
            else
            {
                score += -1.0f;
                if (score < 1) score = 1;
                scores[i + 1] = score;
            }
        }
        line.SetValues(scores);
        line.DrawGraph();
    }
}
