using UnityEngine;
using System.Collections;

public class Propeller : MonoBehaviour {
	public float idleSpinSpeed;
	public float maxSpinSpeed;
	public float engineStopTime;

	private AirplanePhysics plane;
	private AudioSource engineSound;
	private float currSpin = 0;

	// Use this for initialization
	void Start () {
		plane = transform.parent.GetComponent<AirplanePhysics> ();
		engineSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		float thrust = plane.GetCurrentThrustPercentage ();
		if (thrust != 0) {
			currSpin = idleSpinSpeed + (maxSpinSpeed - idleSpinSpeed) * thrust;
			engineSound.pitch = 1 + 2 * thrust;
			if (!engineSound.isPlaying)
				engineSound.Play ();
		} else {
			if (engineSound.pitch <= 0) {
				engineSound.pitch = 0;
				engineSound.Stop ();
			} else {
				engineSound.pitch -= 1/engineStopTime * Time.deltaTime;
			}
			currSpin = currSpin <= 0 ? 0: currSpin - 1/engineStopTime * idleSpinSpeed * Time.deltaTime;
		}
		transform.Rotate (-Vector3.up * currSpin * Time.deltaTime);
	}
}
