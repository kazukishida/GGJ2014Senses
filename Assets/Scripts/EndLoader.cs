using UnityEngine;
using System.Collections;

public class EndLoader : MonoBehaviour {
	
	private Material m_Material = null;
	
	private void Awake () {
		m_Material = new Material("Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }");
	}
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		StartCoroutine (Fade (0.5f, Color.black));
		Invoke ("Load", 3f);
	}
	
	private void DrawQuad(Color aColor,float aAlpha)
	{
		aColor.a = aAlpha;
		m_Material.SetPass(0);
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Color(aColor);
		GL.Vertex3(0, 0, -1);
		GL.Vertex3(0, 1, -1);
		GL.Vertex3(1, 1, -1);
		GL.Vertex3(1, 0, -1);
		GL.End();
		GL.PopMatrix();
	}
	
	private IEnumerator Fade(float aFadeInTime, Color aColor)
	{
		float t = 1.0f;
		while (t>0.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
			DrawQuad(aColor,t);
		}
	}
	
	private void Load() {
		AutoFade.LoadLevel (0, 0.5f, 0.5f, Color.black);
	}
}
