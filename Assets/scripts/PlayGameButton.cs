using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour {

	//public string Level;

	void OnMouseDown() {
		//if(  ) {
			Application.LoadLevel( "cave 2D" );
			Debug.Log ( "Play button was pressed, loading Cave 2D." );
		//}
	}

}
