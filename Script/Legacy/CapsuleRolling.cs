using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleRolling : MonoBehaviour {

    private bool isActive = false;
    private float speed = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            transform.Translate(new Vector3(0, speed, 0));
            if (transform.localPosition.y >= 660) transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        }
	}

    public void ColorSetting(Color[] colors, int index)
    {
        int temp;
        for(int i = 0; i < transform.childCount; i++)
        {
            temp = index + i;
            if(temp > (transform.childCount - 1) ) temp -= (transform.childCount);
            transform.GetChild(i).GetChild(0).GetComponent<Image>().color = colors[temp];
            transform.GetChild(i).GetChild(1).GetComponent<Image>().color = colors[temp];
        }
    }

    public bool IsActive {
        get { return isActive; }
        set { isActive = value; }
    }
}
