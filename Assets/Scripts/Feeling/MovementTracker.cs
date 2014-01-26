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

	private CharacterController cc;

	void Start () {
		prevPosition = transform.position;
		cc = GetComponent<CharacterController>();
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
		if(cc.velocity.magnitude < 0.2f) stepCounter = 0;
	}

	public void DrawFoot() {
		Vector3 v = transform.position;
		v.y = 0.05f;
		v+= transform.forward * 0.4f;
		GameObject g = Instantiate(footstepsPrefab[nextPrefab], v, 
		                           Quaternion.Euler(new Vector3(90, transform.rotation.eulerAngles.y, 0))) as GameObject;
		Destroy(g, 3f);
		SoundFoot();

	}

	public void SoundFoot() {
		// ERWIN PUT SOUND CODE HERE.
		if(cc.isGrounded){
			if(stepCounter % drawFootsBeforeStepSound == 0){
				WorldAudioManager.Instance.PlayFootstep(-0.3f);
				//Debug.Log("PLAYone!");
			} else if (stepCounter % (drawFootsBeforeStepSound/2) == 0){
				WorldAudioManager.Instance.PlayFootstep(0.3f);
				//Debug.Log("Playtwo!");
			}
		}
	}
}
