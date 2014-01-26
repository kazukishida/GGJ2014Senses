using UnityEngine;
using System.Collections;

public class SpikeHandler : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Player") PlayerController.Instance.killPlayer();
	}
}
