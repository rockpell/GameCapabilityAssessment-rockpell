using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSVegetableControl : MonoBehaviour {

    public GameObject[] _slice;
    public float[] _sliceArea;
    public float _slicePosition;

	void Start ()
    {
		
	}

    public void SliceVegetable(float curXPos)
    {
        if(_sliceArea.Length > 1)
        {
            int index = 0;
            for(int i = 0; i < _sliceArea.Length; i++)
            {
                if(curXPos < _sliceArea[i])
                {
                    index = i;
                    break;
                }
            }
            if(curXPos <= _sliceArea[index])
            {
                Instantiate(_slice[index], new Vector3(curXPos - _slicePosition, 0, 0.3f), this.transform.rotation);
            }
            
        }
        else
        {
            if(curXPos <= _sliceArea[0])
                Instantiate(_slice[0], new Vector3(curXPos - _slicePosition, 0, 0.3f), this.transform.rotation);
        }
    }

}
