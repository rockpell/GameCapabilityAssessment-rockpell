using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class KDHDestroyIcon6 : MonoBehaviour
{

    int[] add = new int[6] { 0, 0, 0, 0, 0, 0};
    public GameObject icon, sound, JudgementLine, goodeffect, badeffect;
    public GameObject[] IconButton;
    public Button iconButton;
    public Button[] NotTuchIconButton;
    EventTrigger[] trigger = new EventTrigger[6];
    EventTrigger.Entry[] entry = new EventTrigger.Entry[6];
    int i;

    // Use this for initialization
    void Start()
    {
        JudgementLine = GameObject.Find("JudgementLine");
        IconButton[1] = GameObject.Find("BlueUpButton");
        IconButton[2] = GameObject.Find("BlueLeftButton");
        IconButton[3] = GameObject.Find("BlueRightButton");
        IconButton[4] = GameObject.Find("WhiteUpButton");
        IconButton[0] = GameObject.Find("WhiteRightButton");
        IconButton[5] = GameObject.Find("WhiteLeftButton");
        trigger[0] = IconButton[0].GetComponent<EventTrigger>();
        entry[0] = new EventTrigger.Entry();
        entry[0].eventID = EventTriggerType.PointerDown;
        iconButton = IconButton[0].GetComponentInChildren<Button>();
        for (i = 0; i < 5; i++)
        {
            entry[i + 1] = new EventTrigger.Entry();
            entry[i + 1].eventID = EventTriggerType.PointerDown;
            trigger[i + 1] = IconButton[i + 1].GetComponent<EventTrigger>();
            NotTuchIconButton[i] = IconButton[i + 1].GetComponentInChildren<Button>();
            entry[i + 1].callback.AddListener((data) => { TaskOnClick1(); });
            trigger[i + 1].triggers.Add(entry[i + 1]);
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
            Instantiate(sound, transform.position, transform.rotation);
            Instantiate(goodeffect, new Vector3(-99.56f, 5.97f, 0), transform.rotation);
            Destroy(icon);
        }

    }

    void TaskOnClick1()
    {
        if (add[0] == 1)
        {
            Destroy(icon);
            Instantiate(sound, transform.position, transform.rotation);
            Instantiate(badeffect, new Vector3(-99.56f, 5.97f, 0), transform.rotation);
            JudgementLine.GetComponent<KDHStar>().Addpoint(1);
        }
    }
}
