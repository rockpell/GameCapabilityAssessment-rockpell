using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubjectSelectManager : MonoBehaviour
{

	//이미지 눌렀을때 반응 관리(선택시 하이라이트, 진행된 항목 색깔 변화, 
	//시작 버튼 기능, 각 항목 시작시 화면 이동하는 기능(화면을 덮는거까지만))
	[SerializeField]
	private SubjectManager subjectManager;

    [SerializeField] private GameObject startButton; //시작 버튼
    [SerializeField] private Sprite startButtonDown;
    [SerializeField] private Sprite startButtonUp;

    [SerializeField] private GameObject[] subjectButtons; // 5개의 항목 이미지 버튼
	[SerializeField] private Vector3[] cameraPoint; //5개의 항목의 카메라 이동 위치
	[SerializeField] private BackgroundImageController[] subjectImageController; // 5개의 항목 이미지 & UI

	[SerializeField] float moveSpeed; // 카메라 이동에 걸리는 시간

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource audio;

	//현재 선택된 항목
	private Subject selectSubject;

    //지금 상태
    private UIState uiState;

    //지금 클릭된 이미지 버튼
    GameObject focusedButton;

	//카메라 transform
	private Transform camTrans;

    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
		camTrans = Camera.main.transform;

		if (NewGameManager.Instance != null)
        {
            if (NewGameManager.Instance.GetNowSubject() == Subject.None) // 종합평가 선택화면 보여주기
            {
                NewGameManager.Instance.NowEvalationTime = 0; // 현재 평가 진행 횟수 초기화
            }
            else // 항목평가 화면 보여주기
            {
				Debug.Log(NewGameManager.Instance.GetNowSubject());
				camTrans.position = GetTartgetPoint(NewGameManager.Instance.GetNowSubject());
                subjectManager.EvaluationSetting(false);
            }
        }

        uiState = UIState.INIT;

        findButton();
		SetSubjectImage();

		StartCoroutine(buttonSelect());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	private void SetSubjectImage()
	{
		foreach(Subject sub in System.Enum.GetValues(typeof(Subject)))
		{
			if (NewGameManager.Instance.GetEndSubjectList().Contains(sub)) // 입력한 값이 존재할 경우 이미 진행한 항목
			{
				ClearSubject(sub);
			}
			else
			{
				ActivateSubject(sub);
			}
		}
	}
	private void SelectSubject(Subject subject)
    {
		foreach (Subject sub in System.Enum.GetValues(typeof(Subject)))
			InactivateSubject(sub);

        audio.loop = false;
        audio.Play();
        ActivateSubject(subject);
	}

    public void StartEvaluation() // 선택한 항목 평가 시작, 항목 평가 화면으로 전환도 해줘야한다.
    {
        StartCoroutine(StartEvaluationRoutine(GetTartgetPoint(selectSubject)));
		NewGameManager.Instance.SetNowSubjet(selectSubject);
	}
	IEnumerator StartEvaluationRoutine(Vector3 targetPoint)
	{
		while (true)
		{
			float distance = Vector3.Distance(camTrans.position, targetPoint);
			float speed = distance * moveSpeed;
			if (speed < 1f)
			{
				speed = 1f;
				//Debug.Log("loW");
			}

			speed *= (Time.deltaTime / (1 / 60f));
			//Debug.Log((Time.deltaTime / (1 / 60f)));

			camTrans.position = Vector3.MoveTowards(camTrans.position, targetPoint, speed);
			if (camTrans.position == targetPoint)
				break;
			yield return null;
		}
		//subjectManager 세팅함수 호출 필요
		//===========================================================================================//
		Debug.Log("Done");
		subjectManager.EvaluationSetting(true);
	}
	private void findButton()
    {
        //subjectButtons[ ( int )Subject.Aiming ] = GameObject.Find( "Aiming" );
        //subjectButtons[ ( int )Subject.Concentration ] = GameObject.Find( "Concentration" );
        //subjectButtons[ ( int )Subject.Quickness ] = GameObject.Find( "Quickness" );
        //subjectButtons[ ( int )Subject.RhythmicSense ] = GameObject.Find( "RhythmicSense" );
        //subjectButtons[ ( int )Subject.Thinking ] = GameObject.Find( "Thinking" );

        //subjectButtons[ 5 ] = GameObject.Find( "GameStart" );
    }
    IEnumerator buttonSelect()
    {
        while( uiState == UIState.INIT || uiState == UIState.SELECTED )
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null && hit.transform.tag == "RaycastTarget")
                {
                    focusedButton = hit.transform.gameObject;

                    if (focusedButton.name.Equals("GameStart"))
                    {
                        startButton.GetComponent<SpriteRenderer>().sprite = startButtonDown;
                    }
                }
            }

            if ( Input.GetMouseButtonUp( 0 ))
            {
                startButton.GetComponent<SpriteRenderer>().sprite = startButtonUp;

                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

				if(hit.collider != null && hit.transform.tag == "RaycastTarget")
				{
					focusedButton = hit.transform.gameObject;

                    Debug.Log( focusedButton.transform.name );
                    audio.clip = clips[1];
                    if( focusedButton.name.Equals( "GameStart" ) )
                    {
                        if ( uiState == UIState.SELECTED )//이미 게임이 선택 되어 있다면 게임 시작
                        {
                            uiState = UIState.GAMESTART;
                            audio.clip = clips[0];
                            audio.Play();

                            StartEvaluation();
							//Debug.Log("Start");
						}
                        else// 선택이 안된 상태에서 누르면 아무것도 안함
                        {
                            audio.clip = null;
							//Debug.Log("Not Start");
						}
                    }
                    else if( focusedButton.name.Equals( "Aiming" ) )
                    {
						if (!NewGameManager.Instance.GetEndSubjectList().Contains(Subject.Aiming))
						{
                            subjectSelected(Subject.Aiming);
						}
                    }
                    else if( focusedButton.name.Equals( "Concentration" ) )
                    {
						if (!NewGameManager.Instance.GetEndSubjectList().Contains(Subject.Concentration))
						{
                            subjectSelected(Subject.Concentration);
                        }
                    }
                    else if( focusedButton.name.Equals( "Quickness" ) )
                    {
						if (!NewGameManager.Instance.GetEndSubjectList().Contains(Subject.Quickness))
						{
                            subjectSelected(Subject.Quickness);
                        }
                    }
                    else if( focusedButton.name.Equals( "RhythmicSense" ) )
                    {
						if (!NewGameManager.Instance.GetEndSubjectList().Contains(Subject.RhythmicSense))
						{
                            subjectSelected(Subject.RhythmicSense);
                        }
                    }
                    else if( focusedButton.name.Equals( "Thinking" ) )
                    {
						if (!NewGameManager.Instance.GetEndSubjectList().Contains(Subject.Thinking))
						{
                            subjectSelected(Subject.Thinking);
                        }
                    }
                }
            }
            yield return null;
        }

            yield return null;
    }

    public void subjectSelected(Subject input)
    {
        uiState = UIState.SELECTED;
        SelectSubject(input);
        selectSubject = input;
    }

    private Vector3 GetTartgetPoint(Subject targetSubject)
    {
        Vector3 result = Vector3.zero;
        if (targetSubject == Subject.Aiming)
            result = cameraPoint[0];
        else if (targetSubject == Subject.Concentration)
            result = cameraPoint[1];
        else if (targetSubject == Subject.RhythmicSense)
            result = cameraPoint[2];
        else if (targetSubject == Subject.Quickness)
            result = cameraPoint[3];
        else if (targetSubject == Subject.Thinking)
            result = cameraPoint[4];
        return result;
    }

    private int GetIndex(Subject targetSubject)
	{
		int result = -1;
		if (targetSubject == Subject.Aiming)
			result = 0;
		else if (targetSubject == Subject.Concentration)
			result = 1;
		else if (targetSubject == Subject.RhythmicSense)
			result = 2;
		else if (targetSubject == Subject.Quickness)
			result = 3;
		else if (targetSubject == Subject.Thinking)
			result = 4;

		return result;
	}

	//private void setFocus()
	//{

	//}

	private void InactivateSubject(Subject subject) // 항목 비활성화
    {
		if (subject == Subject.None)
			return;

		if(!NewGameManager.Instance.GetEndSubjectList().Contains(subject))
			subjectImageController[GetIndex(subject)].Inactive();
		else
			subjectImageController[GetIndex(subject)].Clear();
		//print(subject.ToString() + " : Inact");
	}
	private void ActivateSubject(Subject subject) // 항목 활성화
	{
		if (subject == Subject.None)
			return;

		if (!NewGameManager.Instance.GetEndSubjectList().Contains(subject))
			subjectImageController[GetIndex(subject)].Active();
		else
			subjectImageController[GetIndex(subject)].Clear();
		//print(subject.ToString() + " : act");
	}
	private void ClearSubject(Subject subject) // 항목 클리어 표시
	{
		if (subject == Subject.None)
			return;

		if (NewGameManager.Instance.GetEndSubjectList().Contains(subject))
			subjectImageController[GetIndex(subject)].Clear();
		else
			subjectImageController[GetIndex(subject)].Active();
		//print(subject.ToString() + " : clear");
	}

    public bool IsGameStart()
    {
        if (uiState == UIState.GAMESTART)
            return true;
        else
            return false;
    }

    enum UIState
	{
		INIT, SELECTED, GAMESTART
	}
}