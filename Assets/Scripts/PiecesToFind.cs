using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PiecesToFind : MonoBehaviour
{


	[Range (0.2f, 2f)]
	public float
		fadeSpeed = 1f;

	// Forced to get them here 'cause of alpha reason (see below)
	public Image puzzleIcon1;
	public Image puzzleIcon2;
	public Image puzzleIcon3;
	public Image puzzleIcon4;
	public Image puzzleIcon5;
	public Image puzzleIcon6;

	
	
	void Start ()
	{
		Init ();
	}

	public void Init ()
	{
		// Need to set the icons alpha at 255 (max) for the CrossFadeAlpha to work
		// Can only fadeOut but not fadeIn if it isn't like this when the scene's loading
		puzzleIcon1.canvasRenderer.SetAlpha (0.0f);
		puzzleIcon2.canvasRenderer.SetAlpha (0.0f);
		puzzleIcon3.canvasRenderer.SetAlpha (0.0f);
		puzzleIcon4.canvasRenderer.SetAlpha (0.0f);
		puzzleIcon5.canvasRenderer.SetAlpha (0.0f);
		puzzleIcon6.canvasRenderer.SetAlpha (0.0f);
	}
	


	// Alpha fadeIn
	public void iconPuzzleFadeIn (Image aCharIcon)
	{
		// Fade alpha of the UI element
		aCharIcon.CrossFadeAlpha (1f, fadeSpeed, true);
	}
}

