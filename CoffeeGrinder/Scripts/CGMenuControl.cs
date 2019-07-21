using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CGMenuControl : MonoBehaviour {

    public int _level;
    public Image[] _rotationImage;
    public Text[] _rotationCount;
    public Sprite _leftRotate;
    public Sprite _rightRotate;

    private int _failCount;
    private int _listIndex;
    public bool _clearGame;
    
    void Awake()
    {
        InitializeMenu();
    }

    void Update()
    {
        
    }

    private void InitializeMenu()
    {
        _level = NewGameManager.Instance.StartGame();
        if (GameObject.Find("TutorialCanvas") != null) _level = 1;
        int menuSize = (int)((_level-1) / 3) + 3;

        for (int i = 0; i < _rotationImage.Length; i++)
        {
            if (Random.Range(0, 2) == 1)
            {
                _rotationImage[i].sprite = _rightRotate;
            }
            else
            {
                _rotationImage[i].sprite = _leftRotate;
            }
            if(menuSize != _rotationImage.Length)
            {
                if (i >= menuSize)
                {
                    _rotationCount[i].gameObject.SetActive(false);
                    _rotationImage[i].gameObject.SetActive(false);
                }
            }
        }
        RotationCheck(menuSize);

        switch ((_level-1) % 3)
        {
            case 0:
                for (int i = 0; i < menuSize; i++)
                    _rotationCount[i].text = Random.Range(2, 4).ToString();
                break;
            case 1:
                for (int i = 0; i < menuSize; i++)
                    _rotationCount[i].text = Random.Range(2, 5).ToString();
                break;
            case 2:
                for (int i = 0; i < menuSize; i++)
                    _rotationCount[i].text = Random.Range(2, 6).ToString();
                break;
        }
    }

    private void RotationCheck(int menuSize)
    {
        if(_rotationImage[menuSize-2].sprite == _rotationImage[menuSize-1].sprite)
        {
            if (_rotationImage[menuSize-1].sprite == _leftRotate)
            {
                _rotationImage[menuSize-1].sprite = _rightRotate;
            }
            else
            {
                _rotationImage[menuSize-1].sprite = _leftRotate;
            }
                
        }
    }

    public int GetRotationDirection(Sprite image)
    {
        if (image == _leftRotate)
        { return -1; }
        else
        { return 1; }
    }
    public int GetFailCount()
    {
        return _failCount;
    }
    public void SetFailCount(int count)
    {
        _failCount = count;
    }
    public int getLevel()
    { return _level; }
}
