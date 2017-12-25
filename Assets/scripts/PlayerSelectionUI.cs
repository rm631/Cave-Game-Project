using UnityEngine;
using System.Collections;

public class PlayerSelectionUI : MonoBehaviour {
	
	public static bool updateCamera;
	
	public GameObject player2D;
	public GameObject player3D;
	public GameObject horseman2D;
	public GameObject guy2D;

	public GameObject pauseBackground;

	public Font myFont;
	public Texture cavemanTexture;

	public static bool showPause = false;
	bool updating = false;
	bool showCharacters = false;
	
	string currentPlayer = "Caveman";
	
	void OnGUI() {

		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.black;
		myStyle.fontSize = 42;

		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = myFont;
		buttonStyle.normal.textColor = Color.white;
		buttonStyle.hover.textColor = Color.grey;
		buttonStyle.fontSize = 16;

		if(Pauser.paused) {
			
			if(showPause) {
				GUI.Label(new Rect(680, 100, 100, 100), "Game Is Paused", myStyle);
				
				updateCamera = false;
				
				if(GUI.Button(new Rect(810, 210, 300, 100), "Resume", buttonStyle)) {
					Pauser.paused = false;
					pauseBackground.SetActive(false);
					Time.timeScale = 1;
				}
				if(GUI.Button(new Rect(810, 320, 300, 100), "New Game", buttonStyle)) {
					Application.LoadLevel( "cave 2D" );//Application.LoadLevel(1);
				}
				if(GUI.Button(new Rect(810, 430, 300, 100), "Select Character", buttonStyle)) {
					showCharacters = true;
					showPause = false;
				}
				if(GUI.Button(new Rect(810, 540, 300, 100), "Return To Menu", buttonStyle)) {
					Application.LoadLevel( "Main Menu" );
				}
				if(GUI.Button(new Rect(810, 650, 300, 100), "Quit", buttonStyle)) {
					Application.Quit();
				}
			}
			
			if(showCharacters) {
				if(GUI.Button(new Rect(30, 740, 300, 100), "Caveman", buttonStyle)) {
					updateCamera = true;
					
					updating = true;
					Debug.Log ("Current Player is: " + currentPlayer);
					ChangeCharacterToCaveman();
					if( currentPlayer == "player3D" ) {
						player2D.transform.position = player3D.transform.position;
					}
					if( currentPlayer == "Horseman" ) {
						player2D.transform.position = horseman2D.transform.position;
					}
					if( currentPlayer == "Guy2D" ) {
						player2D.transform.position = guy2D.transform.position;
					}
					Invoke("ChangeCharacterToCaveman", 5);
				}
				GUI.DrawTexture(new Rect(55, 400, 250, 400), cavemanTexture, ScaleMode.ScaleToFit);

				if(GUI.Button(new Rect(340, 740, 300, 100), "Horseman", buttonStyle)) {
					updateCamera = true;
					
					updating = true;
					Debug.Log ("Current Player is: " + currentPlayer);
					ChangeCharacterToHorseman();
					if( currentPlayer == "Caveman" ) {
						horseman2D.transform.position = player2D.transform.position;
					}
					if( currentPlayer == "player3D" ) {
						horseman2D.transform.position = player3D.transform.position; 
					}
					if( currentPlayer == "Guy2D" ) { 
						horseman2D.transform.position = guy2D.transform.position; 
					}
					Invoke("ChangeCharacterToHorseman", 5); 
				}

				if(GUI.Button(new Rect(655, 740, 300, 100), "Guy Withsword", buttonStyle)) { 
					updateCamera = true;
					
					updating = true;
					Debug.Log ("Current Player is: " + currentPlayer);
					ChangeCharacterToGuy2D();
					if( currentPlayer == "Caveman" ) {
						guy2D.transform.position = player2D.transform.position;
					}
					if( currentPlayer == "player3D" ) {
						guy2D.transform.position = player3D.transform.position;
					}
					if( currentPlayer == "Horseman" ) {
						guy2D.transform.position = horseman2D.transform.position;
					}
					Invoke("ChangeCharacterToGuy2D", 5);
				}
				
			}
			
		}
		
	}
	
	void ChangeCharacterToCaveman() {
		currentPlayer = "Caveman";
		player3D.SetActive(false);
		horseman2D.SetActive(false);
		guy2D.SetActive(false);
		player2D.SetActive(true);
		Debug.Log ("The character has been changed to Caveman.");
		showCharacters = false;
		showPause = true;
	}
	
	void ChangeCharacterToHorseman() {
		currentPlayer = "Horseman";
		player3D.SetActive(false);
		horseman2D.SetActive(false);
		guy2D.SetActive(false);
		player2D.SetActive(true);
		Debug.Log ("The character has been changed to Horseman.");
		showCharacters = false;
		showPause = true;
	}
	
	void ChangeCharacterToGuy2D() {
		currentPlayer = "Guy2D";
		player3D.SetActive(false);
		horseman2D.SetActive(false);
		guy2D.SetActive(false);
		player2D.SetActive(true);
		Debug.Log ("The character has been changed to guy 2D.");
		showCharacters = false;
		showPause = true;
	}
	
}
