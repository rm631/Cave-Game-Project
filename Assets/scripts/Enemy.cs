using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	//public Transform sightStart, sightEnd;
	//public Transform target;
	//public int HP = 2;					// How many times the enemy can be hit before it dies.
	
	public Transform target;
	public float moveSpeed;
	public float collideWithOtherEnemyForce = 50f;
	
	private Transform myTransform;
	private bool moving;
	private bool targetting;
	//private Animator animator;
	private bool idle;
	private Animator anim;						// Reference to the Animator on the player
	
	void Awake ()
	{
		myTransform = transform; 
		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start ()
	{
		//animator = GetComponent<Animator> ();
		//enemyhealth = GetComponent<EnemyHealth> ();
		if( PlayerHealth.playerIsAlive == true ) {
		
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		
		target = go.transform;
		
		if (moving) {
			moving = false;
			//animator.SetBool ("Moving", false);
		}
		if (moveSpeed > 0) {
			moving = true;
			//animator.SetBool ("Moving", true);
		}
		//if (enemyhealth.hurt == true) {
		//	moveSpeed = 0;
		//}
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate (){
		
		if( PlayerHealth.playerIsAlive == true ) {
			
			Debug.DrawLine (target.position, myTransform.position, Color.red);
			
			float dist = Vector2.Distance(target.position, transform.position);
			
			if (dist < 5) {
				moveSpeed = 1f;
			}
			if (dist > 5) {
				moveSpeed = 0f;
			}
			if (target.position.x < myTransform.position.x)
				myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime; // player is left of enemy, move left
				
			else if (target.position.x > myTransform.position.x)
				myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime; // player is right of enemy, move right
			//Enemy face target
				
			if (target.position.x < myTransform.position.x)
				transform.eulerAngles = (moveSpeed > 0)?Vector2.up * 1:Vector2.zero;
			if (target.position.x > myTransform.position.x)
				transform.eulerAngles = (moveSpeed > 0)?Vector2.up * 180:Vector2.zero;	
				
		}
	}
	
	void OnCollisionWithOtherEnemy(Collider2D col, Transform enemy) {
		
		if(col.gameObject.tag == "Enemy") {
			Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
			GetComponent<Rigidbody2D>().AddForce(hurtVector * collideWithOtherEnemyForce);
		}
		
	}
	
	void OnCollsionWithPlayer( Collider2D col ) {
		if(col.gameObject.tag == "Player") {
			anim.SetTrigger("enemyAttack");
			Debug.Log ( "Enemy attack animation has been played." );
		}
	}
	
}