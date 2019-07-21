using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour {

    private GameObject gameOverPanel;
    private GameObject whiteNoisePanel;

    [SerializeField] private Sprite[] noiseImages;

    int index;
	// Use this for initialization
	void Awake () {
        gameOverPanel = transform.GetChild(0).gameObject;
        whiteNoisePanel = transform.GetChild(1).gameObject;
        index = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WhiteNoiseOn(float delayTime)
    {
        Invoke("StartWhiteNoise", delayTime);
    }

    private void StartWhiteNoise()
    {
        whiteNoisePanel.SetActive(true);
        StartCoroutine(WhiteNoising(0.05f));
    }

    private IEnumerator WhiteNoising(float time)
    {
        while (true)
        {
            whiteNoisePanel.GetComponent<Image>().sprite = noiseImages[(++index) % noiseImages.Length];
            yield return new WaitForSeconds(time);
        }
    }

    public void GameOverPanelOn(string text)
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "" + text;
    }
}
