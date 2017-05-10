using UnityEngine;
using System.Collections;

public class PlayerDuplicate : MonoBehaviour {
	public Vector3 duplicateOffSet = new Vector3 (1.5f, 0, 0);
	public GameObject heroPrefab;
	public float duplicationDelay = .25f;

	// Hero's transparency during inactivity's variables
	public RuntimeAnimatorController fullColorHero;
	public RuntimeAnimatorController fadedHero;
	private Animator animator;

	// So that we don't duplicate in every single frame.
	float coolDownTimer = 0;

    // Makes sure that hero is full color at the start of duplication
	void Start(){
		animator = GetComponent<Animator> ();
		if (animator.runtimeAnimatorController == null) {
			animator.runtimeAnimatorController = fullColorHero;
		}
	}
	
	// Update is called once per frame
	void Update () {
		coolDownTimer -= Time.deltaTime;

		// We don't use Input.GetKey, because it will required us that we specifically
		// name a key, like "the space bar"
		// GetButtonDown -> player will shoot once everytime the space bar is hit
		// GetButton -> player will shoot non-stop while the space bar is being held down
		if(Input.GetButtonDown("Duplicate") && coolDownTimer <= 0){
			// DUPLICATE
			Debug.Log("Hero was duplicated");

            // Once hero duplicates, its ability to duplicate again is disable
            // until the HeroDuplicate destruct itself.
			coolDownTimer = duplicationDelay;
			Vector3 offset = transform.rotation * duplicateOffSet;

			Instantiate (heroPrefab, transform.position + duplicateOffSet, transform.rotation);

			// Disable Hero's scripts when HeroDuplicate become active
			GetComponent<PlayerMovement>().enabled = false;
			GetComponent<PlayerJump> ().enabled = false;
			GetComponent<PlayerDuplicate> ().enabled = false;

			// Gives transparency to Hero
			animator.runtimeAnimatorController = fadedHero;

			//Vector3 pos = transform.position;
			//pos.y += 2; //= new Vector3 (2, 0, 0);
			//transform.position = pos;
		}
	}
}
