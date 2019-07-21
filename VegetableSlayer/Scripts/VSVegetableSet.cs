using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSVegetableSet : MonoBehaviour {

    public GameObject[] _vegetable;
    public Vector3 _settingPos;

    private void Awake()
    {
        Instantiate(_vegetable[Random.Range(0, _vegetable.Length)], _settingPos, Quaternion.identity);
    }

}
