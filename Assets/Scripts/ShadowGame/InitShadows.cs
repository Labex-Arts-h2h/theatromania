using UnityEngine;
using System.Collections;

public class InitShadows : MonoBehaviour
{

	public ARFocusAction[] shadowActions;

	public void Init ()
	{
		foreach (ARFocusAction action in shadowActions) {
			action.Init ();
		}
	}
}
