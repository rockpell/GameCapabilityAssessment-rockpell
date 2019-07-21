using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0) // 터치 입력 처리
        {
            Touch t = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(t.fingerId) == false)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(t.position);
                //Vector3 pos = Camera.main.ScreenToViewportPoint(t.position);
                InputProcessing(pos, t.phase);
            }
        }

        if (EventSystem.current.IsPointerOverGameObject() == false) // 마우스 입력 처리
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                InputProcessing(pos, TouchPhase.Began);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                InputProcessing(pos, TouchPhase.Moved);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                InputProcessing(pos, TouchPhase.Ended);
            }
        }
    }

    void InputProcessing(Vector3 pos, TouchPhase state)
    {
        if(state == TouchPhase.Began)
        {
            transform.position = pos;
        } else if(state == TouchPhase.Moved)
        {

        } else if(state == TouchPhase.Ended)
        {

        }
    }
}
