using UnityEngine;
using System.Collections;

public class ARFocusAction : MonoBehaviour
{

	[Range (0.2f, 2f)]
	public float
		fadeSpeed = 1f;
	public GameObject bio;
	public GameObject realChar;
	public ShadowGameManagement managementScript;

	private bool isChecked = false;
	private Material myMat;
	private Material realCharMat;
	private Color myColor;
	private Color realCharColor;
	private string charToFindName;


	void Start ()
	{
		myMat = GetComponent <Renderer> ().material;
		realCharMat = realChar.GetComponent<Renderer> ().material;
		myColor = myMat.color;
		realCharColor = realCharMat.color;
	}

	public void Init ()
	{
		isChecked = false;
		realChar.SetActive (false);
		if (myMat != null)
			myMat.color = new Color (myColor.r, myColor.g, myColor.b, 1);
		if (realCharMat != null)
			realCharMat.color = new Color (realCharColor.r, realCharColor.g, realCharColor.b, 0);
		if (bio != null)
			bio.SetActive (false);
		StopCoroutine ("AlphaFadeOut");
		StopCoroutine ("BioFadeIn");
		StopCoroutine ("AlphaFadeIn");
		managementScript.Init ();
	}

	void Update ()
	{
		charToFindName = managementScript.charToFind;
	}



	// Fade the shadow to reveal the character behind it
	public void removeShadow ()
	{
		if (isChecked == false) {
			// This is to be sure the coroutine isn't launch multiple times
			isChecked = true;

			// Activate the real character behind the shadow
			realChar.SetActive (true);

			StartCoroutine ("AlphaFadeIn");
		}
	}


	//// Crossfade between the shadow and the real character 
	IEnumerator AlphaFadeIn ()
	{

		// Alpha start value
		float alpha = 1.0f;
		float alpha2 = 0f;

		// Set the alpha of the real char behind the shadow to 0
		realCharMat.color = new Color (realCharColor.r, realCharColor.g, realCharColor.b, alpha2);


		// Loop until aplha is below zero (completely invisibe)
		while (alpha > 0.0f) {
			// Reduce shadow alpha by fadeSpeed amount.
			alpha -= fadeSpeed * Time.deltaTime;

			// Augment real char alpha by fadeSpeed amount.
			alpha2 += fadeSpeed * Time.deltaTime;

			myMat.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			realCharMat.color = new Color (realCharColor.r, realCharColor.g, realCharColor.b, alpha2);

			yield return null;
		}



		// If the character found is part of the needed ones,
		// then, add it in the management script
		if (gameObject.name == charToFindName) {
			// Show the biographie of the character found 
			bio.SetActive (true);
			bio.renderer.material.color = new Color (realCharColor.r, realCharColor.g, realCharColor.b, 0f);
			StartCoroutine ("BioFadeIn");

			managementScript.charFound += 1;
		} else {
			// Wait 2 seconds before putting back the shadow
			yield return new WaitForSeconds (1.5f);

			StartCoroutine ("AlphaFadeOut");
		}
	}



	//// Crossfade back between the shadow and the real char
	IEnumerator AlphaFadeOut ()
	{
		
		// Alpha start value
		float alpha = 0f;
		float alpha2 = 1.0f;
		

		// Loop until aplha is below zero (completely invisibe)
		while (alpha < 1.0f) {
			// Reduce shadow alpha by fadeSpeed amount.
			alpha += fadeSpeed * Time.deltaTime;
			
			// Augment real char alpha by fadeSpeed amount.
			alpha2 -= fadeSpeed * Time.deltaTime;

			myMat.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			realCharMat.color = new Color (realCharColor.r, realCharColor.g, realCharColor.b, alpha2);
			
			yield return null;
		}


		// Re-enable the possibility of finding the char after 2 seconds
		yield return new WaitForSeconds (2f);

		realChar.SetActive (false);
		isChecked = false;
	}



	//// FadeIn the Bio       ++++++++++ STILL NOT WORKING (bio keep showing up instantly) ++++++++++
	IEnumerator BioFadeIn ()
	{

		// Alpha start value
		float alpha = 0f;

		while (alpha < 1.0f) {
			// Up alpha by fadeSpeed amount.
			alpha += fadeSpeed * Time.deltaTime;

			Color tempColor = bio.renderer.material.color;
			tempColor.a = alpha;
			bio.renderer.material.color = tempColor;
		}
		
		yield return null;
	}
}
