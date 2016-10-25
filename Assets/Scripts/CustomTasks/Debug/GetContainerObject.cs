using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetContainerObject : Action {
	public string objectName;
	public SharedGameObject getObject;

	private GameObjectContainer container;

	public override void OnAwake(){
		container = GetComponent<GameObjectContainer> ();
		if (container == null) {
			Debug.LogError ("GetContainerObject (BT Task): GameObjectContainer not found!");
		}
	}

	public override TaskStatus OnUpdate(){
		if (container == null)
			return TaskStatus.Failure;
		getObject.Value = container.GetObjectByName (objectName);
		return TaskStatus.Success;
	}
}
