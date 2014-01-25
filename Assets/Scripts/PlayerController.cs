using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public bool isAlive = true;

	public SenseController senseController;
	public SenseController.SenseType[] activeSenses;
	public int currentSlot;

	private MouseLook[] mouseLooks;
	private bool _captureCursor = true;

	private InteractableObject carryingObject = null;

	void Awake() {
		_singleton = this;
	}

	// Use this for initialization
	void Start () {
		activeSenses = new SenseController.SenseType[2];
		activeSenses[0] = SenseController.SenseType.Sight;
		activeSenses[1] = SenseController.SenseType.None;
		
		senseController = GetComponentInChildren<SenseController>();
		currentSlot = 0;

		mouseLooks = GetComponentsInChildren<MouseLook>();
	}

	public void killPlayer() {
		Debug.Log("You died!");
		//load checkpoint
	}

	// Update is called once per frame
	void Update () {

		/*
		 * Capturing of the Camera
		 */
		if (_captureCursor) {
			Screen.lockCursor = true;
			for (int i = 0; i < mouseLooks.Length; i++)
				mouseLooks[i].enabled = Screen.lockCursor;
		} else {
			Screen.lockCursor = false;
			for (int i = 0; i < mouseLooks.Length; i++)
				mouseLooks[i].enabled = false;
		}

		/*
		 * Sense Controls
		 */
		if (Input.GetButtonDown("Toggle Slot")) {
			currentSlot = (currentSlot == 1)? 0 : 1;
		}
		
		if (Input.GetButtonDown("Sight")) {
			SetSenseToCurrentSlot(SenseController.SenseType.Sight);
		}
		
		if (Input.GetButtonDown("Hearing")) {
			SetSenseToCurrentSlot(SenseController.SenseType.Hearing);
		}
		
		if (Input.GetButtonDown("Scent")) {
			SetSenseToCurrentSlot(SenseController.SenseType.Scent);
		}
		
		if (Input.GetButtonDown("Feeling")) {
			SetSenseToCurrentSlot(SenseController.SenseType.Feeling);
		}

		if (Input.GetButtonDown("Interaction")) {
			if(SenseController.Instance.GetSenseEnabled(SenseController.SenseType.Feeling)) {
				RaycastHit hit;
				Transform sightCamera = transform.root.FindChild("SenseGroup").FindChild("SightCamera");
				//Debug.DrawRay(sightCamera.position, sightCamera.TransformDirection(Vector3.forward));
				if(carryingObject == null) {
					//Debug.DrawRay(sightCamera.position, sightCamera.TransformDirection(Vector3.forward));
					if(Physics.Raycast(sightCamera.transform.position, sightCamera.transform.TransformDirection(Vector3.forward), out hit, 2)) {
						carryingObject = hit.transform.root.gameObject.GetComponentInChildren<InteractableObject>();
						if(carryingObject.canCarry) {
							if(carryingObject != null) {
								hit.transform.parent = PlayerController.Instance.transform;
								carryingObject.collider.enabled = false;
								carryingObject.rigidbody.useGravity = false;
								carryingObject.transform.position += new Vector3(0, 0.25f);
								Debug.Log ("hurrdurr");
							}
						} else if(carryingObject.canActivate) {
							carryingObject.activate();
						}
					}
				} else {
					//carryingObject.rigidbody.useGravity = true;
					carryingObject.collider.enabled = true;
					carryingObject.rigidbody.useGravity = true;
					carryingObject.transform.parent = null;
					carryingObject = null;
				}
			}
		}
	}
	
	public SenseController.SenseType GetSenseInSlot (int slot) {
		return activeSenses[slot];
	}
	
	public SenseController.SenseType GetSenseInCurrentSlot () {
		return activeSenses[currentSlot];
	}
	
	public bool IsSlotActive (int slot) {
		return (currentSlot == slot);
	}
	
	private void SetSenseToCurrentSlot (SenseController.SenseType sense) {
		int otherSlot = (currentSlot == 1)? 0 : 1;
		if (activeSenses[currentSlot] == sense && activeSenses[otherSlot] != SenseController.SenseType.None) {
			senseController.SetSenseEnabled(activeSenses[currentSlot], false);
			activeSenses[currentSlot] = SenseController.SenseType.None;
		} else if (activeSenses[otherSlot] != sense) {
			if (activeSenses[currentSlot] != SenseController.SenseType.None) {
				senseController.SetSenseEnabled(activeSenses[currentSlot], false);
				activeSenses[currentSlot] = SenseController.SenseType.None;
			}
			activeSenses[currentSlot] = sense;
			senseController.SetSenseEnabled(sense, true);
		} else {
			SenseController.SenseType tempSense = activeSenses[0];
			activeSenses[0] = activeSenses[1];
			activeSenses[1] = tempSense;
		}
	}

	public bool CaptureCursor {
		get { return _captureCursor;}
		set { _captureCursor = value; }
	}

	private static PlayerController _singleton;

	public static PlayerController Instance {
		get { return _singleton; }
	}
}
