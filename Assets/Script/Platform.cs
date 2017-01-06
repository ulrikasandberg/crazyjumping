using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public float timeUntilDestroy = 5f;

	void DestroyPlatform() {
		Destroy(gameObject);
		PlatformManager.instance.Respawn();
	}

	void OnCollisionEnter(Collision other) {
		float lerp = Mathf.PingPong (Time.time, timeUntilDestroy) / timeUntilDestroy;
		float alpha = Mathf.Lerp(1f, 0f, lerp) ;
		//renderer
      	//renderer.material.color.a = alpha;
		Invoke("DestroyPlatform", timeUntilDestroy);	
	}
}
