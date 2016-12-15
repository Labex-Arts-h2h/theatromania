using UnityEngine;
using System.Collections;

/// <summary>
/// An audio container class, to aggregate several clips to be played by a unique AudioSource. The container
/// can be used as a "random container", i.e., playing a different random clip at each call, or as a "sequence container",
/// playing all clips one after the other.
/// </summary>
public class AudioContainer : MonoBehaviour
{
	public AudioClip[] clips;

	public AudioClip crtClip { get { return source.clip; } }
	public bool isPlaying{ get { return source.isPlaying; } }
	public float time{ get { return source.time; } set { source.time = value; } }
	private AudioSource source;
	private bool playSequence;
	private int sequenceIndex;

	void Start ()
	{
		source = GetComponent<AudioSource> ();
		playSequence = false;
		sequenceIndex = 0;
	}

	void Update ()
	{
		if (playSequence && !source.isPlaying) {
			PlayNextSequenceClip ();
		}
	}

	public void PlayOneAtRandom ()
	{
		int index = Random.Range (0, clips.Length);
		AudioClip newClip = clips [index];
		source.clip = newClip;
		source.Play ();
	}

	public void PlaySequence ()
	{
		source.clip = clips [sequenceIndex];
		source.Play ();
		playSequence = true;
	}

	public void PauseSequence ()
	{
		playSequence = false;
		source.Pause ();
	}

	public int GetCurrentSequenceIndex ()
	{
		return sequenceIndex;
	}

	public bool IsSequencePlaying ()
	{
		return playSequence;
	}

	public void PlayNextSequenceClip ()
	{
		if (sequenceIndex < clips.Length - 1) {
			sequenceIndex++;
			source.clip = clips [sequenceIndex];
			source.time = 0;
			source.Play ();
		} else {
			sequenceIndex = 0;
			playSequence = false;
			source.Stop ();
		}
	}

	public void StopSequence ()
	{
		playSequence = false;
		source.Stop ();
		source.time = 0;
		sequenceIndex = 0;
	}
}
