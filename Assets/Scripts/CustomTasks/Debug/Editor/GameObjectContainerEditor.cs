using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (GameObjectContainer))]
public class GameObjectContainerEditor : Editor {
	private SerializedProperty m_nameSize;
	private SerializedProperty m_objectSize;

	private static string nameArraySizePath="names.Array.size";
	private static string objectArraySizePath="gameObjects.Array.size";
	private static string nameArrayPath="names.Array.data[{0}]";
	private static string objectArrayPath="gameObjects.Array.data[{0}]";

	private void AddObject(){
		m_nameSize.intValue++;
		m_objectSize.intValue++;
		serializedObject.FindProperty (string.Format (nameArrayPath, m_nameSize.intValue - 1)).stringValue = "";
		serializedObject.FindProperty (string.Format (objectArrayPath, m_objectSize.intValue - 1)).objectReferenceValue = null;
	}

	private void DeleteObjectIndex(int idx){
		for (int i = idx; i < m_nameSize.intValue-1; i++) {
			serializedObject.FindProperty (string.Format (nameArrayPath, i)).stringValue = serializedObject.FindProperty (string.Format (nameArrayPath, i + 1)).stringValue;
			serializedObject.FindProperty(string.Format(objectArrayPath, i)).objectReferenceValue = serializedObject.FindProperty(string.Format(objectArrayPath, i+1)).objectReferenceValue;
		}
		m_nameSize.intValue--;
		m_objectSize.intValue--;
	}

	private void DropAreaGUI(){
		Event evt = Event.current;
		Rect dropArea = GUILayoutUtility.GetRect (0, 50f, GUILayout.ExpandWidth (true));
		GUI.Box (dropArea, "Add Object");

		switch (evt.type) {
		case EventType.DragUpdated:
		case EventType.DragPerform:
			if (!dropArea.Contains (evt.mousePosition))
				break;

			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

			if (evt.type == EventType.DragPerform) {
				DragAndDrop.AcceptDrag ();
				foreach(Object draggedObj in DragAndDrop.objectReferences){
					if (draggedObj == null)
						continue;

					AddObject ();
					serializedObject.FindProperty(string.Format(objectArrayPath, m_objectSize.intValue-1)).objectReferenceValue = draggedObj as GameObject;
				}
			}
			Event.current.Use();

			break;
		}
	}

	public override void OnInspectorGUI(){
		serializedObject.Update ();

		GUILayout.Label ("Container Objects", EditorStyles.label);
		m_nameSize = serializedObject.FindProperty (nameArraySizePath);
		m_objectSize = serializedObject.FindProperty (objectArraySizePath);

		// draw every array object
		for (int i = 0; i < m_nameSize.intValue; i++) {
			GUILayout.BeginHorizontal ();
			SerializedProperty name = serializedObject.FindProperty (string.Format (nameArrayPath, i));
			SerializedProperty Object = serializedObject.FindProperty (string.Format (objectArrayPath, i));
			name.stringValue = GUILayout.TextField (name.stringValue, GUILayout.Width(120f));
			Object.objectReferenceValue = EditorGUILayout.ObjectField (Object.objectReferenceValue, typeof(GameObject), true, GUILayout.Width(120f));

			// delete button
			if (GUILayout.Button ("-", GUILayout.Width(20f))) {
				DeleteObjectIndex (i);
			}
			GUILayout.EndHorizontal ();
		}

		DropAreaGUI ();

		// new object button
		if (GUILayout.Button ("Add Object", GUILayout.Width(250f), GUILayout.ExpandWidth(false))) {
			AddObject ();
		}

		serializedObject.ApplyModifiedProperties ();
	}
}
