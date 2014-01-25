using UnityEngine;
using System.Collections;

public class SenseController : MonoBehaviour {
	public static GameObject SightGO;
	public static GameObject HearingGO;
	public static GameObject ScentGO;
	public static GameObject FeelingGO;
	
	public enum SenseType {
		Sight, Hearing, Scent, Feeling, None
	};
	
	private static ButtonHandler buttonHandler;
	
	void Awake () {
		//SetSenseEnabled(SenseType.Sight, false);
		SightGO = transform.Find("SightCamera").gameObject;
		HearingGO = transform.Find("HearingCamera").gameObject;
		ScentGO = transform.Find("ScentCamera").gameObject;
		FeelingGO = transform.Find("FeelingCamera").gameObject;
		
		buttonHandler = transform.parent.GetComponent<ButtonHandler>();
	}
	
	public bool GetSenseEnabled (SenseType sense) {
		switch(sense) {
			case SenseType.Sight:
				return SightGO.activeInHierarchy;
			case SenseType.Hearing:
				return HearingGO.activeInHierarchy;
			case SenseType.Scent:
				return ScentGO.activeInHierarchy;
			case SenseType.Feeling:
				return FeelingGO.activeInHierarchy;
			default:
				Debug.Log ("GetSenseEnabled: Invalid sense");
				return false;
		}
	}
	
	public void SetSenseEnabled (SenseType sense, bool active) {
		switch(sense) {
			case SenseType.Sight:
				SightGO.SetActive(active);
				OnSightStateChanged(active);
				break;
			case SenseType.Hearing:
				HearingGO.SetActive(active);
				break;
			case SenseType.Scent:
				ScentGO.SetActive(active);
				break;
			case SenseType.Feeling:
				FeelingGO.SetActive(active);
				if (buttonHandler != null) {
					buttonHandler.enabled = active;
				}
				break;
			default:
				Debug.Log ("SetSenseEnabled: Invalid sense");
				break;
		}
	}

	/*
	 * Activation Functions
	 */
	public void OnSightStateChanged(bool newState) {
		GameObject[] g = GameObject.FindGameObjectsWithTag("SightActivatable");
		for (int i = 0; i < g.Length; i++) {
			Collider c = g[i].GetComponent<Collider>();
			if (c != null) c.enabled = newState;
		}
	}
}
