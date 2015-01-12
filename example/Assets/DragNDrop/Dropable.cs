using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D)),
 RequireComponent(typeof(Draggable))]
public class Dropable : MonoBehaviour {

	// how close to the target before we 'snap' and fire events
	public float autoMoveArrivalThreshold = 0.05f;
	
	public Action<Dropable> OnDropComplete = delegate {};
	public Action<Dropable> OnReturnComplete = delegate {};

	// return true from these events to allow the default drop/return behaviour
	// return false in order to interrupt the default MoveToTarget behaviour
	public Func<Dropable, bool> OnDropAccepted = delegate {return true;};
	public Func<Dropable, bool> OnDropRejected = delegate {return true;};

	// TODO add an option to make these dynamic rather than
	// cached at runtime
	// - pjs 12/01/2015

	// a list of dropzones in our scene
	List<Dropzone> dropZones = new List<Dropzone>();
	Dropzone targetDropzone = null;
	public Dropzone TargetDropzone{
		get {return targetDropzone;}
	}

	Collider2D _collider;
	Draggable _dragging;
	
	void Start(){
		_collider = GetComponent<Collider2D>();
		_dragging = GetComponent<Draggable>();

		// cache dropzones
		foreach(Dropzone dz in GameObject.FindObjectsOfType<Dropzone>()){
			dropZones.Add(dz);
		}
		
		_dragging.OnDragStop = (_) => OnDragStop();
	}
	
	void OnDragStop(){
		// try to find a dropzone who can accept this drop
		Dropzone newTargetDropzone = null;
		foreach(Dropzone dropzone in dropZones){
			if(dropzone.CanDrop(_collider)){
				newTargetDropzone = dropzone;
				break;
			}
		}

		if(newTargetDropzone != null){
			// tell our old dropzone, if any, to lift
			if(targetDropzone){
				targetDropzone.Lift();
			}

			targetDropzone = newTargetDropzone;

			// accept drop and if cb returns true, auto move
			if(OnDropAccepted(this)){
				MoveToTarget.Go(
					gameObject, 
					targetDropzone.transform.position,
					autoMoveArrivalThreshold

				).OnArrival = (_) => FinishDrop();
			}
		} else {
			// reject drop and if cb returns true, auto move home
			if(OnDropRejected(this)){
				MoveToTarget.Go(
					gameObject, 
					_dragging.OriginalPos,
					autoMoveArrivalThreshold

				).OnArrival = (_) => FinishReturn();
			}
		}
	}

	bool FinishDrop(){
		targetDropzone.Drop(this);
		_dragging.SetOriginalPos(targetDropzone.transform.position);
		OnDropComplete(this);
		return true;
	}

	bool FinishReturn(){
		OnReturnComplete(this);
		return true;
	}
}
