using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

// lock on target, return success
// lost target, return failure
// to lock on target requires that target is in line of sight of AI
public class TargetLocked : Conditional {
	public SharedGameObject enemy;

	public float visionRange = 20f;
	public LayerMask layerMask;

	public override TaskStatus OnUpdate ()
	{
		Vector3 relativePos = enemy.Value.transform.position - transform.position;
		if (relativePos.magnitude > visionRange) {
			return TaskStatus.Failure;
		}

		// test for line of sight
		RaycastHit hitinfo;
		if (Physics.Raycast (transform.position, relativePos, out hitinfo, relativePos.magnitude, layerMask)) {
			// vision occluded
			return TaskStatus.Failure;
		} else {
			return TaskStatus.Success;
		}
	}
}
