using UnityEngine;
using System.Collections;

public class MovementTracker : MonoBehaviour {

	private Vector3 prevPosition;
	private int nextPrefab = 0;
	private int stepCounter = 0;
	private float currentlyTravelled = 0;
	public float distanceBetweenFoots = 4f;
	public GameObject[] footstepsPrefab;

	public int drawFootsBeforeStepSound = 8;

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
			else if (PlayerController.Instance.senseController.GetSenseEnabled(SenseController.SenseType.Hearing)) {
				SoundFoot();
			}
			nextPrefab = (nextPrefab + 1) % footstepsPrefab.Length;
			stepCounter += 1;

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
		SoundFoot();

	}

	public void SoundFoot() {
		// ERWIN PUT SOUND CODE HERE.
		if(stepCounter % drawFootsBeforeStepSound == 0){
			WorldAudioManager.Instance.PlayFootstep(-0.4f);
			//Debug.Log("PLAYone!");
		} else if (stepCounter % (drawFootsBeforeStepSound/2) == 0){
			WorldAudioManager.Instance.PlayFootstep(0.4f);
			//Debug.Log("Playtwo!");
		}
	}
}
