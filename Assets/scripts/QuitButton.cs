using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

	public void OnClick() {
		
		Application.Quit();
		Debug.Log ( "Quit Button has been clicked, closing application." );
		
	}

}