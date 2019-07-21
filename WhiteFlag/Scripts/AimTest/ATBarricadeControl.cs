using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATBarricadeControl : MonoBehaviour {

    [SerializeField]
    private GameObject _targetArea;
    [SerializeField]
    private GameObject[] _barricadeSet;

    private int _level;

	void Start ()
    { }

    public void SettingBarricade(int level)
    {
        if (level > 1)
        {
            for (int i = 0; i <= (level - 2) / 2; i++)
            {
                _barricadeSet[i].SetActive(true);
            }
        }
    }
}
