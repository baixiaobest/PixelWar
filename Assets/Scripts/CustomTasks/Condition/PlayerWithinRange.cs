using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


//find any gameobject within the range
public class PlayerWithinRange : Conditional {
	public string enemyTag;
	public float range;
	private GameObject[] enemies;
	public SharedGameObject target;
	public override void OnAwake(){
		enemies = GameObject.FindGameObjectsWithTag (enemyTag);
	}
	public override TaskStatus OnUpdate(){
		foreach (GameObject enemy in enemies) {
			if (enemy != null && (transform.position - enemy.transform.position).magnitude <= range) {
				target.Value = enemy;
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Failure;
	}
}
