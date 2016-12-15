using UnityEngine;
using System.Collections;

public class CurtainHandler : MonoBehaviour
{

	public FadingAudioSource brigadierSource;
	public FadingAudioSource curtainOpenSource;
	public FadingAudioSource applauseSource;
	public LogoFadeOut logoFadeOut;
	public Animator leftCurtain;
	public Animator rightCurtain;
	public Animator frontCurtain;
	public Animator backCurtain;
	public CurtainOpenIndicator curtainIndicator;
	public GameObject curtainButtonQuickOpen;
	public SubtitlePrompter subtitlePrompter;
	public ProcessButtonPressed menuButtonScript;
	public GameObject iconMap;

	private bool opening;
	private float brigadierLength;
	private float launchTime;
	private bool logoFaded;
	private bool animLaunched;
	private bool terminated;

	void Start ()
	{
		opening = false;
		logoFaded = false;
		animLaunched = false;
		brigadierLength = brigadierSource.clip.length;
		logoFadeOut.fadeOutSpeed = 1;//logo fade out lasts 1 second
		terminated = false;
	}
	
	void Update ()
	{
		if (opening) {
			float timeSinceLaunch = Time.time - launchTime;
			//fade out logo when brigadier sound is almost finished (2 seconds left)
			if (!logoFaded && (brigadierLength - timeSinceLaunch) <= 2) {
				logoFadeOut.Fading ();
				logoFaded = true;
			}
			//if brigadier sound has finished, open curtains
			if (!animLaunched && !brigadierSource.isPlaying) {
				curtainOpenSource.Play ();
				applauseSource.Play ();
				leftCurtain.SetBool ("launchCurtainsAnim", true);
				rightCurtain.SetBool ("launchCurtainsAnim", true);
				frontCurtain.SetBool ("launchCurtainsAnim", true);
				backCurtain.SetBool ("launchCurtainsAnim", true);
				animLaunched = true;
			}
		}

		if (curtainIndicator.IsCurtainOpened () && !terminated) {
			FinalizeCurtainProcess ();
			terminated = true;
		}
	}

	/// <summary>
	/// Launches the process of curtain opening, including brigadier sound and curtain opening.
	/// </summary>
	public void LaunchCurtainOpening ()
	{
		opening = true;
		brigadierSource.Play ();
		launchTime = Time.time;
	}

	public void FinalizeCurtainProcess ()
	{
		StartCoroutine ("FinalizeCurtainProcessRoutine");
	}

	/// <summary>
	/// Finalize the opening by deactivating curtain related objects and stopping sounds.
	/// </summary>
	private IEnumerator FinalizeCurtainProcessRoutine ()
	{
		opening = false;
		//fade out curtain sounds
		if (curtainOpenSource.isPlaying)
			curtainOpenSource.Fade ();
		if (applauseSource.isPlaying)
			applauseSource.Fade ();
		if (brigadierSource.isPlaying) {
			brigadierSource.Fade ();
		}
		//deactivate logo if necessary
		if (logoFadeOut.gameObject.activeInHierarchy) {
			logoFadeOut.gameObject.SetActive (false);
		}
		//deactivate curtains
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
		}
		//deactivate curtain button
		curtainButtonQuickOpen.SetActive (false);
		//launch presentation speech
		yield return subtitlePrompter.StartCoroutine (subtitlePrompter.StartAudioAndTextRoutineDelayed ("TextTT1", 2));
		//activate Map AFTER the speech is over
		if (!menuButtonScript.IsMenuActivated)
			iconMap.SetActive (true);
	}
}
