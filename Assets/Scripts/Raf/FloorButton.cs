using UnityEngine;
using System.Collections;

public class FloorButton : MonoBehaviour {
	private bool isPressed=false;
	private bool isBeingStepped=false;
	
	public void PressButton(){
		if(!isBeingStepped){
			isPressed=true;
			renderer.material.color=Color.red;
		}else{
			isPressed=false;
			renderer.material.color=Color.gray;
		}
	}

	public void SetIsBeingStepped(bool boo){
		isBeingStepped=boo;
	}
	public bool GetIsBeingStepped(){
		return isBeingStepped;
	}
	
}
