using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SBHInputManager : MonoBehaviour {

    [SerializeField] private new GameObject camera;
    [SerializeField] GameObject bulletPrefab;

    private Vector3 mousePos1, mousePos2;
    private Vector3 touchPos1, touchPos2;

    private bool isClickStart = false;
    private bool isTouchStart = false;

    private float mouseDPI = 6.0f;

    private SBHManager sbhManager;

    void Start () {
        sbhManager = GameObject.Find("SBHMaster").GetComponent<SBHManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (EventSystem.current.IsPointerOverGameObject(t.fingerId) == false)
                {
                    if (t.phase == TouchPhase.Began) isTouchStart = true;
                    if (t.phase == TouchPhase.Moved)
                    {
                        touchPos1 = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, -Camera.main.transform.position.z));
                        if (isTouchStart)
                        {
                            isTouchStart = false;
                            touchPos2 = touchPos1;
                        }
                        MoveView(touchPos1 - touchPos2);
                        touchPos2 = touchPos1;
                    }
                }
            }
        } else
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (Input.GetMouseButtonDown(0)) isClickStart = true;
                if (Input.GetMouseButton(0))
                {
                    mousePos1 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    if (isClickStart)
                    {
                        isClickStart = false;
                        mousePos2 = mousePos1;
                    }
                    MoveView(mousePos1 - mousePos2);
                    mousePos2 = mousePos1;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot(false);
            }
        }

        AimEnemy();
    }

    private void MoveView(Vector3 pos)
    {
        Vector3 rotateValue = new Vector3(pos.y, -pos.x, 0) * mouseDPI;

        camera.transform.localEulerAngles = camera.transform.localEulerAngles - rotateValue;

        if (60 < camera.transform.localEulerAngles.y  && camera.transform.localEulerAngles.y < 100)
        {
            camera.transform.localEulerAngles = new Vector3(camera.transform.localEulerAngles.x, 60, camera.transform.localEulerAngles.z);
        }
        else if (300 > camera.transform.localEulerAngles.y && camera.transform.localEulerAngles.y > 100)
        {
            camera.transform.localEulerAngles = new Vector3(camera.transform.localEulerAngles.x, 300, camera.transform.localEulerAngles.z);
        }

        if (20 < camera.transform.localEulerAngles.x && camera.transform.localEulerAngles.x < 40)
        {
            camera.transform.localEulerAngles = new Vector3(20, camera.transform.localEulerAngles.y, camera.transform.localEulerAngles.z);
        }
        else if (320 < camera.transform.localEulerAngles.x && camera.transform.localEulerAngles.x < 340)
        {
            camera.transform.localEulerAngles = new Vector3(340, camera.transform.localEulerAngles.y, camera.transform.localEulerAngles.z);
        }
    }

    public void Shoot(bool isFakeShoot)
    {
        if (sbhManager.IsShoot())
        {
            if(!isFakeShoot) sbhManager.Shoot();
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit))
            {
                ShootProcess(hit.transform, true);
            }

            if (!isFakeShoot)
            {
                float _compensationValue = 0.15f;
                Vector3 _cameraPosition = camera.transform.position;
                Vector3[] _positions = new Vector3[4];

                _positions[0] = _cameraPosition + Vector3.right * _compensationValue;
                _positions[1] = _cameraPosition + Vector3.left * _compensationValue;
                _positions[2] = _cameraPosition + Vector3.up * _compensationValue;
                _positions[3] = _cameraPosition + Vector3.down * _compensationValue;

                for(int i = 0; i < _positions.Length; i++)
                {
                    if (Physics.Raycast(_positions[i], camera.transform.TransformDirection(Vector3.forward), out hit))
                        ShootProcess(hit.transform, false);
                }
            }
        }
    }

    private void ShootProcess(Transform target, bool isKillCivilian)
    {
        if (target.tag == "Enemy")
        {
            if (target.name == "Head")
            {
                SBHEnemyControl enemyControl = target.parent.GetComponent<SBHEnemyControl>();
                enemyControl.HeadShot();
            }
            else if (target.name.Contains("Enemy"))
            {
                SBHEnemyControl enemyControl = target.GetComponent<SBHEnemyControl>();
                enemyControl.GunShot();
            }
        }
        else if (target.tag == "Unit")
        {
            if (isKillCivilian)
            {
                SBHCivilianControl civilianControl = target.GetComponent<SBHCivilianControl>();
                civilianControl.GunShot();
            }
        }
        else if (target.tag == "Glass")
        {
            target.gameObject.SetActive(false);
            SBHSoundManger.Instance.PlayGlassAudio();
            Shoot(true);
        }
    }

    private void AimEnemy()
    {
        Transform _cameraTran = camera.transform;

        RaycastHit hit;
        float _maxDistance = 20;
        int _enmeyMask = 1 << 13;

        if (Physics.BoxCast(_cameraTran.position, _cameraTran.lossyScale / 2 * 3, _cameraTran.forward, out hit, _cameraTran.rotation, _maxDistance, _enmeyMask))
        {
            AimProcess(hit.transform);
        }
    }

    //void OnDrawGizmos()
    //{
    //    Transform cameraTran = camera.transform;
    //    float maxDistance = 20;
    //    int enmeyMask = 1 << 13;
    //    RaycastHit hit;
    //    // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
    //    bool isHit = Physics.BoxCast(cameraTran.position, cameraTran.lossyScale / 2 * 3, cameraTran.forward, out hit, cameraTran.rotation, maxDistance, enmeyMask);

    //    Gizmos.color = Color.red;
    //    if (isHit)
    //    {
    //        Gizmos.DrawRay(cameraTran.position, cameraTran.forward * hit.distance);
    //        Gizmos.DrawWireCube(cameraTran.position + cameraTran.forward * hit.distance, cameraTran.lossyScale * 3);
    //    }
    //    else
    //    {
    //        Gizmos.DrawRay(cameraTran.position, cameraTran.forward * maxDistance);
    //    }
    //}

    private void AimProcess(Transform target)
    {
        if(target.tag == "Enemy")
        {
            if (target.name.Contains("Enemy"))
            {
                SBHEnemyControl enemyControl = target.GetComponent<SBHEnemyControl>();
                enemyControl.Aimed();
            }
        }
    }
}
