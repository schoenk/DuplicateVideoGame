using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour {
	public GameObject playerPrefab;
	public int numLives = 4;
	GameObject playerInstance;
	float reSpawnTimer;
	public Text playerLives;
	public Text gameOver;

	// Use this for initialization
	void Start () {
		SpawnPlayer ();	
	}

	void SpawnPlayer(){
		numLives--; //Since player is Spawn at the start of the game, then # of lives will be -1 that #
		reSpawnTimer = 1f;

		// Instantiate returns an object, so we cast it to make it a gameObject
		playerInstance = (GameObject)Instantiate (playerPrefab, transform.position, Quaternion.identity);
		DisplayLives ();
	}

	// Update is called once per frame
	void Update () {
		// playerInstance == null when the player dies
		if (playerInstance == null && numLives > 0) {
			reSpawnTimer -= Time.deltaTime;

			if (reSpawnTimer <= 0) {
				SpawnPlayer ();
			}
		}
	}

	void DisplayLives(){
		Debug.Log ("lives");
		if (numLives > 0 || playerInstance != null) {
			playerLives.text = "Lives left: " + numLives.ToString ();
		} else {
			gameOver.text = "Game Over";
		}
	}
}
