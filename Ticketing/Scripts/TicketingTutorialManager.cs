using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketingTutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject remainTimeObj;

    [SerializeField]
    private GameObject remainTouchObj;

    [SerializeField]
    private GameObject successObj;

    [SerializeField]
    private GameObject resultObj;

    [SerializeField]
    private Sprite successSprite;

    [SerializeField]
    private Sprite failSprite;

    [SerializeField]
    private GameObject[] monitors = new GameObject[4];

    [SerializeField]
    private GameObject[] messages = new GameObject[4];

    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>(); //0번은 진짜 이미지 나머지는 페이크 이미지

    


    //
    public TKTLevel levelNow { get; set; }

    public float remainTime { get; set; }

    public float gameTime { get; set; }

    public int remainTouch { get; set; }

    public int success { get; set; }

    private bool[] yemeOn = new bool[4];

    public List<timeStamp> timeStamps { get; set; }

    public Coroutine timer { get; set; }

    public Coroutine ctrMonitor { get; set; }

    public List<Coroutine> pops { get; set; }

    public bool controlCancel { get; set; }

    private void init()
    {
        gameTime = 10.0f;

        //성공 실패 표시 끄기
        resultObj.SetActive(false);

        remainTime = gameTime;

        pops = new List<Coroutine>();

        controlCancel = false;

        //난이도에 따라 사용하는 모니터만 켬
        for (int i = 0; levelNow.monitorNum > i; i++)
        {
            monitors[i].SetActive(true);
            messages[i].SetActive(false);
            messages[i].GetComponent<Image>().sprite = null;
        }

        
        

        timeStamps = makeTimeStamp(levelNow);

        //터치 수 초기화
        remainTouch = levelNow.touchNum;
        remainTouchObj.GetComponent<TextMesh>().text = remainTouch.ToString();

        success = 0;

        timer = StartCoroutine(timeCheck());
        ctrMonitor = StartCoroutine(controlMonitor(timeStamps));
    }

    void Start()
    {
        if (NewGameManager.Instance != null)
        {
            levelNow = selectLevel(NewGameManager.Instance.StartGame());
        }
        else
        {
            levelNow = selectLevel(1);
        }

        init();
    }

    IEnumerator controlMonitor(List<timeStamp> input)
    {
        for (int i = 0; input.Count > i; i++)
        {
            pops.Add(StartCoroutine(popMonitor(input[i])));
        }

        yield return null;
    }

    IEnumerator popMonitor(timeStamp input)
    {
        yield return new WaitForSeconds(input.startTime);

        if (!input.isFake)
        {
            yemeOn[input.monitor] = true;
        }

        messages[input.monitor].SetActive(true);
        messages[input.monitor].GetComponent<Image>().sprite = input.sprite;

        yield return new WaitForSeconds(input.holdingTime);

        messages[input.monitor].SetActive(false);
        messages[input.monitor].GetComponent<Image>().sprite = null;

        yield return new WaitForSeconds(0.03f);

        yemeOn[input.monitor] = false;
    }

    IEnumerator timeCheck()
    {
        while (remainTime >= 0)
        {
            remainTime -= Time.deltaTime;

            if (remainTime >= 0)
            {
                remainTimeObj.GetComponent<TextMesh>().text = remainTime.ToString().Substring(0, 3);
            }
            else
            {
                remainTimeObj.GetComponent<TextMesh>().text = "END";
            }

            yield return null;
        }

        StartCoroutine(gameover());
    }

    private TKTLevel selectLevel(int input)
    {
        switch (input)
        {
            case 1:
                return new TKTLevel(1);

            default:
                return new TKTLevel(1);
        }
    }

    public void touchMessage(int monitor)
    {
        if (!controlCancel)
        {
            remainTouch--;
            remainTouchObj.GetComponent<TextMesh>().text = remainTouch.ToString();

            if (yemeOn[monitor]) //예 매! 가 떠있음
            {
                yemeOn[monitor] = false;

                success++;
                successObj.GetComponent<TextMesh>().text = success.ToString();
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }

            messages[monitor].SetActive(false);


            if (remainTouch <= 0)
            {
                StartCoroutine(gameover());
            }
        }
    }

    private List<timeStamp> makeTimeStamp(TKTLevel level)
    {
        List<timeStamp> tempStemps = new List<timeStamp>();

        //chance만큼 진짜를 만듬
        for (int i = 0;  level.chance > i; i++)
        {
            timeStamp temp = new timeStamp();

            temp.holdingTime = level.holdingTime;

            temp.isFake = false;

            temp.sprite = sprites[0];

            tempStemps.Add(temp);    
        }

        //페이크가 있으면 만듬
        if (level.fake)
        {
            for (int i = 0; level.chance/2 > i; i++)
            {
                timeStamp temp = new timeStamp();

                temp.holdingTime = level.holdingTime;

                temp.isFake = true;

                temp.sprite = sprites[Random.Range(1, sprites.Count)];

                tempStemps.Add(temp);
            }

        }

        List < timeStamp > output = new List<timeStamp>();

        output = tempStemps;

        //순서 랜덤섞기
        ShuffleList<timeStamp>(output);

        // 번호 붙이고 시간 매기기, 어떤 모니터에 나올지 결정

        int order = 0;

        int postMonitor = Random.Range(0, level.monitorNum);

        

        foreach (timeStamp i in output)
        {
            while (true)
            {
                int currMonitor = Random.Range(0, level.monitorNum);

                if (!(postMonitor == currMonitor))
                {
                    i.monitor = currMonitor;

                    postMonitor = currMonitor;

                    break;
                }
            }

            i.order = order;

            i.startTime = ((gameTime-1.0f) / (output.Count + 1)) * (order) + 1.0f;

            order++;

            
        }


        output.Sort(delegate (timeStamp a, timeStamp b) // 내림차순 정렬
        {
            if (a.order < b.order) return -1;
            else if (a.order > b.order) return 1;
            return 0;
        });

        /*
        foreach (timeStamp i in output)
        {
            Debug.Log(i.startTime + " : " + i.holdingTime + " : " + i.monitor + " : " + i.isFake + " : " + i.sprite + " : " + i.order);
        }
        */

        return output;
    }

    public static void ShuffleList<T>(List<T> list)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }

     IEnumerator gameover()
    {
        StopCoroutine(timer);

        StopCoroutine(ctrMonitor);

        foreach (Coroutine i in pops)
        {
            StopCoroutine(i);
        }

        if (success >= levelNow.successNum)
        {
            resultObj.GetComponent<Image>().sprite = successSprite;
            resultObj.SetActive(true);
        }
        else
        {
            resultObj.GetComponent<Image>().sprite = failSprite;
            resultObj.SetActive(true);
        }

        controlCancel = true;

        yield return new WaitForSeconds(2f);

        init();
    }
  
}

