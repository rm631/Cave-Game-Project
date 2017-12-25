using UnityEngine;
using System.Collections;

public class ConsoleCommandRouter : MonoBehaviour {
	
	// https://github.com/suicidejunkie/unity3d-console
	
	void Start () {
		var repo = ConsoleCommandsRepository.Instance;
		
		//repo.RegisterCommand( "kill", Kill );
		
	}
	
	//public string Kill(params string[] args) {
		
		//PlayerHealth.health = 0f; 
		
	//}
	
}
