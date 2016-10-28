using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDead : Conditional {

	private Health health;

	public override void OnAwake () {
		health = GetComponent<Health> ();
	}
	
	public override TaskStatus OnUpdate(){
		if (health == null)
			return TaskStatus.Failure;
		if (health.IsDead ())
			return TaskStatus.Success;
		else
			return TaskStatus.Failure;
	}
}
