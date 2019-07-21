using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSNavigationBar : MonoBehaviour {

    [SerializeField]
    private GameObject _rhythmMaker;
    [SerializeField]
    private GameObject _handKnife;
    
    private float _distance;
    private Vector3 _initPosition;
    private static bool _isPlay;

	void Start ()
    {
        _distance = (_handKnife.GetComponent<VSHandMoving>()._endXPosition - _handKnife.GetComponent<VSHandMoving>()._startXPosition)/_rhythmMaker.GetComponent<VSSampleRhythm>().endTime;
        _initPosition = this.transform.position;
        _isPlay = false;
    }
	
	public IEnumerator StartNavigator()
    {
        if (_isPlay != true)
        {
            _isPlay = true;
            while (this.transform.position.x < _handKnife.GetComponent<VSHandMoving>()._endXPosition)
            {
                this.transform.position = new Vector3(this.transform.position.x + _distance * Time.deltaTime, this.transform.position.y, this.transform.position.z);
                yield return null;
            }
            this.transform.position = _initPosition;
            while (this.transform.position.x < _handKnife.GetComponent<VSHandMoving>()._endXPosition)
            {
                this.transform.position = new Vector3(this.transform.position.x + _distance * Time.deltaTime, this.transform.position.y, this.transform.position.z);
                yield return null;
            }
            _isPlay = false;
        }
    }
	void Update () {
		
	}
}
