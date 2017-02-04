using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetAirplaneThrottle : Action {
	public float throttle;

	private AirplanePhysics planePhy;

	public override void OnAwake () {
		planePhy = GetComponent<AirplanePhysics> ();
	}

	public override TaskStatus OnUpdate () {
		planePhy.SetThrottle (throttle);
		return TaskStatus.Success;
	}
}
