using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	private SenseController sc;
	void Start () {
		sc=transform.FindChild("SenseGroup").GetComponent<SenseController>();
		sc.SetSenseEnabled(SenseController.SenseType.Feeling,true);
	}
}
