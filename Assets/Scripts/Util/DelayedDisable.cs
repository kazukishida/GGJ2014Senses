using UnityEngine;
using System.Collections;

public class DelayedDisable : MonoBehaviour {

	public float timeToDisable = 0;
	
	void Update () {
		if (timeToDisable < Time.time)
			gameObject.SetActive(false);
	}

	public void StartDelayedDisable(float duration) {
		timeToDisable = Time.time + duration;
	}
}
