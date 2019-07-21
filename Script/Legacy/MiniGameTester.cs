using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.Instance.StartGame();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TestGameEnd(int result)
    {
        if(result == -2 || result == -1 || result == 1 || result == 2)
        {
            GameManager.Instance.ClearGame((GameManager.Result)result);
            //CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.Evaluation);
        }
            
    }
}
