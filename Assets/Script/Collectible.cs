using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	[Tooltip("Optional")]
	public ParticleSystem explosion;

	void Update() {
		transform.Rotate(0f, 0f, 270f*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		int points = 1;

		if(gameObject.tag == "SuperCollectible") {
			points = 10;
			if(explosion != null) {
				ParticleSystem p = Instantiate(explosion, transform.position, Quaternion.identity);
				p.Play();
			}	
		}

		Destroy(gameObject);
		CollectibleManager.instance.Respawn();
		GameController.gc.AddScore(points);
	}
}
