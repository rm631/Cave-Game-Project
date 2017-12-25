using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour {

	float deltaTime = 0.0f;

	bool showFPS = true;

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		if (Input.GetKeyDown(KeyCode.F5)) {
			showFPS = !showFPS;
		}

	}
	
	void OnGUI()
	{
		if(showFPS) {
			int w = Screen.width, h = Screen.height;
			
			GUIStyle style = new GUIStyle();
			
			Rect rect = new Rect(0, 0, w, h * 2 / 100);
			style.alignment = TextAnchor.UpperRight;
			style.fontSize = h * 2 / 100;
			style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			GUI.Label(rect, text, style);
		}
	}
	
}
