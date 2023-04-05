using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicController : MonoBehaviour
{
	[SerializeField] private float speed = 5;

	void Update()
	{
		Vector3 direction = Vector3.zero;

		direction.x = Input.GetAxis("Horizontal");
		if (Input.GetKey(KeyCode.E)) direction.y = +1;
		if (Input.GetKey(KeyCode.Q)) direction.y = -1;
		direction.z = Input.GetAxis("Vertical"); ;

		transform.Translate(direction * speed * Time.deltaTime);
	}
}
