using UnityEngine;
using System.Collections;

public class LoopSound : MonoBehaviour {
	private AudioSource a;
	private bool canBeDestroyed=false;
	private float timer=.3f;
	void Start(){
		a=audio;
	}
	void Update(){
		if(!a.isPlaying){
			if(canBeDestroyed){
				timer-=Time.deltaTime;
				if(timer<=0) Destroy(gameObject);
			}else a.PlayDelayed(0.7f);
		}
	}
	void OnTriggerEnter(Collider c){
		if(c.CompareTag("Player")) canBeDestroyed=true;
	}
}
