using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSanta : MonoBehaviour
{
    public static MazeSanta instance;

    public bool isOn = false;
    public bool isCamMove = false;
    public Animator animator;

    private float speed = 4f;
    private List<Vector3> playerPosList;  
    private float camMovePos = 20.0f;
    private int currentMazeIndex = 0;
    private int currentPosIndex = 0;
    public List<Transform> playerMovePos;
    public bool isSelect = false;
    private float startDelayTime = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
        
    }
    private void Start()
    {
        playerMovePos.Clear();
        animator = GetComponent<Animator>();
        StartCoroutine( MazeUIManager.instance.PlayerPointArrow());
    }
    private void Update()
    {
        if (isOn && !isCamMove)
        {
            startDelayTime += Time.deltaTime;
            Vector3 dir = playerMovePos[currentPosIndex].position - transform.position;
            if (currentPosIndex < playerMovePos.Count)
            {
                if (Vector3.Distance(playerMovePos[currentPosIndex].position, transform.position) < 0.2f)
                {
                    currentPosIndex++;
                }
            }
            //딜레이추가-> 산타가 눕는것을 방지(?)
            if (startDelayTime > 0.2f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }
            if (!animator.GetBool("run")) animator.SetBool("run", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //선물을 얻었을때
        if (other.gameObject.tag == "Gift")
        {
            //결과 수정 필요
            MazeUIManager.instance.giftNum++;
            StartCoroutine(MazeUIManager.instance.BonusTime());
            Destroy(other.gameObject);
            isCamMove = true;
            playerMovePos.Clear();
            isOn = false;
            GameObject.Find("WayPoints").GetComponent<MazeWaypoint>().ListClear();
            currentPosIndex = 0;
            animator.SetBool("run", false);
            StartCoroutine(CameraMove());
            if(currentMazeIndex < MazeUIManager.instance.totalGiftNum - 1) currentMazeIndex++;
            transform.position = playerPosList[currentMazeIndex];
        }
        
    }
    public void InputPlayerPos(List<Vector3> playerPos)
    {
        //플레이어가 갈 위치 저장
        playerPosList = new List<Vector3>();
        playerPosList = playerPos;
    }

    public IEnumerator CameraMove()
    {
        if (MazeUIManager.instance.giftNum < MazeUIManager.instance.totalGiftNum)
        {
            Vector3 moveCameraPos = Camera.main.transform.position;
            moveCameraPos.x += 20;
            while (Camera.main.transform.position.x < camMovePos - 0.005f)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveCameraPos, Time.deltaTime * 2.5f);
                yield return new WaitForSeconds(0.001f);
                if (Camera.main.transform.position.x >= camMovePos - 0.005f)
                {
                    Camera.main.transform.position = moveCameraPos;
                    StartCoroutine(MazeUIManager.instance.PlayerPointArrow());
                }
            }
            camMovePos += 20;
            isCamMove = false;
            yield return null;
        }
    }
}