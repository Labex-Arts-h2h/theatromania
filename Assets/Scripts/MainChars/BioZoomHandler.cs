using UnityEngine;
using System.Collections;

public class BioZoomHandler : MonoBehaviour
{
	public GameObject[] bioObjects;
	public GameObject[] zoomMenuObjects;

	public void EnableZoom ()
	{
		//Enable click
		foreach (GameObject bio in bioObjects) {
			bio.GetComponent<ZoomBiography> ().clickIsActive = true;
		}
	}

	public void DisableZoom ()
	{
		//Disable click
		foreach (GameObject bio in bioObjects) {
			bio.GetComponent<ZoomBiography> ().clickIsActive = false;
		}
		//Remove zoom if displayed
		foreach (GameObject menu in zoomMenuObjects) {
			menu.SetActive (false);
		}
		//Enable unzoomed biography
		foreach (GameObject bio in bioObjects) {
			bio.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}
}
