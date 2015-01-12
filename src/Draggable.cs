using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour {
	
	enum DragState {Idle, Dragging};

	// events
	public Action<Draggable> OnDragStart = delegate {};
	public Action<Draggable> OnDragStop = delegate {};

	// our current state
	DragState state = DragState.Idle;

	// the position we started the drag at
	Vector3 originalPos = Vector3.zero;
	public Vector3 OriginalPos {
		get {return originalPos;}
	}

	// record offset rather than snap middle to mouse
	Vector3 dragOffset = Vector3.zero;

	// reset the original position 
	public void SetOriginalPos(Vector3 newOriginalPos){
		originalPos = newOriginalPos;
	}

	void Start(){
		// record our starting position and click offset
		originalPos = transform.position;
	}
	
	void OnMouseUp(){
		if(!enabled || state != DragState.Dragging) {
			return;
		}

		// set state send event
		state = DragState.Idle;
		OnDragStop(this);
	}
	
	void OnMouseDown(){
		if(!enabled || state != DragState.Idle) {
			return;
		}

		dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		dragOffset.z = 0f;

		// set state send event
		state = DragState.Dragging;
		OnDragStart(this);

	}
	
	void OnMouseDrag(){
		if(!enabled){
			return;
		}

		// follow mouse with offset
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 currentPos = transform.position;
		currentPos.y = mousePos.y;
		currentPos.x = mousePos.x;
		transform.position = dragOffset + currentPos;
	}
}
