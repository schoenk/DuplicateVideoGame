using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLevel : MonoBehaviour {
	private float timer = 2.5f;
	private bool wonLevel = false;
	private Text youWon;

	// Use this for initialization
	void Start () {
		youWon = GameObject.Find ("YouWon").GetComponent<Text> ();
	}

	void OnTriggerEnter2D(){
		if(GetComponent<DoorOpener>().isDoorOpen == true){
			wonLevel = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (wonLevel == true) {
			youWon.text = "YOU WON!";
			timer -= Time.deltaTime;
		}
		if (timer < 0) {
			Application.LoadLevel ("Level2");
		}
	}
}
