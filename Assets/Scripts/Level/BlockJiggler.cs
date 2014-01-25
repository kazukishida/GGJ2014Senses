using UnityEngine;
using System.Collections;

public class BlockJiggler : MonoBehaviour {

	void Start () {
		GameObject[] g = GameObject.FindGameObjectsWithTag("Wall");
		for (int i = 0; i < g.Length; i++) {
			int dir = Random.Range(0, 3) == 0 ? 1 : -1;
			Transform t = g[i].transform;
			if (!Physics.Raycast(t.position, Vector3.up * dir, t.localScale.y)) {
				t.position += new Vector3(0, t.localScale.y * 0.1f);
			}
		}
	}
	
	void Update () {
	
	}
}
