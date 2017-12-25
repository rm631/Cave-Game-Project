using UnityEngine;
using System.Collections;

public class MainMenuBackGround : MonoBehaviour {
	
	public static int guiDepth = 0;
	public Texture backGround;
	
	void OnGUI() {
	
	GUI.depth = guiDepth;
	
	GUI.DrawTexture (new Rect(0, 0, MainMenu.ResX, MainMenu.ResY), backGround );
	guiDepth = 1;
	MainMenu.guiDepth = 0;
	
	}
}
