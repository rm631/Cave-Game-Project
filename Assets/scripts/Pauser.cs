using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {
	// Images are gameobjects and text is Text
	public GameObject pauseBackground;
	//public Text pausedText;
	//public AudioSource background;
	//public AudioSource pauseMusic;
	
	public static bool paused = false;
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyUp( Controls.pause ) ) //|| ( Input.GetKeyUp(KeyCode.P) ) )
		{
			//PlayerSelectionUI.showPause = !PlayerSelectionUI.showPause;
			//showPause = !showPause;
			PauseGame();
		}
		
	}
	
	void PauseGame() {
		paused = !paused;
		//PlayerSelectionUI.showPause = !PlayerSelectionUI.showPause;

		if(paused) {
			Time.timeScale = 0;
			pauseBackground.SetActive(true);
			Debug.Log( "Pause menu open." );
			PlayerSelectionUI.showPause = true;
			//background.mute = true;
			//pauseMusic.Play();
		} else {
			Time.timeScale = 1;
			pauseBackground.SetActive(false);
			Debug.Log( "Pause menu closed." );
			PlayerSelectionUI.showPause = false;
			//background.mute = false;
			//pauseMusic.mute = true;
		}
		
	}
	
}
