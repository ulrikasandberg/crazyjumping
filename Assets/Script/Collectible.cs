using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	void Update() {
		transform.Rotate(0f, 0f, 270f*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
		CollectibleManager.instance.Respawn();
	}
}
