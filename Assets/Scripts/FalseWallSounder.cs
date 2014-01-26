using UnityEngine;
using System.Collections;

public class FalseWallSounder : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		WorldAudioManager.Instance.PlayChime();
	}

}
