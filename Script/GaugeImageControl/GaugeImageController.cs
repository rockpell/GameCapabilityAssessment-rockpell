using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GaugeImageController : MonoBehaviour
{
	protected float nowValue;

	//사운드 파일 ( AudioSource에 넣어서 사용하는게 이득인듯 )
	//public AudioClip gaugeSound;
	//public AudioClip specialSound;

	//게이지가 차오를 때나는 사용하는 AudioSource ( 사운드는 짧은게 좋다 )
	[SerializeField] protected AudioSource gaugeSoundPlayer;
	//게이지의 특정 효과가 일어날때 사용하는 AudioSource
	[SerializeField] protected AudioSource specialSoundPlayer;

	public abstract IEnumerator ImageActivate(int value);
    public float GetValue() { return nowValue; }

	//게이지가 차오를때 소리가 나게해주는 코루틴 함수 goal은 게이지 목표값( 1~15 ) term은 소리가 나는 주기 ( ex) term이 0.1이면 게이지가 0.1이 오를 때마다 소리가 난다. )
	protected IEnumerator GaugeSound(int goal, float term)
	{
		float compValue = 0;

		while(compValue < goal)
		{
			if(compValue + term < nowValue)
			{
				gaugeSoundPlayer.Play();
				while(compValue + term < nowValue)
					compValue += term;
				if (compValue > goal)
					break;
			}
			yield return null;
		}
	}
	//
	protected void SpecialSound()
	{
		specialSoundPlayer.Play();
	}
}
