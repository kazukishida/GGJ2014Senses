using UnityEngine;
using System.Collections;

public class MovementTracker : MonoBehaviour {

	private Vector3 prevPosition;
	private int nextPrefab = 0;
	public float distanceBetweenFoots = 0.5f;
	public GameObject[] footstepsPrefab;

	void Start () {
		prevPosition = transform.position;
	}
	
	void Update () {
		float distanceTravelled = Vector3.Distance(transform.position, prevPosition);
	}

	public void DrawFoot() {
		nextPrefab = (nextPrefab + 1) % footstepsPrefab.Length;
	}
}
