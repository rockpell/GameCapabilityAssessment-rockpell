using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrees : MonoBehaviour {
    public static MazeTrees instance;
    public GameObject[] trees;
    private int totalTrees = 60;
    private float xPos;
    private float yPos;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CreateTrees(float x, float y)
    {
        int selectTree = 0;
        for (int i = 0; i < totalTrees; i++)
        {
            if (i % 2 == 0) xPos = Random.Range(x - 8 ,x + 1);
            else xPos = Random.Range(x + 1, x + 10.5f);
            yPos = Random.Range(y - 4.5f, y + 4.5f);
            selectTree = Random.Range(0, 3);
            Instantiate(trees[selectTree], new Vector3(xPos, 0, yPos), Quaternion.identity);
        }
    }
}
