using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
	public Transform turret;
	public Transform mainGun;
	public Transform cam;
	public float lerp;
	public float turretTurningSpeed;

	void LateUpdate () {
		Vector3 turretLookat = cam.transform.forward - Vector3.Dot (cam.transform.forward, transform.up) * transform.up;
		float turretAngle = Vector3.Angle (turretLookat, turret.forward);
		float sign = Mathf.Sign (Vector3.Dot (Vector3.Cross (turret.forward, turretLookat), transform.up));
		if (turretAngle > 3f) {
			turret.RotateAround (turret.position, transform.up, sign * turretTurningSpeed * Time.deltaTime);
		} else {
			turret.RotateAround (turret.position, transform.up, sign * turretAngle);
		}

		if(turretAngle < 60){
			Vector3 mainGunLookat = cam.transform.forward - Vector3.Dot (cam.transform.forward, turret.right) * turret.right;
			mainGun.forward = Vector3.Lerp (mainGun.forward, mainGunLookat, lerp * Time.deltaTime);
		}
	}
}
