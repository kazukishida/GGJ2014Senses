using UnityEngine;
using System.Collections;

public class RandomScentAdder : MonoBehaviour {

	private GameObject smokePrefab;

	void Start () {
	
		smokePrefab = Resources.Load<GameObject>("Prefabs/Particles/SmokeSprite");

		// 1) Get the bounds of the level-ish
		RaycastHit hit;
		float minX, minY, maxX, maxY;
		float tx, ty;

		/*
		tx = 0;
		while (Physics.Raycast(new Vector3(tx, 10, 0f), Vector3.down, out hit, Mathf.Infinity))
			tx += Random.Range(2f, 4f);
		maxX = tx;
		
		tx = 0;
		while (Physics.Raycast(new Vector3(tx, 10, 0f), Vector3.down, out hit, Mathf.Infinity))
			tx -= Random.Range(2f, 4f);
		minX = tx;
		
		ty = 0;
		while (Physics.Raycast(new Vector3((minX + maxX) / 2, 10, ty), Vector3.down, out hit, Mathf.Infinity))
			ty += Random.Range(2f, 4f);
		maxY = ty;
		
		ty = 0;
		while (Physics.Raycast(new Vector3((minX + maxX) / 2, 10, ty), Vector3.down, out hit, Mathf.Infinity))
			ty -= Random.Range(2f, 4f);
		minY = ty;
		*/

		minX = minY = 1000000f;
		maxX = maxY = -1000000f;
		GameObject[] tg = GameObject.FindGameObjectsWithTag("Floor");
		for (int i = 0; i < tg.Length; i++) {
			Vector3 v = tg[i].transform.position;
			if (v.x < minX) minX = v.x;
			if (v.x > maxX) maxX = v.x;
			if (v.z < minY) minY = v.z;
			if (v.z > maxY) maxY = v.z;
		}

		Debug.Log("MinMax: " + minX + ", " + maxX + "] [" + minY+ ", " + maxY);

		for (tx = minX; tx <= maxX; tx += Random.Range(2f, 4f)) {
			for (ty = minY; ty <= maxY; ty += Random.Range(2f, 4f)) {
				if (Physics.Raycast(new Vector3(tx, 10, ty), Vector3.down, out hit, Mathf.Infinity)) {
					if (Random.Range(0, 6) == 0) {
						if (hit.collider.gameObject.tag == "Floor") {
							GameObject g = Instantiate(smokePrefab, hit.point + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
							Vector3 tv = g.transform.localScale;
							tv = new Vector3(tv.x * Random.Range(0.9f, 1.1f), tv.y * Random.Range(0.9f, 1.1f), tv.z * Random.Range(0.9f, 1.1f));
							g.transform.parent = transform;
						}
					}
				}
			}
		}
	}
}
