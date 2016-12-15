using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProcessButtonPressed : MonoBehaviour
{

	public AudioContainer soundContainer;
	public GameObject iconMap;
	public GameObject[] buttonObjects;
	public GameObject[] screenObjects;

	public bool IsMenuActivated { get { return isMenuActivated; } }

	private bool isMenuActivated = false;

	public void ActivateButton (GameObject buttonObj)
	{
		soundContainer.PlayOneAtRandom ();
		foreach (GameObject obj in buttonObjects) {
			if (obj != buttonObj) {
				obj.GetComponent<Button> ().enabled = true;
				foreach (Transform child in obj.transform) {
					child.gameObject.SetActive (false);
				}
			} else {
				obj.GetComponent<Button> ().enabled = false;
				foreach (Transform child in obj.transform) {
					child.gameObject.SetActive (true);
				}
			}
		}
	}

	public void ActivateScreenImage (GameObject screenObj)
	{
		isMenuActivated = true;
		iconMap.SetActive (false);
		foreach (GameObject obj in screenObjects) {
			if (screenObj != obj) {
				obj.SetActive (false);
			} else {
				obj.SetActive (true);
			}
		}
	}

	public void DeactivateButton (GameObject buttonObj)
	{
		buttonObj.GetComponent<Button> ().enabled = true;
		foreach (Transform child in buttonObj.transform) {
			child.gameObject.SetActive (false);
		}
	}

	public void DeactivateScreenImage (GameObject screenObj)
	{
		screenObj.SetActive (false);
		iconMap.SetActive (true);
		isMenuActivated = false;
	}
}
