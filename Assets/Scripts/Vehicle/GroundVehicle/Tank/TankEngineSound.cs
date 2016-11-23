using UnityEngine;
using System.Collections;

public class TankEngineSound : MonoBehaviour {
	public TankPhysics tankPhy;
	public AudioSource engineAudioSource;
	public AudioClip engineAudioClip;

	private bool engineStart = false;

	// Use this for initialization
	void Start () {
		GetComponent<ControlRegistration> ().RegisterControl += EngineStart;
		GetComponent<ControlRegistration> ().UnregisterControl += EngineStop;
	}

	void EngineStart(){
		engineAudioSource.clip = engineAudioClip;
		engineAudioSource.Play ();
		engineStart = true;
	}

	void EngineStop(){
		engineAudioSource.Stop ();
		engineStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (engineStart) {
			engineAudioSource.pitch = 1f + Mathf.Abs (tankPhy.GetAccelerationPercentage ()) * 0.5f + tankPhy.GetSpeedPercentage();
		}
	}
}
