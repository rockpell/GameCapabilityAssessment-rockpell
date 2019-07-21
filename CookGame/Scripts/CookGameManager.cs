using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food
{
    public List<GameObject> ingredientObject;
    public GameObject foodObject;
    public bool isSelect = false;
}

public class CookGameManager : MonoBehaviour {
    public static CookGameManager instance;
    [SerializeField] Transform startPos;
    [SerializeField] private GameObject[] randomFood;
    [SerializeField] private GameObject[] ingredients;
    [SerializeField] private Food foods;
    [SerializeField] private GameObject paper;
    [SerializeField] private AudioSource decision;
    [SerializeField] private CookLevelSet levelSet;
    private List<GameObject> checkRecipeList;
    private GameObject papertmp;
    private int randomIngredient;                    //랜덤 재료 선택
    public int currentingredientNum;
    private List<string> checkFoodAndIngredients;
    public int selectingredientNum = 0;             //현재 선택한 재료 갯수
    private int deleteNumBuffer = 0;                    //삭제된 레시피 인덱스를 저장할 버퍼
    private float createRecipeTime;
    private bool isIncreaseAnswerNum = false;
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
    }
    void Start () {
        currentingredientNum = levelSet.GetIngredientNum();
        createRecipeTime = levelSet.GetCreateRecipeTime();
        checkRecipeList = new List<GameObject>();
        StartCoroutine(MakeRecipe());
    }
    private void Update()
    {
        if (selectingredientNum == currentingredientNum)
        {
            checkFoodAndIngredients = CookUIManager.instance.AddselectFoodAndIngredients();
            CookUIManager.instance.ReseetToggle();
            selectingredientNum = 0;
            CheckFood();
        }
    }
    //화면에 레시피를 보여주기 위한 함수
    IEnumerator MakeRecipe()
    {
        Vector3 foodBasePos = startPos.position;
        foodBasePos.y += 2f;
        while (true)
        {
            yield return new WaitForSeconds(createRecipeTime);
            foods.foodObject = randomFood[Random.Range(0, randomFood.Length)];
            foods.foodObject.transform.position = foodBasePos;
            GameObject food = Instantiate(foods.foodObject);
            foods.ingredientObject.Clear();
            List<int> temp = IngredientSelect();
            for (int j = 0; j < temp.Count; j++)
            {
                foods.ingredientObject.Add(ingredients[temp[j]]);
            }
            for (int j = 0; j < foods.ingredientObject.Count; j++)
            {
                IngredientsSet(j);
                GameObject ingredient = Instantiate(foods.ingredientObject[j]);
                ingredient.name = foods.ingredientObject[j].name;
                ingredient.transform.SetParent(food.transform);
            }
            papertmp = Instantiate(paper, startPos.position, Quaternion.identity);
            food.transform.SetParent(papertmp.transform);
            checkRecipeList.Add(food);
        }
    }
    //재료 선택
    List<int> IngredientSelect()
    {
        List<int> selectNum = new List<int>();

        while (selectNum.Count < currentingredientNum)
        {
            randomIngredient = Random.Range(0, 12);
            if (selectNum.Count == 0) selectNum.Add(randomIngredient);
            else
            {
                for (int i = 0; i < selectNum.Count; i++)
                {
                    if (!selectNum.Contains(randomIngredient))
                    {
                        selectNum.Add(randomIngredient);
                        break;
                    }
                }
            }
        }
        return selectNum;
    }
    
    //종이에 재료들 위치
    void IngredientsSet(int j)
    {
        Vector3 ingredientBasePos = startPos.position;
        ingredientBasePos.y -= 0.5f;
        if (currentingredientNum == 1)
        {
            foods.ingredientObject[j].transform.position = ingredientBasePos;
        }
        else if (currentingredientNum == 2)
        {
            ingredientBasePos.x -= j * 3f - 1.5f;
            foods.ingredientObject[j].transform.position = ingredientBasePos;
        }
        else if (currentingredientNum == 3)
        {
            if (j < 2)
            {
                ingredientBasePos.x -= j * 3f - 1.5f;
                ingredientBasePos.y += 0.5f;
                foods.ingredientObject[j].transform.position = ingredientBasePos;
            }
            else
            {
                ingredientBasePos.y -= 1.5f;
                foods.ingredientObject[j].transform.position = ingredientBasePos;
            }
        }
    }

    public void CheckFood()
    {
        int count = 0;
        List<int> tmpNoInIngredient = new List<int>();
        
        //레시피속 재료와 선택한 재료가 맞는지 확인
        for (int i = 0; i < checkFoodAndIngredients.Count; i++)
        {
            for (int j = 0; j < checkRecipeList.Count; j++)
            {
                for (int k = 0; k < checkRecipeList[j].transform.childCount; k++)
                {
                    if(checkRecipeList[j].transform.GetChild(k).name == checkFoodAndIngredients[i])
                    {
                        count++;
                    }
                }
                //재료가 맞지 않을 경우
                if(count == 0)
                {
                    //숫자가 중복해서 들어가면 안됨
                    if (!tmpNoInIngredient.Contains(j))
                    {
                        tmpNoInIngredient.Add(j);
                    }
                }
                count = 0;
            }
        }
        
        //레시피를 off 시키기 위해서 tmp에 없는 인덱스를 찾아야함
        int[] deleteFoodNumArr = new int[checkRecipeList.Count];        
        int deleteFoodNum = -1;
        for (int i = 0; i < deleteFoodNumArr.Length; i++)
        {
            deleteFoodNumArr[i] = i;
        }
        //모두 똑같은 재료가 올 경우
        if (tmpNoInIngredient.Count > 0)
        {
            //tmp에 있으면 -1을 넣어준다
            for (int i = 0; i < tmpNoInIngredient.Count; i++)
            {
                for (int j = 0; j < deleteFoodNumArr.Length; j++)
                {
                    if (deleteFoodNumArr[j] == tmpNoInIngredient[i] || !checkRecipeList[j].transform.parent.gameObject.activeSelf) deleteFoodNumArr[j] = -1;
                }
            }
            //-1이 아닌것중 가장 작은 인덱스를 찾는다
            for (int i = 0; i < deleteFoodNumArr.Length; i++)
            {
                if (deleteFoodNumArr[i] != -1)
                {
                    deleteFoodNum = deleteFoodNumArr[i];
                    break;
                }
            }
        }
        else
        {
            if (deleteNumBuffer == 0) deleteFoodNum = 0;
            else deleteFoodNum = deleteNumBuffer;
        }
        if (checkRecipeList.Count != 0 && deleteFoodNum != -1)
        {
            StartCoroutine(PaperDelete(deleteFoodNum));
        }
        if(checkRecipeList.Count < deleteFoodNum) deleteNumBuffer = deleteFoodNum + 1;
        CookUIManager.instance.ReseetToggle();
        selectingredientNum = 0;
    }

    private IEnumerator PaperDelete(int paperNum)
    {
        
        GameObject paperObject = checkRecipeList[paperNum].transform.parent.gameObject;
        GameObject checkObject = paperObject.transform.GetChild(0).gameObject;
        float moveSpeed = 0.1f;
        if (!checkObject.activeSelf)
        {
            checkObject.SetActive(true);
            decision.Play();
            CookUIManager.instance.SetBarImage(0.05f);
            while (true)
            {
                if (paperObject.transform.position.y < 12)
                {
                    if (moveSpeed < 0.4f) moveSpeed += 0.04f;
                    paperObject.transform.Translate(0, moveSpeed, 0);
                }
                else
                {
                    paperObject.SetActive(false);
                    break;
                }
                yield return null;
            }
        }
    }
}
