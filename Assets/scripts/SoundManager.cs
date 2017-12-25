using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioSource backgroundSource;
	public GameObject background;
	public AudioSource backgroundextremeSource;
	public GameObject backgroundextreme;
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.		
	public static bool playDeathSound = false;

	int musicChange;
	/*
	void OnLevelWasLoaded() {
		//background.SetActive(true);
		//backgroundextreme.SetActive(false);
		//backgroundSource.Play();
		//backgroundextremeSource.Stop();
		Instance();
	}

	void Awake ()
	{
		Instance();
	}

	void Instance() {
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}
	*/
	void Start() {
		
		if (Application.isEditor) {
			musicChange = 5;
			Debug.Log ( "Unity is in Editor, the music will change on level 5." );
		} else {
			musicChange = 15;
			Debug.Log ( "Not in Unity Editor, the music will change on level 15." );
		}
		
		//background = GameObject.FindGameObjectWithTag( "DefaultBackgroundMusic" );
		//backgroundextreme = GameObject.FindGameObjectWithTag( "BackgroundMusic" );
		
		//backgroundSource = background.GetComponent<AudioSource>();
		//backgroundextremeSource = backgroundextreme.GetComponent<AudioSource>();
		
	}

	void Update() {

		if( MapGenerator.level >= musicChange ) { // 15 for the game, 5 for testing.
			ChangeMusic();
		} //else if( MapGenerator.level <= musicChange ) {
		//	DefaultMusic();
		//}

		if( !PlayerHealth.playerIsAlive ) {
			StopBackgroundMusic();
		}
		
		//background = GameObject.Find( "BackgroundMusic" );
		//backgroundextreme = GameObject.Find( "BackgroundMusic_Extreme" );

		//backgroundSource = background.GetComponent<AudioSource>();
		//backgroundextremeSource = backgroundextreme.GetComponent<AudioSource>();

	}
	
	void ChangeMusic() {
		background.SetActive(false);
		backgroundextreme.SetActive(true);
		//backgroundSource.Stop();
		//backgroundextremeSource.Play();

		//while( MapGenerator.level >= musicChange ) {
		//	background.SetActive(false);
		//	backgroundextreme.SetActive(true);
		//}
		
	}

	void DefaultMusic() {
		//while( MapGenerator.level <= musicChange ) {
		//	background.SetActive(true);
		//	backgroundextreme.SetActive(false);
		//}
	}

	void StopBackgroundMusic() {
		background.SetActive(false);
		backgroundextreme.SetActive(false);
		//backgroundSource.Stop ();
		//backgroundextremeSource.Stop();
	}

}
