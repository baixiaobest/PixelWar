using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

/*
This simulate human vision of AI. If hostile is present in different visual area,
player has different probability of been found. To determine if on a update the
player should be found, poisson process is used.

if player is within sight within certain time ago, node returns success; otherwise, node returns failure
*/

public class PlayerWithinSight : Conditional {
	public string enemyTag;

	//define radii
	public float closeAreaRadius;
	public float farAreaRadius;
	public float sideCloseAreaRadius;
	public float sideFarAreaRadius;
	public float backAreaRadius;

	// probability is defined as probability of enemies being found
	// in one second.
	public float closeAreaProbability;
	public float farAreaProbability;
	public float sideCloseAreaProbability;
	public float sideFarAreaProbability;
	public float backAreaProbability;

	public float frontAngle;  // angle of close area and far area

	public LayerMask visionLayerMask;  // layer that can block AI's vision

	public SharedGameObject sightedEnemy; // gameobject detected by AI
	public float seenExpireTime=0.5f;
	// AI eye and enemy raycast gameobject should be direct child of AI at the moment
	public string AIEyeName;  // gameobject for eye position of AI	
	public string enemyRaycastGameobjectName; // ray is cast from AI eye to this game object to test visibility

	private Transform eyeTransform;   // position of AI's eye
	private GameObject[] enemies;     // array of enemies could possibly see
	private float lastTimeSeen;       // last time enemy is seen

	// called during tree initialization
	// multiples enemies are found. This mean if an enemy is spawn later, it can not be detected
	// random seed is set by current time
	// timer is setup
	public override void OnAwake(){
		enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		if (backAreaRadius > farAreaRadius) {
			Debug.LogError ("PlayerWithinSight: backAreaRadius should be smaller or equal to farAreaRadius");
		}
		lastTimeSeen = -seenExpireTime;

		eyeTransform = transform.Find(AIEyeName);
		if (eyeTransform == null) {
			Debug.LogError ("PlayerWithinSight: AI eye name is not defined");
		}
	}

	// To let AI see the enemy requires that enemy is within range, enemy being in different area 
	// has different probability of been seen by AI. To determine if enemy is seen by AI, AI toss a coin
	// using poisson process. After tossing the coin and AI decides the enemy should be seen, a visual
	// occulsion test is performed; it uses raycast to enemy to see anything is blocking the vision.
	// If there is nothing blocking the vision, then enemy is seen and return success, otherwise, returns failure.
	public override TaskStatus OnUpdate () {
		float timeSinceLastSeen = Time.time - lastTimeSeen;
		if (timeSinceLastSeen < seenExpireTime) {
			return TaskStatus.Success;
		}

		float randomNumber = Random.value;
		// do calculations on all enemies
		for (int i = 0; i < enemies.Length; i++) {
			if (enemies [i] == null)
				continue;
			Vector3 enemyRelativePos = enemies [i].transform.Find(enemyRaycastGameobjectName).position - eyeTransform.position;
			float distanceToEnemy = enemyRelativePos.magnitude;
			float angle = Vector3.Angle (eyeTransform.forward, enemyRelativePos);
			float probability = 0;

			// Within Range ?
			if (distanceToEnemy > Mathf.Max(farAreaRadius,sideFarAreaRadius))
				continue;

			//determine the probability based on relative position
			if (angle <= 0.5f * frontAngle) { // front side
				if(distanceToEnemy >= closeAreaRadius){
					probability = farAreaProbability;
				}else{
					probability = closeAreaProbability;
				}
			} else if (angle <= 90){ // left or right side
				if (distanceToEnemy >= sideCloseAreaRadius) {
					probability = sideFarAreaProbability;
				} else {
					probability = sideCloseAreaProbability;
				}
			}else { // back side
				if (distanceToEnemy <= backAreaRadius) {
					probability = backAreaProbability;
				} else {
					continue;
				}
			}

			//test for visual occlusion
			RaycastHit hitInfo;
			if(Physics.Raycast(eyeTransform.position, enemyRelativePos, out hitInfo, distanceToEnemy, visionLayerMask)){ //occlusion
				continue;
			}else{ //no occlusion, test passed!
				//poisson process
				float DeltaTime = Time.deltaTime;
				if(randomNumber <= DeltaTime * probability){
					sightedEnemy.Value = enemies [i];
					lastTimeSeen = Time.time;
					return TaskStatus.Success;
				}
			}
		}
		return TaskStatus.Failure;
	}
}