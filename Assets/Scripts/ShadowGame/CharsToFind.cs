using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharsToFind : MonoBehaviour
{

	[Range (0.2f, 2f)]
	public float
		fadeSpeed = 1f;
	public Image charIcon1;
	public Image charIcon2;
	public Image charIcon3;



	void Start ()
	{
		// Need to set the icons alpha at 255 (max) for the CrossFadeAlpha to work
		// Can only fadeOut but not fadeIn if it isn't like this
		charIcon1.canvasRenderer.SetAlpha (0.0f);
		charIcon2.canvasRenderer.SetAlpha (0.0f);
		charIcon3.canvasRenderer.SetAlpha (0.0f);
	}

	public void Init ()
	{
		charIcon1.canvasRenderer.SetAlpha (0.0f);
		charIcon2.canvasRenderer.SetAlpha (0.0f);
		charIcon3.canvasRenderer.SetAlpha (0.0f);
	}


	// Alpha fadeOut
	public void iconFadeIn (Image aCharIcon)
	{
		// Fade alpha of the UI element
		aCharIcon.CrossFadeAlpha (1f, fadeSpeed, true);
	}
}




