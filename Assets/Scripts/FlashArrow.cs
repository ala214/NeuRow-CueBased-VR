using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used

public class FlashArrow : MonoBehaviour {
	public static int stimB;
	public static int fps_countL = 0; 
	public static int fps_countR = 0;
	public static float cycleDuration;
	public GameObject arrowL, arrowR;


	
	void Start () {	
		//arrowL = GameObject.Find("LeftArrowVR");
		//arrowR = GameObject.Find("RightArrowVR");
		arrowL = GameObject.Find("arrowLF");
		arrowR = GameObject.Find("arrowRF");
		//Debug.Log("Flash Arrow!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
	}
	
	
	void Update () {	

		//StopAllCoroutines();
		//Debug.Log("stopping all coroutines----------");
		if (Settings.flash) {
			if (MoveBoat.left) {
				// make arrow flash 30 Hz
				//Debug.Log ("inside FlashArrow - move boat left");
				StartCoroutine (blinkLeft (30.0f));
				//Debug.Log("blink call counter total: "+fps_countL);
			}

			if (MoveBoat.right) {
				// make arrow flash 40 Hz
				//Debug.Log ("inside FlashArrow - move boat right");
				StartCoroutine (blinkRight (40.0f));			
			}
		}
	}

	IEnumerator blinkLeft(float frequency) {
		cycleDuration = 1.0f / frequency;
		//Debug.Log("flashing left");
//		while(MoveBoat.left)
		for(int i=0; i<30; i++)
		{
			yield return new WaitForSeconds(cycleDuration);
			arrowL.SetActive(false);
			//Debug.Log("left turned off");
			yield return new WaitForSeconds(cycleDuration);
			//Debug.Log("waited for frequency");
			arrowL.SetActive(true);
			//Debug.Log("left turned on");
//			yield return new WaitForSeconds(cycleDuration);

			fps_countL += 1;
		}

//		Debug.Log("Left Counter: "+fps_countL);
	}

	IEnumerator blinkRight(float frequency) {
		cycleDuration = 1.0f / frequency;
		//Debug.Log("flashing right");
		for(int j=0; j<40; j++)
		{
			yield return new WaitForSeconds(cycleDuration);
			arrowR.SetActive(false);
			//Debug.Log("right turned off");
			yield return new WaitForSeconds(cycleDuration);
			arrowR.SetActive(true);
			//Debug.Log("right turned on");

			fps_countR += 1;
		}

//		Debug.Log("Right Counter: "+fps_countR);
	}



}
