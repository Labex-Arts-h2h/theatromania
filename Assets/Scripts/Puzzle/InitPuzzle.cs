using UnityEngine;
using System.Collections;

public class InitPuzzle : MonoBehaviour
{

	public PuzzlePiecesAction[] puzzleActions;

	public void Init ()
	{
		foreach (PuzzlePiecesAction action in puzzleActions) {
			action.Init ();
		}
	}
}
