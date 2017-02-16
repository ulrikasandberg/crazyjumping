using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[Range(0.0f, 2.0f)]
	public float speed = 0.1f;
	public Vector3 jumpVelocity;
	public Animator childAnimator;
	public AudioClip jumpSFX;

	private Rigidbody rb;
	private bool touchingPlatform;
	private Vector3 currentPos;
	private int playerLayer;
	private int platformLayer;
	private float axis, xPos, xJump, vy;
	private bool isRunning = false;
	private AudioSource audioSrc;
	
	void Start() {
		//transform.position = new Vector3(0f, 2f, 0f);
		rb = GetComponent<Rigidbody>();
		playerLayer = this.gameObject.layer;
		platformLayer = LayerMask.NameToLayer("Platform");
		audioSrc = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		currentPos = transform.position;
		axis = Input.GetAxis("Horizontal");
		vy = rb.velocity.y;
		childAnimator.SetBool("Grounded", touchingPlatform);

		// Determine if player is running.
		if(axis != 0) {
			isRunning = true;
			transform.rotation = (axis < 0) ? Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)) : Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f));
		} else {
			isRunning = false;
			transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
		}

		childAnimator.SetBool("Running", isRunning);

		if(touchingPlatform) {
			xPos = transform.position.x + (axis * speed);
		} else {
			xPos = transform.position.x + (axis * (speed * 0.5f));
		}
		currentPos.x = Mathf.Clamp(xPos, Constants.xMin, Constants.xMax);
		transform.position = currentPos;

		if(touchingPlatform && Input.GetButtonDown("Jump")) {
			xJump = 0f;
			if(axis < 0) {
				xJump = jumpVelocity.x * -1f;
			} else if(axis > 0) {
				xJump = jumpVelocity.x;
			}
			rb.AddForce(new Vector3(xJump, jumpVelocity.y, jumpVelocity.z), ForceMode.VelocityChange);
			childAnimator.SetTrigger("Jumping");
			audioSrc.PlayOneShot(jumpSFX);
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
