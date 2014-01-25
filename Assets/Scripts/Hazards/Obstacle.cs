using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public bool isLethal;
	private Collider collider;

	void Start() {
		collider = this.GetComponentInChildren<Collider>();
		collider.isTrigger = isLethal; //turn collider into a trigger when the obstacle is lethal
	}

	void Update() {

	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			GameController.Instance.killPlayer();
		}
	}
}