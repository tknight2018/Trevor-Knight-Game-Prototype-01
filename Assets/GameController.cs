using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public List<Animator> AnimList = new List<Animator>();
	public List<GameObject> XObjects = new List<GameObject> ();

	public GameObject DeskTwo;
	public GameObject GameOverScreen;

	public int CurrentAnim = 0;
	public int XCount = 0;

	public bool WaitingForCheck;
	public bool NailedIt;

	// Use this for initialization
	void Start () {

		StartCoroutine (GameSequence ());
	}
	
	// Update is called once per frame
	void Update () {

		if (WaitingForCheck == true) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				NailedIt = true;
			}
		}

		if (XCount > 2) {
			GameOverScreen.SetActive (true);
			Debug.Log ("You Lost!");
		}

	}

	void TriggerAnimation (Animator animToTrigger)
	{

		animToTrigger.enabled = true;

	}

	void CheckForClick (){

		if (NailedIt == false) {
			//If We failed to hit the space bar during the reaction period...add an X
			XObjects [XCount].SetActive(true);
			XCount = XCount + 1;
		} else {
			NailedIt = false;	
		}
	}

	IEnumerator WaitForClick (float waitTime){

		WaitingForCheck = true;

		yield return new WaitForSeconds (waitTime);

		WaitingForCheck = false;
		CheckForClick ();

	}

	IEnumerator GameSequence (){

		yield return new WaitForSeconds (2.0f);//First Wait fOR THE MONA LISA

		TriggerAnimation(AnimList[0]);//Mona Lisa
	
		StartCoroutine (WaitForClick (1.0f));//How long they should have to react
		yield return new WaitForSeconds (4.0f); // WAIT TIME FOR RECEPTIONIST

		TriggerAnimation(AnimList[1]);//Receptionist

		StartCoroutine (WaitForClick (1.0f));//How long they should have to react
		yield return new WaitForSeconds (7.0f);// WAIT TIME FOR Clipboard

		TriggerAnimation(AnimList[2]);//Dude in fetal position

		StartCoroutine (WaitForClick (1.0f));//How long they should have to react
		yield return new WaitForSeconds (7.0f);// WAIT For next event

		//Do next event
		DeskTwo.SetActive (false);

		StartCoroutine (WaitForClick (1.0f));//How long they should have to react
		yield return new WaitForSeconds (7.0f);// Wait For Next Event
	}
}
