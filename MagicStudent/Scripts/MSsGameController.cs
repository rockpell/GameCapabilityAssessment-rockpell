using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSsGameController : MonoBehaviour
{
    public int level;

    [SerializeField]
    private Sprite bgImage;

    [SerializeField]
    private GameObject result_AA, result_A, result_C, result_F;

    [SerializeField]
    private GameObject puzzleField;

    [SerializeField]
    private GameObject text;

    //[SerializeField] private GameObject countText;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;
    private bool isGameOver = false;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;
    public int hint;
    public int numberOfCard;
    public int score; //점수
    public int wrongChoise;
    float timer = 0;
    int aa, a, c, f;

    private void Awake() //시작할 떄 한 번 돌아가는 거 start보다 먼저
    {
        level = NewGameManager.Instance.StartGame();
        Level(level);

        for (int i = 0; i < puzzles.Length; i++)
        {
            Sprite temp = puzzles[i];
            int randomIndex = Random.Range(i, puzzles.Length); //카드 만들떄부터 렌덤으로 배치
            puzzles[i] = puzzles[randomIndex];
            puzzles[randomIndex] = temp;
        }
    }

    private void Start()
    {
        score = 0;
        wrongChoise = 0;
        timer = 0;

        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        ShowCard();
        gameGuesses = gamePuzzles.Count / 2;
    }

    private void Update()
    {
        ShowUi();
    }

    private void ShowUi()
    {
        text.GetComponent<Text>().text = "Chance : " + (hint - wrongChoise);
    }

    void ShowCard()
    {
        OpenCard();
        //StartCoroutine(DownCountText(countText, 3, 1, 1));
        Invoke("NoCard", 3f);   //안녕 나는 함수를 실행시켜주는 함수야~
    }

    void NoCard()   //안보이는 카드
    {
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].image.sprite = bgImage;
            btns[i].interactable = true;
        }
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>()); //버튼 새로 만들기
            btns[i].image.sprite = bgImage;

        }
    }

    void AddListeners()    //클릭했을 때 할 일을 등록해준다
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;    //리스트의 개수(크기)
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)    //4개 두개씩 배치
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);

            index++;
        }
    }

    public void PickAPuzzle()
    {
        int _guessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);   //이름을 int로 변환
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        btns[_guessIndex].GetComponent<MSButtonControl>().StopAllCoroutines();

        if (!firstGuess) //여기서 카드 두 장이 같은지 다른지 확인함
        {
            //btns[firstGuessIndex].GetComponent<MSButtonControl>().StopWrongShowCard(); // 틀리면 카드 잠시 보여주는 코루틴 멈추기
            //Debug.Log("처음꺼 열었음");
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);   //이름을 int로 변환
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[firstGuessIndex].interactable = false;     //처음 열었던 카드는 다시 열 수 없음
        }
        else if (!secondGuess)
        {
            //btns[secondGuessIndex].GetComponent<MSButtonControl>().StopWrongShowCard(); // 틀리면 카드 잠시 보여주는 코루틴 멈추기
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);   //이름을 int로 변환
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            btns[firstGuessIndex].interactable = true;     //처음 열었던 카드 다시 터치 가능
            countGuesses++;

            if (firstGuessPuzzle == secondGuessPuzzle)
                score++;
            else
                wrongChoise++;

            StartCoroutine(CheckIfThePuzzlesMatch());
        }

    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        //yield return new WaitForSeconds(0);

        if (firstGuessPuzzle == secondGuessPuzzle /*&& firstGuessIndex != secondGuessIndex*/)   //두장의 카드가 같으면
        {
            firstGuess = secondGuess = false;   //다시 false로
            CheckIfTheGameIsFinished();

            btns[firstGuessIndex].GetComponent<MSButtonControl>().StartShowCard(1);
            btns[secondGuessIndex].GetComponent<MSButtonControl>().StartShowCard(1);

            //yield return new WaitForSeconds(5f); // 0.2f
            //btns[firstGuessIndex].interactable = false;     //열린 카드 터치 불가
            //btns[secondGuessIndex].interactable = false;
            //btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            //btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            firstGuess = secondGuess = false;   //다시 false로

            btns[firstGuessIndex].GetComponent<MSButtonControl>().StartWrongShowCard(bgImage, 0.25f);
            btns[secondGuessIndex].GetComponent<MSButtonControl>().StartWrongShowCard(bgImage, 0.25f);

            //yield return new WaitForSeconds(0.5f);  //틀렸을 떄 보여주고 닫음
            //btns[firstGuessIndex].image.sprite = bgImage;       //카드가 같지 않으면 카드 뒷면으로 바꾸기
            //btns[secondGuessIndex].image.sprite = bgImage;

            if (hint == wrongChoise)
            {
                StopAllButtonCoroutine(btns);
                Debug.Log("GameOver");
                GameOver();
            }
        }

        //firstGuess = secondGuess = false;   //다시 false로
        yield return null;
    }

    void CheckIfTheGameIsFinished() //이건 전부 다 끝났을 때... 이부분 지저분한 코드.. 언젠가 고치기
    {
        countCorrectGuesses++;  //카드 총 개수(4쌍)

        if (countCorrectGuesses == gameGuesses)
        {
            StopAllButtonCoroutine(btns);
            CloseCard();
            CheakScore();       //점수확인
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count); //0부터 list 개수를 랜덤으로 생성~!
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void GameOver()
    {
        OpenCard();
        CheakScore();       //점수확인
    }

    public void OpenCard()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].image.sprite = gamePuzzles[i];
            btns[i].interactable = false;
            ColorBlock bColors = btns[i].colors;
            bColors.disabledColor = new Color(1, 1, 1, 1);
            btns[i].colors = bColors;
        }
    }

    private void CloseCard()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].image.sprite = gamePuzzles[i];
            btns[i].interactable = false;
            ColorBlock bColors = btns[i].colors;
            bColors.disabledColor = new Color(0, 0, 0, 0);
            btns[i].colors = bColors;
        }
    }

    public void CheakScore()
    {
        int leftChance = hint - wrongChoise;

        if (score >= aa && leftChance == hint)        //대성공
            GreatSuccess();

        else if (score >= a && leftChance != hint)    //성공
            Success();

        else if (score >= c)    //실패
            Fail();

        else                    //대실패
            GreatFail();
    }
    public void GreatSuccess()
    {
        result_AA.SetActive(true);
        NewGameManager.Instance.ClearGame(Result.BigSuccessful);
    }
    public void Success()
    {
        result_A.SetActive(true);
        NewGameManager.Instance.ClearGame(Result.Successful);
    }
    public void Fail()
    {
        result_C.SetActive(true);
        NewGameManager.Instance.ClearGame(Result.Fail);
    }
    public void GreatFail()
    {
        result_F.SetActive(true);
        NewGameManager.Instance.ClearGame(Result.Fail);
    }


    public void Level(int level)
    {
        switch (level)
        {
            case 1:
                numberOfCard = 4;
                hint = 2;
                aa = 2;
                a = 2;
                c = 1;
                break;
            case 2:
                numberOfCard = 4;
                hint = 1;
                aa = 2;
                a = 2;
                c = 1;
                break;
            case 3:
                numberOfCard = 6;
                hint = 2;
                aa = 3;
                a = 3;
                c = 2;
                break;
            case 4:
                numberOfCard = 6;
                hint = 1;
                aa = 3;
                a = 3;
                c = 2;
                break;
            case 5:
                numberOfCard = 8;
                hint = 2;
                aa = 4;
                a = 3;
                c = 2;
                break;
            case 6:
                numberOfCard = 8;
                hint = 1;
                aa = 4;
                a = 3;
                c = 2;
                break;
            case 7:
                numberOfCard = 12;
                hint = 3;
                aa = 6;
                a = 4;
                c = 2;
                break;
            case 8:
                numberOfCard = 12;
                hint = 2;
                aa = 6;
                a = 4;
                c = 2;
                break;
            case 9:
                numberOfCard = 12;
                hint = 1;
                aa = 6;
                a = 4;
                c = 2;
                break;
            case 10:
                numberOfCard = 16;
                hint = 3;
                aa = 8;
                a = 6;
                c = 3;
                break;
            case 11:
                numberOfCard = 16;
                hint = 2;
                aa = 8;
                a = 6;
                c = 3;
                break;
            case 12:
                numberOfCard = 16;
                hint = 1;
                aa = 8;
                a = 6;
                c = 3;
                break;
            case 13:
                numberOfCard = 20;
                hint = 3;
                aa = 10;
                a = 7;
                c = 4;
                break;
            case 14:
                numberOfCard = 20;
                hint = 2;
                aa = 10;
                a = 7;
                c = 4;
                break;
            case 15:
                numberOfCard = 20;
                hint = 1;
                aa = 10;
                a = 7;
                c = 4;
                break;
        }
        SetShape();
    }

    public void SetShape()
    {
        switch (numberOfCard)    //카드 개수가 이럴때는 모양을 바꿔줘야한다
        {
            case 6:
                puzzleField.GetComponent<RectTransform>().localScale = new Vector3(1.22f, 1.11f, 1);
                puzzleField.GetComponent<GridLayoutGroup>().constraintCount = 3;
                puzzleField.GetComponent<GridLayoutGroup>().padding.left = 119;
                break;
            case 12:
                puzzleField.GetComponent<RectTransform>().localScale = new Vector3(0.96f, 0.85f, 1);
                break;
            case 16:
                puzzleField.GetComponent<RectTransform>().localScale = new Vector3(0.76f, 0.63f, 1);
                break;
            case 20:
                puzzleField.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.63f, 1);
                puzzleField.GetComponent<GridLayoutGroup>().constraintCount = 5;
                puzzleField.GetComponent<GridLayoutGroup>().padding.left = -60;
                break;
        }
    }

    private void StopAllButtonCoroutine(List<Button> buttons)
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            StopButtonCorutine(buttons, i);
        }
    }

    private void StopButtonCorutine(List<Button> buttons, int index)
    {
        buttons[index].GetComponent<MSButtonControl>().StopAllCoroutines();
    }

    private IEnumerator DownCountText(GameObject textObject, int startValue, int endValue, float delayTime)
    {
        if (startValue > endValue)
        {
            Text _textObject = textObject.GetComponent<Text>();
            if (_textObject != null)
            {
                textObject.SetActive(true);
                while (endValue < startValue + 1)
                {
                    _textObject.text = startValue.ToString();
                    yield return new WaitForSeconds(delayTime);
                    startValue -= 1;
                }
                textObject.SetActive(false);
            }
        }
    } 
}
