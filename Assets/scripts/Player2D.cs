using UnityEngine;
using System.Collections;

public class Player2D : MonoBehaviour {

	/*
	Rigidbody2D rigidbody;
	Vector2 velocity;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized * 10;
	}
	
	void FixedUpdate() {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}
	*/

	//player
	public float speed = 2f; //declare the player move speed in Unity inspector
	public int jumpHeight = 237; //this is set in the Unity Inspector
	//public bool isGrounded = false; //this can be seen working in the Unity inspector
	//public Transform groundedEnd; //declares the empty game object in Unity acting as a collider set to the position of the player

	private float canJump = 0f;
	private Animator animator;
	private int health; 

	Rigidbody2D rigidbody;

	//public GameObject target;
	//float curDir = 0f; // compass indicating direction
	//Vector3 curNormal = Vector3.up; // smoothed terrain normal
	//public float turn;  
	
	/*
	// Update is called once per frame
	void Update () 
	{
		
		curDir = (curDir + turn) % 360; // rotate angle modulo 360 according to input
		RaycastHit hit;
		if (Physics.Raycast(target.transform.position, -curNormal, out hit))
		{
			curNormal = Vector3.Lerp(curNormal, hit.normal, 4*Time.deltaTime);
			Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
			target.transform.rotation = grndTilt * Quaternion.Euler(0, curDir, 0);
		}
	}
	*/
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();

		animator = GetComponent<Animator>(); 

		RefreshKeyBinds();

		//moveRight = KeyCode.D;
		//moveLeft = KeyCode.A;
		//jump = KeyCode.Space;
	}

	public static bool refreshKeybindRequest = false;

	private KeyCode moveRight;
	private KeyCode moveLeft;
	private KeyCode jump;

	void FixedUpdate () 
	{
		//Movement (); //call the movement function below
		//isGrounded = Physics2D.Linecast(this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer("BlockingLayer")); 
		//the above line of code draws a linecast downwards to detect the ground game objects that have been placed in a ground layer
		
		if( MapGenerator.canMove ) {	
			
			//GetComponent<Rigidbody2D>() = RigidbodyConstraints2D.None;
			rigidbody.isKinematic = false;
			//Move Right
			if (Input.GetKey (KeyCode.D)) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector2(0,0);
				animator.SetTrigger("Running");
			}
			//Move Left
			if (Input.GetKey (KeyCode.A)) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector2(0,180); //flip the character on its x axis
				animator.SetTrigger("Running");
			}
			//Jump by detecting if the user presses W and then checking to see if the linecast is touching the ground
			if (Input.GetKeyDown (KeyCode.Space) && Time.time > canJump) //&& isGrounded == true)
			{
				//Add force to the players rigidbody allowing it to move upwards if the above if statement is true
				GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpHeight); 
				canJump = Time.time + 1f; //1.5f 
				
				animator.SetTrigger("Jump");
				
			}
		} else if( !MapGenerator.canMove ) {
			//RigidbodyConstraints2D = RigidbodyConstraints2D.FreezeAll;
			rigidbody.isKinematic = true;
		}
	}

	void Update() {
		if(refreshKeybindRequest) {
			RefreshKeyBinds();
		}
	}

	public void RefreshKeyBinds() {
		refreshKeybindRequest = false;
		moveRight = Controls.keyBinds [ "PlayerRight" ];
		moveLeft = Controls.keyBinds [ "PlayerLeft" ];
		jump = Controls.keyBinds[ "PlayerJump" ];
	}

	/*
	void Movement()
	{
		//Move Right
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,0); 
		}
		//Move Left
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,180); //flip the character on its x axis
		}
		//Jump by detecting if the user presses W and then checking to see if the linecast is touching the ground
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > canJump) //&& isGrounded == true)
		{
			//Add force to the players rigidbody allowing it to move upwards if the above if statement is true
			GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpHeight); 
			canJump = Time.time + 1.5f; 
			
		}
		
	}
*/
	private void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
	/*
	private void CheckIfGameOver() {
		if( health <= 0 ) {
			GameManager.instance.GameOver ();
		}
	}
	*/
}