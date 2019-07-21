using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPlayerMove : MonoBehaviour
{

    public GameObject Mouse;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Mouse.transform.position = new Vector3(newPosition.x, newPosition.y, -5);
        //Debug.Log("x = "+Mouse.transform.position.x+"y = "+ Mouse.transform.position.y+"z = "+ Mouse.transform.position.z);
    }
}
