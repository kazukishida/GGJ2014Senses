using UnityEngine;
using System.Collections;

public class TextureOffsetRandomizer : MonoBehaviour {
	public Shader shader;
	public int materialHash;
	
	private Material[] materials;
	// Use this for initialization
	void Start () {
		materials = new Material[4];
	
		for (int i = 0; i < 4; i++) {
			materials[i] = Resources.Load ("Materials/Materials/Tile" + (i+1),
											typeof(Material)) as Material;
		}
		
		int materialHash = Mathf.Abs(GetInstanceID() % 7);
		//Debug.Log (materialHash);
		this.renderer.material = materials[materialHash % 4];
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
