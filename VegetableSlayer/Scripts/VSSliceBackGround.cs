using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSSliceBackGround : MonoBehaviour {

    public float _scaleMax;
    
    private float _startXPos;
    private float _endXPos;
    private float _distance;

    void Start ()
    {
        _startXPos = GameObject.Find("HandKnife").GetComponent<VSHandMoving>()._startXPosition;
        _endXPos = GameObject.Find("HandKnife").GetComponent<VSHandMoving>()._endXPosition;

        _distance = _endXPos - _startXPos;
    }
	
    public void OverWriteBackGround()
    {
        this.transform.localScale = new Vector3(((GameObject.Find("HandKnife").transform.position.x - _startXPos) * _scaleMax) / _distance, 1, 1);
    }
}
