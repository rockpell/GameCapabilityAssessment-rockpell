using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATShootPredication : MonoBehaviour {

    [SerializeField]
    private int _lineCount;
    [SerializeField]
    private GameObject _shootPosition;
    private LineRenderer _lineRenderer;
    
	void Start ()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _lineCount;
	}
	
	void Update ()
    {	
	}

    public void Prediction(Vector2 force)
    {
        Vector2 targetForce = force;
        Vector2 targetVelocity = force * Time.fixedDeltaTime;
        Vector2 targetPosition = _shootPosition.transform.position;
        
        for (int i = 0; i < _lineCount; i++)
        {
            _lineRenderer.SetPosition(i, targetPosition);
            targetForce = new Vector2(0, - 9.81f);
            targetVelocity = targetVelocity + (targetForce * Time.fixedDeltaTime);
            targetPosition = targetPosition + (targetVelocity * Time.fixedDeltaTime);
        }

    }
}
