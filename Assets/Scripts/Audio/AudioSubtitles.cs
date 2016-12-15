using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioSubtitles : MonoBehaviour
{
	public List<TextAsset> subtitleFiles;
	Dictionary<string, List<Subtitle>> subtitleDic;

	void Start ()
	{
		ParseSubFiles ();
	}
	
	private void ParseSubFiles ()
	{
		subtitleDic = new Dictionary<string, List<Subtitle>> ();
		foreach (TextAsset subFile in subtitleFiles) {
			string name = subFile.name;
			List<Subtitle> subtitles = new List<Subtitle> ();
			string srt = subFile.text;
			string[] lines = srt.Split (new string[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
			int srtIndex = 0;
			int srtSubIndex = 0;
			Subtitle crtSubtitle = null;
			foreach (string line in lines) {
				string lineTrim = line.Trim ();
				if (int.TryParse (lineTrim, out srtIndex)) {
					if (crtSubtitle != null)
						subtitles.Add (crtSubtitle);
					srtSubIndex = 0;
					crtSubtitle = new Subtitle ();
					crtSubtitle.index = srtIndex;
				} else {
					if (srtSubIndex == 1) {
						String[] split = line.Split (new string[]{"-->"}, StringSplitOptions.None);
						crtSubtitle.startTime = StringTimeToFloat (split [0].Trim ());
						crtSubtitle.endTime = StringTimeToFloat (split [1].Trim ());
					} else if (srtSubIndex >= 2) {
						crtSubtitle.text += line;
					}
				}
				srtSubIndex++;
			}
			subtitles.Add (crtSubtitle);
			subtitleDic.Add (name, subtitles);
		}
	}
	
	private float StringTimeToFloat (String strTime)
	{
		String[] split = strTime.Split (new String[]{":", ","}, StringSplitOptions.None);
		int hours = int.Parse (split [0]);
		int minutes = int.Parse (split [1]);
		int seconds = int.Parse (split [2]);
		int milliseconds = int.Parse (split [3]);
		return hours * 3600f + minutes * 60f + seconds + milliseconds / 1000f;
	}

	public Subtitle GetSubtitleAt (string name, float time)
	{
		if (name == null)
			return null;
		List<Subtitle> subs = subtitleDic [name];
		bool found = false;
		int i = 0;
		Subtitle subToReturn = null;
		while (i < subs.Count && !found) {
			Subtitle sub = subs [i];
			if (time >= sub.startTime && time < sub.endTime) {
				subToReturn = sub;
				found = true;
			}
			i++;
		}
		return subToReturn;
	}

	public Subtitle GetSubtitleAt (string name, int index)
	{
		if (name == null)
			return null;
		List<Subtitle> subs = subtitleDic [name];
		bool found = false;
		int i = 0;
		Subtitle subToReturn = null;
		while (i < subs.Count && !found) {
			Subtitle sub = subs [i];
			if (sub.index == index) {
				subToReturn = sub;
				found = true;
			}
			i++;
		}
		return subToReturn;
	}

	public string GetSubtitleTextAt (string name, float time)
	{
		Subtitle sub = GetSubtitleAt (name, time);
		if (sub != null) {
			return sub.text;
		} else {
			return null;
		}
	}

	public string GetSubtitleTextAt (string name, int index)
	{
		Subtitle sub = GetSubtitleAt (name, index);
		if (sub != null) {
			return sub.text;
		} else {
			return null;
		}
	}

	public float GetSubtitleStartTimeAt (string name, float time)
	{
		Subtitle sub = GetSubtitleAt (name, time);
		if (sub != null) {
			return sub.startTime;
		} else {
			return -1;
		}
	}

	public float GetSubtitleStartTimeAt (string name, int index)
	{
		Subtitle sub = GetSubtitleAt (name, index);
		if (sub != null) {
			return sub.startTime;
		} else {
			return -1;
		}
	}

	public int GetSubtitleIndexAt (string name, float time)
	{
		Subtitle sub = GetSubtitleAt (name, time);
		if (sub != null) {
			return sub.index;
		} else {
			return -1;
		}
	}

	public int GetSubtitleIndexAt (string name, int index)
	{
		Subtitle sub = GetSubtitleAt (name, index);
		if (sub != null) {
			return sub.index;
		} else {
			return -1;
		}
	}

	public int GetSubtitleCount (string name)
	{
		if (name == null)
			return 0;
		List<Subtitle> subs = subtitleDic [name];
		return subs.Count;
	}
	
	public class Subtitle
	{
		public String text;
		public float startTime;
		public float endTime;
		public int index;
		
		public Subtitle ()
		{
			
		}

		public Subtitle (string text, int index, float startTime, float endTime)
		{
			this.text = text;
			this.index = index;
			this.startTime = startTime;
			this.endTime = endTime;
		}
		
		public override string ToString ()
		{
			return index + ") [" + startTime + " --> " + endTime + "] " + text;
		}
	}
}
