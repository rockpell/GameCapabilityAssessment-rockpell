using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMover : MonoBehaviour
{

    public float speed = 0.01f;
    public GameObject image1;
    public GameObject image2;
    private bool turnON = false;
    public Sprite backgroundimage;


    // Use this for initialization
    void Start ()
    {
        for( int i = 0; i < 2; i++ )
        {
            for( int j = 0; j < 4; j++ )
            {
                transform.GetChild( i ).GetChild( j ).GetComponent<SpriteRenderer>().sprite = backgroundimage;   
            }

            transform.GetChild( i ).GetChild( 0 ).GetComponent<RectTransform>().anchoredPosition = new Vector2( backgroundimage.bounds.size.x/2, backgroundimage.bounds.size.y / 2 );
            transform.GetChild( i ).GetChild( 1 ).GetComponent<RectTransform>().anchoredPosition = new Vector2( backgroundimage.bounds.size.x / 2, -backgroundimage.bounds.size.y / 2 );
            transform.GetChild( i ).GetChild( 2 ).GetComponent<RectTransform>().anchoredPosition = new Vector2( -backgroundimage.bounds.size.x / 2, backgroundimage.bounds.size.y / 2 );
            transform.GetChild( i ).GetChild( 3 ).GetComponent<RectTransform>().anchoredPosition = new Vector2( -backgroundimage.bounds.size.x / 2, -backgroundimage.bounds.size.y / 2 );

        }

        image1.GetComponent<Transform>().transform.localPosition = new Vector3( -backgroundimage.bounds.size.x, backgroundimage.bounds.size.y, 0);

        image2.GetComponent<Transform>().transform.localPosition = new Vector3( 0, 0, 0 );

    }


    // Update is called once per frame
    void Update()
    {
        Godown(image1);
        Godown(image2);

        if(image1.GetComponent<Transform>().transform.localPosition.x >= 0 && turnON == false)
        {
            turnON = true;
            ReturnPosition(image2);
        }

        if (image2.GetComponent<Transform>().transform.localPosition.x >= 0 && turnON == true)
        {
            turnON = false;
            ReturnPosition(image1);
        }
    }

    private void Godown(GameObject image)
    {
        image.GetComponent<Transform>().transform.localPosition += new Vector3( backgroundimage.bounds.size.x * speed, -backgroundimage.bounds.size.y * speed, 0);
        //image.GetComponent<Transform>().transform.localPosition += new Vector3( Mathf.Sqrt( 3 ) * speed, -2 * speed, 0 );
    }

    private void ReturnPosition(GameObject image)
    {
        image.GetComponent<Transform>().transform.localPosition = new Vector3( -backgroundimage.bounds.size.x, backgroundimage.bounds.size.y, 0 );
    }
}

