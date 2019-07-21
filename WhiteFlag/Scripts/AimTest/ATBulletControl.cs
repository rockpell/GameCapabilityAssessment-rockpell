using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATBulletControl : MonoBehaviour {

    public float _setVertexTime;
    private float _curVectexTime;
    private int _setVertexCount;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private GameObject _burstEffect;

	void Start ()
    {
        GameObject.Find("TargetArea").GetComponent<ATTargetCreate>()._clearGame = false;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
	}
	
    public void ShootBullet(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction);
    }

    public void AddWindForce(Vector3 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        GameObject.Find("Cannon").GetComponent<LineRenderer>().positionCount = _lineRenderer.positionCount;
        GameObject.Find("Cannon").GetComponent<LineRenderer>().SetPositions(positions);
        Instantiate(_burstEffect,this.transform.position,Quaternion.identity);

        Destroy(this.gameObject);
    }

    void Update ()
    {
        _curVectexTime += Time.deltaTime;
        if (_setVertexTime <_curVectexTime)
        {
            _setVertexCount = _lineRenderer.positionCount;
            _lineRenderer.positionCount = _lineRenderer.positionCount + 1;

            _lineRenderer.SetPosition(_setVertexCount, this.transform.position);
            _curVectexTime = 0;
        }
	}
}
