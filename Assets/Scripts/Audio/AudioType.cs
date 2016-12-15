using UnityEngine;
using System.Collections;

public class AudioType : MonoBehaviour
{
	public TypeName typeName;

	private AudioSource source;
	private FadingAudioSource fading;

	void Start ()
	{
		source = GetComponent<AudioSource> ();
		fading = GetComponent<FadingAudioSource> ();
	}

	public void SetVolume (float v)
	{
		if (fading != null) {
			fading.SetTargetVolume (v);
		}
		source.volume = v;
	}

	public enum TypeName
	{
		SOUND,
		VOICE,
		MUSIC,
	}
}
