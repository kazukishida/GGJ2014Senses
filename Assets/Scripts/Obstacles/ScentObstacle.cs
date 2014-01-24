using UnityEngine;
using System.Collections;

public class ScentObstacle : Obstacle {

	// Use this for initialization
	void Start () {
		lethalFor = SenseController.SenseType.Scent;
	}

	void Update () {
		//if he enters trigger, kill player

	}
}
