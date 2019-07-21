using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KDHTutorialManagement : MonoBehaviour
{
    int reposition = 0;
    int save;
    int controlnum1, controlnum2;
    [SerializeField] private NewTutorialManager tutorialCanvas;
    public GameObject[] Arrow;
    public float startWait = 0;
    public float KDHspawnWait;
    public float waveWait = 0;
    private float level = 1;
    float timer;
    public GameObject[] ArrowButton;
    public Button[] arrowButton;
    public GameObject Glove;

    bool gameOver;

    public KDHTutorialManagement(GameObject[] arrow, float startWait, float spawnWait, float waveWait)
    {
        Arrow = arrow;
        this.startWait = startWait;
        this.KDHspawnWait = spawnWait;
        this.waveWait = waveWait;
    }


    void Start()
    {
        controlnum1 = 1;
        controlnum2 = 1;
        Glove = GameObject.Find("glove_L");
        ArrowButton[0] = GameObject.Find("WhiteUpButton");
        ArrowButton[1] = GameObject.Find("BlueUpButton");
        ArrowButton[2] = GameObject.Find("BlueLeftButton");
        ArrowButton[3] = GameObject.Find("BlueRightButton");
        ArrowButton[4] = GameObject.Find("WhiteRightButton");
        ArrowButton[5] = GameObject.Find("WhiteLeftButton");
        for (int i = 0; i < 6; i++)
        {
            arrowButton[i] = ArrowButton[i].GetComponentInChildren<Button>();
            arrowButton[i].onClick.AddListener(TaskOnClick);

        }
        level = 1;
        KDHspawnWait = 0.75f - level / 30;
        for (int i = 0; i < 6; i++)
        {
            Arrow[i].GetComponent<KDHMove>().Speed(8.4 / KDHspawnWait);
        }
        StartCoroutine(SpawnWaves());
    }

    public void GameOver(int i)
    {
        if (i == 1)
        {
            tutorialCanvas.EndTutorial();
            gameOver = true;
            NewGameManager.Instance.ClearGame(Result.Fail);
        }
        else if (i == 2)
        {
            tutorialCanvas.EndTutorial();
            gameOver = true;
            NewGameManager.Instance.ClearGame(Result.Successful);
        }
        else if (i == 3)
        {
            tutorialCanvas.EndTutorial();
            gameOver = true;
            NewGameManager.Instance.ClearGame(Result.BigSuccessful);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        for (; timer < 11;)
        {

            if (Arrow.Length > 0 && controlnum2 == 1)
            {
                save = Random.Range(0, Arrow.Length);
                GameObject Arrow1 = Arrow[save];

                Vector2 spawnPosition = new Vector2(-70, 11);
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                Instantiate(Arrow1, spawnPosition, spawnRotation);
                for (int i = 0; i < 6; i++)
                {
                    Arrow[i].GetComponent<KDHMove>().Speed(8.4 / KDHspawnWait);
                }
            }

            if (gameOver == true)
            {
                tutorialCanvas.EndTutorial();
                break;
            }

            yield return new WaitForSeconds(KDHspawnWait);

            yield return new WaitForSeconds(waveWait);

        }
    }


    void Update()
    {
        if (controlnum1 == 1)
        {
             timer += Time.deltaTime;
        }
       
        GameObject tutorialCanvas = GameObject.Find("TutorialCanvas");
        if (tutorialCanvas != null)
        {
            tutorialCanvas.GetComponent<NewTutorialManager>().GetIsShowExplantion();
        }

        if (tutorialCanvas.GetComponent<NewTutorialManager>().GetIsShowExplantion())
        {
            controlnum1 = 0;
            controlnum2 = 0;
        }
        else
        {
            controlnum1 = 1;
            controlnum2 = 1;
        }

    }

    void TaskOnClick()
    {

    }

    void Reposition()
    {
        Glove.GetComponentInChildren<Transform>().position = new Vector2(-100, 5.3f);
    }

    public NewTutorialManager GetTutorialManager()
    {
        return tutorialCanvas;
    }

}