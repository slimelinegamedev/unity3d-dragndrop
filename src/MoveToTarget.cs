using UnityEngine;
using System.Collections;
using System;

public class MoveToTarget : MonoBehaviour {
	
	const float default_speed = 3.0f;
	const float default_arrival_threshold = 0.08f;
	
	// how close to the target before we 'snap' and fire events
	public float arrivalThreshold = MoveToTarget.default_arrival_threshold;
	
	// how fast we move to target
	public float speed = MoveToTarget.default_speed;

	// utility for creating and starting a move
	public static MoveToTarget Go(
		GameObject toMove, 
		Vector3 pos, 
		float arrivalThreshold = default_arrival_threshold, 
		float speed = default_speed
	)
	{

		MoveToTarget moveTo = toMove.GetComponent<MoveToTarget>();
		if(moveTo == null){
			moveTo = toMove.AddComponent<MoveToTarget>();
		}

		moveTo.arrivalThreshold = arrivalThreshold;
		moveTo.speed = speed;
		moveTo.Go(pos);
		return moveTo;
	}
	
	// return true to allow the component to destory itself
	public Func<MoveToTarget, bool> OnArrival = delegate {return true;};
	
	// where we are moving to
	Vector3 destination;
	
	// our we moving, allows for pausing mid-move
	bool inMotion = false;

	// go toward previous destination
	public void Go(){
		Go (destination);
	}

	// go toward new destination
	public void Go(Vector3 pos){
		if(Vector3.Distance(transform.position, pos) > arrivalThreshold){
			destination = pos;
			inMotion = true;
		}

	}

	// stop moving
	public void Wait(){
		inMotion = false;
	}
	
	void Update(){
		if(inMotion){
			transform.position = Vector3.Lerp(transform.position, destination, speed * Time.fixedDeltaTime);
			
			if(Vector3.Distance(transform.position, destination) < arrivalThreshold){
				transform.position = destination;
				inMotion = false;
				if(OnArrival(this)){
					Destroy(gameObject.GetComponent<MoveToTarget>());
				}
			}
		}
	}

}
