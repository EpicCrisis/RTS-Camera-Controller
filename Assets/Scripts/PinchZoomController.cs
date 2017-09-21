using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoomController : MonoBehaviour
{

	float initDistance;
	float direction;

	Vector3 altFingerPos;
	Vector3 lastMousePos;

	void Start ()
	{
		
	}

	void Update ()
	{
		//TouchPinch ();
		KBMPinch ();
	}

	void TouchPinch ()
	{
		if (Input.touchCount == 2) {
			// Distance between two fingers when first finger touches device
			if (Input.touches [1].phase == TouchPhase.Began) {
				initDistance = Vector3.Distance (Input.touches [0].position, Input.touches [1].position);
			}
			if (Input.touches [0].phase == TouchPhase.Moved || Input.touches [1].phase == TouchPhase.Moved) {
				float newDistance = Vector3.Distance (Input.touches [0].position, Input.touches [1].position);
				direction = newDistance - initDistance;

				this.transform.Translate (Vector3.forward * direction * Time.deltaTime, Space.Self);
			} else {
				direction = Mathf.MoveTowards (direction, 0f, Time.deltaTime);
			}
		}
	}

	void KBMPinch ()
	{
		// Simulate first touch
		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			altFingerPos = Input.mousePosition;
			Debug.Log ("Left Alt Press");
		}

		// Get init distance
		if (Input.GetMouseButtonDown (0) && Input.GetKey (KeyCode.LeftAlt)) {
			initDistance = Vector3.Distance (Input.mousePosition, altFingerPos);
		}

		if (Input.GetKey (KeyCode.LeftAlt) && Input.GetMouseButton (0)) {
			Vector3 mousePosDelta = Input.mousePosition - lastMousePos;
			if (mousePosDelta.magnitude > 0f) {
				float newDistance = Vector3.Distance (Input.mousePosition, altFingerPos);
				direction = newDistance - initDistance;

				this.transform.Translate (Vector3.forward * direction * Time.deltaTime, Space.Self);
			}
		} else {
			direction = Mathf.MoveTowards (direction, 0f, Time.deltaTime);
		}
		lastMousePos = Input.mousePosition;
	}
}
