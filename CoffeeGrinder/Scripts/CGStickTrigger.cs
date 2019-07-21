using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGStickTrigger : MonoBehaviour {

    public int _collisionCount;
    public int _collisionDirection;
    public Vector3 contacts;
    public bool isMove = false;
    public ParticleSystem _particle;
    public int _failCount;

    public int _listIndex;
    private UnityEngine.UI.Image[] _rotationImages;
    private UnityEngine.UI.Text[] _rotationCounts;
    private AudioSource _sound;
    private float _prePosition;
    private float _curPosition;

    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _stickController;
    [SerializeField]
    private GameObject _needle;
    [SerializeField]
    private GameObject _failUI;
    [SerializeField]
    private GameObject _clearUI;


    void Start ()
    {
        _rotationImages = _panel.GetComponent<CGMenuControl>()._rotationImage;
        _rotationCounts = _panel.GetComponent<CGMenuControl>()._rotationCount;
        _sound = GameObject.Find("PowderPosition").GetComponent<AudioSource>();
    }
	
	void Update ()
    {
		if(isMove && !_sound.isPlaying)
        {
            _sound.UnPause();
        }
        else if(!isMove) { _sound.Pause(); }
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            if (isMove)
            {
                _rotationImages = _panel.GetComponent<CGMenuControl>()._rotationImage;
                _rotationCounts = _panel.GetComponent<CGMenuControl>()._rotationCount;
                _collisionCount++;

                if (_stickController.GetComponent<CGStickSpinner>()._isClockwise)
                    _collisionDirection = 1;
                else
                    _collisionDirection = -1;

                if (_listIndex < _rotationCounts.Length)
                {
                    int listRotate = _panel.GetComponent<CGMenuControl>().GetRotationDirection(
                    _rotationImages[_listIndex].sprite);
                    if ((_listIndex < (int)((_panel.GetComponent<CGMenuControl>().getLevel() - 1) / 3) + 3) && (_collisionDirection != listRotate))
                    {
                        var particleMain = _particle.GetComponent<ParticleSystem>().main;
                        particleMain.startColor = Color.red;
                        _particle.Play();

                        _panel.GetComponent<CGMenuControl>().SetFailCount(_panel.GetComponent<CGMenuControl>().GetFailCount() + 1);
                        _failCount = _panel.GetComponent<CGMenuControl>().GetFailCount();
                        _listIndex++;
                        _collisionCount = 0;
                    }
                    else
                    {
                        var particleMain = _particle.GetComponent<ParticleSystem>().main;
                        particleMain.startColor = Color.green;

                        _particle.Play();
                    }
                    if ((_listIndex < _rotationCounts.Length) && (_collisionCount == int.Parse(_rotationCounts[_listIndex].text)))
                    {
                        _listIndex++;
                        _collisionCount = 0;
                    }
                }
                if (_listIndex > (int)((_panel.GetComponent<CGMenuControl>().getLevel() - 1) / 3) + 2)
                {
                    _failCount = _panel.GetComponent<CGMenuControl>().GetFailCount();
                    NewGameManager.Instance.ClearGame(GameScore(_failCount));
                    //Time.timeScale = 0;
                }
                
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }

    public Result GameScore(int failCount)
    {
        if (failCount == 0)
        {
            _clearUI.SetActive(true);
            if (_needle.GetComponent<CGNeedleControl>().CheckRotation() == Result.BigSuccessful)
                return Result.BigSuccessful;
            else if (_needle.GetComponent<CGNeedleControl>().CheckRotation() == Result.Successful)
                return Result.Successful;
            else
                return Result.Fail;
        }
        else if (failCount < 2)
        {
            _clearUI.SetActive(true);
            if (_needle.GetComponent<CGNeedleControl>().CheckRotation() == Result.Fail)
                return Result.Fail;
            else
                return Result.Successful;
        }

        else
        {
            _failUI.SetActive(true);
            return Result.Fail;
        }
            
    }
}
