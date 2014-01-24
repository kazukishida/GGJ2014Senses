using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public SenseController senseController;
	public SenseController.SenseType[] activeSenses;
	public int currentSlot;
	
	// Use this for initialization
	void Start () {
		activeSenses = new SenseController.SenseType[2];
		activeSenses[0] = SenseController.SenseType.None;
		activeSenses[1] = SenseController.SenseType.None;
		
		senseController = GetComponentInChildren<SenseController>();
		currentSlot = 0;
	}
	
	// Update is called once per frame
	void Update () {
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
		} else {
			SenseController.SenseType tempSense = activeSenses[0];
			activeSenses[0] = activeSenses[1];
			activeSenses[1] = tempSense;
		}
	}
}
