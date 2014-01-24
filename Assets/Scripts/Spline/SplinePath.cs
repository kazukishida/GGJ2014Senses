//-----------------------------------
//     Potpourri Unity Framework
//     Copyright © 2013 Soupware
//-----------------------------------

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// This script handles the creation of spline paths.
/// </summary>

[ExecuteInEditMode]
public class SplinePath : MonoBehaviour {
	public bool 	loop;
	public int		betweenNodeCount;
	
	[HideInInspector]
	public float	arcLength;
	[HideInInspector]
	public float	inverseArcLength;
	[HideInInspector]
	public int		pathSize;
	
	private Transform[] 			path;
	private IEnumerable<Vector3>	nodes;
	private Vector3[] 				nodeArray;
	private float					nodeCount;
	
	void Awake () {
		path = _GetTransforms();
		nodes = Interpolate.NewCatmullRom(path, betweenNodeCount, loop);
		nodeArray = nodes.ToArray();
		arcLength = GetArcLength();
		inverseArcLength = 1 / arcLength;
		pathSize = path.Length; 
		nodeCount = nodeArray.Length;
		//Debug.Log("Arc Length: " + arcLength + ", Node Count: " + nodeCount);
	}
	
	public IEnumerable<Vector3> GetSplinePath () {
		return nodes;
	}
	
	public Vector3[] GetSplinePathArray () {
		return nodeArray;
	}
	
	public float GetAdjustedArcLength () {
		float ratio = pathSize / (pathSize + 1f);
		return ratio * arcLength; 
	}
	
	private float GetArcLength () {
		float distance = 0;
		if (path != null && path.Length >= 2) {
			
			// draw spline curve using line segments
			IEnumerator<Vector3> sequence = nodes.GetEnumerator();
			Vector3 firstPoint = path[0].position;
			Vector3 segmentStart = firstPoint;
			sequence.MoveNext(); // skip the first point
			// use "for in" syntax instead of sequence.MoveNext() when convenient
			while (sequence.MoveNext()) {
				distance += Vector3.Distance(segmentStart, sequence.Current);
				segmentStart = sequence.Current;
				// prevent infinite loop, when attribute loop == true
				if (segmentStart == firstPoint) { break; }
			}
		}
		
		return distance;
	}
	
	public float GetNodeCount () {
		return nodeCount;
	}
	
	// optional, use gizmos to draw the path in the editor
	void OnDrawGizmos () {
		if (path != null && path.Length >= 2) {
			
			// draw control points
			for (var i = 0; i < path.Length; i++) {
				Gizmos.DrawWireSphere(path[i].position, 0.15f);
			}
			
			// draw spline curve using line segments
			IEnumerator<Vector3> sequence = Interpolate.NewCatmullRom(path, betweenNodeCount, loop).GetEnumerator();
			Vector3 firstPoint = path[0].position;
			Vector3 segmentStart = firstPoint;
			sequence.MoveNext(); // skip the first point
			// use "for in" syntax instead of sequence.MoveNext() when convenient
			while (sequence.MoveNext()) {
				Gizmos.color = Color.red;
				Gizmos.DrawLine(segmentStart, sequence.Current);
				segmentStart = sequence.Current;
				// prevent infinite loop, when attribute loop == true
				if (segmentStart == firstPoint) { break; }
			}
		}
	}
	
	private Transform[] _GetTransforms () {
		List<Component> components = new List<Component>(GetComponentsInChildren<Transform>());
		List<Transform> transforms = components.ConvertAll(c => (Transform)c);
		//Debug.Log ("Paths: " + components.Count);
		transforms.Remove(this.transform);
		transforms.Sort(delegate(Transform a, Transform b)
		                {
			return a.name.CompareTo(b.name);
		});
		
		return transforms.ToArray();
	}
}