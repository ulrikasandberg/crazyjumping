using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	public int nrOfPlatforms = 10;
	public float minLen, maxLen;
	public Transform player;
	public static PlatformManager instance = null;
	private Vector3 nextPos;
	private static int gridXSize = 5;
	private static int gridYSize = 4;

	// Use this for initialization
	void Start () {
		if(instance == null) {
			instance = this;
		} else if(instance != this) {
			Destroy(gameObject);
		}

		if(player == null) {
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if(player == null) {
			Debug.LogError("No Player has been set.");
		}

		for(int i = 0; i < nrOfPlatforms; i++) {
			SpawnPlatform(RandomPlatformPosition());
		}
	}

	void SpawnPlatform(Vector3 pos) {
		float len = Random.Range(minLen, maxLen);
		Transform t = (Transform)Instantiate(prefab, pos, Quaternion.identity);
		t.localScale = new Vector3(len, 0.5f, 1f);
	}

	public void Respawn() {
		if(player == null) {
			return;
		}
		SpawnPlatform(RandomPlatformPosition());
	}

	Vector3 RandomPlatformPosition() {
		float x = Random.Range(Constants.xMin, Constants.xMax);
		float y = Random.Range(DetermineMinYRange(), DetermineMaxYRange());
		return new Vector3(RoundToGrid(x, gridXSize), RoundToGrid(y, gridYSize), 0f);
	}

	float RoundToGrid(float n, int gridSize) {
		int quota = Mathf.FloorToInt(n) / gridSize;
		return (float) quota * gridSize;
	}

	float DetermineMaxYRange() {
		return Mathf.Clamp(player.position.y + 10f, Constants.yMin, Constants.yMax);
	}

	float DetermineMinYRange() {
		return Mathf.Clamp(player.position.y - 10f, Constants.yMin, Constants.yMax);
	}
}
