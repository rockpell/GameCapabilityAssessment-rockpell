using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookLevelSet : MonoBehaviour {
    private int ingredientNum = 0;
    private int gameLevel = 0;      //게임매니저의 레벨
    [SerializeField]private float paperSpeed = 0;
    private float createRecipeTime = 0;
    [SerializeField] private NewTutorialManager tutorialCanvas;
    public bool isTutorial;
    private void Awake () {
        gameLevel = NewGameManager.Instance.StartGame();
        if (tutorialCanvas != null)
        {
            isTutorial = true;
        }
        else
        {
            isTutorial = false;
        }
        LevelSet();
        CookUIManager.instance.isTutorial = isTutorial;
        CookUIManager.instance.tutorialCanvas = tutorialCanvas;
    }

    void LevelSet()
    {
        if (isTutorial)
        {
            ingredientNum = 1;
            paperSpeed = 3.5f;
            createRecipeTime = 2.01f;
        }
        else
        {
            if(gameLevel < 3) ingredientNum = 1;
            else if(gameLevel > 12) ingredientNum = 3;
            else ingredientNum = 2;
            paperSpeed = 3.5f + (gameLevel - 1) * 0.6f;
            createRecipeTime = 2 - (gameLevel - 1) * 0.1f;
            if (gameLevel > 9 && gameLevel < 13)
            {
                paperSpeed -= 3f;
                createRecipeTime -= 0.05f;
            }
            if (gameLevel > 12)
            {
                paperSpeed -= 4.5f;
                createRecipeTime += (0.6f - (gameLevel - 13) * 0.025f);
            }
        }
        
    }
    public int GetGameLevel()
    {
        return gameLevel;
    }
    public int GetIngredientNum()
    {
        return ingredientNum;
    }
    public float GetPaperSpeed()
    {
        return paperSpeed;
    }
    public float GetCreateRecipeTime()
    {
        return createRecipeTime;
    }
    public NewTutorialManager GetTutorialCanvas()
    {
        return tutorialCanvas;
    }
}
