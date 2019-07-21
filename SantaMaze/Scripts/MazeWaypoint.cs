using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWaypoint : MonoBehaviour {

    public List<MazeFloor> floorPoint = new List<MazeFloor>();
    public bool notClickFloor = false;
    private Ray checkWallRay;
    private RaycastHit hit;
   //리스트 초기화
   //선택된 floor가 1이상일때만 작동
    public void ListClear()
    {
        if (MazeSanta.instance.isOn) return;
        
        if (floorPoint.Count > 0)
        {
            for (int i = 0; i < floorPoint.Count; i++)
            {
                floorPoint[i].isSelect = false;
                floorPoint[i].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }
        MazeSanta.instance.isSelect = false;
        floorPoint.Clear();
    }
    private void Update()
    {
        if (!MazeSanta.instance.isOn)
        {
            //손을 땔때
            if (Input.GetMouseButtonUp(0) && floorPoint.Count > 0)
            {
                bool isInPlayer = false;
                if (MazeSanta.instance.isSelect) isInPlayer = true;
                //벽이있으면 마지막 선택한것을 지우고 그전위치로 간다
                if (notClickFloor)
                {
                    floorPoint[floorPoint.Count - 1].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    floorPoint[floorPoint.Count - 2].transform.GetComponent<Renderer>().material.SetColor("_Color", new Color32(255, 128, 50, 100));
                    floorPoint[floorPoint.Count - 1].isSelect = false;
                    floorPoint.RemoveAt(floorPoint.Count - 1);
                    notClickFloor = false;
                }
                //현재위치는 초록색으로 보여줌
               // floorPoint[floorPoint.Count - 1].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                if (isInPlayer)
                {
                    if (floorPoint[floorPoint.Count - 1].inGift)
                    {
                        MazeSanta.instance.isOn = true;
                        floorPoint[floorPoint.Count - 1].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);

                        for (int i = 0; i < floorPoint.Count; i++)
                        {
                            MazeSanta.instance.playerMovePos.Add(floorPoint[i].transform);
                        }
                    }
                }
                else
                {
                    ListClear();
                }
                
            }
            if (floorPoint.Count > 1)
            {

                Vector3 pos = floorPoint[floorPoint.Count - 1].transform.position;
                Vector3 pos1 = floorPoint[floorPoint.Count - 2].transform.position;
                if(Vector3.Distance(pos,pos1) - 1.21f > Mathf.Epsilon)
                {
                    floorPoint[floorPoint.Count - 1].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    floorPoint[floorPoint.Count - 2].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    notClickFloor = true;
                    return;
                }
                pos.y += 0.4f;
                pos1.y += 0.4f;
                checkWallRay = new Ray(pos, pos1 - pos);
                //레이를 쏴서 벽이 있는지확인
                if (Physics.Raycast(checkWallRay, out hit, Vector3.Distance(pos, pos1)))
                {
                    if (hit.transform.tag == "Wall")
                    {
                        floorPoint[floorPoint.Count - 1].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        floorPoint[floorPoint.Count - 2].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        notClickFloor = true;
                    }
                }
            }
        }
    }
    public void AddFloor(MazeFloor floor)
    {
        floorPoint.Add(floor);
       // if (MazeSanta.instance.isSelect)
        //{
            floorPoint[floorPoint.Count - 1].transform.GetComponent<Renderer>().material.SetColor("_Color", new Color32(255, 128, 50, 100));
            for (int i = 0; i < floorPoint.Count - 1; i++)
            {
                floorPoint[i].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            }
       // }
        /*else
        {
            for (int i = 0; i < floorPoint.Count; i++)
            {
                floorPoint[i].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }*/
    }
    public void IsInMazeFloor(MazeFloor floor)
    {
        if (floorPoint.Contains(floor) && floorPoint.IndexOf(floor) != floorPoint.Count)
        {
            for (int i = floorPoint.Count - 1; i > floorPoint.IndexOf(floor); i--)
            {
                floorPoint[i].isSelect = false;
                floorPoint[i].transform.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                floorPoint.RemoveAt(i);
            }
        }
    }
}
