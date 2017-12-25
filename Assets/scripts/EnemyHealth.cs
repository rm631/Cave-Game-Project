using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	
	public int enemyHealth;
	GameObject player;
	
	void Start() {
		
		enemyHealth = MapGenerator.level; 
		
	}  
	
	void OnCollisionEnter2D (Collision2D col)
	{
		
		// If the colliding gameobject is the player and the player is attacking...
		if( col.gameObject.tag == "Player" ) {
			
			if( PlayerAttack.playerIsAttacking == true ) {
				
				enemyHealth = enemyHealth - 1; // .. Decrease the enemies health by 1
				Debug.Log ( "Enemy Health is: " + enemyHealth ); 
			}
				
			if( enemyHealth == 0 ) { // If the enemy has no health left..
				// Find all of the colliders on the gameobject and set them all to be triggers.
				Collider2D[] cols = GetComponents<Collider2D>();
				foreach(Collider2D c in cols)
				{
					c.isTrigger = true;
				}
				
				// Move all sprite parts of the player to the front
				SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
				foreach(SpriteRenderer s in spr)
				{
					s.sortingLayerName = "UI";
				}
				
				// ... disable enemy script
				GetComponent<Enemy>().enabled = false;
				
				// .. Send a message to console that an enemy has died.
				Debug.Log ( "An enemy has died." );
			}
			
		}
		
	}
	/*
	void EnemyDeath() {
		if( enemyHealth <= 0 ) {
			Destroy (gameObject); 
		} 
	}
	*/
}
