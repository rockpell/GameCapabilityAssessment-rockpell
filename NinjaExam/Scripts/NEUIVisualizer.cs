using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEUIVisualizer : MonoBehaviour {

	public Sprite[] uiSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	public void ShowUI(int index)
	{
		GetComponent<SpriteRenderer>().sprite = uiSprite[index];
		GetComponent<SpriteRenderer>().enabled = true;

		StartCoroutine(SizeUp());
		GetComponent<AudioSource>().Play();
	}
	IEnumerator SizeUp()
	{
		for(; transform.localScale.y < 1; )
		{
			transform.localScale += Vector3.up * 0.2f;
			if (transform.localScale.y > 1)
				transform.localScale -= Vector3.up * (transform.localScale.y - 1);
			yield return new WaitForSeconds(0.01f);
		}
	}
	public void HideUI()
	{
		StartCoroutine(SizeDown());
	}
	IEnumerator SizeDown()
	{
		for (; transform.localScale.y > 0;)
		{
			transform.localScale -= Vector3.up * 0.2f;
			if (transform.localScale.y < 0)
				transform.localScale += Vector3.up * (-transform.localScale.y);
			yield return new WaitForSeconds(0.01f);
		}
		GetComponent<SpriteRenderer>().enabled = false;
	}
}
