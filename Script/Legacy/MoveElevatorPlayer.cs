using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevatorPlayer : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoveable;
    private float journeyLength;
    private float fracJourney;
    private float speed = 0.05f;

    private SelectSceneManager selectSceneManager;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        targetPosition = transform.position;
        isMoveable = false;

        selectSceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isMoveable)
        {
            fracJourney += speed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fracJourney);
            if(transform.position == targetPosition)
            {
                isMoveable = false;
                StartCoroutine(GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>().CameraShake(0.5f, 0.1f));
                selectSceneManager.ArriveEndSound();
            }
        }
	}

    public void DesignatePosition(Vector3 position)
    {
        if(position != targetPosition)
        {
            fracJourney = 0;
            targetPosition = position;
            startPosition = transform.position;
            isMoveable = true;
            selectSceneManager.ArriveStartSound();
        }
    }

    public bool IsMoveable {
        get { return isMoveable; }
    }
}
