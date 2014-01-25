using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public SenseController.SenseType type;
	public bool isLethal;
	private Collider obstacleCollider;
	
	void Start() {
		obstacleCollider = this.GetComponentInChildren<Collider>();
		obstacleCollider.isTrigger = isLethal; //turn collider into a trigger when the obstacle is lethal
	}

	void Update() {

	}

	void OnTriggerEnter(Collider other) {
		if(isLethal) {
			if(other.gameObject.tag == "Player") {
				PlayerController.Instance.killPlayer();
			}
		}
	}
}