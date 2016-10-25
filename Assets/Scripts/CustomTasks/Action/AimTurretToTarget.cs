using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AimTurretToTarget : Action {
	public SharedGameObject Target;
	public SharedGameObject Turret;
	public float bulletSpeed;
	
	// Update is called once per frame
	public override TaskStatus OnUpdate () {
		float prediectedImpactTime = (Target.Value.transform.position - Turret.Value.transform.position).magnitude / bulletSpeed;
		Vector3 predictedPosition = Target.Value.transform.position + Target.Value.GetComponent<Rigidbody> ().velocity * prediectedImpactTime;
		Turret.Value.transform.forward = predictedPosition - Turret.Value.transform.position;
		return TaskStatus.Success;
	}
}
