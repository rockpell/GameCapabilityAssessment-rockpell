using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGStickSpinner : MonoBehaviour {

    public GameObject _stick;
    public bool _isClockwise;
    public float _rotationSpeedRate;
    private float _angle;

    private Vector3 _firstD;
    private Vector3 _secondD;
    private Vector3 _pivot;

    private RaycastHit2D _raycastHit2D;
    private Ray _ray;

    [SerializeField]
    private GameObject _needle;
    [SerializeField]
    private GameObject _counter;

	// Use this for initialization
	void Start ()
    {
        _stick = GameObject.Find("UpGrinder_Stick");
        _pivot = _stick.GetComponent<SpriteRenderer>().bounds.center;
        _pivot.y = _pivot.y - 1.0f;
        _pivot.z = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FindObject();
            }
            if ((Input.GetTouch(0).phase == TouchPhase.Moved) && (_stick != null) && _counter.GetComponent<CGCounter>().GetStart())
            {
                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPoint.z = 0;
                _secondD = (_pivot - touchPoint);
                _secondD.Normalize();
                if ((_firstD.magnitude * _secondD.magnitude) == 0)
                    _angle = 0;
                else
                    _angle = Mathf.Asin((_firstD.x * _secondD.y - _firstD.y * _secondD.x) / (_firstD.magnitude * _secondD.magnitude)) * Mathf.Rad2Deg;

                if (_angle > 0)
                    _isClockwise = false;
                else
                    _isClockwise = true;

                _stick.transform.rotation *= Quaternion.Euler(0, 0, _angle);

                if (_isClockwise)
                    _needle.GetComponent<RectTransform>().transform.rotation *= Quaternion.Euler(0, 0, -_angle * _rotationSpeedRate);
                else
                    _needle.GetComponent<RectTransform>().transform.rotation *= Quaternion.Euler(0, 0, +_angle * _rotationSpeedRate);

                GameObject.Find("CollisionArea").GetComponent<CGStickTrigger>().isMove = true;
                _firstD = _secondD;
            }
            else if ((Input.GetTouch(0).phase == TouchPhase.Stationary) && (_stick != null))
            {
                GameObject.Find("CollisionArea").GetComponent<CGStickTrigger>().isMove = true;
            }
            
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameObject.Find("CollisionArea").GetComponent<CGStickTrigger>().isMove = false;
                _stick = null;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                FindObject();
            }
            else if(Input.GetMouseButton(0) && (_stick != null) && _counter.GetComponent<CGCounter>().GetStart())
            {
                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPoint.z = 0;
                _secondD = (_pivot - touchPoint);
                _secondD.Normalize();
                if ((_firstD.magnitude * _secondD.magnitude) == 0)
                    _angle = 0;   
                else
                    _angle = Mathf.Asin((_firstD.x*_secondD.y - _firstD.y*_secondD.x)/(_firstD.magnitude * _secondD.magnitude)) * Mathf.Rad2Deg;
                if(_angle > 0)
                {
                    _isClockwise = false;
                }
                else
                {
                    _isClockwise = true;
                }
                _stick.transform.rotation *= Quaternion.Euler(0, 0, _angle);

                if (_isClockwise)
                    _needle.GetComponent<RectTransform>().transform.rotation *= Quaternion.Euler(0, 0, -_angle * _rotationSpeedRate);
                else
                    _needle.GetComponent<RectTransform>().transform.rotation *= Quaternion.Euler(0, 0, +_angle * _rotationSpeedRate);

                GameObject.Find("CollisionArea").GetComponent<CGStickTrigger>().isMove = true;
                _firstD = _secondD;
            }
            if(Input.GetMouseButtonUp(0))
            {
                GameObject.Find("CollisionArea").GetComponent<CGStickTrigger>().isMove = false;
                _stick = null;
            }
        }
	}

    private void FindObject()
    {
        
        if(Input.touchCount > 0)
            _ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        else
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _raycastHit2D = Physics2D.Raycast(_ray.origin, _ray.direction);
        if (_raycastHit2D.collider != null)
        {
            if(_raycastHit2D.collider.gameObject.name == "UpGrinder_Stick")
            {
                _stick = _raycastHit2D.collider.gameObject;
                Vector3 initPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                initPosition.z = 0;
                _firstD = _pivot - initPosition;
                _firstD.Normalize();
            }
                
        }
    }
    
}
