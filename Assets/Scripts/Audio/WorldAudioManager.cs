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
	public float ambienceVolume = 0.4f;

	public AudioSource changeSenseSFX;
	public AudioSource switchSenseSFX;
	public AudioSource goalReachedSFX;
	public AudioSource footstepSFX;
	public AudioSource chimeSFX;
	public float footstepSFXvariance = 0.2f;

	public AudioSource[] distractionSounds;
	public float distractionDistributionRange = 60.0f;
	public float distractionSpawnProbability = 0.6f;
	public int distractionMaxSpawn = 6;

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
			ambience.volume = ambienceVolume;
			ambience.loop = true;
			ambience.Play();

			footstepSFX = transform.FindChild ("_sfx-footstep").audio;
			changeSenseSFX = transform.FindChild("_sfx-lockSense").audio;
			goalReachedSFX = transform.FindChild("_sfx-goalReached").audio;
			chimeSFX = transform.FindChild("_sfx-chime").audio;
			// adjustments
			changeSenseSFX.pitch = 1.2f;
		} catch(UnityException ue){
			Debug.Log ("child not found \n" + ue.StackTrace);
			ambience = null;
		}
	}

	public void ToggleAudioSource (bool state) {
		if (audioSourcePool != null) {
			foreach(AudioSource source in audioSourcePool) {
				if (source != null && !source.gameObject.CompareTag("GlobalAudio")) {
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
		goalReachedSFX.pitch = Random.Range (0.95f, 1.05f);
		goalReachedSFX.Play();
	}	

	public void PlayChime() {
		chimeSFX.pitch = Random.Range (0.95f, 1.05f);
		chimeSFX.Play();
	}

	public void PlayLockSense(){
		changeSenseSFX.Play ();
	}

	public void PlaySwitchSlotSense(){
		switchSenseSFX.Play();
	}

	public void ResetDistractions(){
		for(int i = 0; i < distractionSounds.Length; i++){
			distractionSounds[i].Stop();
			distractionSounds[i].transform.position = Vector3.zero;
		}
	}

	private void DistributeDistractions(){
		DistributeDistractions(-1 ,0.0f);
	}

	private void DistributeDistractions(int amount){
		DistributeDistractions(amount, 0.0f);
	}

	public void DistributeDistractions(int amount, float distanceVariance){
		int spawnSuccess = 0;
		int i = 0;
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		while(true){
			i = Random.Range(0, distractionSounds.Length);
			if(Random.Range (0.0f, 1.0f) <= distractionSpawnProbability){
				distractionSounds[i].transform.position = 
					playerPosition + new Vector3(
									playerPosition.x + Random.Range(
										-distractionDistributionRange - distanceVariance, distractionDistributionRange + distanceVariance), 
									0, 
									playerPosition.z + Random.Range(
										-distractionDistributionRange - distanceVariance, distractionDistributionRange + distanceVariance)
									);
				distractionSounds[i].Play();
				spawnSuccess += 1;
				if((amount != -1 && spawnSuccess >= amount) || 
					spawnSuccess == distractionSounds.Length || 
					spawnSuccess >= distractionMaxSpawn){
					break;
				}
			} else {
				distractionSounds[i].transform.position = Vector3.down;
			}
		}
	}

	void OnLevelWasLoaded(int level){
		// audioSourcePool = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		Debug.Log("load new level! -- " + level);
		ResetDistractions();
		// -- distractions scale linearly in level
		DistributeDistractions(level);
	}

	// Use this for initialization
	void Start () {
		// -- remove comment on this to enable sound distractions on first level
		// DistributeDistractions();
	}
	
	// Update is called once per frame
	void Update () {
		ambience.volume = ambienceVolume;
	}
}
