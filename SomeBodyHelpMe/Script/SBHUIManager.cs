using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SBHUIManager : MonoBehaviour {

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject headShootText;

    [SerializeField] private Text leftBulletText;
    [SerializeField] private Text leftEnemyText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text leftTimeText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshLeftBullet(int num)
    {
        leftBulletText.text = num.ToString();
    }

    public void RefreshNowScore(int num)
    {
        scoreText.text = num.ToString();
    }

    public void RefreshLeftEnemy(int num)
    {
        leftEnemyText.text = num.ToString();
    }

    public void RefreshLeftTime(int num)
    {
        leftTimeText.text = num.ToString();
    }

    public void ActiveGameOver(Result result)
    {
        if(result == Result.BigSuccessful || result == Result.Successful)
        {
            gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "Clear!";
        }
        else if(result == Result.Fail)
        {
            gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "Fail";
        }

        gameOverPanel.SetActive(true);
    }

    public void ActiveHeadShotText()
    {
        headShootText.GetComponent<Text>().color = new Color(1, 0, 0, 1);

        headShootText.SetActive(true);
        StartCoroutine(SlowHideHeadShotText());
    }

    //public void RefreshHeadShot(int num)
    //{
    //    headShootText.text = num.ToString();
    //}

    public void MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        muzzleFlash.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        StartCoroutine(SlowHideMuzzleFlash());
    }

    private IEnumerator SlowHideMuzzleFlash()
    {
        float _alpha = 1;
        while(_alpha > 0)
        {
            muzzleFlash.GetComponent<Image>().color = new Color(1, 1, 1, _alpha);
            _alpha -= 0.15f;

            yield return new WaitForSeconds(0.03f);
        }
        muzzleFlash.SetActive(false);
    }

    private IEnumerator SlowHideHeadShotText()
    {
        float _alpha = 1;

        yield return new WaitForSeconds(0.5f);
        while (_alpha > 0)
        {
            headShootText.GetComponent<Text>().color = new Color(1, 0, 0, _alpha);
            _alpha -= 0.3f;
            yield return new WaitForSeconds(0.04f);
        }
        headShootText.SetActive(false);
    }

    private void HideHeadShotText()
    {
        headShootText.SetActive(false);
    }
}
