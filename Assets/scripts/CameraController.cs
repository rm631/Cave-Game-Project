using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player; //Initalises a public variable called player, it stores a GameObject
	
	private Vector3 offset; //Initalises a private variable called offset, stores a Vector3 value

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player"); //The player variable is assigned the value of the game object tagged 'player' eg. the player
		offset = transform.position - player.transform.position; //Assigns offset a value
	}
	
	//void Update() {
	//	if(PlayerSelectionUI.updateCamera = false) { //If the updateCamera variable is set to false in the PlayerSelectionUI script then..
	//		player = GameObject.FindGameObjectWithTag("Player"); //Reset the the variable of player for the new player
	//	}
	//}
	
	void Update ()
	{
		transform.position = player.transform.position + offset; //Sets the position of the camera to the pkayer and then offsets the camera but the offset variable
	}
}