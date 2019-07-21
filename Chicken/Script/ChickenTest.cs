using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenTest : MonoBehaviour
{
    private Touch tempTouchs1;
    private Touch tempTouchs2;
    private bool touching1;
    private bool touching2;
    private Vector3 TouchPos1;
    private Vector3 TouchPos2;
    private GameObject Target1;
    private GameObject Target2;

    public Text testText;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            tempTouchs1 = Input.GetTouch(0);

           
            testText.GetComponent<Text>().text = "터치 1개 이상 if 문 진입";
            /////////////////////////////////////////////////////////////////////////////////////터치 1 시작
            if (tempTouchs1.phase == TouchPhase.Began)
            {
                testText.GetComponent<Text>().text = "Mouse1터치시작";
                RaycastHit hit1 = new RaycastHit();
                Ray ray1 = Camera.main.ScreenPointToRay(tempTouchs1.position);
                if (Physics.Raycast(ray1, out hit1))
                {
                    if (hit1.collider.tag == "Mouse")
                    {
                        touching1 = true;
                        Target1 = hit1.collider.gameObject;
                    }
                }
            }
            if (tempTouchs1.phase == TouchPhase.Moved)
            {
                if(touching1 == true)
                {
                    testText.GetComponent<Text>().text = "Mouse1터치움직임";
                    TouchPos1 = Camera.main.ScreenToWorldPoint(tempTouchs1.position);
                    Vector3 touchPosition1 = new Vector3(TouchPos1.x, TouchPos1.y, -1);
                    Target1.transform.position = touchPosition1;
                }
            }
            if(tempTouchs1.phase == TouchPhase.Ended)
            {
                touching1 = false;
            }
            ///////////////////////////////////////////////////////////////////////////////////////터치 2 시작
            if (Input.touchCount > 1)
            {
                tempTouchs2 = Input.GetTouch(1);
                if (tempTouchs2.phase == TouchPhase.Began)
                {
                    testText.GetComponent<Text>().text = "Mouse2터치시작";
                    RaycastHit hit2 = new RaycastHit();
                    Ray ray2 = Camera.main.ScreenPointToRay(tempTouchs2.position);
                    if (Physics.Raycast(ray2, out hit2))
                    {
                        if (hit2.collider.tag == "Mouse")
                        {
                            touching2 = true;
                            Target2 = hit2.collider.gameObject;
                        }
                    }
                }
                if (tempTouchs2.phase == TouchPhase.Moved)
                {
                    if(touching2 == true)
                    {
                        testText.GetComponent<Text>().text = "Mouse2터치움직임";
                        TouchPos2 = Camera.main.ScreenToWorldPoint(tempTouchs2.position);
                        Vector3 touchPosition2 = new Vector3(TouchPos2.x, TouchPos2.y, -1);
                        Target2.transform.position = touchPosition2;
                    }
                    if(tempTouchs2.phase == TouchPhase.Ended)
                    {
                        touching2 = false;
                    }
                }
            }
        }  
    }
}
