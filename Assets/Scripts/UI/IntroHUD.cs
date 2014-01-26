using UnityEngine;
using System.Collections;

public class IntroHUD : MonoBehaviour {
	private Texture2D image;
	private Rect rect;
	private int size = 256;
	
	// Use this for initialization
	void Start () {
		image = Resources.Load ("Images/keyboard1", typeof(Texture2D)) as Texture2D;
		rect = new Rect((Screen.width - size) * 0.5f, (Screen.height - size) * 0.5f, size, size);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerController.Instance.senseController.GetSenseEnabled(SenseController.SenseType.Sight)) {
			this.enabled = false;
		}
	}
	
	void OnGUI () {
		GUI.DrawTexture (rect, image);
	}
}
