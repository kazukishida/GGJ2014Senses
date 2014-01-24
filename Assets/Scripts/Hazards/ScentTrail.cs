using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SplinePath))]
public class ScentTrail : MonoBehaviour {
	public SplinePath splinePath;
	public LineRenderer lineRenderer;
	
	private IEnumerable<Vector3> nodes;
	// Use this for initialization
	void Start () {
		splinePath = GetComponent<SplinePath>();
		nodes = splinePath.GetSplinePath();
		
		lineRenderer = GetComponent<LineRenderer>();
		
		if (lineRenderer != null) {
			lineRenderer.SetVertexCount(Mathf.FloorToInt(splinePath.GetNodeCount()));
		}
	}
	
	// Update is called once per frame
	void Update () {
		DrawSmoke();
	}
	
	private void DrawSmoke () {
		IEnumerator<Vector3> sequence = nodes.GetEnumerator();
		Vector3 firstPoint = sequence.Current;
		Vector3 segmentStart = firstPoint;
		
		int i = 0;
		if(lineRenderer != null) {
			lineRenderer.SetPosition(i++, segmentStart);
		}
		
		sequence.MoveNext(); // skip the first point
		
		// use "for in" syntax instead of sequence.MoveNext() when convenient
		while (sequence.MoveNext()) {
			
			if(lineRenderer != null) {
				lineRenderer.SetPosition(i++, sequence.Current);
			}
			
			segmentStart = sequence.Current;
			// prevent infinite loop, when attribute loop == true
			if (segmentStart == firstPoint) { break; }
		}
	} 
}
