using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChickenGameController : MonoBehaviour
{
    
    //private GameObject target;
    public GameObject mouse;
    public GameObject food;
    private int balance = 1;
    public bool TestOn = true;
    public int Test_balance = 1;
    public float dealay = 1;
    public static int Score = 100;
    public static int Combo = 0;
    public Text ScoreText;
    public Text CountText;
    public GameObject ComboText;
    public GameObject GameoverText;
    public static bool life = true;
    string Grade = "대성공!";
    public Transform UI;
    private Result result;
	private int randomNumber = 0;
	private int overlapCheck = 0;
    private bool comboTextOn = true;
    public static bool Damaging = false;
    public GameObject DamageUI;
    public GameObject panel;

    float Count = 10f;
    float SpawnposX;
	float SpawnposY;

	// Use this for initialization

	void Start()
    {
        Combo = 0;    //콤보 초기화
        balance = NewGameManager.Instance.StartGame();
        Score = 100;
        life = true;
        Level();
        if(TestOn == true)
        {
            balance = Test_balance; // 테스트용
            ChickenMouseMove.Damage = 0;  //테스트용
        }
        StartCoroutine(MouseSpwan());
    }

    public void Damaged()
    {
        if (!(TestOn))
        {
            Instantiate(DamageUI, UI);
        }
    }

    IEnumerator MouseSpwan()
    {
        while (life)
        {
			overlapCheck = randomNumber;            // 똑같은 좌표 반복 안되게 하기위해 저번 값을 기억함
			randomNumber = UnityEngine.Random.Range(0, 5);
			for (int i = 0; i < 5; i ++ )           // 다양한 위치에서 나타내기 위해 5번 반복           
			{
				if (randomNumber == overlapCheck)           // 저번 값과 같은 값이라면 랜덤함수를 다시한번 실행함.
				{
					randomNumber = UnityEngine.Random.Range(0, 3);
				}
				else            //중복되는 값이 나오지 않았다면 탈출
				{
					break;
				}
			}
			switch (randomNumber)           //랜덤 값을 받아 몹 스폰위치를 정함
			{
				case 0:
                case 1:           //왼쪽
					SpawnposX = -9.5f;
					SpawnposY = UnityEngine.Random.Range(-4.5f, 1.85f);
					break;
				case 2:
                case 3://오른쪽
					SpawnposX = 9.7f;
					SpawnposY = UnityEngine.Random.Range(-4.5f, 1.85f);
					break;
				case 4:           //밑쪽
					SpawnposX = UnityEngine.Random.Range(-8.0f, 8.0f);
					SpawnposY = -5.8f;
					break;
			}
            Vector3 Mouseposition = new Vector3(SpawnposX, SpawnposY, 0);
            Quaternion Spwanrotaion = Quaternion.Euler(new Vector3(0, 0, 0));
            Instantiate(mouse, Mouseposition, Spwanrotaion);
            yield return new WaitForSeconds(dealay);
        }
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        ScoreText.GetComponent<Text>().text = "HP : " + Score;
        CountText.GetComponent<Text>().text = "Time : " + string.Format("{0:N0}", Count);
        if(Combo >= 10)
        {
            if (Combo % 10 == 0)
            {
                comboTextOn = true;
                ComboUIOn();
            }
            else
            {
                comboTextOn = false;
            }
        }
        if (Score <= 0)
        {
            life = false;
            Grade = "실패!";
            result = Result.Fail;
            GameoverText.GetComponent<Text>().text = Grade;
            GameoverText.SetActive(true);
            panel.SetActive(true);
            NewGameManager.Instance.ClearGame(result);
        }
        if (life)
        {
            Count = Count - Time.deltaTime;     //원래 흘러야하는 시간
            if (TestOn == true)
            {
                Count = 10; //테스트를 위한 설정
            }
        }
        if (Count <= 0)
        {
            life = false;
            if (Score >= 80)
            {
                Grade = "대성공!";
                result = Result.BigSuccessful;
                
            }
            if (Score > 0 && Score < 80)
            {
                Grade = "성공!";
                result = Result.Successful;
            }
            if (Score <= 0)
            {
                Grade = "실패!";
                result = Result.Fail;
            }

            GameoverText.GetComponent<Text>().text = Grade;
            GameoverText.SetActive(true);
            panel.SetActive(true);
            NewGameManager.Instance.ClearGame(result);
        }
    }


    void Level()
    {
        switch (balance)
        {
            case 1:
                dealay = 0.5f;
                ChickenMouseMove.Speed = 3;
                break;
            case 2:
                dealay = 0.5f;
                ChickenMouseMove.Speed = 3.5f;
                break;
            case 3:
                dealay = 0.45f;
                ChickenMouseMove.Speed = 4.5f;
                break;
            case 4:
                dealay = 0.4f;
                ChickenMouseMove.Speed = 4.5f;
                break;
            case 5:
                dealay = 0.35f;
                ChickenMouseMove.Speed = 5;
                ChickenMouseMove.Damage = 15;
                break;
            case 6:
                dealay = 0.3f;
                ChickenMouseMove.Speed = 5.5f;
                ChickenMouseMove.Damage = 15;
                break;
            case 7:
                dealay = 0.3f;
                ChickenMouseMove.Speed = 6;
                ChickenMouseMove.Damage = 15;
                break;
            case 8:
                dealay = 0.27f;
                ChickenMouseMove.Speed = 6;
                ChickenMouseMove.Damage = 15;
                break;
            case 9:
                dealay = 0.25f;
                ChickenMouseMove.Speed = 6;
                ChickenMouseMove.Damage = 15;
                break;
            case 10:
                dealay = 0.2f;
                ChickenMouseMove.Speed = 6;
                ChickenMouseMove.Damage = 15;
                break;
            case 11:
                dealay = 0.15f;
                ChickenMouseMove.Speed = 6;
                ChickenMouseMove.Damage = 20;
                break;
            case 12:
                dealay = 0.15f;
                ChickenMouseMove.Speed = 7;
                ChickenMouseMove.Damage = 20;
                break;
            case 13:
                dealay = 0.1f;
                ChickenMouseMove.Speed = 7;
                ChickenMouseMove.Damage = 20;
                break;
            case 14:
                dealay = 0.1f;
                ChickenMouseMove.Speed = 7.5f;
                ChickenMouseMove.Damage = 20;
                break;
            case 15:
                dealay = 0.1f;
                ChickenMouseMove.Speed = 8;
                ChickenMouseMove.Damage = 20;
                break;
        }
    }

    private IEnumerator ComboUI()
    {
        yield return new WaitForSeconds(5);
        if(comboTextOn == false)
        {
            ComboText.SetActive(false);
        }
    }

    public void ComboUIOn()
    {
        ComboText.GetComponent<Text>().text = Combo + " COMBO!!";
        ComboText.SetActive(true);
        StartCoroutine(ComboUI());
    }


    public void Showcombo()
    {
        Debug.Log(Combo);
    }
}
