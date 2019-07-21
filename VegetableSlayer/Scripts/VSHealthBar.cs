using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSHealthBar : MonoBehaviour {

    [SerializeField]
    private GameObject _knifeControl;
    [SerializeField]
    private GameObject _rhythmMaker;
    [SerializeField]
    private GameObject _fill;

    private int _maxScore;
    private UnityEngine.UI.Slider slider;
    
	void Start ()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        _maxScore = _rhythmMaker.GetComponent<VSSampleRhythm>().count * 100;
	}
	
    public void UpdateScore(int score)
    {
        _maxScore = _maxScore - (100 - score)*5;
        if (_maxScore < 0) _maxScore = 0;
        slider.value = (float)(_maxScore / _rhythmMaker.GetComponent<VSSampleRhythm>().count)/100;
        if (slider.value > 0.5)
            _fill.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        else
            _fill.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
    }

	void Update ()
    {
	}
}
