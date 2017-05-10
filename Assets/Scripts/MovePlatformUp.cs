using UnityEngine;
using System.Collections;

public class MovePlatformUp : MonoBehaviour {

	public Vector2 velocity;
	public Rigidbody2D rb2d;
	public float maxDistance;
	float distance = 0f;
	bool moveRight = true;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		if (moveRight == true && distance < maxDistance) {
			rb2d.MovePosition (rb2d.position + velocity * Time.fixedDeltaTime);
			distance += velocity.y;
		}
		if (distance >= maxDistance) {
			moveRight = false;
		}

		if (moveRight == false && distance > -maxDistance) {
			rb2d.MovePosition (rb2d.position - velocity * Time.fixedDeltaTime);
			distance -= velocity.y;
		}
		if (distance <= -maxDistance) {
			moveRight = true;
		}


	}
}
