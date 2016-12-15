using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ARFocus : MonoBehaviour
{


	public Camera myCam;
	public GameObject debugObject;

	private DebugConsole debugScript;
	private GameObject lastObjectHit = null;
	private GameObject lastObjectHit2 = null;
	private GameObject lastObjectHit3 = null;

	private bool isSameObject = false;
	private bool isTimerOn = false;
	private string ARTag = "AR";
	private string PuzzleTag = "Puzzle";
	private string MainCharTag = "MainChar";

	private GameObject actualObject = null;
	private GameObject actualObject2 = null;
	private GameObject actualObject3 = null;

	void Start ()
	{
		debugScript = debugObject.GetComponent<DebugConsole> ();
		debugScript.ClearMessages ();
	}

	void Update ()
	{
		try {
			// Launch a raycast from the center of the ARCamera
			Ray ray = myCam.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			RaycastHit hit;
			
			if (Physics.Raycast (ray, out hit)) {


				//// Raycast hitting a shadowed character
				//
				if (hit.transform.gameObject.CompareTag (ARTag)) {
					actualObject = hit.transform.gameObject;
					if (lastObjectHit == null) {
						lastObjectHit = actualObject;
					}
					if (actualObject.name != lastObjectHit.name) {
						lastObjectHit = actualObject;

						// Target lost, stop the timer (if started)
						isTimerOn = false;
						isSameObject = false;

						StopCoroutine ("checkObject");
					} else if (isSameObject) {
						// Same target kept during the whole time
						ARFocusAction ARScript = lastObjectHit.GetComponent<ARFocusAction> ();
						if (ARScript != null) {
							ARScript.removeShadow ();
						}

						isSameObject = false;
					}

					if (!isTimerOn && actualObject != null) {
						// Start a one second timer
						StartCoroutine ("checkObject", actualObject);
					}
				}

				//// Raycast hitting a puzzle piece
				//
				else if (hit.transform.gameObject.CompareTag (PuzzleTag)) {
					actualObject2 = hit.transform.gameObject;
					if (lastObjectHit2 == null) {
						lastObjectHit2 = actualObject2;
					}
					if (actualObject2.name != lastObjectHit2.name) {
						lastObjectHit2 = actualObject2;
						
						// Target lost, stop the timer (if started)
						isTimerOn = false;
						isSameObject = false;
						StopCoroutine ("checkObject");
					} else if (isSameObject) {
						// Same target kept during the whole time
						PuzzlePiecesAction puzzleScript = lastObjectHit2.GetComponent<PuzzlePiecesAction> ();
						if (puzzleScript != null) {
							puzzleScript.removePiece ();
						}
						isSameObject = false;
					}
					if (!isTimerOn && actualObject2 != null) {
						// Start a one second timer
						StartCoroutine ("checkObject", actualObject2);
					}
				}



				//// Raycast hitting a Main Character
				//
				else if (hit.transform.gameObject.CompareTag (MainCharTag)) {
					actualObject3 = hit.transform.gameObject;
					if (lastObjectHit3 == null) {
						lastObjectHit3 = actualObject3;
					}
					if (actualObject3.name != lastObjectHit3.name) {
						lastObjectHit3 = actualObject3;
						
						// Target lost, stop the timer (if started)
						isTimerOn = false;
						isSameObject = false;
						
						StopCoroutine ("checkObject");
					} else if (isSameObject) {
						// Same target kept during the whole time
						MainCharFocusAction MainCharScript = actualObject3.GetComponent<MainCharFocusAction> ();
						if (MainCharScript != null) {
							MainCharScript.showBio ();
						}
						isSameObject = false;
					}
					
					if (!isTimerOn && actualObject3 != null) {
						// Start a one second timer
						StartCoroutine ("checkObject", actualObject3);
					}
				}



				//// Raycast hitting nothing -> reset values
				//
				else {
					actualObject = null;
					actualObject2 = null;
					actualObject3 = null;
					lastObjectHit = null;
					lastObjectHit2 = null;
					lastObjectHit3 = null;

					isTimerOn = false;
					isSameObject = false;
					StopCoroutine ("checkObject");
				}
			}
		} catch (Exception e) {
			debugScript.AddMessage ("ERROR : " + e);
		}
	}



	public void resetARFocus ()
	{
		actualObject = null;
		actualObject2 = null;
		actualObject3 = null;
		lastObjectHit = null;
		lastObjectHit2 = null;
		lastObjectHit3 = null;
		
		isTimerOn = false;
		isSameObject = false;
		StopCoroutine ("checkObject");
	}



	// Launch a one second timer to see if the target stays the same
	private IEnumerator checkObject (GameObject focusObject)
	{
		isTimerOn = true;

		GameObject actualObjectTemp = null;

		//// +++ TODO +++ : launch crosshair animation + sound

		yield return new WaitForSeconds (1f);

		// Launch a raycast from the center of the ARCamera
		Ray ray = myCam.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			// Raycast hitting an AR character
			if (hit.transform.gameObject.CompareTag (ARTag)) {
				actualObjectTemp = hit.transform.gameObject;
			}

			// Raycast hitting a puzzle piece
			if (hit.transform.gameObject.CompareTag (PuzzleTag)) {
				actualObjectTemp = hit.transform.gameObject;
			}

			// Raycast hitting a main char
			if (hit.transform.gameObject.CompareTag (MainCharTag)) {
				actualObjectTemp = hit.transform.gameObject;
			}
		}

		if (actualObjectTemp.name == focusObject.name) {
			// Same object kept during the timer
			isSameObject = true;
		}

		isTimerOn = false;
	}

}
