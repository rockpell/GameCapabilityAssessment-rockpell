using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    [SerializeField] private Text[] resultText;

	// Use this for initialization
	void Start () {
        RefreshReusltText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RefreshReusltText()
    {
        for(int i = 0; i < resultText.Length; i++)
        {
            resultText[i].text = GameManager.Instance.SubjectScore[(Subject)i].ToString();
        }
    }
}
