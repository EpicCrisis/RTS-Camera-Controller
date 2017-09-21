using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMoveController : MonoBehaviour
{
	public bool mouseMode = true;
	Vector3 targetPosition = Vector3.zero;
	Vector3 offset;
	public float speed = 10f;

	void Start ()
	{
		Ray ray = Camera.main.ScreenPointToRay (new Vector2 (Screen.width / 2f, Screen.height / 2f));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 10000f)) {
			offset = this.transform.position - hit.point;
		} 
	}

	void Update ()
	{
		if (mouseMode) {
			// Mouse Simulation
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 10000f)) {
					targetPosition = hit.point;
				} 
			}
		} else {
			// Touch Control
			if (Input.touchCount > 0) {
				Ray ray2 = Camera.main.ScreenPointToRay (Input.touches [0].position);
				RaycastHit hit2;
				if (Physics.Raycast (ray2, out hit2, 10000f)) {
					targetPosition = hit2.point;
				}
			}
		}
	}

	void LateUpdate ()
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, targetPosition + offset, Time.deltaTime * speed);
	}
}
