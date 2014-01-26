using UnityEngine;
using System.Collections;

public class RandomRotitulator : MonoBehaviour {

	private float time;
	private float rotSpeed;

	void Start() {
		time = Time.time;
		rotSpeed = Random.Range(30f, 80f);
	}

	void Update() {
		// Slowly move it upwards
		transform.position += Vector3.up * Time.deltaTime * 0.25f;
	}

	void LateUpdate () {
		Vector3 v = transform.rotation.eulerAngles;
		v.z = (Time.time - time) * rotSpeed;
		transform.rotation = Quaternion.Euler(v);

//		Vector3 s = transform.localScale;
//		s.x = Mathf.Sin (Time.time - time) * rotSpeed / 4;
//		s.y = Mathf.Cos (Time.time - time) * rotSpeed / 4;
	}
}
