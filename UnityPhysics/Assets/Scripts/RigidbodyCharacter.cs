using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{
	[SerializeField] float speed = 5;
	[SerializeField] float turnRate = 180;
	[SerializeField] float jump = 5;
	[SerializeField] bool isRelative = false;

	Rigidbody rb;
	Vector3 position;
	Quaternion rotation;

	Vector3 direction;
	Vector3 rotate;

	void Start()
	{
		position = transform.position;
		rotation = transform.rotation;

		rb = GetComponent<Rigidbody>();
	}

	
	void Update()
	{
		// reset
		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = position;
			transform.rotation = rotation;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		// movement
		direction = Vector3.zero;
		direction.z = Input.GetAxis("Vertical");

		rotate = Vector3.zero;
		rotate.y = Input.GetAxis("Horizontal");


		// jump
		if (Input.GetButtonDown("Jump"))
		{
			rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
		}
	}

	private void FixedUpdate()
	{
		if (isRelative)
		{
			// <force / torque>
			rb.AddRelativeForce(direction * speed, ForceMode.Force);
			//rb.AddRelativeTorque(rotate * turnRate);
		}
		else
		{
			// <force / torque>
			rb.AddForce(direction * speed, ForceMode.Force);
			//rb.AddTorque(rotate * turnRate);
		}
	}
}
