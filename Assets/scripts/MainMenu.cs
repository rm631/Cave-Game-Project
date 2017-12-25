using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	

	private bool showSettings = false;
	private bool showGraphics = false;
	private bool showBinds = false;
	private bool showVolume = false;
	
	private bool showMasterVolume = false;

	public Font myFont;

	public float shadowDrawDistance;
	public static int ResX = 1920;
	public static int ResY = 1080;
	public bool Fullscreen;
	
	float masterVolume = 1.0f;
	float sfxVolume = 1.0f;
	float musicVolume = 1.0f;

	private bool awaitingPlayerInput = false;
	private int[] allPossibleKeyBinds = {
		(int) KeyCode.A,
		(int) KeyCode.B,
		(int) KeyCode.C,
		(int) KeyCode.D,
		(int) KeyCode.E,
		(int) KeyCode.F,
		(int) KeyCode.G,
		(int) KeyCode.H,
		(int) KeyCode.I,
		(int) KeyCode.J,
		(int) KeyCode.K,
		(int) KeyCode.L,
		(int) KeyCode.M,
		(int) KeyCode.N,
		(int) KeyCode.O,
		(int) KeyCode.P,
		(int) KeyCode.Q,
		(int) KeyCode.R,
		(int) KeyCode.S,
		(int) KeyCode.T,
		(int) KeyCode.U,
		(int) KeyCode.U,
		(int) KeyCode.W,
		(int) KeyCode.X,
		(int) KeyCode.Y,
		(int) KeyCode.Z,
		(int) KeyCode.Space,
		(int) KeyCode.Tab,
		(int) KeyCode.Keypad0,
		(int) KeyCode.Keypad1,
		(int) KeyCode.Keypad2,
		(int) KeyCode.Keypad3,
		(int) KeyCode.Keypad4,
		(int) KeyCode.Keypad5,
		(int) KeyCode.Keypad6,
		(int) KeyCode.Keypad7,
		(int) KeyCode.Keypad8,
		(int) KeyCode.Keypad9,
		(int) KeyCode.Alpha0,
		(int) KeyCode.Alpha1,
		(int) KeyCode.Alpha2,
		(int) KeyCode.Alpha3,
		(int) KeyCode.Alpha4,
		(int) KeyCode.Alpha5,
		(int) KeyCode.Alpha6,
		(int) KeyCode.Alpha7,
		(int) KeyCode.Alpha8,
		(int) KeyCode.Alpha9,
		(int) KeyCode.UpArrow,
		(int) KeyCode.DownArrow,
		(int) KeyCode.RightArrow,
		(int) KeyCode.LeftArrow,
		(int) KeyCode.BackQuote,
		(int) KeyCode.Mouse0,
		(int) KeyCode.Mouse1,
		(int) KeyCode.Mouse2,
		(int) KeyCode.Mouse3,
		(int) KeyCode.Mouse4,
		(int) KeyCode.Mouse5,
		(int) KeyCode.Mouse6,
		(int) KeyCode.Plus,
		(int) KeyCode.Comma,
		(int) KeyCode.Period,
		(int) KeyCode.Slash,
		(int) KeyCode.Colon,
		(int) KeyCode.Semicolon,
		(int) KeyCode.F1,
		(int) KeyCode.F2,
		(int) KeyCode.F3,
		(int) KeyCode.F4,
		(int) KeyCode.F5,
		(int) KeyCode.F6,
		(int) KeyCode.F7,
		(int) KeyCode.F8,
		(int) KeyCode.F9,
		(int) KeyCode.F10,
		(int) KeyCode.F11,
		(int) KeyCode.F12,
		(int) KeyCode.F13,
		(int) KeyCode.F14,
		(int) KeyCode.F15,
	} ;

	//AudioListener main;
	//string volumeDisplay;// = volume;
	//string masterVolumeTitle = "Master Volume";
	//float volumeTimesThousand;

	// Use this for initialization
	void Start () {
		showSettings = false;  
		showGraphics = false; 
		showBinds = false;
		showVolume = false; 
		//volumeDisplay = volume;

		//main = Camera.main.GetComponent<AudioListener>();
	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.volume = masterVolume;
		//volumeDisplay = volume;
		//masterVolumeTitle = "Master Volume";

		if( awaitingPlayerInput == true ) {
			for(int i = 0; i < allPossibleKeyBinds.Length; i++) {
				if(Input.GetKeyDown ( ( KeyCode ) allPossibleKeyBinds [i] ) ) {
					Controls.keyBinds.Add ( bindAddString, ( KeyCode )allPossibleKeyBinds [i] );
					Debug.Log ( bindAddString + " is now bound to " + ( KeyCode )allPossibleKeyBinds [i] );
					awaitingPlayerInput = false;
					Player2D.refreshKeybindRequest = true;
				}
			}
		}

	}
	
	public static int guiDepth = 1;
	
	void OnGUI() {
	
		GUI.depth = guiDepth;
		guiDepth = 1;
		
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 50;

		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.font = myFont;
		buttonStyle.normal.textColor = Color.white;
		buttonStyle.hover.textColor = Color.grey;
		buttonStyle.fontSize = 16;

		GUIStyle button = new GUIStyle(GUI.skin.button);
		button.font = myFont;
		button.normal.textColor = Color.white;
		button.hover.textColor = Color.grey;
		button.fontSize = 12;

		GUIStyle label = new GUIStyle(GUI.skin.label);
		label.font = myFont;
		label.normal.textColor = Color.white;
		//buttonStyle.hover.textColor = Color.grey;
		label.fontSize = 12;

		GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
		boxStyle.font = myFont;
		boxStyle.normal.textColor = Color.white;
		boxStyle.hover.textColor = Color.grey;
		boxStyle.fontSize = 16;
		
		GUI.Label(new Rect(30, 100, 100, 100), "Caveman", myStyle);

		if(GUI.Button(new Rect(30, 210, 300, 100), "Resume", buttonStyle)) {

		}
		if(GUI.Button(new Rect(30, 320, 300, 100), "New Game", buttonStyle)) {
			Application.LoadLevel( "cave 2D" );//Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(30, 430, 300, 100), "Load Game", buttonStyle)) {
			
		}
		if(GUI.Button(new Rect(30, 540, 300, 100), "Settings", buttonStyle)) {
			//Application.Quit();
			//showGraphics = !showGraphics;
			showSettings = !showSettings;
			if(showGraphics) { 
				showGraphics = false;
			}
			if(showBinds) {
				showBinds = false;
			}
			if(showVolume) {
				showVolume = false;
			}
		}
		//if(GUI.Button(new Rect(30, 210, 300, 100), "Controls")) {
			
		//}
		//if(GUI.Button(new Rect(30, 210, 300, 100), "Volume")) {

		//}
		if(GUI.Button(new Rect(30, 650, 300, 100), "Quit Game", buttonStyle)) {
			//showOptions = true;
			//showOptions = !showOptions;
			Application.Quit();
		}

		if(showSettings == true) {
			if(GUI.Button(new Rect(345, 430, 300, 100), "Graphics", buttonStyle)) {
				showGraphics = !showGraphics;
				showBinds = false;
				showVolume = false;
			}
			if(GUI.Button(new Rect(345, 540, 300, 100), "Key Bindings", buttonStyle)) {
				showBinds = !showBinds;
				showGraphics = false;
				showVolume = false;
			}
			if(GUI.Button(new Rect(345, 650, 300, 100), "Volume", buttonStyle)) {
				showVolume = !showVolume;
				showGraphics = false; 
				showBinds = false;
			}
		}

		if(showGraphics == true) {
			//INCREASE QUALITY PRESET
			if(GUI.Button(new Rect(660, 210, 145, 100), "Increase \n Quality", button)) {
				QualitySettings.IncreaseLevel();
				Debug.Log ("Increased quality");
			}
			//DECREASE QUALITY PRESET
			if(GUI.Button(new Rect(815, 210, 145, 100), "Decrease \n Quality", button)) {
				QualitySettings.DecreaseLevel();
				Debug.Log ("Decreased quality");
			}
			//0 X AA SETTINGS
			if(GUI.Button(new Rect(660, 430, 70, 100), "No AA", button)) {
				QualitySettings.antiAliasing = 0;
				Debug.Log ("0 AA");
			}
			//2 X AA SETTINGS
			if(GUI.Button(new Rect(735, 430, 70, 100), "2x AA", button)) {
				QualitySettings.antiAliasing = 2;
				Debug.Log ("2 x AA");
			}
			//4 X AA SETTINGS
			if(GUI.Button(new Rect(815, 430, 70, 100), "4x AA", button)) {
				QualitySettings.antiAliasing = 4;
				Debug.Log ("4 x AA");
			}
			//8 x AA SETTINGS
			if(GUI.Button(new Rect(890, 430, 70, 100), "8x AA", button)) {
				QualitySettings.antiAliasing = 8;
				Debug.Log ("8 x AA");
			}
			//TRIPLE BUFFERING SETTINGS
			if(GUI.Button(new Rect(660, 540, 145, 100), "Triple\n Buffering\n On", button)) {
				QualitySettings.maxQueuedFrames = 3;
				Debug.Log ("Triple buffering on");
			}
			if(GUI.Button(new Rect(815, 540, 145, 100), "Triple\n Buffering\n Off", button)) {
				QualitySettings.maxQueuedFrames = 0;
				Debug.Log ("Triple buffering off");
			}
			//ANISOTROPIC FILTERING SETTINGS
			if(GUI.Button(new Rect(660, 650, 145, 100), "Anisotropic\n On", button)) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
				Debug.Log ("Force enable anisotropic filtering!");
			}
			if(GUI.Button(new Rect(815, 650, 145, 100), "Anisotropic\n Off", button)) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
				Debug.Log ("Disable anisotropic filtering!");
			}
			//RESOLUTION SETTINGS
			//60Hz
			//if(GUI.Button(new Rect(660, 650, 145, 100), "60Hz")) {
			//	Screen.SetResolution(ResX, ResY, Fullscreen, 60);
			//	Debug.Log ("60Hz");
			//}
			//120Hz
			//if(GUI.Button(new Rect(815, 650, 145, 100), "120Hz")) {
			//	Screen.SetResolution(ResX, ResY, Fullscreen, 120);
			//	Debug.Log ("120Hz");
			//}
			//1080p
			if(GUI.Button(new Rect(867, 100, 93, 100), "1920\nx\n1080", button)) {
				Screen.SetResolution(1920, 1080, Fullscreen);
				ResX = 1920;
				ResY = 1080;
				Debug.Log ("1080p");
			}
			//720p
			if(GUI.Button(new Rect(763, 100, 93, 100), "1280\nx\n720", button)) {
				Screen.SetResolution(1280, 720, Fullscreen);
				ResX = 1280;
				ResY = 720;
				Debug.Log ("720p");
			}
			//480p
			if(GUI.Button(new Rect(660, 100, 93, 100), "640\nx\n480", button)) {
				Screen.SetResolution(640, 480, Fullscreen);
				ResX = 640;
				ResY = 480;
				Debug.Log ("480p");
			}
			if(GUI.Button(new Rect(660, 760, 300, 100), "Fullscreen", button)) {
				Screen.fullScreen = !Screen.fullScreen;
			}
			if(GUI.Button(new Rect(660, 320, 145, 100), "Vsync On", button)) {
				QualitySettings.vSyncCount = 1;
			}
			if(GUI.Button(new Rect(815, 320, 145, 100), "Vsync Off", button)) {
				QualitySettings.vSyncCount = 0;
			}
		}

		if(showBinds) {

			GUI.Box (new Rect(660, 210, 500, 540 ), "Key Bindings", boxStyle);

			if(awaitingPlayerInput) {
				GUI.Label(new Rect(950, 430, 100, 100), "Press a key to bind", label);
			}

			if(GUI.Button (new Rect(0,0, 300, 100), "moveRight" , button)) {

				RebindKeyRight();
				
			}
			
		}

		if(showVolume) {
			//GUI.TextField.fontSize = 30;
			//volume = GUI.HorizontalSlider (new Rect(660, 715, 256, 500), volume, 0.0F, 1.0F); //(new Rect(660, 650, 256, 32), volume, 0.0F, 1.0F)
			//AudioListener.volume = volume;
			//volumeTimesThousand = volume*100;
			//volumeDisplay = volumeTimesThousand.ToString();  
			//Debug.Log ("Volume is: " + volume);

			//if(GUI.Button (new Rect(660, 650, 300, 100), "Master Volume", buttonStyle)) {
			//	showMasterVolume = !showMasterVolume;
			//}

			//if(showMasterVolume) {
			//GUI.skin.font = myFont;
			GUI.skin.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

				GUI.Box (new Rect(660, 650, 330, 100), "Volume Mixer", boxStyle);

				AudioListener.volume = masterVolume;

				//volumeTimesThousand = volume*100;
				//volumeDisplay = volumeTimesThousand.ToString();  
				Debug.Log ("Volume is: " + masterVolume);
				//volumeDisplay = GUI.TextField(new Rect(970, 730, 40, 20), volumeDisplay, 5);
				//volume = GUI.HorizontalSlider (new Rect(970, 715, 256, 500), volume, 0.0F, 1.0F);
				
				GUI.Label(new Rect( 665, 670, 100, 30 ), "Master", label );
				masterVolume = GUI.HorizontalSlider(new Rect( 745, 675, 200, 30 ), masterVolume, 0.0f, 1.0f );
				GUI.Label(new Rect( 950, 670, 50, 30 ), "(" + masterVolume.ToString("f2") + ")");
				
				GUI.Label(new Rect( 665, 695, 100, 30 ), "SFX", label );
				sfxVolume = GUI.HorizontalSlider(new Rect( 745, 700, 200, 30 ), sfxVolume, 0.0f, 1.0f );
				GUI.Label(new Rect( 950, 695, 50, 30 ), "(" + sfxVolume.ToString("f2") + ")");

				GUI.Label(new Rect( 665, 720, 100, 30 ), "Music", label );
				sfxVolume = GUI.HorizontalSlider(new Rect( 745, 725, 200, 30 ), musicVolume, 0.0f, 1.0f );
				GUI.Label(new Rect( 950, 720, 50, 30 ), "(" + musicVolume.ToString("f2") + ")");
				
			//}
			//masterVolumeTitle = GUI.TextField(new Rect(660, 650, 100, 40) , masterVolumeTitle, 50);
			//volumeDisplay = GUI.TextField(new Rect(660, 730, 40, 20), volumeDisplay, 5);
		}
		
	}

	string bindAddString = "";

	void RebindkeyLeft() {
		Controls.keyBinds.Remove( "PlayerLeft" );
		bindAddString = "PlayerLeft";
		awaitingPlayerInput = true;
	}

	void RebindKeyRight() {
		Controls.keyBinds.Remove( "PlayerRight" );
		bindAddString = "PlayerRight";
		awaitingPlayerInput = true;
	}

	void RebindKeyJump() {
		Controls.keyBinds.Remove( "PlayerJump" );
		bindAddString = "PlayerJump";
		awaitingPlayerInput = true;
	}

	void RebindKeyattack() {
		Controls.keyBinds.Remove( "PlayerAttack" );
		bindAddString = "PlayerAttack";
		awaitingPlayerInput = true;
	}

	void RebindKeyPause() {
		Controls.keyBinds.Remove( "PauseGame" );
		bindAddString = "PauseGame";
		awaitingPlayerInput = true;
	}

	void RebindKeyConsole() {
		Controls.keyBinds.Remove( "DeveloperConsole" );
		bindAddString = "DeveloperConsole";
		awaitingPlayerInput = true;
	}

	void RebindKeyConsoleMac() {
		Controls.keyBinds.Remove( "DeveloperConsoleMac" );
		bindAddString = "DeveloperConsoleMac";
		awaitingPlayerInput = true;
	}

}
