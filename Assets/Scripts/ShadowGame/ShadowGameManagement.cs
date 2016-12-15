using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShadowGameManagement : MonoBehaviour
{


	public GameObject ARObject;
	public GameObject mainGestionObject;
	public CharsToFind iconScript;

	public Image charIcon1;
	public Image charIcon2;
	public Image charIcon3;

	public GameObject findFritz;
	public GameObject findDuchesse;
	public GameObject findBoum;

	public SubtitlePrompter prompter;

	public int charFound{ get; set; }
	public string charToFind{ get; set; }
	private mainGestion mainGestionScript;
	private bool char1Done;
	private bool char2Done;
	private bool char3Done;



	void Start ()
	{
		mainGestionScript = mainGestionObject.GetComponent<mainGestion> ();
		Init ();
	}

	public void Init ()
	{
		char1Done = false;
		char2Done = false;
		char3Done = false;
		charFound = 0;
		charToFind = "Fritz1-s";
		StopAllCoroutines ();
		iconScript.Init ();
	}

	void Update ()
	{
		// Fritz found -> search Boum
		if (charFound == 1 && !char1Done) {
			char1Done = true;
			StartCoroutine ("SearchBoum");
		}

		// Boum found -> search Duchesse
		else if (charFound == 2 && !char2Done) {
			char2Done = true;
			StartCoroutine ("SearchDuchesse");
		}

		// All characters found -> launch next step
		else if (charFound == 3 && !char3Done) {
			char3Done = true;
			StartCoroutine ("LaunchNextStep");
		}
	}

	private IEnumerator SearchBoum ()
	{
		findFritz.SetActive (false);
		yield return prompter.StartCoroutine ("StartAudioAndTextRoutine", "TextTC1");
		charToFind = "Boum-s";
		prompter.StartCoroutine (prompter.StartAudioAndTextRoutineDelayed ("TextTC5", 1));
		findBoum.SetActive (true);
		iconScript.iconFadeIn (charIcon1);
	}

	private IEnumerator SearchDuchesse ()
	{

		findBoum.SetActive (false);
		yield return prompter.StartCoroutine ("StartAudioAndTextRoutine", "TextTC3");
		charToFind = "Duchesse-s";
		prompter.StartCoroutine (prompter.StartAudioAndTextRoutineDelayed ("TextTC6", 1));
		findDuchesse.SetActive (true);
		iconScript.iconFadeIn (charIcon2);
	}

	private IEnumerator LaunchNextStep ()
	{
		findDuchesse.SetActive (false);
		yield return prompter.StartCoroutine ("StartAudioAndTextRoutine", "TextTC3-B");
		iconScript.iconFadeIn (charIcon3);
		mainGestionScript.shadowGameCond2 = true;
		mainGestionScript.launchARstep4 ();
	}
	
	

	public void setFindFritz ()
	{
		findFritz.SetActive (true);
	}
}
