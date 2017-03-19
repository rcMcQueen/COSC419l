using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	public float turnSpeed = 16.0f;		// Speed of camera turning when mouse moves in along an 
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isRotating;	// Is the camera being rotated?

	//
	// UPDATE
	//

	void Update ()
	{

		// Get the right mouse button
		if (Input.GetMouseButtonDown (1)) {
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton (1))
			isRotating = false;

		// Rotate camera along X and Z axis
		if (isRotating) {
			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);
			transform.RotateAround (transform.position, transform.right, -pos.z * turnSpeed);
			transform.RotateAround (transform.position, Vector3.up, pos.x * turnSpeed);
		}

		// TODO: Fix this as it doesn't do shit.
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) // Checks for backward movement
		{
			Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize - 1, 1);
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0) // Checks for forward movement
		{
			Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize-1, 6);
		}
	}
}