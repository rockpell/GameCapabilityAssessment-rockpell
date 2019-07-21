using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookUIManager : MonoBehaviour {
    public static CookUIManager instance;
    [SerializeField] private Toggle[] ingredientToggles;
    [SerializeField] private Text tutorialClearText;
    [SerializeField] private Image barImage;
    [SerializeField] private CookLevelSet levelSet;
    public NewTutorialManager tutorialCanvas;
    public float currentTime = 20;
    private List<string> selectFoodAndIngredients;
    
    public Text timeText;
    public Text answerText;
    public bool isTutorial;
    private int currentingredientNum = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        RandomIngredientsPos();
    }
    void Update () {
        if (barImage.fillAmount == 0) NewGameManager.Instance.ClearGame(Result.Fail);
        else
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timeText.text = currentTime.ToString("N1") + "초";
            }
            else if (currentTime <= 0)
            {
                ResultGame();
            }
        }
    }
    void ResultGame()
    {
        if (!isTutorial)
        {
            if (barImage.fillAmount >= 0.79f) NewGameManager.Instance.ClearGame(Result.BigSuccessful);
            else if (barImage.fillAmount < 0.39f) NewGameManager.Instance.ClearGame(Result.Fail);
            else NewGameManager.Instance.ClearGame(Result.Successful);
        }
        else
        {
            tutorialClearText.gameObject.SetActive(true);
            if (barImage.fillAmount < 0.39f) tutorialClearText.text = "게임 실패";
            else tutorialClearText.text = "게임 성공";
            tutorialCanvas.EndTutorial();
        }
    }

    public void ReseetToggle()
    {
        for (int i = 0; i < ingredientToggles.Length; i++)
        {
            ingredientToggles[i].isOn = false;
        }
    }
    public void IncreaseSelectNum(int num)
    {
        if (CookGameManager.instance.selectingredientNum < CookGameManager.instance.currentingredientNum && ingredientToggles[num].isOn)
        {
            ingredientToggles[num].targetGraphic.color = Color.yellow;
            CookGameManager.instance.selectingredientNum++;
        }
        else if (!ingredientToggles[num].isOn && CookGameManager.instance.selectingredientNum > 0)
        {
            ingredientToggles[num].targetGraphic.color = Color.white;
            CookGameManager.instance.selectingredientNum--;
        }
    }
    public List<string> AddselectFoodAndIngredients()
    {
        selectFoodAndIngredients = new List<string>();
        for (int i = 0; i < ingredientToggles.Length; i++)
        {
            if(ingredientToggles[i].isOn)
                selectFoodAndIngredients.Add(ingredientToggles[i].name);
        }
        return selectFoodAndIngredients;
    }
    public void SetBarImage(float value)
    {
        if (levelSet.GetGameLevel() == 1) barImage.fillAmount += value * 2;
        else barImage.fillAmount += value;
        if (barImage.fillAmount >= 0.79f) barImage.color = Color.green;
        else if (barImage.fillAmount < 0.39f) barImage.color = Color.red;
        else barImage.color = Color.yellow;
        
    }
    private void RandomIngredientsPos()
    {
        Vector3[] trans = new Vector3[12];
        List<int> indexList = new List<int>();
        for (int i = 0; i < ingredientToggles.Length; i++)
        {
            trans[i] = ingredientToggles[i].transform.position;
        }
        while(true)
        {
            int random = Random.Range(0, 12);
            if (indexList.Count >= 12) break;
            if (!indexList.Contains(random))
            {
                indexList.Add(random);
            }
        }
        for (int i = 0; i < ingredientToggles.Length; i++)
        {
            ingredientToggles[i].transform.position = new Vector3(trans[indexList[i]].x, trans[indexList[i]].y, trans[indexList[i]].z);
        }
    }
}
