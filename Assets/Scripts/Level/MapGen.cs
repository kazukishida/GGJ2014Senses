using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

	public GameObject floorPrefab;
	public GameObject wallPrefab;

	void Start () {
		string[] s = { 
			"    11111                                ", 
			"    10001                                ", 
			"    10001                                ", 
			"    1000111111111111111111111111111111111", 
			"    1000000000000000000000000000000000001", 
			"    1000000000000000000000000000000000001", 
			"    1000000000000000000000000000000000001", 
			"11111111111110001111111110001111111110001", 
			"1 000000100000001000000010001000000010001", 
			"1 000000100000001000000010001000000010001", 
			"1 00000010000000100000001000100000001   1", 
			"11111000100010001000100010001000111111111", 
			"    10001000100000001000100010001        ", 
			"    10001000100000001000100010001        ", 
			"    10001000100000001000100010001        ", 
			"    10001000111111111000100010001        ", 
			"    10000000000000001000100010001        ", 
			"    10000000000000001000100010001        ", 
			"    10000000000000001000111110001        ", 
			"    10001111000100001000000000001        ", 
			"    10000001000100001000000000001        ", 
			"    10000001000100001000000000001        ", 
			"    10000001111100001111111110001        ", 
			"    10000000000000000000010000001        ", 
			"    10000000000000000000010000001        ", 
			"    10000000000000000000010000001        ", 
			"    11111111111111111100011111111        ", 
			"                     10001               ", 
			"                     10001               ", 
			"                     11111               "};

		GameObject tg = new GameObject("Prefabber");
		tg.transform.parent = transform;
		for (int i = 0; i < s.Length; i++) {
			char[] c = s[i].ToCharArray();
			for (int j = 0; j < c.Length; j++) {
				if (c[j] == ' ') {
				} else if (c[j] == '0') {
					GameObject g = Instantiate(floorPrefab, new Vector3(i * 2, 0, j * 2), Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject;
					g.transform.parent = tg.transform;
				} else if (c[j] == '1') {
					GameObject g = Instantiate(wallPrefab, new Vector3(i * 2, 1, j * 2), Quaternion.identity) as GameObject;
					g.transform.parent = tg.transform;
					g = Instantiate(wallPrefab, new Vector3(i * 2, 3, j * 2), Quaternion.identity) as GameObject;
					g.transform.parent = tg.transform;
				}
			}
		}
	}
	
	void Update () {
	}
}
