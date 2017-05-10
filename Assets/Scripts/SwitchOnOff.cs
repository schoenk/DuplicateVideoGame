using UnityEngine;
using System.Collections;

public class SwitchOnOff : MonoBehaviour {
	private float switchTimer;
	public bool isSwitchOn = false;

	public Sprite switchOff;
	public Sprite switchOn;
	private SpriteRenderer spriteRenderer;
	private Vector3 switchPosition;

	// Use this for initialization
	void Start () {
		switchTimer = -1;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		if (spriteRenderer.sprite == null) {
			spriteRenderer.sprite = switchOff; 
		}
		switchPosition = transform.position;
	}

	void OnTriggerEnter2D(){
		if (isSwitchOn == false) {
			Debug.Log ("Switch is ON");
			isSwitchOn = true;
			spriteRenderer.sprite = switchOn;
			switchTimer = 2f;

			// to change the sprite position to be at the right coordinates (lower)
			switchPosition.y -= 0.20f;  //new Vector3 (-3.95f, 3.60f, 0);
			transform.position = switchPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switchTimer -= Time.deltaTime;
		if (switchTimer < 0 && isSwitchOn == true) {
			Debug.Log ("Switch is OFF");
			isSwitchOn = false;
			spriteRenderer.sprite = switchOff;

			// to change the sprite position to be at the right coordinates (higher)
			switchPosition.y += 0.20f;  //new Vector3 (-3.95f, 3.90f, 0);
			transform.position = switchPosition;
		}
	}
}
