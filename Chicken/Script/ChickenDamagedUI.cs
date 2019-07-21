using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChickenDamagedUI : MonoBehaviour {
    float delay = 0.01f;
    float speed = 0.3f;
	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<Text>().text = "- " + string.Format("{0:N0}", ChickenMouseMove.Damage);
        StartCoroutine(DamagedUI());        //오브젝트가 생성되었을때 코루틴 함수를 실행시킴.

    }
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator DamagedUI()       //데미지 받았을때 UI를 생성시키는 함수.
    {
        int temp = 50;
        while (temp > 0)
        {
            temp -= 1;
            this.transform.Translate(0, speed, 0);
            yield return new WaitForSeconds(delay);
        }
        Destroy(this.gameObject);
    }

}
