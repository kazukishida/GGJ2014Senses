using UnityEngine;
using System.Collections;

public class PermissionsController : MonoBehaviour {
	IEnumerator Start() {
		if(!Application.isWebPlayer) {
			this.enabled = false;
			yield return 0;
		} else {
			Screen.lockCursor = false;
			yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
			Screen.lockCursor = true;
		}
		Application.LoadLevel(1);
	}
}
