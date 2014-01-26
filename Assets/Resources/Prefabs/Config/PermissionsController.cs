using UnityEngine;
using System.Collections;

public class PermissionsController : MonoBehaviour {
	private static PermissionsController _singleton;
	private bool canUseMic = false;

	void Awake() {
		_singleton = this;
	}

	public static PermissionsController Instance {
		get {
			DontDestroyOnLoad(GameObject.Find("PermissionsControllerObject"));
			return _singleton;
		}
	}

	IEnumerator Start() {
		Screen.lockCursor = false;
		Loader[] loaders = GameObject.FindObjectsOfType<Loader>();
		foreach(Loader l in loaders) {
			l.enabled = false;
		}
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
		if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone)) {
			canUseMic = true;
		} else {
			canUseMic = false;
		}

		Screen.lockCursor = true;

		foreach(Loader l in loaders) {
			l.enabled = true;
		}
	}

	public bool CanUseMic {
		get { return canUseMic; }
	}
}
