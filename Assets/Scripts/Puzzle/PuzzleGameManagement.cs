using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PuzzleGameManagement : MonoBehaviour
{


	public int piecesMax = 6;
	public GameObject debugObject;
	public PiecesToFind iconsScript;
	public GameObject puzzlePRObject;
	public Image puzzlePRImage;

	public Image puzzleIcon1;
	public Image puzzleIcon2;
	public Image puzzleIcon3;
	public Image puzzleIcon4;
	public Image puzzleIcon5;
	public Image puzzleIcon6;

	public SubtitlePrompter prompterAR;
	public AudioContainer puzzlePieceContainer;
	public AudioContainer puzzlePieceReconstructionContainer;

	public ResetAll resetAll;

	private DebugConsole debugScript;
	private int piecesGot;
	private float fadeSpeed = 3f;
	private bool finished = false;


	void Start ()
	{
		debugScript = debugObject.GetComponent<DebugConsole> ();
		debugScript.ClearMessages ();
		puzzlePRImage.canvasRenderer.SetAlpha (0.0f);
	}

	public void Init ()
	{
		piecesGot = 0;
		finished = false;
		StopCoroutine ("AllPiecesFoundRoutine");
		puzzlePRImage.canvasRenderer.SetAlpha (0.0f);
		iconsScript.Init ();
	}


	
	void Update ()
	{
		// If all the pieces are found, show the picture of all pieces together
		if ((piecesGot == piecesMax || Input.GetKeyDown (KeyCode.P)) && !finished) {
			StartCoroutine ("AllPiecesFoundRoutine");
			finished = true;
		}
	}

	private IEnumerator AllPiecesFoundRoutine ()
	{
		yield return new WaitForSeconds (puzzlePieceContainer.crtClip.length - puzzlePieceContainer.time);
		puzzlePieceReconstructionContainer.PlayOneAtRandom ();
		yield return new WaitForSeconds (puzzlePieceReconstructionContainer.crtClip.length);
		fadeInFinalPict ();
		yield return prompterAR.StartCoroutine ("StartAudioAndText", "TextTD3");
		yield return new WaitForSeconds (5);
		resetAll.Reset ();
	}


	// FadeIn final pict
	private void fadeInFinalPict ()
	{
		puzzlePRObject.SetActive (true);

		// Fade alpha of the UI element
		puzzlePRImage.CrossFadeAlpha (1f, fadeSpeed, true);
	}



	public void addPiece ()
	{
		puzzlePieceContainer.PlayOneAtRandom ();
		piecesGot += 1;
		//debugScript.AddMessage ("Pieces : " + piecesGot + "/" + piecesMax);

		// Icon fadeIn depending on the number of pieces found
		// Yeah, ugly way to do it, but too short on time to think of something else
		if (piecesGot == 1) {
			iconsScript.iconPuzzleFadeIn (puzzleIcon1);
		} else if (piecesGot == 2) {
			iconsScript.iconPuzzleFadeIn (puzzleIcon2);
		} else if (piecesGot == 3) {
			iconsScript.iconPuzzleFadeIn (puzzleIcon3);
		} else if (piecesGot == 4) {
			iconsScript.iconPuzzleFadeIn (puzzleIcon4);
		} else if (piecesGot == 5) {
			iconsScript.iconPuzzleFadeIn (puzzleIcon5);
		} else if (piecesGot == 6) {
			iconsScript.iconPuzzleFadeIn (puzzleIcon6);
		}
	}
}
