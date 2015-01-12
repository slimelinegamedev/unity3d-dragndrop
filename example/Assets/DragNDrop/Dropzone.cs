using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Dropzone : MonoBehaviour {

	Collider2D _collider;
	Dropable _ref = null;

	void Start () {
		_collider = GetComponent<Collider2D> ();
	}

	public System.Action<Dropzone> OnDrop = delegate {};
	public System.Action<Dropzone> OnLift = delegate {};

	public bool IsFull {
		get {
			return _ref != null;
		}
	}
	
	public bool CanDrop(Collider2D dropCollider){
		return enabled 
			&& !IsFull 
			&& (dropCollider.bounds.Intersects(_collider.bounds) 
				||  _collider.bounds.Contains(dropCollider.transform.position));
	}

	public void Drop(Dropable obj){
		_ref = obj;
		OnDrop (this);
	}

	public void Lift(){
		_ref = null;
		OnLift (this);
	}
}
