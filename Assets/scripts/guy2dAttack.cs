using UnityEngine;
using System.Collections;

public class guy2dAttack : MonoBehaviour {

	private Animator anim;						// Reference to the Animator on the player
	
	void Awake() {
		anim = GetComponent<Animator>(); // Get the animator on the player and assign to private variable
	}
	
	void Update() {
		if( Input.GetKey (KeyCode.W) ) { // If the W key is pressed.. // Originally GetKeyDown - this proved to be too slow 
			anim.SetTrigger("Attack"); // Play the playerChop animation
			PlayerAttack.playerIsAttacking = true;
			Debug.Log ( "Player is attacking." );
			
			//yield return new WaitForSeconds(1.5f);
			
			Invoke ( "NoLongerAttacking", 1.5f);
			
			//playerIsAttacking = false;
			//Debug.Log ( "Player is no longer attacking." );
		}
	}
	
	void NoLongerAttacking() {
		
		PlayerAttack.playerIsAttacking = false;
		Debug.Log ( "Player is no longer attacking." );
		
	}
	
}
