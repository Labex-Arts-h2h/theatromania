using UnityEngine;
using System.Collections;

/// <summary>
/// A class to synchronize an AudioContainer and its corresponding AudioSubtitles.
/// Functions :
/// - Get the subtitle corresponding to the playing audio
/// - Skip audio to the next subtitle.
/// </summary>
public class SubtitleSynchronizer : MonoBehaviour
{
	AudioContainer container;
	AudioSource source;
	AudioSubtitles subtitles;
	AudioSubtitles.Subtitle crtSubtitle;
	float crtTime = -1;
	AudioClip prevClip = null;

	void Start ()
	{
		source = GetComponent<AudioSource> ();
		container = GetComponent<AudioContainer> ();
		subtitles = GetComponent<AudioSubtitles> ();
		crtSubtitle = new AudioSubtitles.Subtitle ("", -1, -1, -1);
	}

	public void Reset ()
	{
		crtSubtitle = new AudioSubtitles.Subtitle ("", -1, -1, -1);
		crtTime = -1;
		prevClip = null;
	}
	
	void Update ()
	{
		if (container.IsSequencePlaying ()) {
			if (crtTime == -1 || container.crtClip != prevClip) {
				crtTime = 0;
				prevClip = container.crtClip;
			}
			crtSubtitle = subtitles.GetSubtitleAt (container.crtClip.name, crtTime);
			crtTime += Time.deltaTime;
			if (crtSubtitle == null) {
				crtSubtitle = new AudioSubtitles.Subtitle ("", -1, -1, -1);
			}
		} else {
			crtTime = -1;
			crtSubtitle = new AudioSubtitles.Subtitle ("", -1, -1, -1);
			prevClip = null;
		}
	}

	public string GetCurrentName ()
	{
		if (container.crtClip == null) {
			return "";
		}
		return container.crtClip.name;
	}

	public string GetCurrentText ()
	{
		return crtSubtitle.text;
	}

	public int GetCurrentIndex ()
	{
		return crtSubtitle.index;
	}

	public int GetSubtitleCount ()
	{
		return subtitles.GetSubtitleCount (container.crtClip.name);
	}

	public bool IsPlaying ()
	{
		return container.IsSequencePlaying ();
	}

	public void SkipToNextSubtitle ()
	{
		int index = subtitles.GetSubtitleIndexAt (container.crtClip.name, crtTime);
		float nextStartTime = subtitles.GetSubtitleStartTimeAt (container.crtClip.name, index + 1);
		if (nextStartTime != -1) {
			source.time = nextStartTime;
			crtTime = nextStartTime;
		} else {
			container.PlayNextSequenceClip ();
			prevClip = container.crtClip;
			crtTime = 0;
		}
	}
}
