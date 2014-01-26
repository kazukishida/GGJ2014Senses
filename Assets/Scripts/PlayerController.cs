using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public GameObject sightCamera;
	public GameObject hearingCamera;

	public bool canUseMic = false;

	public AudioClip voiceClip;
	public float echoDistance = 0f;

	public bool isAlive = true;

	public SenseController senseController;
	public SenseController.SenseType[] activeSenses;
	public int currentSlot;

	private MouseLook[] mouseLooks;
	private bool _captureCursor = true;

	public InteractableObject carryingObject = null;
	
	public float defaultDeathBound = -7;
	public Dictionary<int, float> deathBoundOverride;
	
	private Transform _cacheTransform;
	private bool killed;

	void Awake() {
		_singleton = this;
	}

	// Use this for initialization
	void Start () {
		sightCamera = GameObject.Find("SightCamera");
		hearingCamera = GameObject.Find("HearingCamera");

		senseController = GetComponentInChildren<SenseController>();

		activeSenses = new SenseController.SenseType[2];
		
		activeSenses[0] = senseController.startWithNoSense? 
								SenseController.SenseType.None : SenseController.SenseType.Sight;
		activeSenses[1] = SenseController.SenseType.None;
		
		currentSlot = 0;

		mouseLooks = GetComponentsInChildren<MouseLook>();
		
		deathBoundOverride = new Dictionary<int, float>();
		
		_cacheTransform = this.transform;
		//
		//deathBoundOverride.Add ();
	}

	public void killPlayer() {
		//Debug.Log("You died!");
		killed = true;
		AutoFade.LoadLevel(Application.loadedLevel, 0.3f, 0.3f, Color.red);
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
			WorldAudioManager.Instance.PlaySwitchSlotSense();
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
			if(IsSenseActive(SenseController.SenseType.Feeling)) {
				RaycastHit hit;
				Transform sightCamera = transform.root.FindChild("SenseGroup").FindChild("SightCamera");
				//Debug.DrawRay(sightCamera.position, sightCamera.TransformDirection(Vector3.forward));
				if(carryingObject == null) {
					//Debug.DrawRay(sightCamera.position, sightCamera.TransformDirection(Vector3.forward));
					if(Physics.Raycast(sightCamera.transform.position, sightCamera.transform.TransformDirection(Vector3.forward), out hit, 2)) {
						carryingObject = hit.transform.root.gameObject.GetComponentInChildren<InteractableObject>();
						if (carryingObject != null) {
							if(carryingObject.canCarry) {
								if(carryingObject != null) {
									hit.transform.parent = PlayerController.Instance.transform;
									carryingObject.collider.enabled = false;
									carryingObject.rigidbody.useGravity = false;
									Debug.Log ("hurrdurr");
								}
							} else if(carryingObject.canActivate) {
								carryingObject.activate();
							}
						}
					}
				}
			}
		}

		if (Input.GetButtonUp("Interaction")){
			//carryingObject.rigidbody.useGravity = true;
			if(carryingObject != null) {
				carryingObject.collider.enabled = true;
				carryingObject.rigidbody.useGravity = true;
				carryingObject.transform.parent = null;
				carryingObject = null;
			}
		}

		RaycastHit echoHit = new RaycastHit();

		if(PermissionsController.Instance.CanUseMic) {
			if(Input.GetButtonDown("Shout")) {
				if(IsSenseActive(SenseController.SenseType.Hearing)) {
					Transform sightCamera = transform.root.FindChild("SenseGroup").FindChild("SightCamera");

					Physics.Raycast(sightCamera.position, sightCamera.TransformDirection(Vector3.forward), out echoHit, 100f);
					echoDistance = echoHit.distance;

					audio.clip = Microphone.Start("", false, 99, AudioSettings.outputSampleRate);
				}
			}
			
			if(Input.GetButtonUp("Shout")) {
				if(IsSenseActive(SenseController.SenseType.Hearing)) {
					Microphone.End("");
					Debug.Log(echoDistance);
					voiceClip = AudioClip.Create("MyVoice", 44100, 1, 44100, true, false);
					AudioEchoFilter echoFilter = GameObject.Find("Player").GetComponentInChildren<AudioEchoFilter>();

					if(echoDistance >= 10f && echoDistance <= 15f) {
						echoFilter.enabled = true;
						echoFilter.wetMix = 0.1f;
						echoFilter.decayRatio = 0.1f;
					} else if(echoDistance > 15f && echoDistance <= 20f) {
						echoFilter.enabled = true;
						echoFilter.wetMix = 0.1f;
						echoFilter.decayRatio = 0.4f;
					} else if(echoDistance > 20f) {
						echoFilter.enabled = true;
						echoFilter.wetMix = 0.1f;
						echoFilter.decayRatio = 0.75f;
					} else {
						echoFilter.enabled = false;
					}

					float[] samples = new float[44100];
					audio.clip.GetData(samples, 0);
					
					voiceClip.SetData(samples, 0);
					audio.clip = voiceClip;
					
					audio.PlayOneShot(voiceClip);

					echoDistance = 0f;
				}
			}
		}

		if (!killed) {
			_checkDeath();
		}
		
	}
	
	public void _checkDeath () {
		float bound = deathBoundOverride.ContainsKey(Application.loadedLevel)?
						deathBoundOverride[Application.loadedLevel] : defaultDeathBound;
		if (_cacheTransform.position.y < bound) {
			killPlayer ();
		}
	}
	
	public bool IsSenseActive (SenseController.SenseType sense) {
		return (activeSenses[0] == sense || activeSenses[1] == sense);
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
		if (activeSenses[currentSlot] == sense) {
			senseController.SetSenseEnabled(activeSenses[currentSlot], false);
			activeSenses[currentSlot] = SenseController.SenseType.None;
		} else if (activeSenses[otherSlot] != sense) {
			if (activeSenses[currentSlot] != SenseController.SenseType.None) {
				senseController.SetSenseEnabled(activeSenses[currentSlot], false);
				activeSenses[currentSlot] = SenseController.SenseType.None;
			}
			activeSenses[currentSlot] = sense;
			senseController.SetSenseEnabled(sense, true);
		} /*else {
			SenseController.SenseType tempSense = activeSenses[0];
			activeSenses[0] = activeSenses[1];
			activeSenses[1] = tempSense;
		}*/
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
