using UnityEngine;
using System.Collections;

public class RandomScentAdder : MonoBehaviour {

	void Start () {
	
		// 1) Get the bounds of the level-ish
		RaycastHit hit;
		float minX, minY, maxX, maxY;
		int tx, ty;
		
		tx = 0;
		while (Physics.Raycast(new Vector3(tx, 10, 0f), Vector3.down, out hit, Mathf.Infinity))
			tx += Random.Range(2f, 4f);
		maxX = tx;
		       
		tx = 0;
		while (Physics.Raycast(new Vector3(tx, 10, 0f), Vector3.down, out hit, Mathf.Infinity))
			tx += Random.Range(2f, 4f);
		smaxX = tx;
	}
	
}
