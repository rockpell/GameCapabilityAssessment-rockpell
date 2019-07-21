using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSButtonControl : MonoBehaviour {

    private IEnumerator showCard;
    private IEnumerator wrongShowCard;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartShowCard(float time)
    {
        showCard = ShowCard(time);
        StartCoroutine(showCard);
    }

    public void StartWrongShowCard(Sprite sprite, float time)
    {
        wrongShowCard = WrongShowCard(sprite, time);
        StartCoroutine(wrongShowCard);
    }

    public void StopWrongShowCard()
    {
        if(wrongShowCard != null)
            StopCoroutine(wrongShowCard);
    }

    private IEnumerator ShowCard(float time)
    {
        yield return new WaitForSeconds(time);
        SetInvisible();
        SetInteractable(false);
    }

    private IEnumerator WrongShowCard(Sprite sprite, float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Button>().image.sprite = sprite;
    }

    private void SetInvisible()
    {
        GetComponent<Button>().image.color = new Color(0, 0, 0, 0);
    }

    private void SetInteractable(bool value)
    {
        GetComponent<Button>().interactable = value;
    }
}
