using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ParticleEffect : Action {
	public GameObject particleEffectPrefab;
	public SharedGameObject newEffectGameObject;

	public override TaskStatus OnUpdate(){
		newEffectGameObject.Value = GameObject.Instantiate (particleEffectPrefab, transform.position, Quaternion.identity) as GameObject;
		ParticleSystem particle = newEffectGameObject.Value.GetComponent<ParticleSystem> ();
		if (particle != null) {
			particle.Play ();
			return TaskStatus.Success;
		} else {
			return TaskStatus.Failure;
		}
	}
}
