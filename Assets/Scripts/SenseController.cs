using UnityEngine;
using System.Collections;

public class SenseController : MonoBehaviour {
	public GameObject SightGO;
	public GameObject HearingGO;
	public GameObject ScentGO;
	public GameObject FeelingGO;
	
	public enum SenseType {
		Sight, Hearing, Scent, Feeling, None
	};
	
	void Awake () {
		SetSenseEnabled(SenseType.Sight, false);
		SetSenseEnabled(SenseType.Hearing, false);
		SetSenseEnabled(SenseType.Scent, false);
		SetSenseEnabled(SenseType.Feeling, false);
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
				break;
			case SenseType.Hearing:
				HearingGO.SetActive(active);
				break;
			case SenseType.Scent:
				ScentGO.SetActive(active);
				break;
			case SenseType.Feeling:
				FeelingGO.SetActive(active);
				break;
			default:
				Debug.Log ("SetSenseEnabled: Invalid sense");
				break;
		}
	}
}
