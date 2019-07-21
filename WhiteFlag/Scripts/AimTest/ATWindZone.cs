using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATWindZone : MonoBehaviour {

    public float _windForce;
    
	void Start ()
    {

    }
	
	void Update ()
    {
		
	}

    public void ChangeWindForce()
    {
        _windForce = float.Parse(GameObject.Find("WindForce").GetComponent<UnityEngine.UI.Text>().text) / 20;
        GetComponent<WindZone>().windMain = _windForce;

        if (GameObject.Find("WindDirection").GetComponent<RectTransform>().rotation == Quaternion.Euler(0, 0, 0))
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        else
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}
