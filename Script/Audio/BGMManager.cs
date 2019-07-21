using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

	[SerializeField]
	private AudioSource BGMaudioSource;
	[SerializeField]
	[Range(0,1)]
	private float MaxVolume;

	
	[ContextMenu("Start")]
	public void StartBGM(float fadeInTime = 0.3f)
	{
		if (!BGMaudioSource.isPlaying)
		{
			BGMaudioSource.Play();
			StartCoroutine(VolumeFadeIn(fadeInTime));
		}
	}
	[ContextMenu("Pause")]
	public void PauseBGM(float fadeOutTime = 0.3f)
	{
		StartCoroutine(PauseMusic(fadeOutTime));
	}
	[ContextMenu("Stop")]
	public void StopBGM(float fadeOutTime = 0.3f)
	{
		StartCoroutine(StopMusic(fadeOutTime));
	}

	private IEnumerator VolumeFadeIn(float fadeInTime)
	{
		float timer = 0;
		while (timer < fadeInTime)
		{
			timer += Time.deltaTime;
			if (timer > fadeInTime)
				timer = fadeInTime;

			BGMaudioSource.volume = Mathf.Lerp(0, MaxVolume, timer / fadeInTime);
			yield return null;
		}
	}
	private IEnumerator VolumeFadeOut(float fadeOutTime)
	{
		float timer = 0;
		while (timer < fadeOutTime)
		{
			timer += Time.deltaTime;
			if (timer > fadeOutTime)
				timer = fadeOutTime;

			BGMaudioSource.volume = Mathf.Lerp(MaxVolume, 0, timer / fadeOutTime);
			yield return null;
		}
	}
	private IEnumerator PauseMusic(float fadeOutTime)
	{
		yield return StartCoroutine(VolumeFadeOut(fadeOutTime));
		BGMaudioSource.Pause();
	}
	private IEnumerator StopMusic(float fadeOutTime)
	{
		yield return StartCoroutine(VolumeFadeOut(fadeOutTime));
		BGMaudioSource.Stop();
	}
}
