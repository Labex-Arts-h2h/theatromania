using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class to handle all audio sources at once, either being AudioContainers, FadingAudioSources or simple AudioSources.
/// </summary>
public class AudioManager : MonoBehaviour
{

	List<AudioSource> sources;
	List<FadingAudioSource> fadings;
	List<AudioContainer> containers;
	List<AudioType> types;
	List<SubtitleSynchronizer> synchronizers;

	void Start ()
	{
		sources = new List<AudioSource> ();
		fadings = new List<FadingAudioSource> ();
		containers = new List<AudioContainer> ();
		synchronizers = new List<SubtitleSynchronizer> ();
		types = new List<AudioType> ();

		foreach (Transform child in transform) {
			AudioContainer container = child.GetComponent<AudioContainer> ();
			FadingAudioSource fading = child.GetComponent<FadingAudioSource> ();
			AudioSource source = child.GetComponent<AudioSource> ();
			SubtitleSynchronizer synchronizer = child.GetComponent<SubtitleSynchronizer> ();
			AudioType type = child.GetComponent<AudioType> ();

			if (container != null) {
				containers.Add (container);
			} else if (fading != null) {
				fadings.Add (fading);
			} else if (source != null) {
				sources.Add (source);
			} else if (synchronizer != null) {
				synchronizers.Add (synchronizer);
			}

			types.Add (type);
		}
	}

	public void StopAll ()
	{
		foreach (AudioContainer container in containers) {
			container.StopSequence ();
		}
		foreach (FadingAudioSource fading in fadings) {
			fading.Stop ();
		}
		foreach (AudioSource source in sources) {
			source.Stop ();
			source.time = 0;
		}
		foreach (SubtitleSynchronizer sync in synchronizers) {
			sync.Reset ();
		}

	}

	public void SetVolume (AudioType.TypeName typeName, float v)
	{
		foreach (AudioType t in types) {
			if (t.typeName == typeName) {
				t.SetVolume (v);
			}
		}
	}

	public void SetMusicVolume (float v)
	{
		SetVolume (AudioType.TypeName.MUSIC, v);
	}

	public void SetVoiceVolume (float v)
	{
		SetVolume (AudioType.TypeName.VOICE, v);
	}

	public void SetSoundVolume (float v)
	{
		SetVolume (AudioType.TypeName.SOUND, v);
	}
}
