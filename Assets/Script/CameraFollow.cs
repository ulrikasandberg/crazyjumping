using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	[Range(0.0f, 10.0f)]
	public float smoothTime = 0.3f;

	private Vector3 nextPos;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		if(target == null) {
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if(target == null) {
			Debug.LogError("No target has been set.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null) {
			return;
		}

		float delta = Mathf.Abs(transform.position.y - target.position.y);

		if(delta > 5f) {
			nextPos = new Vector3(transform.position.x, target.position.y + 5f, transform.position.z);
			transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, smoothTime);
		}
	}
}
