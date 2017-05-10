using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour {
	public float timer = 10f;

	// variable to enable hero's abilities
	public GameObject hero;

	// GUI variables
	private Text duplicateTimerText;
	private Text secondsLeftDuplicate;
	private Text secondsDuplicateReplay;
	private float replayTimer;

	// Replay path variables
	bool repeatingPath = false;
	public static List<Vector3> path = new List<Vector3>();

	// Hero's transparency during inactivity's variables
	public RuntimeAnimatorController fullColorHero;
	public RuntimeAnimatorController fadedHero;
	private Animator heroAnimator;
	private Animator duplicateAnimator;

	void Start(){
		hero = GameObject.Find("Hero(Clone)");

		duplicateTimerText = GameObject.Find ("DuplicateTimer").GetComponent<Text> ();
		secondsLeftDuplicate = GameObject.Find ("SecondsLeftDuplicate").GetComponent<Text> ();
		secondsDuplicateReplay = GameObject.Find ("SecondsDuplicateReplay").GetComponent<Text> ();
		replayTimer = timer;

        // Coroutines allow us to break from a method and retrun at a later frame to finish it.
		StartCoroutine ("TrackLocation");

		// Makes sure Hero is faded at start of selfDestruct
		heroAnimator = hero.GetComponent<Animator> ();
		if (heroAnimator.runtimeAnimatorController == null) {
			heroAnimator.runtimeAnimatorController = fadedHero;
		}
			
		// Makes sure Duplicate is fullColor at start of selfDestruct
		duplicateAnimator = GetComponent<Animator> ();
		if (duplicateAnimator.runtimeAnimatorController == null) {
			duplicateAnimator.runtimeAnimatorController = fullColorHero;
		}
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer >= 0) {
			duplicateTimerText.text = "Duplicate's time left: ";
			secondsLeftDuplicate.text = timer.ToString ("00");
		}
		if (timer <= 0 && repeatingPath == false) {

            // disable HeroDuplicate abilities when timer is over
			GetComponent<PlayerMovement> ().enabled = false;
			GetComponent<PlayerJump> ().enabled = false;

			// Enable Hero's scripts when duplicate is distructed
			if (hero != null) {
				
				// Enable scripts in Hero GameObject
				hero.GetComponent<PlayerMovement> ().enabled = true;
				hero.GetComponent<PlayerJump> ().enabled = true;
			}
			repeatingPath = true;
			StartCoroutine ("RepeatPath");
		}
	}

	IEnumerator TrackLocation() {
		Debug.Log ("TrackLocation Started");	
		while (timer > 0) {
			Vector3 location = new Vector3();
			location = transform.position;
			path.Add(location);

            // to save memory, we save the location
            // after waiting .002 scaled time.
			yield return new WaitForSeconds (.002f);
			Debug.Log (transform.position);	
		}
	}

	IEnumerator RepeatPath() {
		Debug.Log ("RepeatPath Started");

		// Makes duplicate faded and active hero to be fullColored
		heroAnimator.runtimeAnimatorController = fullColorHero;
		duplicateAnimator.runtimeAnimatorController = fadedHero;

		while (path.Count > 1) {
			// Dictates walking direction for duplicate animation 
			float duplicateDirection = path [1].x - path [0].x;
			duplicateAnimator.SetFloat ("speed", duplicateDirection);

			Vector3 location = path[0];
			transform.position = location;
			path.RemoveAt (0);
			yield return new WaitForSeconds (.002f);
			Debug.Log (transform.position);	

			// GUI
			duplicateTimerText.text = "Duplicate's replay time left:";
			secondsLeftDuplicate.text = "";

			replayTimer -= Time.deltaTime;
			if (replayTimer >= 0) {
				secondsDuplicateReplay.text = replayTimer.ToString ("00");
			}
		}

		// GUI
		duplicateTimerText.text = "";
		secondsDuplicateReplay.text = "";

		hero.GetComponent<PlayerDuplicate> ().enabled = true;
		Destroy (gameObject);
	}
}
