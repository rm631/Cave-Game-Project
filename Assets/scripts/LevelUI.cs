using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {

	public Text level;
	public int levelminusone;

	void Update() {

		levelminusone = MapGenerator.level - 1;

		level.text = "Level: " + levelminusone;
	}

}
