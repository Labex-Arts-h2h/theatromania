using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SubtitlePrompter : MonoBehaviour
{
	public Text prompterText;
	public GameObject soundManager;

	private SubtitleSynchronizer synchronizer;
	private AudioContainer container;

	void Update ()
	{
		if (synchronizer != null)
			prompterText.text = synchronizer.GetCurrentText ();
	}

	public void StartAudioAndText (string name)
	{
		if (container != null && container.IsSequencePlaying ()) {
			container.StopSequence ();
		}
		if (synchronizer != null)
			synchronizer.Reset ();

		GameObject audioObj = soundManager.transform.FindChild (name).gameObject;
		if (audioObj != null) {
			synchronizer = audioObj.GetComponent<SubtitleSynchronizer> ();
			container = audioObj.GetComponent<AudioContainer> ();
			container.PlaySequence ();
		}
	}

	public IEnumerator StartAudioAndTextRoutineDelayed (string name, float delay)
	{
		yield return new WaitForSeconds (delay);
		StartAudioAndText (name);
		while (container.IsSequencePlaying()) {
			yield return new WaitForSeconds (0.1f);
		}
	}

	public IEnumerator StartAudioAndTextRoutine (string name)
	{
		return StartAudioAndTextRoutineDelayed (name, 0);
	}
	
	public IEnumerator WaitForAudioToFinish ()
	{
		while (container.IsSequencePlaying()) {
			yield return new WaitForSeconds (0.1f);
		}
	}

	public void SkipToNextSubtitle ()
	{
		if (synchronizer != null && container != null && container.IsSequencePlaying ())
			synchronizer.SkipToNextSubtitle ();
	}

	public string getCurrentAudioObjectName ()
	{
		if (container == null)
			return "";
		return container.gameObject.name;
	}

	public int GetCurrentSubtitleIndex ()
	{
		return synchronizer.GetCurrentIndex ();
	}
}
