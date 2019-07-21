using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHCivilianControl : MonoBehaviour {

    [SerializeField] GameObject xMark;

    private float maxDistance = 1.15f;
    private float distance;
    private float speed = 5f;

    private bool isStandUp = false;
    private bool isStop = false;
    private bool isDead = false;

    //private int positionIndex;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            if (!isStandUp)
            {
                this.transform.Translate(new Vector3(0, 0.01f * speed, 0));
                distance += 0.01f * speed;
                if (distance >= maxDistance)
                {
                    isStandUp = true;
                    isStop = true;
                    distance = 0;
                }
            }
            transform.LookAt(Camera.main.transform);
        }
    }

    private IEnumerator AllowMove(float time)
    {
        yield return new WaitForSeconds(time);
        isStop = false;
    }

    public void GunShot()
    {
        if (!isDead)
        {
            Debug.Log("GunShot Civilian!");
            SBHManager.Instance.KillCivilian();
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Instantiate(xMark, targetPosition, Quaternion.identity);
            Destroy(this.gameObject, 1.0f);
            isDead = true;
        }
    }

    //public void AllocationPositionIndex(int index)
    //{
    //    positionIndex = index;
    //}
}
