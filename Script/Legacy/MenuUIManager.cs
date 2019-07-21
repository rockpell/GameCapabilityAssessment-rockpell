using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMenuUI()
    {
        this.gameObject.SetActive(true);
    }

    public void HideMenuUI()
    {
        this.gameObject.SetActive(false);
    }

    public void TriggerMenuUI()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    private void MoveLobby()
    {
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.SeletScene);
    }

    private void MoveTraining()
    {
        GameManager.Instance.InitTrigger();
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.TrainingScene);
    }

    public void SceneBack()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsTrainingStart)
            {
                MoveTraining();
            }
            else
            {
                MoveLobby();
            }
        }
    }
}
