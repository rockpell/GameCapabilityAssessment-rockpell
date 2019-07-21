using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONTutorialManager : MonoBehaviour
{
    public CONLevelOption levelNow { get; set; }

    public List<CONMenu> shoppingList { get; set; }

    //플레이어의 답
    public int playersAnswer { get; set; }

    //정답
    public int rightAnswer { get; set; }

    //손님이 준 돈
    public int givenMoney { get; set; }

    //현제 패드에 있는 숫자
    public string padNum { get; set; }

    public GameObject givenMoneyObj { get; set; }

    //public List<GameObject> menuList{ get; set; }

    public List<GameObject> ledPanel { get; set; }

    [SerializeField]
    private GameObject resultObj;

    public void Start()
    {
        init();       
    }

    private CONLevelOption selectLevel(int input)
    {
        switch (input)
        {
            case 1:
                return new CONLevelOption( 1, 1, 1000, 1, 10f);

            default:
                return new CONLevelOption( 1, 1, 1000, 1, 10f );
        }
    }

    private void init()
    {
        resultObj.GetComponent<SpriteRenderer>().sprite = null;

        if ( GameManager.Instance != null )
        {
            levelNow = selectLevel( GameManager.Instance.StartGame() );
        }
        else
        {
            levelNow = selectLevel( 1 );
        }


        shoppingList = new List<CONMenu>();

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

        //led패널 초기화
        ledPanel = new List<GameObject>();

        padNum = "00000";

        for(int i = 0; 5 > i; i++ )
        {
            ledPanel.Add(GameObject.Find("NumPad " + i));
        }

        resetNumPad();

        resultObj = GameObject.Find("Result");  
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
        StartCoroutine( submitAnswer( int.Parse( padNum ) ));
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

    IEnumerator submitAnswer( int answer )
    {
        if (answer == rightAnswer)
        {
            resultObj.GetComponent<SpriteRenderer>().sprite = Resources.Load("CONImage/" + "Correct", typeof(Sprite)) as Sprite;
        }
        else
        {
            resultObj.GetComponent<SpriteRenderer>().sprite = Resources.Load("CONImage/" + "Incorrect", typeof(Sprite)) as Sprite;
        }

        yield return new WaitForSeconds(2);

        init();

        yield return null;
    }

    IEnumerator wait(float input)
    {
        yield return new WaitForSeconds(input);
    }
}
