using UnityEngine;
using System.Collections;

public class InitMainChars : MonoBehaviour
{

	public MainCharFocusAction[] charActions;

	public void Init ()
	{
		foreach (MainCharFocusAction action in charActions) {
			action.Init ();
		}
	}
}
