using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamgackManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bigHappySound;
    [SerializeField] private AudioClip happySound;
    [SerializeField] private AudioClip sadSound;

    public Transform[] target;
    public enum Status { BigSuccess, Success, Fail, Idle }; // 삼각이의 상태
    private Vector3[] targetPoint;
    private bool isAppear = false;
    private Status currentStatus = Status.Idle;
    private float timer = 0;
    private float maxTime = 1;
	// Use this for initialization
	private void Awake()
	{
		targetPoint = new Vector3[target.Length];
	}

	void Start ()
    {
        //AppearSamgack(false);
        //SetSamgackStatus(Status.Success);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isAppear)
        {
            float speed = 15 * Time.deltaTime;
            for(int i = 0; i< target.Length; i++)
            {
                targetPoint[i] = new Vector3(target[i].localPosition.x, -3, 0);
                target[i].localPosition = Vector3.MoveTowards(target[i].localPosition, targetPoint[i], speed);
            }
                
        }
        if (currentStatus == Status.BigSuccess)
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                int i = Random.Range(0, target.Length);
                if (!target[i].GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    target[i].GetChild(0).GetComponent<Animator>().SetTrigger("jump");
                timer = 0;
                maxTime = Random.Range(0.3f, 0.7f);
            }
        }
        else if(currentStatus == Status.Success)
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                int i = Random.Range(0, target.Length);
                if (!target[i].GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    target[i].GetChild(0).GetComponent<Animator>().SetTrigger("jump");
                timer = 0;
                maxTime = Random.Range(1f, 2f);
            }
        }
	}
    private void AllAnimatorOff()
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i].GetChild(0).GetComponent<Animator>().SetBool("big_happy", false);
            target[i].GetChild(0).GetComponent<Animator>().SetBool("happy", false);
            target[i].GetChild(0).GetComponent<Animator>().SetBool("sad", false);
            target[i].GetChild(0).GetComponent<Animator>().SetBool("sad2", false);
        }
    }

    public void SetSamgackStatus(Status status) // 삼각이의 상태를 정해준다
    {
        currentStatus = status;
        AllAnimatorOff();
        if (status == Status.BigSuccess)
        {
            for (int i = 0; i < target.Length; i++)
                target[i].GetChild(0).GetComponent<Animator>().SetBool("big_happy" ,true);
        }
        else if(status == Status.Success)
        {
            for (int i = 0; i < target.Length; i++)
                target[i].GetChild(0).GetComponent<Animator>().SetBool("happy", true);

            target[Random.Range(0, 3)].GetChild(0).GetComponent<Animator>().SetBool("happy", false);
            target[Random.Range(3, 6)].GetChild(0).GetComponent<Animator>().SetBool("happy", false);
        }
        else if(status == Status.Fail)
        {
            for (int i = 0; i < target.Length; i++)
                target[i].GetChild(0).GetComponent<Animator>().SetBool("sad", true);
            for (int i = 0; i < 2; i++)
            {
                int random = Random.Range(0, 3);
                target[random].GetChild(0).GetComponent<Animator>().SetBool("sad", false);
                target[random].GetChild(0).GetComponent<Animator>().SetBool("sad2", true);

                random = Random.Range(3, 6);
                target[random].GetChild(0).GetComponent<Animator>().SetBool("sad", false);
                target[random].GetChild(0).GetComponent<Animator>().SetBool("sad2", true);
            }
        }
        //Idle은 기본상태 이므로 생략
    }
	public void SetSamgackStatus(Result result)
	{
		SetSamgackStatus(GetStatus(result));
	}
	private Status GetStatus(Result gameResult)
	{
		Status st = Status.Idle;
		if (gameResult == Result.BigSuccessful)
			st = Status.BigSuccess;
		else if (gameResult == Result.Successful)
			st = Status.Success;
		else if (gameResult == Result.Fail)
			st = Status.Fail;
		return st;
	}

	public void AppearSamgack(bool isAnimated) // 삼각이가 밑에서 등장한다.
    {
        if(isAnimated)
        {
            isAppear = true;
            for (int i = 0; i < target.Length; i++)
                target[i].GetChild(0).GetComponent<Animator>().SetTrigger("jump");
        }
        else
        {
            for (int i = 0; i < target.Length; i++)
                target[i].localPosition = new Vector3(target[i].localPosition.x, -3, 0);
        }
        SamgackSound(currentStatus);
    }

    private void SamgackSound(Status status)
    {
        if(audioSource != null)
        {
            audioSource.loop = false;
            if (status == Status.BigSuccess)
            {
                if (bigHappySound != null)
                {
                    audioSource.clip = bigHappySound;
                    audioSource.Play();
                }
            }
            else if (status == Status.Success)
            {
                if (happySound != null)
                {
                    audioSource.clip = happySound;
                    audioSource.Play();
                }
            }
            else if (status == Status.Fail)
            {
                if (sadSound != null)
                {
                    audioSource.clip = sadSound;
                    audioSource.Play();
                }
            }
        }
        else
        {
            Debug.Log("audioSource null");
        }
    }
}
