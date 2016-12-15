using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollText : MonoBehaviour
{
	public float scrollDuration = 10;
	public float minY = 0;
	public float maxY = 0;

	public Image[] logos;

	private float scrollPercent;
	private float waitTime;
	private float currentWaitTime;

	private bool fadeInLogos;

	void Start ()
	{
		scrollPercent = 1;
		waitTime = 8;
		currentWaitTime = waitTime / 2;
		fadeInLogos = true;
	}

	void Update ()
	{
		if (scrollPercent <= 1) {
			transform.localPosition = new Vector3 (transform.localPosition.x, Mathf.Lerp (minY, maxY, scrollPercent), transform.localPosition.z);
			scrollPercent += Time.deltaTime / scrollDuration;
		} else {
			currentWaitTime += Time.deltaTime;
			if (fadeInLogos) {
//				StartCoroutine ("FadeInLogos");
				foreach (Image img in logos) {
					img.color = new Color (img.color.r, img.color.g, img.color.b, 0.8f);
				}
				fadeInLogos = false;
			}
			if (currentWaitTime >= waitTime / 2) {
				transform.localPosition = new Vector3 (transform.localPosition.x, minY, transform.localPosition.z);
				foreach (Image img in logos) {
					img.color = new Color (img.color.r, img.color.g, img.color.b, 0f);
				}
			}
			if (currentWaitTime >= waitTime) {
				fadeInLogos = true;
				scrollPercent = 0;
				currentWaitTime = 0;
			}
		}
	}

	//Doesn't work, don't know why
	public IEnumerator FadeInLogos ()
	{
		foreach (Image img in logos) {
			img.CrossFadeAlpha (0.8f, 1, true);
		}
		return null;
	}
}
