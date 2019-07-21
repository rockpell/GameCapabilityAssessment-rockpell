using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KDHDeadIcon : MonoBehaviour
{
    
    public GameObject icon, DeadLine;


    // Use this for initialization
    void Start()
    {
        DeadLine = GameObject.Find("DeadLine");
    }

    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeadLine")
        {
            Destroy(icon);
        }
        
    }


}