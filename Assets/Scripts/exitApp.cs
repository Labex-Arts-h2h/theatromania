using UnityEngine;
using System.Collections;

public class exitApp : MonoBehaviour {


	void Update() 
	{
		int fingerCount = 0;

		foreach (Touch touch in Input.touches) 
		{
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;
			
		}

		// If 4 fingers on screen, then exit app
		if (fingerCount == 4)
		{
			leaveApp();
		}
	}


	public void leaveApp ()
	{
		Application.Quit ();
	}
}
