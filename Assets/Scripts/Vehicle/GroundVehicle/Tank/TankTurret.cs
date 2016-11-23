using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour {
	public Transform turret;
	public Transform mainGun;
	public Transform cam;
	public float lerp;
	public float turretTurningSpeed;
	public float maxUpAngle;
	public float maxDownAngle;

	// use late update because turret should update later than update of tank body
	void LateUpdate () {
		// by removing up component of where camera is looking at
		// you get where turret should look at
		Vector3 turretLookat = cam.transform.forward - Vector3.Dot (cam.transform.forward, turret.up) * turret.up;
		float turretAngle = Vector3.Angle (turretLookat, turret.forward);
		float sign = Mathf.Sign (Vector3.Dot (Vector3.Cross (turret.forward, turretLookat), turret.up));  // direction of turret rotation
		if (turretAngle > 3f) {
			turret.RotateAround (turret.position, turret.up, sign * turretTurningSpeed * Time.deltaTime);
		} else {
			turret.RotateAround (turret.position, turret.up, sign * turretAngle);
		}
			
		if(turretAngle < 60){
			// by removing left or right component of where camera is looking at
			// you get where main gun should look at
			Vector3 mainGunLookat = cam.transform.forward - Vector3.Dot (cam.transform.forward, turret.right) * turret.right;
			float mainGunAngle = Vector3.Angle (turret.up, mainGunLookat);
			if (mainGunAngle < 90 - maxUpAngle) {
				mainGunLookat = Quaternion.AngleAxis (-maxUpAngle, mainGun.right) * turret.forward;
			} else if (mainGunAngle > 90 + maxDownAngle) {
				mainGunLookat = Quaternion.AngleAxis (maxDownAngle, mainGun.right) * turret.forward;
			}
			mainGun.forward = Vector3.Lerp (mainGun.forward, mainGunLookat, lerp * Time.deltaTime);
		}
	}
}
