using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {

	public string[] messages;

	void Start () {
		TextMesh[] tm = GetComponentsInChildren<TextMesh>();

		if (messages.Length == 0) {
			for (int i = 0; i < tm.Length; i++)
				tm[i].gameObject.SetActive(false);
		} else if (messages.Length == 1) {
			tm[0].transform.localPosition = new Vector3(0.1f, 0f, 0f);
			tm[1].gameObject.SetActive(false);
			tm[2].gameObject.SetActive(false);
		} else if (messages.Length == 2) {
			tm[0].transform.localPosition = new Vector3(0.1f, 0.5f, 0f);
			tm[1].transform.localPosition = new Vector3(0.1f, -0.5f, 0f);
			tm[2].gameObject.SetActive(false);
		} else if (messages.Length == 3) {
			tm[0].transform.localPosition = new Vector3(0.1f, 1f, 0f);
			tm[1].transform.localPosition = new Vector3(0.1f, 0f, 0f);
			tm[2].transform.localPosition = new Vector3(0.1f, -1f, 0f);
		}

		for (int i = 0; i < tm.Length; i++) {
			if (i < messages.Length && messages[i] != "") {
				tm[i].text = messages[i];
			}
		}
	}
}
