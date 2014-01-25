using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {
	public bool canCarry;
	public bool canActivate;

	public virtual bool activate() {
		return true;
	}
}
