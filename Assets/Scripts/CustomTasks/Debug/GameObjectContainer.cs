using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this script is used because of a bug in behavior tree
// behavior tree deletes drag-and-drop gameobject after build
// this script is attached to AI gameobject so that Behavior Tree can access drag-and-drop gameobject'
[ExecuteInEditMode]
public class GameObjectContainer : MonoBehaviour {
	public string[] names;
	public GameObject[] gameObjects;

	public GameObject GetObjectByName(string name){
		for (int i = 0; i < names.Length; i++) {
			if (name == names [i]) {
				if (gameObjects.Length <= i) {
					return null;
				}
				return gameObjects [i];
			}
		}
		return null;
	}
	public GameObject GetObjectByIndex(int idx){
		return gameObjects [idx];
	}
}
