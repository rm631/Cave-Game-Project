using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour {
	
	public static KeyCode moveRight = KeyCode.D;
	public static KeyCode moveLeft = KeyCode.A;
	public static KeyCode jump = KeyCode.Space;
	public static KeyCode playerAttack = KeyCode.W;
	public static KeyCode pause = KeyCode.Escape;

	void Start() {
		SavedKeyBinds();
	}

	public static Dictionary < string, KeyCode > keyBinds = new Dictionary< string, KeyCode >();

	public void SavedKeyBinds() {

		keyBinds.Add ( "PlayerRight", KeyCode.D ); // Right
		keyBinds.Add ( "PlayerLeft", KeyCode.A ); // Left
		keyBinds.Add ( "PlayerJump", KeyCode.Space ); // Jump
		keyBinds.Add ( "PlayerAttack", KeyCode.W ); // Attack
		keyBinds.Add ( "PauseGame", KeyCode.Escape ); // Pause
		keyBinds.Add ( "DeveloperConsole", KeyCode.BackQuote ); // Console
		keyBinds.Add ( "DeveloperConsoleMac", KeyCode.Delete );

	}
// http://www.pathtogamedev.com/2015/05/17/how-to-create-custom-keybind-scripts-in-unity-c/
}
