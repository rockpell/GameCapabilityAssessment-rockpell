using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATTargetControl : MonoBehaviour {

    public float _speed;
    public float _stayTime;
    private float _curTime;
    public GameObject _burstEffect;

    private Vector3[] _initPointSet;
    private float _curSpeed;
    private int _point;
    private bool _mode;
    private GameObject _particle;

    public void SetMovingPoint(float pointX, float pointY)
    {
        _initPointSet[1].x = pointX;
        _initPointSet[1].y = pointY;
        
    }

	void Awake ()
    {
        _initPointSet = new Vector3[2];
        _initPointSet[0] = transform.position;
        _point = 0;
        _speed = UnityEngine.Random.Range(1.0f, _speed);
    }
    private void Start()
    {
        
    }
    void FixedUpdate ()
    {
        _curSpeed = Mathf.Sin(_curTime * (1/_speed) * Mathf.PI) * _speed;
        transform.position = Vector3.MoveTowards(transform.position, _initPointSet[_point], _curSpeed * Time.deltaTime);
        if(transform.position == _initPointSet[_point])
        {
            _point = (_point + 1) % 2;
        }

        _curTime += Time.deltaTime;
        if(_stayTime < _curTime)
        {
            SetMovingPoint(UnityEngine.Random.Range((float)0.5, 8.0f), UnityEngine.Random.Range((float)-4.3, (float)0));
            _curTime = 0;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            _particle = Instantiate(_burstEffect, transform.position, transform.rotation);
            _particle.GetComponent<AudioSource>().Play();
            
            GameObject.Find("TargetArea").GetComponent<ATTargetCreate>()._gameScore++;

            Destroy(this.gameObject);
        }
    }
}
