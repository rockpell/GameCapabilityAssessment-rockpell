using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSAddButtons : MonoBehaviour { // MonoBehaviour 
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    private int num;

    private void Start()
    {
        num = GetComponent<MSsGameController>().numberOfCard;
        for (int i = 0; i < num; i++)
        {
            GameObject button = Instantiate(btn); //1~7까지 버튼 이름 짓기
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
            button.gameObject.AddComponent<MSButtonControl>();
        }
    }
}
