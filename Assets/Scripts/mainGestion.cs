using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO; 
using System;

public class mainGestion : MonoBehaviour
{


	// Public variables
	public DioramaManagement dioramaScript;

	public TestLocationService testLocationService;

	public SubtitlePrompter prompterAR;
	//public GameObject learnMoreButton;
	public GameObject validateARButton;
	public Button buttonOption;
	public Button buttonExit;
	public GameObject posterDuchesse;

	public TestMap mapScript;
	public DebugConsole debugScript;
	public GameObject mainCam;
	public GameObject crosshair;
	public ARFocus crosshairScript;

	public GameObject dioramas;
	public GameObject puzzlePieces;
	public GameObject shadowChars;
	public GameObject mainChars;
	public GameObject iconsChars;
	public GameObject iconsPuzzle;

	public GameObject MainCharWalls;
	public GameObject ShadowCharWalls;
	public GameObject puzzleWalls;

	public ShadowGameManagement shadowScript;

	public GameObject tutoBio;
	public GameObject tutoShadow;
	public GameObject tutoPuzzle;

	public GameObject passTutoBio;
	public GameObject passTutoShadow;
	public GameObject passTutoPuzzle;
	public GameObject touchSkipTuto;

	public FadingAudioSource applauseSource;

	public bool shadowGameCond1 = false;
	public bool shadowGameCond2 = false;

	public BioZoomHandler mainCharBioZoomHandler;
	public BioZoomHandler shadowBioZoomHandler;

	public UIImageFading varieteImageFading;
	public UIImageFading vaudevilleImageFading;
	public UIImageFading peletierImageFading;

	//+++++++++++++++++++++++++ Public functions +++++++++++++++++++++++++

	public void Init ()
	{
		shadowGameCond1 = false;
		shadowGameCond2 = false;
		StopAllCoroutines ();
	}

	// Hide Map
	public void hideMap ()
	{
		GameObject tempMap = GameObject.Find ("[Map]");
		if (tempMap != null)
			tempMap.SetActive (false);
	}

	// Disable interactions with AR
	public void disableInteractions ()
	{
		crosshairScript.enabled = false;

	}

	// Enable interactions with AR
	public void enableInteractions ()
	{
		crosshairScript.enabled = true;
	}


	// Wait a moment before adding markers on the map
	public void launchMapMarker ()
	{
		StartCoroutine ("mapMarker");
	}
	public IEnumerator mapMarker ()
	{
		yield return new WaitForSeconds (0.1f);

		mapScript.placeMarker ();
	}

	//+++++++++++++++++++++++++ Progression functions +++++++++++++++++++++++++

	public void launchPreARStep ()
	{
		StartCoroutine ("PreARStep");
	}

	public IEnumerator PreARStep ()
	{
		StartCoroutine ("TheatreIconFading");
		yield return prompterAR.StartCoroutine (prompterAR.StartAudioAndTextRoutineDelayed ("TextTA1", 2));
		testLocationService.enableAwayMode ();
	}

	private IEnumerator TheatreIconFading ()
	{
		yield return new WaitForSeconds (9f);
		varieteImageFading.StartFadingIn ();
		yield return new WaitForSeconds (2f);
		vaudevilleImageFading.StartFadingIn ();
		yield return new WaitForSeconds (1f);
		peletierImageFading.StartFadingIn ();
	}

	//// Launch the dioramas steps
	public void launchARstep1 ()
	{
		StartCoroutine ("ARstep1");
	}
	public IEnumerator ARstep1 ()
	{
		disableInteractions ();
		varieteImageFading.Reset ();
		vaudevilleImageFading.Reset ();
		peletierImageFading.Reset ();

		//Launch the arrival audio files.
		yield return prompterAR.StartCoroutine (prompterAR.StartAudioAndTextRoutineDelayed ("TextTA3", 2));
		yield return prompterAR.StartCoroutine (prompterAR.StartAudioAndTextRoutineDelayed ("TextTB1", 2));
		// Launch the diorama steps
		dioramaScript.launchPictures ();

		// Diorama launched, start audio with subtitles
		yield return prompterAR.StartCoroutine (prompterAR.StartAudioAndTextRoutine ("TextTB2"));

		dioramas.SetActive (false);
		// Hide crosshair
		crosshair.SetActive (false);
		passTutoBio.SetActive (true);
		
		//Play audio, wait for the end before allowing to skip the tuto
		posterDuchesse.SetActive (true);
		yield return prompterAR.StartCoroutine ("StartAudioAndTextRoutine", "TextTB6-2A");
		posterDuchesse.SetActive (false);
		
		//MainCharWalls.SetActive (true);
		//mainChars.SetActive (true);
		tutoBio.SetActive (true);
		yield return prompterAR.StartCoroutine ("StartAudioAndTextRoutine", "TextTB6-2B");
		passTutoBio.SetActive (false);
		touchSkipTuto.SetActive (true);
	}


	//// Launch the diorama second steps (part 1)
	public void launchARstep2 ()
	{
		StartCoroutine ("ARstep2");
	}
	public IEnumerator ARstep2 ()
	{
		disableInteractions ();

		yield return new WaitForSeconds (1);

		//Disable biography zoom
		mainCharBioZoomHandler.DisableZoom ();

		// Show poster, play corresponding audio
		posterDuchesse.SetActive (true);
		yield return prompterAR.StartCoroutine ("StartAudioAndTextRoutine", "TextTB6");

		//Play brigadier
		//brigadierSource.time = 4.1f;
		//brigadierSource.Play ();
	}

	//// Launch the diorama second steps (part 2)
	public void launchARstep2b ()
	{
		// Poster touched
		StartCoroutine ("ARstep2b");
	}
	public IEnumerator ARstep2b ()
	{

		// Shadow game start
		mainChars.SetActive (false);
		MainCharWalls.SetActive (false);
		iconsChars.SetActive (true);
		//ShadowCharWalls.SetActive (true);
		//shadowChars.SetActive (true);

		// Show tuto (and hide crosshair)
		crosshair.SetActive (false);
		passTutoShadow.SetActive (true);
		tutoShadow.SetActive (true);

		//Play audio wait for the end before allowing to skip the tuto
		yield return prompterAR.StartCoroutine ("StartAudioAndTextRoutine", "TextTC2");
		passTutoShadow.SetActive (false);
		touchSkipTuto.SetActive (true);
	}

	// Enable objectif and controls if tuto's gone
	public void prepARStep3 ()
	{
		enableInteractions ();
		shadowScript.setFindFritz ();
		launchARstep3 ();
	}

	//// Launch the shadow game 
	public void launchARstep3 ()
	{
		StartCoroutine ("ARstep3");
	}
	public IEnumerator ARstep3 ()
	{
		prompterAR.StartAudioAndText ("TextTC4");
		shadowGameCond1 = true;
		return null;
	}

	//// End of the shadow game
	public void launchARstep4 ()
	{
		if (shadowGameCond1 == true && shadowGameCond2 == true) {
			StartCoroutine ("ARstep4");
		}
	}

	private IEnumerator ARstep4 ()
	{
		// To avoid multiples coroutines
		shadowGameCond1 = false;
		shadowGameCond2 = false;

		yield return new WaitForSeconds (1f);

		//Disable biography zoom
		shadowBioZoomHandler.DisableZoom ();

		// Shadow game end
		applauseSource.SetTargetVolume (0.02f);
		applauseSource.Play ();
		yield return prompterAR.StartCoroutine ("StartAudioAndTextRoutine", "TextTC7");
		applauseSource.Fade ();
		yield return prompterAR.StartCoroutine (prompterAR.StartAudioAndTextRoutineDelayed ("TextTD1", 2));

		// Launch puzzle game
		ShadowCharWalls.SetActive (false);
		shadowChars.SetActive (false);
		//puzzleWalls.SetActive (true);
		//puzzlePieces.SetActive (true);
		iconsChars.SetActive (false);
		iconsPuzzle.SetActive (true);

		// Show tuto (and hide crosshair)
		yield return new WaitForSeconds (2);
		crosshair.SetActive (false);
		passTutoPuzzle.SetActive (true);
		tutoPuzzle.SetActive (true);
		disableInteractions ();
		
		// Wait then allow to skip the tuto
		yield return prompterAR.StartCoroutine ("StartAudioAndTextRoutine", "TextTD2");
		passTutoPuzzle.SetActive (false);
		touchSkipTuto.SetActive (true);

		crosshairScript.resetARFocus ();
	}
}



