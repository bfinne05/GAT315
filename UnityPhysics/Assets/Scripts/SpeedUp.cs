using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    GameObject other;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		other = collision.gameObject;
        rb = other.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1000);
       Debug.Log("Hit");
	}
}
