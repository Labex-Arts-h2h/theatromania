using UnityEngine;
using System.Collections;

public class ResetAll : MonoBehaviour
{

	public GameObject[] toDeactivate;
	public GameObject[] toActivate;
	public TestMap testMap;
	public TestLocationService testLocationService;
	public AudioManager audioManager;
	public mainGestion mainGestionScripts;
	public InitMainChars initMainChars;
	public InitShadows initShadow;
	public InitPuzzle initPuzzle;
	public ARFocus arFocus;
	public BioZoomHandler mainCharBioZoomHandler;
	public BioZoomHandler shadowBioZoomHandler;
	public UIImageFading varieteFadingImage;
	public UIImageFading vaudevilleFadingImage;
	public UIImageFading peletierFadingImage;

	public void Reset ()
	{
		foreach (GameObject go in toDeactivate) {
			go.SetActive (false);
		}
		foreach (GameObject go in toActivate) {
			go.SetActive (true);
		}
		testMap.enabled = false;
		testLocationService.disableAwayMode ();
		audioManager.StopAll ();
		initMainChars.Init ();
		initShadow.Init ();
		initPuzzle.Init ();
		arFocus.resetARFocus ();
		mainGestionScripts.Init ();
		//Enable bio zooms for next session
		mainCharBioZoomHandler.EnableZoom ();
		shadowBioZoomHandler.EnableZoom ();
		varieteFadingImage.Reset ();
		vaudevilleFadingImage.Reset ();
		peletierFadingImage.Reset ();
	}
}
