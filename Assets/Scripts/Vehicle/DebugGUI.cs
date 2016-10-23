using UnityEngine;
using System.Collections;

public class DebugGUI : MonoBehaviour {

	public delegate void GUIcallback ();
	public static event GUIcallback DebugGUICallback;

	void OnGUI(){
		if (DebugGUICallback != null)
			DebugGUICallback ();
	}
}
