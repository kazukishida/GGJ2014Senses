using UnityEngine;
using System.Collections;

public class Endpoint : MonoBehaviour {

	public int nextIndex = 0;

	void Start () {
	}

	void OnTriggerEnter(Collider other) {
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
