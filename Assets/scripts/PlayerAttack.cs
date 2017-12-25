using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	
	public static bool playerIsAttacking = false;
	//public bool variableForInspectorToSeeIfPlayerIsAttacking = false;
	
	private Animator anim;						// Reference to the Animator on the player
	
	void Awake() {
		anim = GetComponent<Animator>(); // Get the animator on the player and assign to private variable
	}
	
	void Update() {
		if( Input.GetKey (Controls.playerAttack) ) { // If the W key is pressed.. // Originally GetKeyDown - this proved to be too slow 
			anim.SetTrigger("playerChop"); // Play the playerChop animation
			playerIsAttacking = true;
			Debug.Log ( "Player is attacking." );
			
			//yield return new WaitForSeconds(1.5f);
			
			Invoke ( "NoLongerAttacking", 1f);
			
			//playerIsAttacking = false;
			//Debug.Log ( "Player is no longer attacking." );
		}
	}
	
	void NoLongerAttacking() {
		
		playerIsAttacking = false;
		Debug.Log ( "Player is no longer attacking." );
		
	}
	
}
