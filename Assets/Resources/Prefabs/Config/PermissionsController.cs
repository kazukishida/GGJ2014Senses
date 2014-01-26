using UnityEngine;
using System.Collections;

public class PermissionsController : MonoBehaviour {
	private static PermissionsController _singleton;
	private bool canUseMic = false;

	public static PermissionsController Instance {
		get {
			if (_singleton == null){
				Screen.lockCursor = false;
				Debug.Log("in here!");
				GameObject go = Instantiate (Resources.Load<GameObject>("Prefabs/Config/PermissionsControllerObject")) as GameObject;
				DontDestroyOnLoad(go);
				_singleton = go.GetComponent<PermissionsController>();
			}
			return _singleton;
		}
	}

	IEnumerator Start() {
		Screen.lockCursor = false;
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
		if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone)) {
			canUseMic = true;
		} else {
			canUseMic = false;
		}

		Screen.lockCursor = true;
		this.enabled = false;
	}

	public bool CanUseMic {
		get { return canUseMic; }
	}
}
