using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoFadeOut : MonoBehaviour
{


	[Range (0f, 5f)]
	public float
		fadeOutSpeed = 1f;

	[Range (0f, 5f)]
	public float
		fadeInSpeed = 2f;

	public Image myPanel;
	


	void Start ()
	{
		//myPanel.CrossFadeAlpha (0f, 0f, true);
	}

	
	public void Fading ()
	{
		StartCoroutine ("logoAnim");
	}


	public IEnumerator logoAnim ()
	{
		//myPanel.CrossFadeAlpha (1f, fadeInSpeed, true);

		//yield return new WaitForSeconds (2f);

		myPanel.CrossFadeAlpha (0f, fadeOutSpeed, true);

		yield return new WaitForSeconds (fadeOutSpeed);

		gameObject.SetActive (false);
	}


}






