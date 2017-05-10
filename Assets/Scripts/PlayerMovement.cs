using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float maxSpeed = 5;
	public float heroBoundaryRadius = 0.5f;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// MOVES hero
		Vector3 pos = transform.position;
		pos.x += Input.GetAxis ("Horizontal") * maxSpeed * Time.deltaTime;

		// Changes Hero's animation 
		anim.SetFloat("speed", Input.GetAxis("Horizontal"));

		// RESTRIC the hero to camera's boundaries (top and bottom)
		if(pos.y + heroBoundaryRadius > Camera.main.orthographicSize){
			pos.y = Camera.main.orthographicSize - heroBoundaryRadius;
		}
		if (pos.y - heroBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + heroBoundaryRadius;
		}

		// CALCULATES the orthographic width based on the screen ratio (sides of
		// screen). We cast the width and height to float so taht division give 
		// us decimals
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		// RESTRIC the player to camera's boundaries (sides)
		if(pos.x + heroBoundaryRadius > widthOrtho){
			pos.x = widthOrtho - heroBoundaryRadius;
		}
		if (pos.x - heroBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + heroBoundaryRadius;
		}
			
		// Updates our position
		transform.position = pos;
	}
}
