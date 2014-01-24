using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {
	private Transform tc;
	private Transform t;
	private LayerMask mask=1<<11;
	private Collider[] collider;
	void Start(){
		tc=transform.FindChild("SenseGroup").FindChild("FeelingCamera");
		t=transform;

	}

	void Update(){
		if(Input.GetButtonDown("Fire1")){
			collider=Physics.OverlapSphere(tc.position+tc.forward/2,0.3f,mask);
			foreach(Collider c in collider){
				WallButton wb = c.GetComponent<WallButton>();
				wb.PressButton();
			}

		}

		collider=Physics.OverlapSphere(t.position - t.up,0.5f,mask);
		foreach(Collider c in collider){
			Transform ct=c.transform;
			FloorButton fb = c.GetComponent<FloorButton>();
			if(Vector3.Distance(ct.position,t.position-t.up)<=0.35f){
				if(!fb.GetIsBeingStepped()) fb.PressButton();
				fb.SetIsBeingStepped(true);
			}else{
				if(fb.GetIsBeingStepped()) fb.PressButton();
				fb.SetIsBeingStepped(false);
			}
		}
	}
	/*
	void OnDrawGizmos(){
		Transform b=transform.FindChild("SenseGroup").FindChild("FeelingCamera");
		Gizmos.color=Color.red;
		//Gizmos.DrawWireSphere(b.position+b.forward/2,0.3f);
		Gizmos.DrawWireSphere(transform.position - transform.up,0.35f);
	}
	*/
}
