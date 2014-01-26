using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		Application.LoadLevel(Application.loadedLevel);
	}
}
