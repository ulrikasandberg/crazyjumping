using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gc = null;
	public Text scoreText;
	public Text gameoverText;
	public AudioClip gameOverSFX;
	public AudioClip scoreSFX;

	private int score;
	private AudioSource audioSrc;

	// Use this for initialization
	void Awake () {
		if (gc == null) {
			gc = this;
		} else if(gc != this) {
			Destroy(gameObject);
		}

		if(scoreText == null) {
			Debug.LogError("You need to set the score text to the game controller.");
		}

		audioSrc = GetComponent<AudioSource>();
	}

	public void AddScore(int points) {
		audioSrc.PlayOneShot(scoreSFX);
		score += points;
		scoreText.text = "Score: " + score.ToString();
	}

	public void Restart() {
		gameoverText.enabled = true;
		StartCoroutine(ReloadScene());
	}

	IEnumerator ReloadScene() {
		audioSrc.PlayOneShot(gameOverSFX);
		yield return new WaitForSeconds(8f);
		gameoverText.enabled = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
