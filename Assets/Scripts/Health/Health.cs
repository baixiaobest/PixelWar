using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health :  NetworkBehaviour{

	[SyncVar]
	public int health=100;

	public void TakeDamage(int amount){
		if (!isServer)
			return;
		health -= amount;
		if (health < 0)
			health = 0;
	}

	public void Heal(int amount){
		health += amount;
	}

	public int GetHealth(){
		return health;
	}

	public bool IsDead(){
		return health == 0;
	}
}
