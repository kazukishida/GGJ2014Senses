using UnityEngine;
using System.Collections;

public class WallButton : MonoBehaviour {
	private bool isPressed=false;
	
	public void PressButton(){
		if(!isPressed){
			isPressed=true;
			renderer.material.color=Color.red;
		}else{
			isPressed=false;
			renderer.material.color=Color.gray;
		}


	}
}
