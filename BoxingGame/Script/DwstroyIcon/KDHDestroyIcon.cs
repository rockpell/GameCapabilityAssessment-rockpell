using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KDHDestroyIcon : MonoBehaviour
{

    int[] add = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    public GameObject icon, sound, JudgementLine, DestroyLine;
    public GameObject[] IconButton;
    public Button iconButton;
    public Button[] NotTuchIconButton;
    EventTrigger[] trigger = new EventTrigger[8];
    EventTrigger.Entry[] entry = new EventTrigger.Entry[8];
    int i;
    //Event c_Event = Event.current;


    // Use this for initialization
    void Start()
    {
        
        JudgementLine = GameObject.Find("JudgementLine");
        IconButton[0] = GameObject.Find("BlueDownButton");
        IconButton[1] = GameObject.Find("BlueUpButton");
        IconButton[2] = GameObject.Find("BlueLeftButton");
        IconButton[3] = GameObject.Find("BlueRightButton");
        IconButton[4] = GameObject.Find("WhiteUpButton");
        IconButton[5] = GameObject.Find("WhiteRightButton");
        IconButton[6] = GameObject.Find("WhiteLeftButton");
        IconButton[7] = GameObject.Find("WhiteDownButton");
        
        trigger[0] = IconButton[0].GetComponent<EventTrigger>();
        entry[0] = new EventTrigger.Entry();
        entry[0].eventID = EventTriggerType.PointerDown;
        iconButton = IconButton[0].GetComponentInChildren<Button>();
        for (i = 0; i < 7; i++)
        {
            entry[i+1] = new EventTrigger.Entry();
            entry[i+1].eventID = EventTriggerType.PointerDown;
            trigger[i+1] = IconButton[i+1].GetComponent<EventTrigger>();
            NotTuchIconButton[i] = IconButton[i + 1].GetComponentInChildren<Button>();
            entry[i+1].callback.AddListener((data) => { TaskOnClick1(); });
            trigger[i+1].triggers.Add(entry[i+1]);
        }

        entry[0].callback.AddListener((data) => { TaskOnClick(); });
        trigger[0].triggers.Add(entry[0]);


    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "DestroyLine")
        {
            add[0]++;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "DestroyLine")
        {
            if (add[0] == 1)
            {
                add[0]--;


            }
        }
    }

    void TaskOnClick()
    {
        if (add[0] == 1)
        {
            Destroy(icon);
            Instantiate(sound, transform.position, transform.rotation);
        }

    }

    void TaskOnClick1()
    {
        if (add[0] == 1)
        {
            Destroy(icon);
            Instantiate(sound, transform.position, transform.rotation);
            JudgementLine.GetComponent<KDHStar>().Addpoint(1);
        }
    }

}
