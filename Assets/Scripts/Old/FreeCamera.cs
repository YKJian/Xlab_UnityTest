using UnityEngine;

namespace Old
{
	[RequireComponent(typeof(Rigidbody))]

	public class FreeCamera : MonoBehaviour
	{
		public float speed = 1.5f;
		public float acceleration = 10f;
		public float sensitivity = 5f;
		public Camera mainCamera;

		private Rigidbody body;
		private float rotY;
		private Vector3 direction;

		private void Start()
		{
			body = GetComponent<Rigidbody>();
			body.freezeRotation = true;
			body.useGravity = false;
			body.mass = 0.1f;
			body.linearDamping = 10;
		}

		public void Move()
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			float rotX = mainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
			rotY += Input.GetAxis("Mouse Y") * sensitivity;
			rotY = Mathf.Clamp(rotY, -90, 90);

			if (Input.GetKey(KeyCode.Mouse1))
			{
				mainCamera.transform.localEulerAngles = new Vector3(-rotY, rotX, 0);
			}

			direction = new Vector3(h, 0, v);
			direction = mainCamera.transform.TransformDirection(direction);
		}

		void FixedUpdate()
		{
			body.AddForce(direction.normalized * speed * acceleration);

			if (Mathf.Abs(body.linearVelocity.x) > speed) body.linearVelocity = new Vector3(Mathf.Sign(body.linearVelocity.x) * speed, body.linearVelocity.y, body.linearVelocity.z);
			if (Mathf.Abs(body.linearVelocity.z) > speed) body.linearVelocity = new Vector3(body.linearVelocity.x, body.linearVelocity.y, Mathf.Sign(body.linearVelocity.z) * speed);
			if (Mathf.Abs(body.linearVelocity.y) > speed) body.linearVelocity = new Vector3(body.linearVelocity.x, Mathf.Sign(body.linearVelocity.y) * speed, body.linearVelocity.z);
		}
	}
}
