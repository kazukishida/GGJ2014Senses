using UnityEngine;
using System.Collections;

public class BrightLightSpawner : MonoBehaviour {

	private float distanceInstantiate = 6f;
	public GameObject lightQuad;

	void Start () {
		Vector3 scale = transform.localScale;
		int numToInstantiate = (int) ((scale.x * scale.y * scale.z) / distanceInstantiate);
		for (int i = 0; i < numToInstantiate; i++) {
			GameObject g = Instantiate(lightQuad, transform.position + 
			                           new Vector3(Random.Range(-0.5f, 0.5f) * scale.x, Random.Range(-0.5f, 0.5f) * scale.y, Random.Range(-0.5f, 0.5f) * scale.z), 
			                           Quaternion.identity) as GameObject;
		}
	}
	
	void Update () {
	
	}
}
