using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGCounter : MonoBehaviour {

    [SerializeField]
    private int _counter;

    private bool _isStart;
    
	void Start ()
    {
        StartCoroutine(StartCounter());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartCounter()
    {
        float count = _counter;
        float curTime = 0;
        while (curTime < count)
        {
            curTime += Time.deltaTime;
            GetComponent<UnityEngine.UI.Text>().text = (count - Mathf.Round(curTime)).ToString();
            yield return null;
        }
        GetComponent<UnityEngine.UI.Text>().text = "Start!";
        _isStart = true;

        yield return new WaitForSeconds(0.5f);
        GetComponent<UnityEngine.UI.Text>().text = "";

    }

    public bool GetStart()
    { return _isStart; }
}
