using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public float timeUntilDestroy = 5f;

	private Renderer rend;
	private Color c;
	private bool fadeStarted = false;

	void Start() {
		rend = GetComponent<Renderer>();
		c = rend.material.color;
	}

	void DestroyPlatform() {
		Destroy(gameObject);
		PlatformManager.instance.Respawn();
	}

	void OnCollisionEnter(Collision other) {
		if(!fadeStarted) {
      		StartCoroutine(Blink());
      		fadeStarted = true;
      	}
		Invoke("DestroyPlatform", timeUntilDestroy);	
	}

	IEnumerator Blink() {
		c.a = 0.8f;
		rend.material.color = c;
		yield return new WaitForSeconds(1.0f);
		c.a = 0.6f;
		rend.material.color = c;
		yield return new WaitForSeconds(1.0f);
		c.a = 0.4f;
		rend.material.color = c;
		yield return new WaitForSeconds(1.0f);
		c.a = 0.2f;
		rend.material.color = c;
		yield return new WaitForSeconds(1.0f);
		c.a = 0.0f;
		rend.material.color = c;
	}
}
