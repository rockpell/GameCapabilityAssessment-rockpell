using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationScreenManager : MonoBehaviour {

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image explanationImage;
    [SerializeField] private Image whiteBox;

    [SerializeField] private Sprite[] backgroundImages;
    [SerializeField] private Sprite[] explanationImages;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip btnClickSound;

    private bool isClickButton = false;

    // Use this for initialization
    void Start () {
        SetBackgroundImage();
        if (audioSource != null)
            audioSource.loop = false;
    }

    public void NextScene()
    {
        if (!isClickButton)
        {
            if (CustomSceneManager.Instance != null)
            {
                if (NewGameManager.Instance.NowMiniGame == "")
                {
                    Debug.Log("현재 선택된 미니게임이 없습니다.");
                }
                else
                {
                    CustomSceneManager.Instance.ChangeScene(NewGameManager.Instance.NowMiniGame + "Tutorial");
                }
            }
            ButtonClickSound();
            isClickButton = true;
        }
    }

    public void SkipTutorial()
    {
        if (!isClickButton)
        {
            if (ShutterManager.Instance != null)
                ShutterManager.Instance.ShutterSequence(Result.BigSuccessful, NewGameManager.Instance.NowMiniGame, false);
            else
                Debug.Log("ShutterManager를 찾을 수 없음");

            ButtonClickSound();
            isClickButton = true;
        }
    }

    private void SetBackgroundImage()
    {
        int _index = GetIndexToMiniGame(NewGameManager.Instance.NowMiniGame);

        if (_index != -1)
        {
            backgroundImage.sprite = backgroundImages[_index];
            explanationImage.sprite = explanationImages[_index];
        }
        else // 필드에 등록 되지 않은 이미지라면 미리 지정된 폴더에서 불러온다.(여기에도 없으면 없는거)
        {
            Sprite _sprite = LoadBackgroundImage();
            if (_sprite != null)
            {
                backgroundImage.sprite = _sprite;

                explanationImage.gameObject.SetActive(false);
                whiteBox.gameObject.SetActive(false);
            }
        }
    }

    private Sprite LoadBackgroundImage()
    {
        string _name = NewGameManager.Instance.NowMiniGame;

        return Resources.Load<Sprite>("TutorialImage/" + _name + "Explanation");
    }

    private int GetIndexToMiniGame(string gameName)
    {
        int _result = -1;

        switch (gameName)
        {
            case "Chicken":
                _result = 0;
                break;
            case "CoffeeGrinder":
                _result = 1;
                break;
            case "Cooking":
                _result = 2;
                break;
            case "MyPresent":
                _result = 3;
                break; 
            case "VegetableSlayer":
                _result = 4;
                break;
            case "WhiteFlag":
                _result = 5;
                break;
            case "Convini":
                _result = 6;
                break;
            case "SomeBodyHelpMe":
                _result = 7;
                break;
            case "BoxingGame":
                _result = 8;
                break;
            case "Ticketing":
                _result = 9;
                break;
        }

        return _result;
    }

    private void ButtonClickSound()
    {
        if (audioSource != null && btnClickSound != null)
        {
            audioSource.clip = btnClickSound;
            audioSource.Play();
        }
        else
        {
            Debug.Log("audioSource null or clickSound null");
        }
    }
}
