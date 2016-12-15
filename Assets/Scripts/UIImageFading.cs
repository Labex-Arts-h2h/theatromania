using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImageFading : MonoBehaviour
{
	Image image;

	void Start ()
	{
		image = GetComponent<Image> ();
	}

	public void StartFadingIn ()
	{
		//image.CrossFadeAlpha (1, 1, true);
		StartCoroutine (FadingRoutine ());
	}

	public IEnumerator FadingRoutine ()
	{
		float i = 0;
		while (i <= 1) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, i);
			i += 0.1f;
			yield return new WaitForSeconds (0.1f);
		}
	}

	public void Reset ()
	{
		image.color = new Color (image.color.r, image.color.g, image.color.b, 0);
	}
}