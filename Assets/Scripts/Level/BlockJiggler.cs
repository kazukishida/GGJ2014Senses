using UnityEngine;
using System.Collections;

public class BlockJiggler : MonoBehaviour {

	void Start () {
		GameObject[] g = GameObject.FindGameObjectsWithTag("Wall");
		for (int i = 0; i < g.Length; i++) {
			if (Random.Range(0, 2) == 0) {
				int dir = Random.Range(0, 2) == 0 ? 1 : -1;
				Transform t = g[i].transform;
				if (!Physics.Raycast(t.position, Vector3.up * dir, t.localScale.y))
					t.position += new Vector3(0, t.localScale.y * Random.Range(1, 4) * 0.2f) * dir;
				else
					t.position -= new Vector3(0, t.localScale.y * Random.Range(1, 4) * 0.2f) * dir;
			}
		}
	}
	
	void Update () {
	
	}
}
