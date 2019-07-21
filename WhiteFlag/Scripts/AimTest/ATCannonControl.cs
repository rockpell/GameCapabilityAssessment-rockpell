using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATCannonControl : MonoBehaviour {

    public float _sensitive;
    public GameObject _bullet;
    public GameObject _fireEffect;
    public float _maxPower;

    private Vector3 _touchPoint;
    private Vector3 _releasePoint;
    private Vector3 _invDirection;
    private Vector3 _windForceDirection;

    [SerializeField]
    private GameObject _shootPosition;
    [SerializeField]
    private GameObject _timeField;

    private int _shootCount;
    private float _accuracy;

    private float _windForce;

    [SerializeField]
    private float aimingTime;
    private float curTime;
    [SerializeField]
    private GameObject _prediction;

    private bool _isFire;
    
	void Start ()
    {
        _shootCount = 0;
        _accuracy = 0.0f;
	}

    public void MakeWindForce()
    {
        _windForce = Random.Range(50.0f,100.0f);
        _windForce = (float)System.Math.Round((double)_windForce, 2);
        if(((int)(_windForce)%2) == 0)
        {
            _windForceDirection = new Vector3(1, 0, 0);
            GameObject.Find("WindDirection").GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            _windForceDirection = new Vector3(-1, 0, 0);
            GameObject.Find("WindDirection").GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -180);
        }
        GameObject.Find("WindForce").GetComponent<UnityEngine.UI.Text>().text = _windForce.ToString();
        _windForceDirection.Normalize();
        GameObject.Find("WindZone").GetComponent<ATWindZone>().ChangeWindForce();
        GameObject.Find("Ship").GetComponent<ATShipShaking>().SetShakeAngle(_windForce);
    }
    public void InitWindForce()
    {
        _windForce = 0;
        _windForceDirection.Set(0, 0, 0);
    }
    
    public int GetShootCount() { return _shootCount; }

    private void Awake()
    {
        //Screen.SetResolution(Screen.width, Screen.width/16*9, true);
    }

    void Update ()
    {
        _timeField.GetComponent<UnityEngine.UI.Text>().text = Mathf.Round(aimingTime - curTime).ToString();
        if (curTime > aimingTime)
        {
            FireBullet();
        }
        if (Input.GetMouseButtonDown(0))
        {
            _isFire = false;
            _touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _touchPoint.z = 0;
        }
        if(Input.GetMouseButton(0))
        {
            _releasePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _releasePoint.z = 0;
            
            _invDirection = (_touchPoint - _releasePoint) * _sensitive;

            float angle = Mathf.Atan2(_invDirection.y, _invDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle-15);
            //각도 계산, 대포 몸체 회전
            
            if (Vector3.Distance(_shootPosition.transform.position, _invDirection) > _maxPower)
            {
                _invDirection.Normalize();
                _invDirection = _invDirection * _maxPower;
            }
            Vector3 linePoint;
            if((Vector3.Distance(_touchPoint,_releasePoint) * _sensitive) >= _maxPower)
                linePoint = _shootPosition.transform.position + _invDirection.normalized * (_maxPower/_sensitive);
            else
                linePoint = _shootPosition.transform.position + _invDirection.normalized * (Vector3.Distance(_touchPoint, _releasePoint));
            
            _shootPosition.GetComponent<LineRenderer>().positionCount = 3;
            _shootPosition.GetComponent<LineRenderer>().SetPosition(0, _shootPosition.transform.position);
            _shootPosition.GetComponent<LineRenderer>().SetPosition(1,linePoint);
            //GameObject.Find("ShootPosition").GetComponent<LineRenderer>().SetPosition(2, PointRotation(linePoint-_invDirection.normalized*0.5f,linePoint,30));
            _shootPosition.GetComponent<LineRenderer>().SetPosition(2, linePoint - _invDirection.normalized);
            //GameObject.Find("ShootPosition").GetComponent<LineRenderer>().SetPosition(3, linePoint);
            //GameObject.Find("ShootPosition").GetComponent<LineRenderer>().SetPosition(4, PointRotation(linePoint - _invDirection.normalized*0.5f, linePoint, -30));
            //GameObject.Find("ShootPosition").GetComponent<LineRenderer>().SetPosition(5, linePoint);

            _prediction.GetComponent<ATShootPredication>().Prediction(_invDirection);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isFire == false)
            {
                FireBullet();
            }
        }
        curTime += Time.deltaTime;
        /*
        //여기서부터 터치 스크립트
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                _touchPoint.z = 0;
                lineRenderer.SetPosition(0, _shootPosition);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _releasePoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            _releasePoint.z = 0;
            
            _invDirection = (_touchPoint - _releasePoint) * _sensitive;

            float angle = Mathf.Atan2(_invDirection.y, _invDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle-15);
            //각도 계산, 대포 몸체 회전
            _shootPosition = GameObject.Find("ShootPosition").transform.position;
            
            if (Vector3.Distance(_shootPosition, _invDirection) > _maxPower)
            {
                _invDirection.Normalize();
                _invDirection = _invDirection * _maxPower;
            }
            lineRenderer.SetPosition(0, _shootPosition);
            lineRenderer.SetPosition(1, _shootPosition+ _invDirection.normalized * (Vector3.Distance(_touchPoint, _releasePoint) - 1));
        }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameObject bullet;
            GameObject effect;

            bullet = Instantiate(_bullet, _shootPosition, Quaternion.identity);
            
            effect = Instantiate(_fireEffect, _shootPosition, transform.rotation * Quaternion.Euler(0,0,-90));
            
            bullet.GetComponent<ATBulletControl>().ShootBullet(_invDirection);
            bullet.GetComponent<ATBulletControl>().AddWindForce(_windForceDirection * _windForce);
            //발사 스크립트 호출
            _shootCount++;
            CalculateAccuracy();
            }
        }
        */
    }

    private void FireBullet()
    {
        if (_shootCount < GameObject.Find("TargetArea").GetComponent<ATTargetCreate>()._leftBulletCount)
        {
            GameObject bullet;
            GameObject effect;

            bullet = Instantiate(_bullet, _shootPosition.transform.position, Quaternion.identity);

            effect = Instantiate(_fireEffect, _shootPosition.transform.position, transform.rotation * Quaternion.Euler(0, 0, -90));

            bullet.GetComponent<ATBulletControl>().ShootBullet(_invDirection);
            bullet.GetComponent<ATBulletControl>().AddWindForce(_windForceDirection * _windForce);
            //발사 스크립트 호출
            _shootCount++;
            curTime = 0;
            _isFire = true;
        }
    }

    Vector3 PointRotation(Vector3 point, Vector3 original, float angle)
    {
        Vector3 rotationPoint;
        angle = angle * Mathf.Deg2Rad;

        rotationPoint.x = ((point.x - original.x) * Mathf.Cos(angle) - (point.y - original.y) * Mathf.Sin(angle)) + original.x;
        rotationPoint.y = ((point.x - original.x) * Mathf.Sin(angle) + (point.y - original.y) * Mathf.Cos(angle)) + original.y;
        rotationPoint.z = point.z;

        return rotationPoint;
    }
}
