using UnityEngine;
using System.Collections;

/*
WorldAudioManager.cs
- must always be present in all levels; is static
- loads _amb-source gameobject in child
- creates instance of itself if not found in scene
*/

public class WorldAudioManager : MonoBehaviour {

	public AudioSource ambience;

	private static WorldAudioManager instance = null;
	public static WorldAudioManager Instance {
		get {
			if (instance == null){
				GameObject go = Instantiate (Resources.Load<GameObject>("Audio/_WorldAudioManager")) as GameObject;
				instance = go.GetComponent<WorldAudioManager>();
			}
			return instance;
		}
	}

	void Awake() {
		if(instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);

		// --- AWAKE INITZZ HERE
		try {
			ambience = transform.FindChild("_amb-source").audio;

			ambience.loop = true;
			ambience.Play();

		} catch(UnityException ue){
			Debug.Log ("child _amb-source not found \n" + ue.StackTrace);
			ambience = null;
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
