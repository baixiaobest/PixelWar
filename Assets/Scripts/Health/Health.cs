using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health=100;

	public void TakeDamage(int amount){
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
