using UnityEngine;
using System.Collections;

public class BlockCloudGenerator : MonoBehaviour {
	public GameObject prefab;
	public float height;
	
	private GameObject[] blockClouds;
	private int blockCount = 300;
	
	// Use this for initialization
	void Start () {
		blockClouds = new GameObject[blockCount];
		for (int i = 0; i < blockCount; i++) {
			blockClouds[i] = Instantiate(prefab, new Vector3(Random.Range(-200, 200), Random.Range(height, height + 100), Random.Range(-200, 200)),
											Quaternion.identity) as GameObject;
											
			blockClouds[i].transform.localScale = new Vector3(Random.Range(3, 10), Random.Range(3, 5), Random.Range(3, 10));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
