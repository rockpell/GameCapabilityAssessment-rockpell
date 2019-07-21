using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHEnemyControl : MonoBehaviour {

    private enum State { STAND_UP = 0, STAND_DOWN = 1, MOVE = 2}

    [SerializeField] private GameObject damagedEffect;
    [SerializeField] private GameObject damagedUI;

    private float maxDistance = 1.15f;
    private float distance;
    private float upDownSpeed = 5f;
    private float speed = 0.01f;
    private float fracJourney;
    private float warigariSpeedPercent = 0.1f;

    private bool isStop = false;
    private bool isMovealbe = false;
    private bool isDead = false;
    private bool isAimed = false;

    private int positionIndex;
    private State state = 0; // 0 : 일어서기 1 : 앉기 2 : 움직이기 

    private Vector3 startPosition;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start () {
        targetPosition = this.transform.position;
        startPosition = this.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isStop)
        {
            if (state == State.STAND_UP) // 일어서기
            {
                this.transform.Translate(new Vector3(0, 0.01f * upDownSpeed, 0));
                distance += 0.01f * upDownSpeed;
                if (distance >= maxDistance)
                {
                    isStop = true;
                    distance = 0;
                    state = State.STAND_DOWN;
                    StartCoroutine(AllowUpDown(1.0f * warigariSpeedPercent));
                }
            }
            else if(state == State.STAND_DOWN)// 앉기
            {
                this.transform.Translate(new Vector3(0, -0.01f * upDownSpeed, 0));
                distance += 0.01f * upDownSpeed;
                if (distance >= maxDistance)
                {
                    isStop = true;
                    distance = 0;
                    if (isMovealbe && isAimed && RandomAimedAvoid(SBHManager.Instance.RandomAimedAvoidPercent))
                    {
                        state = State.MOVE;
                        AllocationMovePosition();
                        StartCoroutine(AllowUpDown(0.1f));
                    } else
                    {
                        state = State.STAND_UP;
                        StartCoroutine(AllowUpDown(1.0f * warigariSpeedPercent));
                    }
                }
            }
            else if(state == State.MOVE) // 움직이기
            {
                if(startPosition == targetPosition)
                {
                    fracJourney = 1;
                }
                else
                {
                    fracJourney += speed;
                    if (fracJourney > 1) fracJourney = 1;
                    transform.position = Vector3.Lerp(startPosition, targetPosition, fracJourney);
                }

                if (fracJourney == 1)
                {
                    isStop = true;
                    state = 0;
                    isAimed = false;
                    StartCoroutine(AllowUpDown(1.0f * warigariSpeedPercent));
                }
            }

            transform.LookAt(Camera.main.transform);
        }
    }

    private IEnumerator AllowUpDown(float time)
    {
        yield return new WaitForSeconds(time);
        isStop = false;
    }

    private IEnumerator Move()
    {
        yield return null;
    }

    public void GunShot()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("GunShot!");
            SBHManager.Instance.KillEnemy();
            DeadProcess();
        }
    }

    public void HeadShot()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Head Shot!");
            SBHManager.Instance.KillEnmeyHead();
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        SBHManager.Instance.FreeEnemyPositionIndex(positionIndex);
        Instantiate(damagedEffect, transform.position, Quaternion.identity);
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Instantiate(damagedUI, targetPosition, Quaternion.identity);
        SBHSoundManger.Instance.PlayScreamAudio();
        Destroy(this.gameObject);
    }

    private void AllocationMovePosition()
    {
        positionIndex = SBHManager.Instance.AllocationMovePosition(positionIndex);
        targetPosition = SBHManager.Instance.IndexToFindPosition(positionIndex, true);
        startPosition = this.transform.position;
        fracJourney = 0;
    }

    public void AllocationMovePositionIndex(int num)
    {
        positionIndex = num;
    }

    public void AllowMove()
    {
        isMovealbe = true;
    }

    public void SpeedUp(float multiple)
    {
        speed *= multiple;
    }

    public void UpDownSpeedUp(float multiple)
    {
        upDownSpeed *= multiple;
    }

    public float WarigariSpeedPercent {
        get { return warigariSpeedPercent; }
        set { warigariSpeedPercent = value; }
    }

    public void Aimed()
    {
        isAimed = true;
    }

    private bool RandomAimedAvoid(int percent)
    {
        int _randValue = Random.Range(0, 100);
        Debug.Log("_randValue: " + _randValue);
        if(_randValue < percent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
