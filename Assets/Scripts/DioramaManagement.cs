using UnityEngine;
using System.Collections;

public class DioramaManagement : MonoBehaviour
{
	[Range (0.2f, 2f)]
	public float
		fadeSpeed = 1f;

	public GameObject pict1;
	public GameObject pict2;
	public GameObject pict3;
	public GameObject pict4;
	public GameObject pict5;

	public mainGestion mainGestionScript;
	public SubtitlePrompter prompter;
	public int numberMainCharFound = 0;

	private Color myColor;
	private bool part2Launched = false;



	void Start ()
	{
		myColor = pict1.renderer.material.color;

		pict1.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
		pict2.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
		pict3.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
		pict4.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
		pict5.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
	}

	public void Init ()
	{
		numberMainCharFound = 0;
		part2Launched = false;
		StopCoroutine ("launchNextStep");
		StopCoroutine ("AlphaFade");
	}



	void Update ()
	{
		if (numberMainCharFound == 3 && part2Launched == false) {
			part2Launched = true;
			StartCoroutine ("launchNextStep");

		}
	}


	IEnumerator launchNextStep ()
	{
		// Wait for the last text to finish
		yield return prompter.StartCoroutine ("WaitForAudioToFinish");
		mainGestionScript.launchARstep2 ();
		yield return null;
	}


	// FadeIn the pictures, one after another
	public void launchPictures ()
	{
		StartCoroutine ("AlphaFade");
	}
	
	IEnumerator AlphaFade ()
	{
		yield return new WaitForSeconds (2f);


		// FadeIn for the first pict
		float alpha = 0f;
		while (alpha < 1.0f) {
			alpha += fadeSpeed * Time.deltaTime;
			pict1.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			yield return null;
		}

		// FadeIn for the second
		yield return new WaitForSeconds (0.3f);
		alpha = 0f;
		while (alpha < 1.0f) {
			alpha += fadeSpeed * Time.deltaTime;
			pict2.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			yield return null;
		}

		// Etc.
		yield return new WaitForSeconds (0.3f);
		alpha = 0f;
		while (alpha < 1.0f) {
			alpha += fadeSpeed * Time.deltaTime;
			pict3.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			yield return null;
		}

		yield return new WaitForSeconds (0.3f);
		alpha = 0f;
		while (alpha < 1.0f) {
			alpha += fadeSpeed * Time.deltaTime;
			pict4.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			yield return null;
		}

		yield return new WaitForSeconds (0.3f);
		alpha = 0f;
		while (alpha < 1.0f) {
			alpha += fadeSpeed * Time.deltaTime;
			pict5.renderer.material.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			yield return null;
		}
	}
}






