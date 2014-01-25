using UnityEngine;
using System.Collections;

/*
WorldAudioManager.cs
- must always be present in all levels; is static
- loads gameobjects in child
- creates instance of itself if not found in scene
*/

public class WorldAudioManager : MonoBehaviour {

	public AudioSource ambience;
	
	public AudioSource goalReachedSFX;
	public AudioSource footstepSFX;
	public float footstepSFXvariance = 0.2f;

	public AudioSource[] distractionSounds;
	public float distractionDistributionRange = 50.0f;
	public float distractionSpawnProbability = 0.6f;

	public AudioSource[] audioSourcePool;

	private static WorldAudioManager instance = null;
	private static Vector3 playerPosition;

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
			goalReachedSFX = transform.FindChild("_sfx-goalReached").audio;

		} catch(UnityException ue){
			Debug.Log ("child not found \n" + ue.StackTrace);
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
		footstepSFX.pitch = Random.Range(1.0f - footstepSFXvariance, 1.0f + footstepSFXvariance);
		footstepSFX.Play();
	}

	public void PlayGoalReached(){
		goalReachedSFX.Play();
	}	

	private void DistributeDistractions(){
		DistributeDistractions(0.0f);
	}

	public void ResetDistractions(){
		for(int i = 0; i < distractionSounds.Length; i++){
			distractionSounds[i].transform.position = Vector3.zero;
		}
	}

	public void DistributeDistractions(float distanceVariance){
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		for(int i = 0; i < distractionSounds.Length; i++){
			if(Random.Range (0.0f, 1.0f) <= distractionSpawnProbability){
				distractionSounds[i].transform.position = 
					playerPosition + new Vector3(
									playerPosition.x + Random.Range(
										-distractionDistributionRange - distanceVariance, distractionDistributionRange + distanceVariance), 
									playerPosition.y, 
									playerPosition.z + Random.Range(
										-distractionDistributionRange - distanceVariance, distractionDistributionRange + distanceVariance)
									);
				distractionSounds[i].Play();
			} else {
				distractionSounds[i].transform.position = Vector3.down;
			}
		}
	}

	void OnLevelWasLoaded(int level){
		ResetDistractions();
		DistributeDistractions();
	}

	// Use this for initialization
	void Start () {
		// -- remove comment on this to enable sound distractions on first level
		// DistributeDistractions();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
