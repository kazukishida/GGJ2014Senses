using UnityEngine;
using System.Collections;

public class PermissionsController : MonoBehaviour {
	IEnumerator Start() {
		if(Application.isWebPlayer) {
			this.enabled = false;
			yield return 0;
		}

		Screen.lockCursor = false;
		Loader[] loaders = GameObject.FindObjectsOfType<Loader>();
		foreach(Loader l in loaders) {
			l.enabled = false;
		}
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);

		Screen.lockCursor = true;

		foreach(Loader l in loaders) {
			l.enabled = true;
		}
	}
}
