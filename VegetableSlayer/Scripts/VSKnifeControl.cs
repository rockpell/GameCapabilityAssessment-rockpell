using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSKnifeControl : MonoBehaviour {

    public int _score;

    private float _playTime;
    private float _curTime;
    private float _startXPos;
    private float _endXPos;
    private float _distance;
    private int _index;
    private GameObject _vegetable;

    [SerializeField]
    private GameObject _handKnife;
    [SerializeField]
    private GameObject _rhythmMaker;
    [SerializeField]
    private GameObject _healthBar;
    [SerializeField]
    private GameObject _table;
    [SerializeField]
    private GameObject _failUI;

    void Start ()
    {
        _vegetable = GameObject.FindGameObjectWithTag("Vegetable");
        _startXPos = _handKnife.GetComponent<VSHandMoving>()._startXPosition;
        _endXPos = _handKnife.GetComponent<VSHandMoving>()._endXPosition;
        _playTime = _rhythmMaker.GetComponent<VSSampleRhythm>().endTime;
        _distance = (_endXPos - _startXPos) / _playTime;
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_handKnife.GetComponent<VSHandMoving>()._gameStart)
            {
                GetComponent<AudioSource>().Play();
                if (_index < _table.GetComponent<VSHighlight>().HighLightsPos.Count &&
                    (this.transform.position.x >= _table.GetComponent<VSHighlight>().HighLightsPos[_index]))
                {
                    CalculateScore(CalculateRange());
                    _vegetable.GetComponent<VSVegetableControl>().SliceVegetable(this.transform.position.x);
                    GameObject.Find("Slice").GetComponent<VSSliceBackGround>().OverWriteBackGround();
                    _index++;
                }
            }
        }
        else if (_index < _table.GetComponent<VSHighlight>().HighLightsPos.Count && 
            _table.GetComponent<VSHighlight>().HighLightsPos[_index]
            + _distance * _rhythmMaker.GetComponent<VSSampleRhythm>().rhythm < this.transform.position.x)
        {
            _index++;
            _healthBar.GetComponent<VSHealthBar>().UpdateScore(0);
        }
        GameFailCheck();
	}

    private int CalculateRange()
    {
        float range = this.transform.position.x - _table.GetComponent<VSHighlight>().HighLightsPos[_index];
        if (range < 0) range = range * -1;
        return (int)(range / (_distance * _rhythmMaker.GetComponent<VSSampleRhythm>().rhythm) * 100);
    }

    public void CalculateScore(int range)
    {
        int score = (int)(-0.04f * (range - 50) * (range - 50) + 100);
        _score += score;
        _healthBar.GetComponent<VSHealthBar>().UpdateScore(score);
        if (score > 80)
        {
            _table.GetComponent<VSHighlight>().HighLights[_index].GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (score > 60)
        {
            _table.GetComponent<VSHighlight>().HighLights[_index].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if(score > 40)
        {
            _table.GetComponent<VSHighlight>().HighLights[_index].GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            _table.GetComponent<VSHighlight>().HighLights[_index].GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    
    private void GameFailCheck()
    {
        int count = _rhythmMaker.GetComponent<VSSampleRhythm>().count;
        if (((count - _index) * 100 + _score) < count * 80)
        {
            NewGameManager.Instance.ClearGame(Result.Fail);
            _failUI.SetActive(true);
        }
    }
}
