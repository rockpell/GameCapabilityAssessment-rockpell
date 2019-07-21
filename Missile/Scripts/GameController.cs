using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject Enemy;
    public GameObject Alarm;
    public float delay;  //Enmey 생성시간 간격
	// Use this for initialization
	void Start () {
        StartCoroutine(EnemySpwan());  //Enemy 를 생성하는 코루틴 함수를 실행시킴
	}
	
    IEnumerator EnemySpwan()  //Enemy를 딜레이마다 생성함.
    {
        float spwanpos_x = -20;  //생성될 고정된 x좌표 위치
        float spwanpos_y;  //생성될 y좌표 위치
        while (true)  //무한루프 나중에 Life 같은걸로 대체함
        {
            spwanpos_y = Random.Range(-3.0f,6.0f);  //Enemy가 생성될 랜덤한 y좌표
            Vector3 spwanpos = new Vector3(spwanpos_x, spwanpos_y, 0);
            Vector3 Alarmpos = new Vector3(-8, spwanpos_y, 0);  //Alarm 이 생성될 Vector3 좌표
            Instantiate(Enemy, spwanpos, Quaternion.identity);  //Enemy 생성
            Instantiate(Alarm, Alarmpos, Quaternion.identity);  //Alarm 생성
            //Debug.Log(spwanpos);
            yield return new WaitForSeconds(delay);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
