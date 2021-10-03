using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform targ;
	public Vector3 target;
	public float dstFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2(-40, 85);
	public float scrollSensitivity;
	public int clamp;

	public float rotationSmoothTime = .12f;
	private Vector3 rotationSmoothVelocity;
	private Vector3 currentRotation;

	private float yaw;
	private float pitch;

	private void Start()
	{
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;
	}

	private void LateUpdate()
	{
		target = new Vector3(targ.position.x, targ.position.y + 4, targ.position.z);
		var scroll = Input.GetAxis("Mouse ScrollWheel");
		dstFromTarget -= scroll * scrollSensitivity;

		if (Input.GetMouseButton(2))
		{
			yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
			pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

			currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
			transform.eulerAngles = currentRotation;
		}

		if(dstFromTarget >= 1200)
		{
			dstFromTarget = 1190;
		}
		else if(dstFromTarget <= 10)
		{
			// Go to first person camera
			dstFromTarget = 10;
		}

		transform.position = target - transform.forward * dstFromTarget;

	}
}
