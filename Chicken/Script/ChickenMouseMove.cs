using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMouseMove : MonoBehaviour
{

    public Transform food;
    public Transform Mouse;
    public static float Speed = 5;
    public static int Damage = 10;
    public Sprite sprMousedie;
    private Animator anima;
    private int comboCount = 0;
    public static bool DamagedON = false;


    // Use this for initialization
    void Start()
    {
        food = GameObject.Find("Chicken").transform;
        anima = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            ChickenGameController.Combo = 0;        //콤보 초기화
            ChickenGameController.Score = ChickenGameController.Score - Damage;
            ChickenSoundController.instance.playEatSound();
            GameObject.Find("GameController").GetComponent<ChickenGameController>().Damaged();

            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (ChickenGameController.life == true)
        {
            if(this.gameObject.tag == "Mouse")
            {
                anima.SetBool("Die", false);  // 죽는 애니메이션 끄기
                Vector3 dir = (transform.position - food.position).normalized;   // normalized 정규화.(방향값만 주기 위함)
                transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180);
                if (this.gameObject.tag == "Mouse")
                {
                    transform.position = Vector3.MoveTowards(transform.position, food.transform.position, Speed * Time.deltaTime);  // 이 오브젝트의 위치가 food의 위치로 이동함
                }
            }
            else if(this.gameObject.tag == "Mousedie")
            {
                Vector3 savePosition = transform.position;
                this.gameObject.transform.position = new Vector3(savePosition.x, savePosition.y, 3);
                anima.SetBool("Die",true);
                if(ChickenGameController.life == false)
                {
                    anima.SetBool("life", false);
                }
            }
        }
    }
}
