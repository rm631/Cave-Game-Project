using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour {
	
	public GameObject pauseBackground;
	
	public void OnClick() {
		
		//Pauser.paused = false;
		Time.timeScale = 1;
		pauseBackground.SetActive(false);
		Debug.Log( "Game has been resumed." );
		
	}
	
}
