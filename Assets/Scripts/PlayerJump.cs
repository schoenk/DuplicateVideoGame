using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {
	public float jumpHeight = 100f;
	public bool isFalling = false;
	public Vector2 jumpVector;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump") && isFalling == false) {
			Debug.Log ("Jump");
			GetComponent<Rigidbody2D> ().AddForce (jumpVector, ForceMode2D.Impulse);
			isFalling = true;
		}
	}

	void OnCollisionEnter2D(){
		isFalling = false;
	}
}
