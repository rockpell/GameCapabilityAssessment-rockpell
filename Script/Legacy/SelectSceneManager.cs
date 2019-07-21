using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSceneManager : MonoBehaviour {
    [SerializeField] private GameObject positions;
    [SerializeField] private GameObject evaluationDoor;
    [SerializeField] private GameObject trainingDoor;
    [SerializeField] private GameObject computerSectorPanel;
    [SerializeField] private GameObject bottomUI;

    [SerializeField] private AudioClip arriveStart;
    [SerializeField] private AudioClip arriveMiddle;
    [SerializeField] private AudioClip arriveEnd;

    private AudioSource audioSource;
    private Transform[] targetPosition;
    private MoveElevatorPlayer player;

    private float originCameraYPos;
    //private float originCameraSize;

    private float swipeSensitivity = 4;

    private int playerPositionIndex;

    private bool isEnterStart = false;

    private Vector3 originCameraPos;
    private Vector3 mousePos1, mousePos2;

    private Camera mainCamera;
    private delegate void Del();
    private delegate void Del2(float value);

	void Start () {
        targetPosition = new Transform[positions.transform.childCount];
        mainCamera = GameObject.Find("Player").transform.Find("Main Camera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<MoveElevatorPlayer>();

        playerPositionIndex = 2; // 플레이어 초기 위치 로비로 설정

        originCameraPos = mainCamera.transform.localPosition;
        originCameraYPos = mainCamera.transform.position.y;
        //originCameraSize = mainCamera.orthographicSize;

        for (int i = 0; i < positions.transform.childCount; i++)
        {
            targetPosition[i] = positions.transform.GetChild(i);
        }
        
        GameManager.Instance.InitTrigger();
    }
	
	// Update is called once per frame
	void Update () {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (!player.IsMoveable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                }
                if (Input.GetMouseButtonUp(0))
                {
                    mousePos1 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    if (mousePos2.x - mousePos1.x > swipeSensitivity) // 오른쪽 이동
                    {
                        if (playerPositionIndex < 4)
                            MoveElevatorButton(playerPositionIndex + 1);
                    }
                    else if (mousePos2.x - mousePos1.x < -swipeSensitivity) // 왼쪽 이동
                    {
                        if (playerPositionIndex > 0)
                            MoveElevatorButton(playerPositionIndex - 1);
                    }
                    
                    mousePos2 = mousePos1;
                }
            }
            
        }
    }

    public void MoveElevatorButton(int index)
    {
        if (!isEnterStart)
        {
            playerPositionIndex = index;
            player.DesignatePosition(targetPosition[index].position);
            targetPosition[5].position = new Vector3(targetPosition[index].position.x, targetPosition[5].position.y, targetPosition[5].position.z);
        }
    }

    public void EnterTraining()
    {
        if (!isEnterStart)
        {
            isEnterStart = true;
            StartCoroutine(OpenGate(trainingDoor));
            StartCoroutine(ZoomIn(MoveComputerSector(StartTraining() )));
            StartCoroutine(BottomUIDown());
        }
    }

    public void EnterEvaluation()
    {
        if (!isEnterStart)
        {
            isEnterStart = true;
            StartCoroutine(OpenGate(evaluationDoor));
            StartCoroutine(ZoomIn(MoveComputerSector(StartEvaluation() )));
            StartCoroutine(BottomUIDown());
            //StartCoroutine(IntervalMoveScene(CustomSceneManager.UIScenes.Evaluation));
        }
    }

    IEnumerator OpenGate(GameObject door)
    {
        float speed = 0.05f;
        float fracJourney = 0;

        Vector3 startPosition1 = door.transform.GetChild(0).transform.position;
        Vector3 startPosition2 = door.transform.GetChild(1).transform.position;
        Vector3 endPosition1 = new Vector3(startPosition1.x - 4.5f, startPosition1.y, startPosition1.z);
        Vector3 endPosition2 = new Vector3(startPosition2.x + 4.5f, startPosition2.y, startPosition2.z);

        while (fracJourney <= 1)
        {
            fracJourney += speed;
            door.transform.GetChild(0).position = Vector3.Lerp(startPosition1, endPosition1, fracJourney);
            door.transform.GetChild(1).position = Vector3.Lerp(startPosition2, endPosition2, fracJourney);
            yield return new WaitForSeconds(0.02f);
        }
        //yield return new WaitForSeconds(1f);
        //StartCoroutine(CloseGate(evaluationDoor, startPosition1, startPosition2, endPosition1, endPosition2, false));
    }

    IEnumerator CloseGate(GameObject door, Vector3 startPosition1, Vector3 startPosition2, Vector3 endPosition1, Vector3 endPosition2, bool isSlow)
    {
        float speed = 0.05f;
        float fracJourney = 0;

        if (isSlow)
        {
            while (fracJourney <= 1)
            {
                fracJourney += speed;
                door.transform.GetChild(0).position = Vector3.Lerp(endPosition1, startPosition1, fracJourney);
                door.transform.GetChild(1).position = Vector3.Lerp(endPosition2, startPosition2, fracJourney);
                yield return new WaitForSeconds(0.03f);
            }
        } else
        {
            fracJourney = 1;
            door.transform.GetChild(0).position = Vector3.Lerp(endPosition1, startPosition1, fracJourney);
            door.transform.GetChild(1).position = Vector3.Lerp(endPosition2, startPosition2, fracJourney);
            yield return null;
        }
    }

    IEnumerator ZoomIn(IEnumerator callback)
    {
        int temp = 40;
        while (temp > 0)
        {
            temp -= 1;
            mainCamera.orthographicSize -= 0.1f;
            mainCamera.transform.Translate(new Vector3(0, -0.03f, 0));
            yield return new WaitForSeconds(0.03f);
        }

        if (callback != null) StartCoroutine(callback);
    }

    IEnumerator ComputerRoomZoomIn(IEnumerator callback)
    {
        int temp = 40;
        while (temp > 0)
        {
            temp -= 1;
            mainCamera.orthographicSize -= 0.145f;
            mainCamera.transform.Translate(new Vector3(0.001f, 0.02f, 0));
            yield return new WaitForSeconds(0.03f);
        }
        
        if (callback != null) StartCoroutine(callback);
    }

    IEnumerator IntervalMoveScene(CustomSceneManager.UIScenes sceneName)
    {
        yield return new WaitForSeconds(2.0f);
        CustomSceneManager.Instance.LoadUIScene(sceneName);
    }

    private IEnumerator StartEvaluation()
    {
        EndUI _endUI = GameManager.Instance.CreateEndUI();
        _endUI.WhiteNoiseOn(0);
        yield return new WaitForSeconds(0.7f);
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.Evaluation);
    }

    private IEnumerator StartTraining()
    {
        EndUI _endUI = GameManager.Instance.CreateEndUI();
        _endUI.WhiteNoiseOn(0);
        yield return new WaitForSeconds(0.7f);
        CustomSceneManager.Instance.LoadUIScene(CustomSceneManager.UIScenes.TrainingScene);
    }

    private IEnumerator MoveComputerSector(IEnumerator callback)
    {
        player.transform.position = targetPosition[6].position;
        mainCamera.orthographicSize = 6.2f;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, originCameraYPos, mainCamera.transform.position.z);
        StartCoroutine(ComputerSectorFadeIn(callback));
        yield return null;
    }

    IEnumerator ComputerSectorFadeIn(IEnumerator callback)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            computerSectorPanel.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
            alpha -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(ComputerRoomZoomIn(callback));
        //StartCoroutine(ZoomIn(StartEvaluation));
    }

    IEnumerator BottomUIDown()
    {
        float _moveDistance = 0;
        //Vector3 _targetPosition = bottomUI.transform.localPosition;
        GameObject.Find("EventManager").GetComponent<EventManager>().HideSpeechBalloon();
        while (_moveDistance < 200)
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                bottomUI.transform.Translate(new Vector3(0, -20f, 0));
            }
            else
            {
                bottomUI.transform.Translate(new Vector3(0, -9f, 0));
            }
            

            //bottomUI.transform.localPosition = new Vector3(_targetPosition.x, _targetPosition.y - 12f, _targetPosition.z);
            _moveDistance += 12;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public IEnumerator CameraShake(float amount, float duration)
    {
        float _timer = 0;
        while (_timer <= duration)
        {
            //mainCamera.transform.localPosition = (Vector3)Random.insideUnitCircle * amount + originCameraPos;
            mainCamera.transform.localPosition = new Vector3(Random.insideUnitCircle.x, 0, 0) * amount + originCameraPos;
            _timer += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.localPosition = originCameraPos;
    }

    public void ArriveStartSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(arriveStart);
        Invoke("ArriveMiddleSound", 0.05f);
    }

    public void ArriveMiddleSound()
    {
        audioSource.Play();
    }

    public void ArriveEndSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(arriveEnd);
    }

}
