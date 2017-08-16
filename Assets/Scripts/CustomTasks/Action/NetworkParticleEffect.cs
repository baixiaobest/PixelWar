using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Networking;

public class NetworkParticleEffect : Action {
	public GameObject particleEffectPrefab;
	public SharedGameObject newEffectGameObject;

	public override TaskStatus OnUpdate(){
		newEffectGameObject.Value = GameObject.Instantiate (particleEffectPrefab, transform.position, Quaternion.identity) as GameObject;
		NetworkServer.Spawn (newEffectGameObject.Value);
		ParticleSystem particle = newEffectGameObject.Value.GetComponent<ParticleSystem> ();
		if (particle != null) {
			particle.Play ();
			return TaskStatus.Success;
		} else {
			return TaskStatus.Failure;
		}
	}
}
