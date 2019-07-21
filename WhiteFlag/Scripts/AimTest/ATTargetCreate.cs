using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATTargetCreate : MonoBehaviour {

    public GameObject _target;
    public int _gameScore;
    public int _targetCount;
    private int _gameLevel;
    public bool _clearGame;
    public int _leftBulletCount;
    [SerializeField]
    private GameObject _failUI;
    [SerializeField]
    private GameObject _clearUI;
    [SerializeField]
    private GameObject _barricades;

    private NewGameManager _manager;

    void Start()
    {
        _manager = NewGameManager.Instance;
        _gameLevel = _manager.StartGame();
        if (GameObject.Find("TutorialCanvas") != null) _gameLevel = 1;
        _targetCount = 0;

        Debug.Log("Cannon Position: " + GameObject.Find("Cannon").transform.position + " Barricade Position: " + GameObject.Find("BarricadeSet").transform.position);
        GetComponent<AudioSource>().Play();
        GameObject.Find("LeftShoot Field").GetComponent<Text>().text = _leftBulletCount.ToString();
        _barricades.GetComponent<ATBarricadeControl>().SettingBarricade(_gameLevel);
    }

    public void CreateTarget(int level)
    {
        int count = (level / 2) + 1;
        if (count > _targetCount)
        {
            _targetCount++;
            SetTarget(level);
            if (level > 5)
            {
                GameObject.Find("Cannon").GetComponent<ATCannonControl>().MakeWindForce();
            }
            else
            {
                GameObject.Find("Cannon").GetComponent<ATCannonControl>().InitWindForce();
            }
        }

    }

    private void SetTarget(int level)
    {
        float pointX = Random.Range(0.5f, 8.0f);
        float pointY = Random.Range(-4.3f, 0.0f);
        GameObject target = Instantiate(_target, new Vector3(pointX, pointY, 0), this.transform.rotation);

        float x = Random.Range((float)0.5, 8.0f);
        float y = Random.Range((float)-4.3, (float)0);
        target.GetComponent<ATTargetControl>().SetMovingPoint(x, y);
    }
    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
            _clearGame = true;
        GameClearCheck();

        GameObject.Find("LeftTarget Field").GetComponent<Text>().text = ((_gameLevel / 2 + 1) - _gameScore).ToString();
        GameObject.Find("LeftShoot Field").GetComponent<Text>().text = (_leftBulletCount
            - GameObject.Find("Cannon").GetComponent<ATCannonControl>().GetShootCount()).ToString();
    }

    public void GameClearCheck()
    {
        int shootCount = GameObject.Find("Cannon").GetComponent<ATCannonControl>().GetShootCount();
        int createCount = _gameLevel / 2 + 1;
        if (_clearGame)
        {
            if ((_leftBulletCount - shootCount) > 0)
            {
                if (createCount == _gameScore)
                {
                    if (shootCount <= _gameScore)
                    {
                        _manager.ClearGame(Result.BigSuccessful);
                        _clearUI.SetActive(true);
                    }
                    else
                    {
                        _manager.ClearGame(Result.Successful);
                        _clearUI.SetActive(true);
                    }
                }
                else if (GameObject.FindGameObjectsWithTag("Enemy").Length < 3)
                {
                    CreateTarget(_gameLevel);
                }
            }
            else if (createCount >= _gameScore)
            {
                if (createCount == _gameScore)
                {
                    _manager.ClearGame(Result.Successful);
                    _clearUI.SetActive(true);
                }
                else
                {
                    _manager.ClearGame(Result.Fail);
                    _failUI.SetActive(true);
                }
            }
        }
    }

    public int GetLevel()
    { return _gameLevel; }
}
