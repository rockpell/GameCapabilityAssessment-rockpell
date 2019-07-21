using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSHighlight : MonoBehaviour {

    public GameObject _highlight;
    private List<GameObject> _hightLights;
    [SerializeField]
    private GameObject _handKnife;
    [SerializeField]
    private GameObject _rhythmMaker;
    public int _speed;
    private List<float> _rhythmList;
    private List<float> _highLightsPos;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _startXPos;
    [SerializeField]
    private float _rhythmTime;
    private float _curTime;
    private int _index;

	void Start ()
    {
        _rhythmList = _rhythmMaker.GetComponent<VSSampleRhythm>().GetList();
        _rhythmTime = _rhythmMaker.GetComponent<VSSampleRhythm>().rhythm;
        HighLightsPos = new List<float>();
        HighLights = new List<GameObject>();
        
        _startXPos = _handKnife.GetComponent<VSHandMoving>()._startXPosition;
        _distance = (_handKnife.GetComponent<VSHandMoving>()._endXPosition - _startXPos) /
            _rhythmMaker.GetComponent<VSSampleRhythm>().endTime;
        MakeHighLight();
    }
	
    private void MakeHighLight()
    {
        foreach(float time in _rhythmList)
        {
            HighLights.Add(Instantiate(_highlight, new Vector3((time + _rhythmTime / 2) * _distance + _startXPos, 0, 0.5f),Quaternion.identity));
            HighLightsPos.Add(time * _distance + _startXPos);
        }
    }

    private void HighLightRange(int index)
    {
        GameObject right = Instantiate(_highlight, new Vector3(HighLightsPos[_index] + _rhythmTime * _distance, 0, 0.5f), Quaternion.identity);
        GameObject left = Instantiate(_highlight, new Vector3(HighLightsPos[_index], 0, 0.5f), Quaternion.identity);

        Color color = Color.blue;
        color.a = 0.5f;
        right.GetComponent<SpriteRenderer>().color = color;
        left.GetComponent<SpriteRenderer>().color = color;
        
        StartCoroutine(HighLightMove(right, left, index));
    }

    IEnumerator HighLightMove(GameObject right, GameObject left, int index)
    {
        
        for(float time = 0.0f; time < _rhythmTime; time += Time.deltaTime)
        {
            if(right != null)
                right.transform.position = new Vector3(right.transform.position.x - _distance * _rhythmTime * _speed * Time.deltaTime, 0, 0.5f);
            else
                yield return null;
            if (left != null)
                left.transform.position = new Vector3(left.transform.position.x + _distance * _rhythmTime * _speed * Time.deltaTime, 0, 0.5f);
            else
                yield return null;
            /*
            if ((right != null && right.transform.position.x <= HighLightsPos[index]) || (left != null && left.transform.position.x >= HighLightsPos[index] + _rhythmTime * _distance))
            {
                
                yield return null;
            }
            */
            yield return null;
        }
        Destroy(right);
        Destroy(left);
    }

    void Update ()
    {

	}

    public List<float> HighLightsPos
    {
        get
        {return _highLightsPos;}
        set
        {_highLightsPos = value;}
    }

    public List<GameObject> HighLights
    {
        get { return _hightLights; }
        set { _hightLights = value; }
    }
}
