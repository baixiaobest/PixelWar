using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EngageAutopilot : Action {
	public AutoPilot.AutopilotMode autopilotMode;

	private AutoPilot autopilot;
	// Use this for initialization
	public override void OnStart () {
		autopilot = GetComponent<AutoPilot> ();
	}
	
	// Update is called once per frame
	public override TaskStatus OnUpdate () {
		autopilot.EngageAutopilot (autopilotMode);
		return TaskStatus.Success;
	}
}
