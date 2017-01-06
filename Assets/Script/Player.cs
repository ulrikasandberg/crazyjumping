using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[Range(0.0f, 2.0f)]
	public float speed = 0.1f;
	public Vector3 jumpVelocity;

	private Rigidbody rb;
	private bool touchingPlatform;
	private Vector3 currentPos;
	private int playerLayer;
	private int platformLayer;
	private float axis, xPos, xJump, vy;
	
	void Start() {
		transform.position = new Vector3(0f, 2f, 0f);
		rb = GetComponent<Rigidbody>();
		playerLayer = this.gameObject.layer;
		platformLayer = LayerMask.NameToLayer("Platform");
	}

	// Update is called once per frame
	void Update () {
		currentPos = transform.position;
		axis = Input.GetAxis("Horizontal");
		vy = rb.velocity.y;

		if(touchingPlatform) {
			xPos = transform.position.x + (axis * speed);
		} else {
			xPos = transform.position.x + (axis * (speed * 0.5f));
		}
		currentPos.x = Mathf.Clamp(xPos, -21.3f, 21.3f);
		transform.position = currentPos;

		if(touchingPlatform && Input.GetButtonDown("Jump")) {
			xJump = 0f;
			if(axis < 0) {
				xJump = jumpVelocity.x * -1f;
			} else if(axis > 0) {
				xJump = jumpVelocity.x;
			}
			rb.AddForce(new Vector3(xJump, jumpVelocity.y, jumpVelocity.z), ForceMode.VelocityChange);
			//rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
		}

		Physics.IgnoreLayerCollision(playerLayer, platformLayer, (vy > 0.0f));
	}

	void OnCollisionEnter() {
		touchingPlatform = true;
	}

	void OnCollisionExit() {
		touchingPlatform = false;
	}
}
