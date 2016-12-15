using UnityEngine;

/// <summary>
///   Audio source that can fade in and fade out.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class FadingAudioSource : MonoBehaviour
{
	public float fadeSpeed;
	public bool isPlaying { get { return source.isPlaying; } }
	public AudioClip clip { get { return source.clip; } set { source.clip = value; } }
	public float time { get { return source.time; } set { source.time = value; } }
	private AudioSource source;
	private bool fadeIn;
	private bool fadeOut;
	private float targetVolume;

	void Start ()
	{
		source = GetComponent<AudioSource> ();
		fadeIn = false;
		fadeOut = false;
		targetVolume = source.volume;
	}

	void Update ()
	{
		if (fadeIn && source.volume < targetVolume) {
			source.volume += fadeSpeed * Time.deltaTime;
			if (source.volume >= targetVolume) {
				fadeIn = false;
			}
		}

		if (fadeOut) {
			source.volume -= fadeSpeed * Time.deltaTime;
			if (source.volume <= 0) {
				Stop ();
				fadeOut = false;
			}
		}
	}

	/// <summary>
	/// If the AudioSouce is playing, fade out. If it's not playing, fade in.
	/// </summary>
	public void Fade ()
	{
		if (source.isPlaying && !fadeOut) {
			fadeOut = true;
		} else if (!source.isPlaying && !fadeIn) {
			source.volume = 0;
			Play ();
			fadeIn = true;
		}
	}

	public void Play ()
	{
		fadeIn = false;
		fadeOut = false;
		source.volume = targetVolume;
		source.Play ();
	}

	public void Stop ()
	{
		fadeIn = false;
		fadeOut = false;
		source.Stop ();
	}

	/// <summary>
	/// Volume to be reached by the fade in. Initially the value set on the AudioSource in the UnityEditor, can be changed by script here.
	/// </summary>
	/// <param name="v">V.</param>
	public void SetTargetVolume (float v)
	{
		targetVolume = v;
	}
}