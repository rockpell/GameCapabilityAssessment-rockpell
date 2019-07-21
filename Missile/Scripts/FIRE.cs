using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRE : MonoBehaviour {
    public Transform Tank;  //Tank의 위치정보
    public GameObject Bullet;  //발사할 Bullet 오브젝트
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire_Bullet()  //Bullet을 발사하는 함수
    {
        Vector3 BulletPOS = new Vector3(Tank.position.x, Tank.position.y,0);  //Bullet이 발사될 Tank의 위치값
        Quaternion BulletAngle = Tank.rotation;  //Bullet이 발사될 Tank의 회전값
        Instantiate(Bullet,BulletPOS,BulletAngle);  //Bullet 생성
    }
}
