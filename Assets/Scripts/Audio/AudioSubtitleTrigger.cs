using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A class to trigger events on an audio source (AudioSource or FadingAudioSource), given the subtitles scrolling of another audio source.
/// For an event triggering at the end of the subtitles, give an index of -1.
/// </summary>
public class AudioSubtitleTrigger : MonoBehaviour
{
	public SubtitleSynchronizer synchronizer;
	public List<EventIndexCouple> eventsIndicationList;
	
	private AudioSource source;
	private FadingAudioSource fadingSource;
	private bool hasBeenStarted;
	private List<EventIndexCouple> triggeredEvents;

	void Start ()
	{
		source = GetComponent<AudioSource> ();
		fadingSource = GetComponent<FadingAudioSource> ();
		hasBeenStarted = false;
		triggeredEvents = new List<EventIndexCouple> ();
	}

	void Update ()
	{
		if (synchronizer.IsPlaying () && !hasBeenStarted) {
			hasBeenStarted = true;
		}

		int index = synchronizer.GetCurrentIndex ();
		string name = synchronizer.GetCurrentName ();

		bool stopped = hasBeenStarted && !synchronizer.IsPlaying ();
		foreach (EventIndexCouple couple in eventsIndicationList) {
			if (triggeredEvents.Contains (couple))
				continue;
			bool eventAtStopping = couple.subtitleIndex == -1;
			bool subtitleReached = couple.subtitleName == name && couple.subtitleIndex == index;
			if ((eventAtStopping && stopped) || (!eventAtStopping && subtitleReached)) {
				if (couple.eventToTrigger == Event.PLAY) {
					PlayEvent ();
				} else if (couple.eventToTrigger == Event.PLAY_SKIP) {
					PlaySkipEvent (couple);
				} else if (couple.eventToTrigger == Event.FADE_OUT) {
					FadeOutEvent ();
				} else if (couple.eventToTrigger == Event.STOP) {
					StopEvent ();
				}
				triggeredEvents.Add (couple);
			}
		}

		if (!synchronizer.IsPlaying () && hasBeenStarted) {
			hasBeenStarted = false;
			triggeredEvents.Clear ();
		}
	}

	private void PlayEvent ()
	{
		if (source.isPlaying)
			return;
		if (fadingSource != null) {
			fadingSource.Play ();
		} else {
			source.Play ();
		}
	}

	private void PlaySkipEvent (EventIndexCouple couple)
	{
		source.time = couple.playSkip;
		PlayEvent ();
		                     
	}

	private void FadeOutEvent ()
	{
		if (!source.isPlaying)
			return;
		fadingSource.Fade ();
	}

	private void FadeInEvent ()
	{
		if (source.isPlaying)
			return;
		fadingSource.Fade ();
	}

	private void StopEvent ()
	{
		if (fadingSource != null) {
			fadingSource.Stop ();
		} else {
			source.Stop ();
		}
	}

	public enum Event
	{
		PLAY,
		PLAY_SKIP,
		STOP,
		FADE_OUT,
		FADE_IN,
	}

	[System.Serializable]
	public class EventIndexCouple
	{
		public string subtitleName;
		public int subtitleIndex;
		public Event eventToTrigger;
		public float playSkip = 0;
	}
}
