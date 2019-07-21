using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDrag : MonoBehaviour
{
    //float distance = 10;
    //public bool YouPC = false;
    private Touch tempTouchs1;
    private Touch tempTouchs2;
    private Touch tempTouchs3;
    private Touch tempTouchs4;
    private bool touching1;
    private bool touching2;
    private bool touching3;
    private bool touching4;
    private Vector3 TouchPos1;
    private Vector3 TouchPos2;
    private Vector3 TouchPos3;
    private Vector3 TouchPos4;
    private GameObject Target1;
    private GameObject Target2;
    private GameObject Target3;
    private GameObject Target4;
    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit1 = new RaycastHit();
            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray1, out hit1))
            {
                if (hit1.collider.tag == "Mouse")
                {
                    touching1 = true;
                    Target1 = hit1.collider.gameObject;
                    hit1.collider.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            touching1 = false;
        }
        if (touching1 == true)
        {
            if (Target1 != null)
            {
                Vector3 PlayerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Target1.transform.position = new Vector3(PlayerPosition.x, PlayerPosition.y, -1);
            }

        }
        if (ChickenGameController.life == true)
        {
            if (Input.touchCount > 0)
            {
                tempTouchs1 = Input.GetTouch(0);

                /////////////////////////////////////////////////////////////////////////////////////터치 1 시작
                if (tempTouchs1.phase == TouchPhase.Began)
                {
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
                    if (touching1 == true)
                    {
                        if (Target1 != null)
                        {
                            TouchPos1 = Camera.main.ScreenToWorldPoint(tempTouchs1.position);
                            Vector3 touchPosition1 = new Vector3(TouchPos1.x, TouchPos1.y, -1);
                            Target1.transform.position = touchPosition1;
                        }
                    }
                    else
                    {
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
                    if (tempTouchs1.phase == TouchPhase.Ended)
                    {
                        touching1 = false;
                    }
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////터치 2 시작
            if (Input.touchCount > 1)
            {
                tempTouchs2 = Input.GetTouch(1);
                if (tempTouchs2.phase == TouchPhase.Began)
                {
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
                    if (touching2 == true)
                    {
                        if (Target2 != null)
                        {
                            TouchPos2 = Camera.main.ScreenToWorldPoint(tempTouchs2.position);
                            Vector3 touchPosition2 = new Vector3(TouchPos2.x, TouchPos2.y, -1);
                            Target2.transform.position = touchPosition2;
                        }
                    }
                    else
                    {
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
                    if (tempTouchs2.phase == TouchPhase.Ended)
                    {
                        touching2 = false;
                    }
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////터치 3 시작
            if (Input.touchCount > 2)
            {
                tempTouchs3 = Input.GetTouch(2);
                if (tempTouchs3.phase == TouchPhase.Began)
                {
                    RaycastHit hit3 = new RaycastHit();
                    Ray ray3 = Camera.main.ScreenPointToRay(tempTouchs3.position);
                    if (Physics.Raycast(ray3, out hit3))
                    {
                        if (hit3.collider.tag == "Mouse")
                        {
                            touching3 = true;
                            Target3 = hit3.collider.gameObject;
                        }
                    }
                }
                if (tempTouchs3.phase == TouchPhase.Moved)
                {
                    if (touching3 == true)
                    {
                        if (Target3 != null)
                        {
                            TouchPos3 = Camera.main.ScreenToWorldPoint(tempTouchs3.position);
                            Vector3 touchPosition3 = new Vector3(TouchPos3.x, TouchPos3.y, -1);
                            Target3.transform.position = touchPosition3;
                        }
                    }
                    else
                    {
                        RaycastHit hit3 = new RaycastHit();
                        Ray ray3 = Camera.main.ScreenPointToRay(tempTouchs3.position);
                        if (Physics.Raycast(ray3, out hit3))
                        {
                            if (hit3.collider.tag == "Mouse")
                            {
                                touching3 = true;
                                Target3 = hit3.collider.gameObject;
                            }
                        }
                    }
                    if (tempTouchs3.phase == TouchPhase.Ended)
                    {
                        touching3 = false;
                    }
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////터치 4 시작
            if (Input.touchCount > 3)
            {
                tempTouchs4 = Input.GetTouch(3);
                if (tempTouchs4.phase == TouchPhase.Began)
                {
                    RaycastHit hit4 = new RaycastHit();
                    Ray ray4 = Camera.main.ScreenPointToRay(tempTouchs4.position);
                    if (Physics.Raycast(ray4, out hit4))
                    {
                        if (hit4.collider.tag == "Mouse")
                        {
                            touching4 = true;
                            Target4 = hit4.collider.gameObject;
                        }
                    }
                }
                if (tempTouchs4.phase == TouchPhase.Moved)
                {
                    if (touching4 == true)
                    {
                        if (Target4 != null)
                        {
                            TouchPos4 = Camera.main.ScreenToWorldPoint(tempTouchs4.position);
                            Vector4 touchPosition4 = new Vector4(TouchPos4.x, TouchPos4.y, -1);
                            Target4.transform.position = touchPosition4;
                        }
                    }
                    else
                    {
                        RaycastHit hit4 = new RaycastHit();
                        Ray ray4 = Camera.main.ScreenPointToRay(tempTouchs4.position);
                        if (Physics.Raycast(ray4, out hit4))
                        {
                            if (hit4.collider.tag == "Mouse")
                            {
                                touching4 = true;
                                Target4 = hit4.collider.gameObject;
                            }
                        }
                    }
                    if (tempTouchs4.phase == TouchPhase.Ended)
                    {
                        touching4 = false;
                    }
                }
            }
        }
    }
}
