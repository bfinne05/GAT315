using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class ControllerCharacter : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float turnRate;
	[SerializeField] float jumpHeight;
	[SerializeField] float hitForce;
	[SerializeField] float doubleJumpHeight;
	[SerializeField, Range(1, 5)] float fallRateMultiplier;
	[SerializeField, Range(1, 5)] float lowJumpRateMultiplier;
	[Header("Ground")]
	[SerializeField] Transform groundTransform;
	[SerializeField] LayerMask groundLayerMask;
	CharacterController characterController;
	Vector3 velocity = Vector3.zero;

	IEnumerator DoubleJump()
	{
		// wait a little after the jump to allow a double jump
		yield return new WaitForSeconds(0.01f);
		// allow a double jump while moving up
		while (velocity.y > 0)
		{
			// if "jump" pressed add jump velocity
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y += Mathf.Sqrt(doubleJumpHeight * -2 * Physics.gravity.y);
				break;
			}
			yield return null;
		}
	}

	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}
	void Update()
	{
		// check if the character is on the ground
		bool onGround = Physics.CheckSphere(groundTransform.position, 0.2f, groundLayerMask, QueryTriggerInteraction.Ignore);
		// get direction input
		Vector3 direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		// set velocity
		if (onGround)
		{
			if (velocity.y < 0) velocity.y = 0;
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y += Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
				StartCoroutine(DoubleJump());
			}
		}
		//player movement
		velocity.x = direction.x * speed;
		velocity.z = direction.z * speed;
		// adjust gravity for jump
		float gravityMultiplier = 1;
		if (!onGround && velocity.y < 0) gravityMultiplier = fallRateMultiplier;
		if (!onGround && velocity.y > 0 && !Input.GetButton("Jump")) gravityMultiplier = lowJumpRateMultiplier;
		velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
		// move character
		characterController.Move(velocity * Time.deltaTime);
		// rotate character to face direction of movement (velocity)
		Vector3 face = new Vector3(velocity.x, 0, velocity.z);
		if (face.magnitude > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(face), Time.deltaTime * turnRate);
		}
	}


	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		// no rigidbody
		if (body == null || body.isKinematic)
		{
			return;
		}
		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3)
		{
			return;
		}
		// Calculate push direction from move direction,
		// we only push objects to the sides never up and down
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		// If you know how fast your character is trying to move,
		// then you can also multiply the push velocity by that.
		// Apply the push
		body.velocity = pushDir * hitForce;
	}


}
