using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImageController : MonoBehaviour {
	// 종합평가 선택화면에서 등장하는 RenderImage를 관리하는 클래스
	// 항목아이콘은 RenderImage 위에 생성된다.
	// RenderImage가 비추고 있는 이미지는 배경화면이 움직이고 있다.(배경화면 움직이는 기능까지 이 클래스에 담당한다.)

	public Vector2 directionAndSpeed;
	public Vector2 tiling = Vector2.one;

	[SerializeField]
	private GameObject subjectIcon;
	[SerializeField]
	private GameObject clearStamp;

	private bool isActivate;
	private Material mat;
	private Vector2 offset;

    void Awake ()
	{
		mat = GetComponent<MeshRenderer>().material;
		mat.SetTextureScale("_MainTex", tiling);
	}

	private void Start()
	{
		//Active();
		//Clear();
		//Invoke("Inactive", 5f);
		//Invoke("Clear", 10f);
	}

	void Update ()
	{
		if(isActivate)
		{
			offset += (Time.deltaTime * directionAndSpeed);
			mat.SetTextureOffset("_MainTex", offset);
		}
	}

    public void Active() // 화면이 움직이고 본래의 색감이다.
    {
		isActivate = true;
		mat.color = Color.white;

		if (subjectIcon)
		{
			subjectIcon.GetComponent<SpriteRenderer>().color = Color.white;
			subjectIcon.GetComponent<Animator>().Play(0);
		}
		if(clearStamp)
		{
			clearStamp.SetActive(false);
		}
	}

    public void Inactive() // 화면이 멈추고 어두워진다, 항목아이콘도 같이 어두워진다.
    {
		isActivate = false;
		mat.color = Color.gray;
		if (subjectIcon)
		{
			subjectIcon.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
			//subjectIcon.GetComponent<SpriteRenderer>().color = Color.gray;
		}
		if (clearStamp)
		{
			clearStamp.SetActive(false);
		}
	}

    public void Clear()
    {
		isActivate = true;
		mat.color = Color.gray;
		if (subjectIcon)
		{
			subjectIcon.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
		}
		if (clearStamp)
		{
			clearStamp.SetActive(true);
		}
	}
    // 비활성화 되있는 상태에서 Clear 도장 이미지가 찍혀있다
    // (도장 이미지는 RenderImage 위에 생성된다, NewGameManager.Instance.GetEndSubjectList를 이용하면 클리어한 항목을 알 수 있다.)
    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
        mat = material;
        mat.SetTextureScale("_MainTex", tiling);
    }
}
