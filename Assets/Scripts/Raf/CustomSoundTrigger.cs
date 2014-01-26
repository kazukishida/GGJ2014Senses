using UnityEngine;
using System.Collections;

public class CustomSoundTrigger : MonoBehaviour {
	private LoopSound ls;
	void Start(){
		ls=transform.parent.GetComponent<LoopSound>();
	}
	void OnTriggerEnter(){
		ls.enabled=false;
	}
	void OnTriggerExit(){
		ls.enabled=true;
	}
}
