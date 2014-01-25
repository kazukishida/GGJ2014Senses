using UnityEngine;
using System.Collections;

public class Endpoint : MonoBehaviour {

	public int nextIndex = 0;

	void Start () {
	}

	void OnTriggerEnter(Collider other) {
		AutoFade.LoadLevel(Application.loadedLevel + 1, 0.5f, 0.2f, Color.white);
	}
}
