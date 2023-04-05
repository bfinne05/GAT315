using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
	readonly float size = 1;

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 position = collision.GetContact(0).point;
		DrawMarker(position, size, Color.red, 2);
	}

	private void OnCollisionExit(Collision collision) 
	{
		Vector3 position = collision.GetContact(0).point;
		DrawMarker(position, size, Color.green, 2);
	}

	private void OnCollisionStay(Collision collision)
	{
		Vector3 position = collision.GetContact(0).point;
		DrawMarker(position, size * 0.15f, Color.yellow, 2);
	}

	private void OnTriggerEnter(Collider other)
	{
		Vector3 position = other.transform.position;
		DrawMarker(position, size, Color.red, 2);
	}

	private void OnTriggerExit(Collider other)
	{
		Vector3 position = other.transform.position;
		DrawMarker(position, size, Color.green, 2);
	}

	private void OnTriggerStay(Collider other)
	{
		Vector3 position = other.transform.position;
		DrawMarker(position, size * 0.15f, Color.yellow, 2);
	}

	private void DrawMarker(Vector3 position, float size, Color color, float duration)
	{
		Debug.DrawLine(position + Vector3.left * size, position + Vector3.right * size, color, duration);
		Debug.DrawLine(position + Vector3.up * size, position + Vector3.down * size, color, duration);
		Debug.DrawLine(position + Vector3.back * size, position + Vector3.forward * size, color, duration);
	}
}
