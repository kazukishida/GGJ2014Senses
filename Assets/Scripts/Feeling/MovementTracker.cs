using UnityEngine;
using System.Collections;

public class MovementTracker : MonoBehaviour {

	private Vector3 prevPosition;
	private int nextPrefab = 0;
	private float currentlyTravelled = 0;
	public float distanceBetweenFoots = 4f;
	public GameObject[] footstepsPrefab;

	void Start () {
		prevPosition = transform.position;
	}
	
	void Update () {
		float distanceTravelled = Vector3.Distance(transform.position, prevPosition);
		currentlyTravelled += distanceTravelled;
		if (currentlyTravelled >= distanceBetweenFoots) {
			currentlyTravelled -= distanceBetweenFoots;
			if (PlayerController.Instance.senseController.GetSenseEnabled(SenseController.SenseType.Feeling))
				DrawFoot();
			else if (PlayerController.Instance.senseController.GetSenseEnabled(SenseController.SenseType.Hearing))
				SoundFoot();
		}
		prevPosition = transform.position;
	}

	public void DrawFoot() {
		Vector3 v = transform.position;
		v.y = 0;
		v+= transform.forward * 0.4f;
		GameObject g = Instantiate(footstepsPrefab[nextPrefab], v, 
		                           Quaternion.Euler(new Vector3(90, transform.rotation.eulerAngles.y, 0))) as GameObject;
		Destroy(g, 3f);
		nextPrefab = (nextPrefab + 1) % footstepsPrefab.Length;
	}

	public void SoundFoot() {
		// ERWIN PUT SOUND CODE HERE.
	}
}
