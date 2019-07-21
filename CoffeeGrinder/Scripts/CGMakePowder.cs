using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGMakePowder : MonoBehaviour {

    public GameObject _powder;
    public float _grindTime;
    public int _count;

    private Vector3 _makePosition;
    private float _currentTime;
    private int _coffeeCount;

	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        _currentTime += Time.deltaTime;
        if(_currentTime > _grindTime)
        {
            if (GameObject.Find("CollisionArea").GetComponent<CGStickTrigger>().isMove && (_coffeeCount < _count))
            {
                _makePosition = this.transform.position;
                _makePosition.x = _makePosition.x + Random.Range(-this.transform.lossyScale.x / 2, this.transform.lossyScale.x / 2);
                Instantiate(_powder, _makePosition, transform.rotation);
                _coffeeCount++;
            }
            _currentTime = 0;
        }
	}
}
