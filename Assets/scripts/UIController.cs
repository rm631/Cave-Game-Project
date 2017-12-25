using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	//private int level = 1;

	float time = 3f; //Seconds to read the text
	
	void Start() {

		Invoke("Hide", time);
		
	}
	
	void Hide() {

		//Destroy(gameObject);
		gameObject.SetActive(false); 
		
	}
	
}