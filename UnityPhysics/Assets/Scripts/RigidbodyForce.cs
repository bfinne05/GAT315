using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RigidbodyForce : MonoBehaviour
{
	[SerializeField] Vector3 force;
	[SerializeField] ForceMode forceMode;
	[SerializeField] Vector3 torque;
	[SerializeField] ForceMode torqueMode;
	[SerializeField] bool isRelative = false;

	private Vector3 position;
	private Quaternion rotation;
	private Rigidbody rb;

	private void Start()
	{
		position = transform.position;
		rotation = transform.rotation;

		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		// reset transform and rigidbody
		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.position = position;
			transform.rotation = rotation;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		// force
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (isRelative) 
			{
				rb.AddRelativeForce(force, forceMode);
			}
			else
			{
				rb.AddForce(force, forceMode);
			}
		}

		// torque
		if (Input.GetKeyDown(KeyCode.T))
		{
			if (isRelative)
			{
				rb.AddRelativeTorque(torque, torqueMode);
			}
			else
			{
				rb.AddTorque(torque, torqueMode);
			}
		}

		// draw axis
		Debug.DrawRay(transform.position, transform.forward * 2, Color.blue);
		Debug.DrawRay(transform.position, transform.right * 2, Color.red);
		Debug.DrawRay(transform.position, transform.up * 2, Color.green);

	}
}
