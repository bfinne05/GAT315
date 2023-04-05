using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static CarController;

public class CarController : MonoBehaviour
{
	[System.Serializable]
	public class Wheel
	{
		public WheelCollider collider;
		public Transform transform;
	}
	[System.Serializable]
	public class Axle
	{
		public Wheel leftWheel;
		public Wheel rightWheel;
		public bool isMotor;
		public bool isSteering;
	}
	[SerializeField] Axle[] axles;
	[SerializeField] float maxMotorTorque;
	[SerializeField] float maxSteeringAngle;
	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical"); // motor = <max motor torque* input axis "Vertical" >
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); // steering = <max steering angle* input axis "Horizontal" >
	foreach (Axle axle in axles)
		{
			if (axle.isSteering)
			{
				axle.leftWheel.collider.steerAngle = steering;
				axle.rightWheel.collider.steerAngle = steering;
				// <set right wheel steer angle >
				
			}
			if (axle.isMotor)
			{
				axle.leftWheel.collider.motorTorque = motor;
				axle.rightWheel.collider.motorTorque = motor;
				// <set right wheel motor torque >
			}
			//might need to change if does not work
			UpdateWheelTransform(axle.leftWheel);
			UpdateWheelTransform(axle.rightWheel);
		}
	}

	public void UpdateWheelTransform(Wheel wheel)
	{
		wheel.collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
		wheel.transform.position = position;
		wheel.transform.rotation = rotation;
	}


}