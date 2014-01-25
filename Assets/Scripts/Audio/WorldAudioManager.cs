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
	
	public AudioSource footstepSFX;
	public AudioSource[] audioSourcePool;

	private static WorldAudioManager instance = null;
	public static WorldAudioManager Instance {
		get {
			if (instance == null){
				GameObject go = Instantiate (Resources.Load<GameObject>("Prefabs/Level/Audio/__WorldAudioManager")) as GameObject;
				instance = go.GetComponent<WorldAudioManager>();
			}
			return instance;
		}
	}

	void Awake() {
	
		audioSourcePool = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		
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

			footstepSFX = transform.FindChild ("_sfx-footstep").audio;

		} catch(UnityException ue){
			Debug.Log ("child _amb-source not found \n" + ue.StackTrace);
			ambience = null;
		}
	}

	public void ToggleAudioSource (bool state) {
		if (audioSourcePool != null) {
			foreach(AudioSource source in audioSourcePool) {
				if (!source.gameObject.CompareTag("GlobalAudio")) {
					source.mute = !state;
				} 
			}
		}
	} 

	public void PlayFootstep(float pan){
		footstepSFX.pan = pan;

		footstepSFX.Stop();
		footstepSFX.Play();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
