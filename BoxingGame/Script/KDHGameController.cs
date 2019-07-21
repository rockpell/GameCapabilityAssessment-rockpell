using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KDHGameController : MonoBehaviour
{
    int reposition= 0;
    int save;
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

    public KDHGameController(GameObject[] arrow, float startWait, float spawnWait, float waveWait)
    {
        Arrow = arrow;
        this.startWait = startWait;
        this.KDHspawnWait = spawnWait;
        this.waveWait = waveWait;
    }

    void Start()
    {
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
        level = NewGameManager.Instance.StartGame();
        KDHspawnWait = 0.75f - level/30;
        for(int i = 0; i < 6; i++)
        {
            Arrow[i].GetComponent<KDHMove>().Speed(8.4/KDHspawnWait);
        }
        StartCoroutine(SpawnWaves());
    }

    public void GameOver(int i)
    {
        if (i == 1)
        {
            NewGameManager.Instance.ClearGame(Result.Fail);
            gameOver = true;
        }
        else if (i == 2)
        {
            NewGameManager.Instance.ClearGame(Result.Successful);
            gameOver = true;
        }
        else if (i == 3)
        {
            NewGameManager.Instance.ClearGame(Result.BigSuccessful);
            gameOver = true;
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        for(; timer < 11;)
        {
            
            if(Arrow.Length > 0)
            {
                save = Random.Range(0, Arrow.Length);
                GameObject Arrow1 = Arrow[save];

                Vector2 spawnPosition = new Vector2(-70,11);
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 0,0));
                Instantiate(Arrow1, spawnPosition, spawnRotation);
               
              


            }

            if(gameOver == true)
            {
                break;
            }
            
            yield return new WaitForSeconds(KDHspawnWait);

            yield return new WaitForSeconds(waveWait);
            
        }
    }
    

    void Update()
    {
        timer+= Time.deltaTime;
    }

    void TaskOnClick()
    {
        
    }

    void Reposition()
    {
        Glove.GetComponentInChildren<Transform>().position = new Vector2(-100, 5.3f);
    }
}