using UnityEngine;
using System.Collections;

public class AudioContainerTest : MonoBehaviour
{

	public bool startSequence = false;
	public bool pauseSequence = false;
	private AudioContainer container;
	private bool started;
	private bool paused;

	void Start ()
	{
		container = GetComponent<AudioContainer> ();
		started = false;
	}
	
	void Update ()
	{
		if (startSequence) {
			if (!started) {
				container.PlaySequence ();
				started = true;
			}
		} else {
			if (started) {
				container.StopSequence ();
				started = false;
				pauseSequence = false;
			}
		}

		if (pauseSequence) {
			if (!paused) {
				container.PauseSequence ();
				paused = true;
			}
		} else {
			if (paused) {
				container.PlaySequence ();
				paused = false;
			}
		}
	}
}
