using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelector : MonoBehaviour {
    [SerializeField] GameObject gameButtonsUI;
    [SerializeField] GameObject difficultSelector;

    private List<Transform> gameButtons;
    private int selectButtonIndex = 0; // 1 ~ 5, 0 is nothing select
    private int difficultLevle = 5;

	void Start () {
        gameButtons = new List<Transform>();
        for (int i = 0; i < gameButtonsUI.transform.childCount; i++)
        {
            gameButtons.Add(gameButtonsUI.transform.GetChild(i));
        }
        RefreshUIValue();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickButton(int index)
    {
        if (selectButtonIndex != index)
        {
            gameButtons[index - 1].GetComponent<Image>().color = Color.white;
            if(selectButtonIndex != 0)
                gameButtons[selectButtonIndex - 1].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        selectButtonIndex = index;
    }

    public void ClickUpButton(bool isUpButton)
    {
        if (isUpButton)
        {
            if(difficultLevle < 10)
                difficultLevle += 1;
        }
        else
        {
            if(difficultLevle > 1)
                difficultLevle -= 1;
        }
        RefreshUIValue();
    }

    private void RefreshUIValue()
    {
        difficultSelector.transform.Find("value").GetComponent<Text>().text = difficultLevle.ToString();
    }

    public int DifficultLevle {
        get { return difficultLevle; }
        set { difficultLevle = value; }
    }
}
