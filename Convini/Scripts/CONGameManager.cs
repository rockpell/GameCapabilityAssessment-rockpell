using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONGameManager : MonoBehaviour
{
    public CONLevelOption levelNow { get; set; }

    public List<CONMenu> shoppingList { get; set; }

    [SerializeField]
    private GameObject mask;

    public float remainTime { get; set; }

    //플레이어의 답
    public int playersAnswer { get; set; }

    //정답
    public int rightAnswer { get; set; }

    //손님이 준 돈
    public int givenMoney { get; set; }

    //현제 패드에 있는 숫자
    public string padNum { get; set; }

    Coroutine timer { get; set; }
    public GameObject remainTimeObj { get; set; }

    public GameObject givenMoneyObj { get; set; }

    //public List<GameObject> menuList{ get; set; }

    public List<GameObject> ledPanel { get; set; }

    [SerializeField]
    private GameObject resultObj;

    public void Start()
    {
        if(NewGameManager.Instance != null)
        {
            levelNow = selectLevel( NewGameManager.Instance.StartGame());
        }
        else
        {
            levelNow = selectLevel(7);
        }
        
        init();
        
    }

    private CONLevelOption selectLevel(int input)
    {
        switch (input)
        {
            case 1:
                return new CONLevelOption(1, 1, 1000, 1, 10f);

            case 2:
                return new CONLevelOption(2, 1, 1000, 1, 9f);

            case 3:
                return new CONLevelOption(3, 2, 1000, 1, 10f);

            case 4:
                return new CONLevelOption(4, 2, 1000, 1, 9f);

            case 5:
                return new CONLevelOption(5, 2, 1000, 2, 10f);

            case 6:
                return new CONLevelOption(6, 2, 1000, 2, 9f);

            case 7:
                return new CONLevelOption(7, 2, 500, 2, 10f);

            case 8:
                return new CONLevelOption(8, 2, 500, 2, 9f);

            case 9:
                return new CONLevelOption(9, 3, 500, 2, 10f);

            case 10:
                return new CONLevelOption(10, 3, 500, 2, 9f);

            case 11:
                return new CONLevelOption(11, 4, 500, 2, 10f);

            case 12:
                return new CONLevelOption(12, 4, 500, 2, 9f);

            case 13:
                return new CONLevelOption(13, 4, 100, 3, 10f);

            case 14:
                return new CONLevelOption(14, 4, 100, 3, 9f);

            case 15:
                return new CONLevelOption(15, 4, 100, 3, 8f);

            default:
                return new CONLevelOption(15, 4, 100, 3, 8f);
        }
    }

    private void init()
    {
        shoppingList = new List<CONMenu>();

        mask.transform.localPosition = new Vector3(0, 0, 0);

        remainTime = levelNow.timeLimit;

        //매뉴 가격의 합
        int temp = 0;

        string tempString = "";

        GameObject menu;

        //나올 매뉴 랜덤 설정
        for (int i = 0; levelNow.listCap > i; i++)
        {
            CONMenu tMenu = new CONMenu(levelNow);

            shoppingList.Add(tMenu);

            temp += tMenu.price * tMenu.number;

            menu = GameObject.Find( "Menu " + (i+1) );

            menu.SetActive( true );

            tempString = "Icons/" + tMenu.name;

            menu.transform.GetChild( 0 ).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + tMenu.name, typeof( Sprite ) ) as Sprite;

            menu.transform.GetChild( 1 ).gameObject.GetComponent<TextMesh>().text = tMenu.price.ToString() + '\\';

            menu.transform.GetChild( 2 ).gameObject.GetComponent<TextMesh>().text = "X " + tMenu.number.ToString();

          
        }

        //가격의 총계에 따라 손님이 준돈 설정

        givenMoneyObj = GameObject.Find("GivenMoney");

        if (temp < 5000)
        {
            givenMoney = 5000;

            givenMoneyObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/5000won", typeof(Sprite)) as Sprite;
        }
        else if (5000 <= temp && temp < 10000)
        {
            givenMoney = 10000;

            givenMoneyObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/10000won", typeof( Sprite ) ) as Sprite;
        }
        else
        {
            givenMoney = 50000;

            givenMoneyObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/50000won", typeof( Sprite ) ) as Sprite;
        }

        //정답
        rightAnswer = givenMoney - temp;

        Debug.Log(rightAnswer);


        //시계
        remainTimeObj = GameObject.Find("TimeText");

        //led패널 초기화
        ledPanel = new List<GameObject>();

        padNum = "00000";

        for(int i = 0; 5 > i; i++ )
        {
            ledPanel.Add(GameObject.Find("NumPad " + i));
        }

        resetNumPad();

        resultObj = GameObject.Find("Result");

        timer = StartCoroutine( timeCheck() );
    }

    IEnumerator timeCheck()
    {
        Debug.Log(remainTime);

        
        while (remainTime >= 0)
        {
            remainTime -= Time.deltaTime;

            if (remainTime >= 0)
            {
                remainTimeObj.GetComponent<TextMesh>().text = remainTime.ToString().Substring(0, 3);

                //시간에 따라 얼굴이 빨게지게 함
                mask.transform.localPosition = new Vector3(0, (float)(3.7 - (3.7 * remainTime / levelNow.timeLimit)), 0);
            }
            else
            {
                remainTimeObj.GetComponent<TextMesh>().text ="END";
            }
            yield return null;
        }

        

        Debug.Log("end");

        gameover(CONResult.BigFail);
    }

    public void keypad0()
    {
        padNum = padNum.Substring(1,4) + 0;
        resetNumPad();
    }
    public void keypad1()
    {
        padNum = padNum.Substring( 1, 4 ) + 1;
        resetNumPad();
    }
    public void keypad2()
    {
        padNum = padNum.Substring( 1, 4 ) + 2;
        resetNumPad();
    }
    public void keypad3()
    {
        padNum = padNum.Substring( 1, 4 ) + 3;
        resetNumPad();
    }
    public void keypad4()
    {
        padNum = padNum.Substring( 1, 4 ) + 4;
        resetNumPad();
    }
    public void keypad5()
    {
        padNum = padNum.Substring( 1, 4 ) + 5;
        resetNumPad();
    }
    public void keypad6()
    {
        padNum = padNum.Substring( 1, 4 ) + 6;
        resetNumPad();
    }
    public void keypad7()
    {
        padNum = padNum.Substring( 1, 4 ) + 7;
        resetNumPad();
    }
    public void keypad8()
    {
        padNum = padNum.Substring( 1, 4 ) + 8;
        resetNumPad();
    }
    public void keypad9()
    {
        padNum = padNum.Substring( 1, 4 ) + 9;
        resetNumPad();
    }
    public void keypadB()
    {
        padNum = 0 + padNum.Substring( 0, 4 );
        resetNumPad();
    }
    public void keypadClr()
    {
        padNum = "00000";
        resetNumPad();
    }
    public void keypadSubmit()
    {
        submitAnswer( int.Parse( padNum ) );
    }

    void resetNumPad()
    {
        //Debug.Log(padNum);

        for( int i = 0; 5 > i; i++ )
        {
            switch( padNum[padNum.Length-i-1] )
            {
                case '0':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 0, typeof( Sprite ) ) as Sprite;

                    break;

                case '1':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 1, typeof( Sprite ) ) as Sprite;

                    break;

                case '2':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 2, typeof( Sprite ) ) as Sprite;

                    break;

                case '3':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 3, typeof( Sprite ) ) as Sprite;

                    break;

                case '4':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 4, typeof( Sprite ) ) as Sprite;

                    break;

                case '5':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 5, typeof( Sprite ) ) as Sprite;

                    break;

                case '6':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 6, typeof( Sprite ) ) as Sprite;

                    break;

                case '7':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 7, typeof( Sprite ) ) as Sprite;

                    break;

                case '8':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 8, typeof( Sprite ) ) as Sprite;

                    break;

                case '9':

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + 9, typeof( Sprite ) ) as Sprite;

                    break;

                default:

                    ledPanel[ i ].GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/BlankLED", typeof( Sprite ) ) as Sprite;

                    break;

            }
        }
    }

    //
    void gameover(CONResult result)
    {
        StopCoroutine(timer);

        switch (result)
        {
            case CONResult.BigSuccess:

                resultObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + "Correct", typeof(Sprite) ) as Sprite;
                NewGameManager.Instance.ClearGame(Result.BigSuccessful);
                break;

            case CONResult.Success:

                resultObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + "Correct", typeof( Sprite ) ) as Sprite;
                NewGameManager.Instance.ClearGame(Result.Successful);
                break;

            case CONResult.BigFail:

                resultObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + "Incorrect", typeof( Sprite ) ) as Sprite;
                NewGameManager.Instance.ClearGame( Result.Fail );
                break;

            case CONResult.Fail:

                resultObj.GetComponent<SpriteRenderer>().sprite = Resources.Load( "CONImage/" + "Incorrect", typeof( Sprite ) ) as Sprite;
                NewGameManager.Instance.ClearGame(Result.Fail );
                break;

        }

        Debug.Log(result);
    }

    void submitAnswer(int answer)
    {
        //맞음
        if (answer == rightAnswer)
        {
            // 맞고 재한시간 1/2안
            if (remainTime > levelNow.timeLimit/2)
            {
                gameover( CONResult.BigSuccess);
            }
            else
            {
                gameover( CONResult.Success );
            }
        }
        //틀림
        else
        {
            gameover( CONResult.Fail);
        }
    }
}

public class CONLevelOption
{
    public int level { get; set; }

    public int listCap { get; set; }

    public int moneyRange { get; set; }

    public int prodNum { get; set; }

    public float timeLimit { get; set; }

    public CONLevelOption(int level, int listCap, int moneyRange, int prodNum, float timeLimit)
    {
        this.level = level;
        this.listCap = listCap;
        this.moneyRange = moneyRange;
        this.prodNum = prodNum;
        this.timeLimit = timeLimit;
    }
}


public enum CONProduct
{
    Bread, Can, CandyBar, FistRice, Juice, Lollipop, Pizza, Snack, Tuna, Water
}

public enum CONResult
{
    BigFail, Fail, Success, BigSuccess
}