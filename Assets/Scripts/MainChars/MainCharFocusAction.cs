using UnityEngine;
using System.Collections;

public class MainCharFocusAction : MonoBehaviour
{
	
	
	[Range (0.2f, 2f)]
	public float
		fadeSpeed = 1f;
	public SubtitlePrompter prompterAR;
	public DioramaManagement dioramaScript;
	public GameObject charBio;
	
	private bool isChecked = false;
	private Material myMat;
	private Color myColor;
	
	private bool blockHortenseBio = false;
	private bool blockHenriBio = false;
	private bool blockJacquesBio = false;

	private string[] audioNameArray = new string[]{"TextTB3", "TextTB4", "TextTB5"};

	void Start ()
	{
		myMat = charBio.GetComponent <Renderer> ().material;
		myColor = myMat.color;
		myMat.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
	}

	public void Init ()
	{
		isChecked = false;
		if (myMat != null)
			myMat.color = new Color (myColor.r, myColor.g, myColor.b, 0f);
		blockHortenseBio = false;
		blockHenriBio = false;
		blockJacquesBio = false;
		StopCoroutine ("AlphaFade");
		dioramaScript.Init ();
	}
	

	// Alpha fading
	public void showBio ()
	{
		if (isChecked == false) {
			// This is to be sure the coroutine isn't launch multiple times
			isChecked = true;
			
			StartCoroutine ("AlphaFade");
		}
	}
	
	
	
	IEnumerator AlphaFade ()
	{
		float alpha = 0f;

		while (alpha < 1.0f) {
			// Up alpha by fadeSpeed amount.
			alpha += fadeSpeed * Time.deltaTime;

			myMat.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			
			yield return null;
		}

		// Launch prompter text depending on the char found
		if (gameObject.name == "Hortense-Schneider" && !blockHortenseBio) {
			prompterAR.StartAudioAndText (audioNameArray [dioramaScript.numberMainCharFound]);
			dioramaScript.numberMainCharFound += 1;
			blockHortenseBio = true;
		} else if (gameObject.name == "Henri-Meilhac" && !blockHenriBio) {
			prompterAR.StartAudioAndText (audioNameArray [dioramaScript.numberMainCharFound]);
			dioramaScript.numberMainCharFound += 1;
			blockHenriBio = true;
		} else if (gameObject.name == "Jacques-Offenbach" && !blockJacquesBio) {
			prompterAR.StartAudioAndText (audioNameArray [dioramaScript.numberMainCharFound]);
			dioramaScript.numberMainCharFound += 1;
			blockJacquesBio = true;
		}
	}
}




