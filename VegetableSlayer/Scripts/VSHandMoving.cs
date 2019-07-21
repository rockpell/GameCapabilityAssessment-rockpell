using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSHandMoving : MonoBehaviour {

    public float _startXPosition;
    public float _endXPosition;
    private float _rhythmTime;
    private bool _gameClear;
    private float _distance;
    public bool _gameStart;

    [SerializeField]
    private GameObject _rhythmMaker;
    [SerializeField]
    private GameObject _backGround;
    [SerializeField]
    private GameObject _failUI;
    [SerializeField]
    private GameObject _clearUI;

    void Start ()
    {
        _rhythmTime = _rhythmMaker.GetComponent<VSSampleRhythm>().endTime;
        _distance = (_endXPosition - _startXPosition)/_rhythmTime;
	}
	
	void Update ()
    {
        if (this.transform.position.x > _endXPosition)
        {
            _gameStart = false;
            _backGround.GetComponent<AudioSource>().Stop();
            GameClear();
        }
        else if (_gameStart)
            this.transform.position = new Vector3(this.transform.position.x + _distance * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        
	}

    private void GameClear()
    {
        int score = (int)(GameObject.Find("UpKnife").GetComponent<VSKnifeControl>()._score / _rhythmMaker.GetComponent<VSSampleRhythm>().count);
        if(score > 90)
        {
            NewGameManager.Instance.ClearGame(Result.BigSuccessful);
            _clearUI.SetActive(true);
        }
        else if(score > 80)
        {
            NewGameManager.Instance.ClearGame(Result.Successful);
            _clearUI.SetActive(true);
        }
        else
        {
            NewGameManager.Instance.ClearGame(Result.Fail);
            _failUI.SetActive(true);
        }
    }
}
