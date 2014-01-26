using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SplinePath))]
public class ScentTrail : MonoBehaviour {
	public SplinePath splinePath;
//	public LineRenderer lineRenderer;
	public GameObject smokePrefab;
	
	private IEnumerable<Vector3> nodes;
	// Use this for initialization
	void Start () {
		splinePath = GetComponent<SplinePath>();
		nodes = splinePath.GetSplinePath();
		
//		lineRenderer = GetComponent<LineRenderer>();
//		
//		if (lineRenderer != null) {
//			lineRenderer.SetVertexCount(Mathf.FloorToInt(splinePath.GetNodeCount()));
//		}

		GenerateSmoke();
//		int numParticles = (splinePath.GetNodeCount() / splinePath.betweenNodeCount) * 10;
//		for (int i = 0; i < splinePath.GetNodeCount(); i++) {
//			GameObject g = Instantiate(smokePrefab, 
//		}
	}
	
	// Update is called once per frame
	void Update () {
//		DrawSmoke();
	}
	
	private void GenerateSmoke () {
		IEnumerator<Vector3> sequence = nodes.GetEnumerator();
		Vector3 firstPoint = sequence.Current;
		Vector3 segmentStart = firstPoint;
		
		int i = 0;
//		if(lineRenderer != null) {
//			lineRenderer.SetPosition(i++, segmentStart);
//		}
		
		sequence.MoveNext(); // skip the first point
		
		// use "for in" syntax instead of sequence.MoveNext() when convenient
		while (sequence.MoveNext()) {
			/* 
			 * Smoke Particles
			 */
			if (Random.Range(0, splinePath.betweenNodeCount / 30) == 0) {
				GameObject g = Instantiate(smokePrefab, sequence.Current, Quaternion.identity) as GameObject;
				g.transform.parent = transform;
			}

			/*
			 * Line Renderer
			 */
//			if(lineRenderer != null) {
//				lineRenderer.SetPosition(i++, sequence.Current);
//			}

			segmentStart = sequence.Current;
			// prevent infinite loop, when attribute loop == true
			if (segmentStart == firstPoint) { break; }
		}
	} 
}
