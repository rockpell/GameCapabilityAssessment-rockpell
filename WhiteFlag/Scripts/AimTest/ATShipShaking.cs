using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATShipShaking : MonoBehaviour {

    public float _shakeAngle;

	void Start ()
    {
    }
	
	void Update ()
    {
        ShakingShip();
	}

    public void SetShakeAngle(float windForce) { _shakeAngle = windForce / 10;}

    private void ShakingShip()
    {
        float curAngle = this.transform.rotation.eulerAngles.z;
        if (curAngle > 180) curAngle = curAngle - 360;

        if (_shakeAngle > 0 && curAngle < _shakeAngle)
            this.transform.rotation *= Quaternion.Euler(0, 0, _shakeAngle * Time.deltaTime);
        else if (_shakeAngle < 0 && curAngle > _shakeAngle)
            this.transform.rotation *= Quaternion.Euler(0, 0, _shakeAngle * Time.deltaTime);
        else
            _shakeAngle = -_shakeAngle;
    }
}
