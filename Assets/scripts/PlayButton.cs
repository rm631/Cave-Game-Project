using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public void OnClick() {

		Application.LoadLevel( "cave 2D" );
		Debug.Log ( "Play Button has been clicked." );

	}

}
