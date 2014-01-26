using UnityEngine;
using System.Collections;

public class TextureScaler : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Vector3 scale = this.transform.localScale;
		this.renderer.material.mainTextureScale = new Vector2(scale.x, scale.y);
		Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
