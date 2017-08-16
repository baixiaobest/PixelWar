using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TurretFireGun : Action {
	public bool fireOneRound=true;  // only fire one bullet
	public SharedFloat fireForSeconds; // fire the weapon for this many seconds
	TurretAIWeaponControl weaponControl;

	public override void OnStart ()
	{
		weaponControl = GetComponent<TurretAIWeaponControl> ();
	}

	public override TaskStatus OnUpdate(){
		if (fireOneRound) {
			weaponControl.Fire ();
		} else {
			weaponControl.FireForTime (fireForSeconds.Value);
		}
		return TaskStatus.Success;
	}
}
