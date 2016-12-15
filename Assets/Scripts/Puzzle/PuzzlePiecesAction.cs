using UnityEngine;
using System.Collections;

public class PuzzlePiecesAction : MonoBehaviour
{


	[Range (0.2f, 2f)]
	public float
		fadeSpeed = 1f;
	public PuzzleGameManagement managementScript;
	
	private bool isChecked = false;
	private Material myMat;
	private Color myColor;
	
	
	
	void Start ()
	{
		myMat = GetComponent <Renderer> ().material;
		myColor = myMat.color;
	}

	public void Init ()
	{
		gameObject.SetActive (true);
		isChecked = false;
		StopCoroutine ("AlphaFade");
		if (myMat != null)
			myMat.color = new Color (myColor.r, myColor.g, myColor.b, 0.5f);
		managementScript.Init ();
	}

	
	// Alpha fading
	public void removePiece ()
	{
		if (isChecked == false) {
			// This is to be sure the coroutine isn't launch multiple times
			isChecked = true;
			StartCoroutine ("AlphaFade");
		}
	}
	
	
	
	IEnumerator AlphaFade ()
	{
		// Alpha start value
		float alpha = 0.5f;
		
		// Loop until aplha is below zero (completely invisibe)
		while (alpha < 1.0f) {
			// Reduce alpha by fadeSpeed amount.
			alpha += fadeSpeed * Time.deltaTime;
			
			// Create a new color using original color RGB values combined
			// with new alpha value. We have to do this because we can't 
			// change the alpha value of the original color directly.
			myMat.color = new Color (myColor.r, myColor.g, myColor.b, alpha);
			
			yield return null;
		}

		// Add the puzzle piece to pieces count
		managementScript.addPiece ();
		gameObject.SetActive (false);
	}
}
