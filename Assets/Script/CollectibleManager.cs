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
			Spawn();
		}
	}

	public void Respawn() {
		Spawn();
	}

	void Spawn() {
		pos = new Vector3(Random.Range(Constants.xMin, Constants.xMax), Random.Range(Constants.yMin, Constants.yMax), 0f);
		Instantiate(prefab, pos, Quaternion.Euler(90f, 0f, 0f));
	}
}
