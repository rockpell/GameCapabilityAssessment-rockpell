using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenTouch : MonoBehaviour
{
    public bool PC = false;              //true = PC, false = Phone
    public GameObject cat_attack;
    private Touch tempTouchs;

	[SerializeField]
	private int maxTouch;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //////////////PC
        if (PC)
        {
            if (ChickenGameController.life == true)
            {
                if (Input.GetMouseButton(0))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 catPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        catPosition -= new Vector3(0, 0, Camera.main.transform.position.z + 1);
                        Instantiate(cat_attack, catPosition, Quaternion.identity);  //고양이 손바닥 모양 생성
                        RaycastHit hit1 = new RaycastHit();
                        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray1, out hit1))
                        {
                            if (hit1.collider.tag == "Mouse")
                            {
                                ChickenGameController.Combo++;        //콤보 증가
                                GameObject.Find("GameController").GetComponent<ChickenGameController>().Showcombo();
                                hit1.collider.tag = "Mousedie";
                                ChickenSoundController.instance.playMouseSound();
                                Destroy(hit1.collider.gameObject, 0.5f);
                            }
                        }
                    }
                }
            }
        }
        //////////////PC
        
        /////////////Phone
        else
        {
            if (ChickenGameController.life == true)
            {
                if (Input.touchCount > 0)
                {
					int touchCount = 0;
                    for (int i = 0; i < Input.touchCount; i++)
                    {
						if (touchCount >= maxTouch)
							break;

                        tempTouchs = Input.GetTouch(i);
                        if (tempTouchs.phase == TouchPhase.Began)
                        {
							touchCount++;
                            Vector2 tempPosition = Camera.main.ScreenToWorldPoint(tempTouchs.position);
                            Vector3 catPosition = new Vector3(tempPosition.x, tempPosition.y, -1);
                            Instantiate(cat_attack, catPosition, Quaternion.identity);
                            RaycastHit hit1 = new RaycastHit();
                            Ray ray1 = Camera.main.ScreenPointToRay(tempTouchs.position);
                            if (Physics.Raycast(ray1, out hit1))
                            {
                                if (hit1.collider.tag == "Mouse")
                                {
                                    hit1.collider.tag = "Mousedie";
                                    ChickenGameController.Combo++;        //콤보 증가
                                    ChickenSoundController.instance.playMouseSound();
                                    Destroy(hit1.collider.gameObject, 0.5f);
                                }
                            }
                        }
                    }
                }
            }
        }
        /////////////Phone
    }
}
