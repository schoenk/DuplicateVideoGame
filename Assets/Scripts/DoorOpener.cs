using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {
	public bool isDoorOpen = false;
	public GameObject switchPrefab;

	public Sprite doorClose;
	public Sprite doorOpen;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		if (spriteRenderer.sprite == null) {
			spriteRenderer.sprite = doorClose;
		}

		// We don't need the next line of code since the switch is being assigned at the inspector
		//switchPrefab = GameObject.Find("switch");
		if (switchPrefab != null) {
			Debug.Log ("found switch");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (switchPrefab.GetComponent<SwitchOnOff> ().isSwitchOn == true) {
			Debug.Log ("door is open");
			isDoorOpen = true;
			spriteRenderer.sprite = doorOpen;
		}
		else {
			isDoorOpen = false;
			spriteRenderer.sprite = doorClose;
		}
	}
}
