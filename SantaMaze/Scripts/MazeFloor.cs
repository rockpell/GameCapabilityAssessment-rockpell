using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFloor : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;
    public bool isSelect = false;
    public bool inGift = false;
    MazeWaypoint waypoint;
    private void Awake()
    {
        waypoint = GameObject.Find("WayPoints").GetComponent<MazeWaypoint>();
    }
    private void OnMouseOver()
    {
        //선택이 안되었거나 처음시작했을때 화살표 돌때, 벽이 사이에 없을때
        if (MazeUIManager.instance.startSignalEnd && !waypoint.notClickFloor && !MazeSanta.instance.isOn)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isSelect)
                {

                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        isSelect = true;
                        waypoint.AddFloor(this);
                    }
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        waypoint.IsInMazeFloor(this);
                    }
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Tree")
        {
            Destroy(other.gameObject);
        }
        if (isSelect && !waypoint.notClickFloor)
        {
            if (other.gameObject.tag == "Gift")
            {
                inGift = true;
            }
            if (other.gameObject.tag == "Player")
            {
                transform.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            }
        }
        else if(!isSelect && !MazeUIManager.instance.startSignalEnd && other.gameObject.tag == "Player")
        {
            isSelect = true;
            waypoint.AddFloor(this);
            MazeSanta.instance.isSelect = true;
            transform.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
    }
}
