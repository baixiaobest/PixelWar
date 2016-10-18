using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public AnimationCurve recoilZ;
	public AnimationCurve recoilY;
	public float recoilTime = 0.5f;
	public Transform scopeAlignment;

	private float recoilTimer;
	protected bool triggerPressed=false;
	protected Vector3 GunOriginalRelativePosition;   // Gun relative position to parent
	private Vector3 GunInitialRelativePosition;
	private Vector3 alignmentVec;

	public void Start(){
		GunOriginalRelativePosition = transform.localPosition;
		GunInitialRelativePosition = GunOriginalRelativePosition;
		recoilTimer = recoilTime;
		alignmentVec = transform.localPosition + Vector3.Scale(scopeAlignment.transform.localPosition, transform.localScale);
	}

	public void TriggerPressed(){
		triggerPressed = true;
	}
	public void TriggerUp(){
		triggerPressed = false;
	}
	virtual public void Fire(){}

	public void StartRecoil(){
		recoilTimer = 0;
	}

	public void AnimateRecoil(){
		if (recoilTimer < recoilTime) {
			transform.localPosition = GunOriginalRelativePosition + new Vector3 (0, recoilY.Evaluate (recoilTimer / recoilTime), recoilZ.Evaluate (recoilTimer / recoilTime));
			recoilTimer += Time.deltaTime;
		} else {
			transform.localPosition = GunOriginalRelativePosition;
		}
	}

	public void UseScope ()
	{
		GunInitialRelativePosition = GunOriginalRelativePosition;
		GunOriginalRelativePosition = new Vector3 (GunOriginalRelativePosition.x-alignmentVec.x, GunOriginalRelativePosition.y-alignmentVec.y, GunOriginalRelativePosition.z);
	}

	public void UnScope(){
		GunOriginalRelativePosition = GunInitialRelativePosition;
		transform.position = GunOriginalRelativePosition;
	}
}
