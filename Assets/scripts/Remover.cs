using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Remover : MonoBehaviour
{
	//public GameObject splash;
	//public Text gameOverText;
	//public GUIText gameOver;
	
	public Text gameOverText; 
	public GameObject gameOverImage;
	
	public AudioSource deathSound;
	
	//void Start() {
	//	gameOverText = GetComponent<guiText>("GameOverText");
	//}
	
	/*
	void Start() {
		gameOver.text = "";
		
	}
	*/

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			
			deathSound.Play(); 
			//SoundManager.playDeathSound = true;
			
			// .. A player has fallen out of the map (If player is alive)
			if( PlayerHealth.playerIsAlive == true ) {
				Debug.Log ( "Player has fallen out of the map - Restarting level." );
			}
			
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;

			// .. stop the Health Bar following the player
			if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			// ... instantiate the splash where the player falls in.
			//Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
			PlayerHealth.playerIsAlive = false;
			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			//Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
		
		if( col.gameObject.tag == "Enemy" ) {
			Destroy (col.gameObject);
		}
		
	}

	IEnumerator ReloadGame()
	{
		GameOver();
		//gameOver.text = "Game Over!";	
		// ... pause briefly
		yield return new WaitForSeconds(3);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		// ... and then reset the playerIsAlive variable
		PlayerHealth.playerIsAlive = true;
	}
	
	void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOverImage.SetActive (true);
		// ... pause briefly
		//yield return new WaitForSeconds(2);
		// ... and then destroy the text
		
		MapGenerator.level = 1;
		
		Invoke ( "HideGameOverImage", 2 );
		
		//gameOverImage.SetActive(false);
	}
	
	void HideGameOverImage() {
		gameOverImage.SetActive(false);
		Invoke ( "ShowFirstLevelText", 0 );
	}
	
	public Text levelText; 
	public GameObject levelImage; 
	
	void ShowFirstLevelText() {
		
		levelText.text = "Level " + MapGenerator.level;
		levelImage.SetActive(true);
		
		//yield return new WaitForSeconds(2);
		
		Invoke( "HideLevelText", 5 );
		
	}
	
	void HideLevelText(){
		
		levelImage.SetActive(false);
		
	}
	
}
