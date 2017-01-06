using UnityEngine;
using System.Collections;

public class CollectibleManager : MonoBehaviour {

	public Transform prefab;
	public int nrOfCollectibles = 10;
	public static CollectibleManager instance = null;

	private Vector3 pos;

	// Use this for initialization
	void Start () {
		if(instance == null) {
			instance = this;
		} else if(instance != this) {
			Destroy(gameObject);
		}

		for(int i = 0; i < nrOfCollectibles; i++) {
			pos = new Vector3(Random.Range(-21.3f, 20), Random.Range(0f, 25f), 0f);
			Instantiate(prefab, pos, Quaternion.Euler(90f, 0f, 0f));
		}
	}

	public void Respawn() {
		pos = new Vector3(Random.Range(-21.3f, 20f), Random.Range(0f, 25f), 0f);
		Instantiate(prefab, pos, Quaternion.Euler(90f, 0f, 0f));
	}
}
