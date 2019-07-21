using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketingGameManager : MonoBehaviour
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
            levelNow = selectLevel(10);
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

        gameover();
    }

    private TKTLevel selectLevel(int input)
    {
        switch (input)
        {
            case 1:
                return new TKTLevel(1);

            case 2:
                return new TKTLevel(2);

            case 3:
                return new TKTLevel(3);

            case 4:
                return new TKTLevel(4);

            case 5:
                return new TKTLevel(5);

            case 6:
                return new TKTLevel(6);

            case 7:
                return new TKTLevel(7);

            case 8:
                return new TKTLevel(8);

            case 9:
                return new TKTLevel(9);

            case 10:
                return new TKTLevel(10);

            case 11:
                return new TKTLevel(11);

            case 12:
                return new TKTLevel(12);

            case 13:
                return new TKTLevel(13);

            case 14:
                return new TKTLevel(14);

            case 15:
                return new TKTLevel(15);

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
                gameover();
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

    private void gameover()
    {
        StopCoroutine(timer);

        StopCoroutine(ctrMonitor);

        foreach (Coroutine i in pops)
        {
            StopCoroutine(i);
        }

        controlCancel = true;

        if(success >= levelNow.monitorNum) //대성공
        {
            Debug.Log("게임결과 : 대성공");

            resultObj.GetComponent<Image>().sprite = successSprite;
            resultObj.SetActive(true);

            NewGameManager.Instance.ClearGame(Result.BigSuccessful);
        }

        else if (success >= levelNow.successNum) // 성공
        {
            resultObj.GetComponent<Image>().sprite = successSprite;
            resultObj.SetActive(true);

            Debug.Log("게임결과 : 성공");
            NewGameManager.Instance.ClearGame(Result.Successful);
        }

        else // 실패
        {

            resultObj.GetComponent<Image>().sprite = failSprite;
            resultObj.SetActive(true);

            Debug.Log("게임결과 : 실패");
            NewGameManager.Instance.ClearGame(Result.Fail);
        }
    }

    
}

public class TKTLevel
{
    public int level { get; set; }

    public int monitorNum { get; set; } // 모니터 수 == 예매 해야하는 수 == 이 수 이상이면 대성공

    public int successNum { get; set; } // 이 수 이상이면 성공

    public float holdingTime { get; set; } // 답이 나타나는 시간

    public bool fake { get; set; } // 페이크 들어가는지

    public int touchNum { get; set; } // 터치 가능 횟수

    public int chance { get; set; } // 총 나타나는 예 매! 수

    public TKTLevel(int input)
    {
        level = input;

        switch (input)
        {
            case 1:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 1.0f;

                    fake = false;

                    touchNum = 8;

                    chance = 10;

                    break;
                }

            case 2:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 1.0f;

                    fake = false;

                    touchNum = 6;

                    chance = 10;

                    break;
                }

            case 3:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 1.0f;

                    fake = false;

                    touchNum = 4;

                    chance = 10;

                    break;
                }

            case 4:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 0.8f;

                    fake = false;

                    touchNum = 6;

                    chance = 8;

                    break;
                }

            case 5:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 0.8f;

                    fake = false;

                    touchNum = 4;

                    chance = 8;

                    break;
                }

            case 6:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 0.8f;

                    fake = false;

                    touchNum = 3;

                    chance = 6;

                    break;
                }
            case 7:
                {
                    monitorNum = 3;

                    successNum = monitorNum - 1;

                    holdingTime = 0.5f;

                    fake = false;

                    touchNum = 6;

                    chance = 8;

                    break;
                }

            case 8:
                {
                    monitorNum = 3;

                    successNum = monitorNum - 1;

                    holdingTime = 0.4f;

                    fake = true;

                    touchNum = 5;

                    chance = 8;

                    break;
                }

            case 9:
                {
                    monitorNum = 3;

                    successNum = monitorNum - 1;

                    holdingTime = 0.35f;

                    fake = true;

                    touchNum = 5;

                    chance = 8;

                    break;
                }

            case 10:
                {
                    monitorNum = 3;

                    successNum = monitorNum - 1;

                    holdingTime = 0.3f;

                    fake = true;

                    touchNum = 5;

                    chance = 7;

                    break;
                }

            case 11:
                {
                    monitorNum = 4;

                    successNum = monitorNum - 1;

                    holdingTime = 0.25f;

                    fake = true;

                    touchNum = 6;

                    chance = 8;

                    break;
                }

            case 12:
                {
                    monitorNum = 4;

                    successNum = monitorNum - 1;

                    holdingTime = 0.23f;

                    fake = true;

                    touchNum = 5;

                    chance = 7;

                    break;
                }

            case 13:
                {
                    monitorNum = 4;

                    successNum = monitorNum - 1;

                    holdingTime = 0.23f;

                    fake = true;

                    touchNum = 4;

                    chance = 8;

                    break;
                }

            case 14:
                {
                    monitorNum = 4;

                    successNum = monitorNum - 1;

                    holdingTime = 0.2f;

                    fake = true;

                    touchNum = 4;

                    chance = 6;

                    break;
                }

            case 15:
                {
                    monitorNum = 4;

                    successNum = monitorNum - 1;

                    holdingTime = 0.2f;

                    fake = true;

                    touchNum = 4;

                    chance = 4;

                    break;
                }

            default:
                {
                    monitorNum = 2;

                    successNum = monitorNum - 1;

                    holdingTime = 2f;

                    fake = false;

                    touchNum = 8;

                    chance = 10;

                    break;
                }
        }

    }
}

public class timeStamp
{
    public float startTime { get; set; } // 시작시간

    public float holdingTime { get; set; } // 지속시간
    
    public int monitor { get; set; } // 어느모니터에 나타나는지

    public bool isFake { get; set; } // 진짜인지 가까인지

    public Sprite sprite { get; set; } // 모양

    public int order { get; set; }// 순번

    public timeStamp()
    {

    }
}

public enum TKTResult
{
    Fail, Success, BigSuccess
}