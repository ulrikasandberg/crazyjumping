using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	public int nrOfPlatforms = 10;
	public float minLen, maxLen;
	public Transform player;
	public static PlatformManager instance = null;
	private Vector3 nextPos;
	private static float xMin = -20f;
	private static float xMax = 20f;
	private static float yMin = 0f;
	private static float yMax = 24f;
	private static int gridXSize = 5;
	private static int gridYSize = 7;

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
			//len = Random.Range(minLen, maxLen);	
			//nextPos = new Vector3(Random.Range(-21.3f, (20f - len)), DetermineYRange(), 0f);
			//Transform t = (Transform)Instantiate(prefab, nextPos, Quaternion.identity);
			//t.localScale = new Vector3(len, 0.5f, 1f);
			SpawnPlatform(RandomPlatformPosition());
		}
		/*for(int y = 0; y < gridYSize; y++) {
			for(int x = 0; x < gridXSize; x++) {
				SpawnPlatform(new Vector3(x, y, 0f));
			}
		}*/
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
		//nextPos = new Vector3(Random.Range(-21.3f, (20f - len)),Random.Range(-7f, 7f), 0f);
		//Transform t = (Transform)Instantiate(prefab, nextPos, Quaternion.identity);
		SpawnPlatform(RandomPlatformPosition());
	}

	float GridPosX(int index) {
		float tileSize = (xMax - xMin) / gridXSize;
		return xMin + index * tileSize;
	}

	float GridPosY(int index) {
		float tileSize = (yMax - yMin) / gridYSize;
		return yMin + index * tileSize;
	}

	Vector3 RandomPlatformPosition() {
		float tileSize = (yMax - yMin) / gridYSize;
		int cell = Mathf.FloorToInt(player.position.y / tileSize);
		int cellMax = DetermineYRange(cell, true);
		int cellMin = DetermineYRange(cell, false);
		float x = GridPosX(Random.Range(0, gridXSize));
		float y = GridPosY(Random.Range(cellMin, cellMax));
		return new Vector3(x, y, 0f);
	}

	int DetermineYRange(int cell, bool max) {
		if(max) {
			if(cell+3 <= gridYSize) {
				return cell+3;
			} else if(cell+2 <= gridYSize) {
				return cell+2;
			} else if(cell+1 <= gridYSize) {
				return cell+1;
			} else {
				return cell;
			}
		} else {
			if(cell-3 >= 0) {
				return cell-3;
			} else if(cell-2 >= 0) {
				return cell-2;
			} else if(cell-1 >= 0) {
				return cell-1;
			} else {
				return cell;
			}
		}
	}
}
