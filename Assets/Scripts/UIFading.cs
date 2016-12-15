using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFading : MonoBehaviour {


	public GameObject myButton;
	public Image myPanel;
	public Text myText = null;
	[Range(0f, 5f)]
	public float fadeTime = 1.5f;

	
	public void Fading() 
	{
		// Fade alpha of the UI element
		myPanel.CrossFadeAlpha (0f, fadeTime, true);
		if (myText != null)
		{
			myText.CrossFadeAlpha (0f, fadeTime, true);
		}
	}


	void Update ()
	{
		// Disable the UI element when the fading is done
		if (myPanel.canvasRenderer.GetAlpha() == 0f)
		{
			myButton.SetActive(false);
		}
	}
}
