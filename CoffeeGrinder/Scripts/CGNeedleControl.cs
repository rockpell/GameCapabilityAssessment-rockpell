using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGNeedleControl : MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private GameObject _failUI;
    private Quaternion _init;

    [SerializeField]
    private float _bigSuccessAngle;
    [SerializeField]
    private GameObject _counter;

    void Start ()
    {
        _init = this.transform.rotation;
	}
	
	void Update ()
    {
        if (_counter.GetComponent<CGCounter>().GetStart())
        {
            GetComponent<RectTransform>().transform.rotation *= Quaternion.Euler(0, 0, -_rotateSpeed);
            if (Quaternion.Angle(_init, transform.rotation) > 90)
            {
                _failUI.SetActive(true);
                NewGameManager.Instance.ClearGame(Result.Fail);
            }
        }
    }

    public Result CheckRotation()
    {
        float angle = Quaternion.Angle(_init, transform.rotation);
        if (angle < _bigSuccessAngle)
        {
            return Result.BigSuccessful;
        }
        else if (angle < 90)
        {
            return Result.Successful;
        }
        else
        {
            return Result.Fail;
        }
            
    }
}
