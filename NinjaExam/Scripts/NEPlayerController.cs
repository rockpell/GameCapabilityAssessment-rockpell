using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEPlayerController : MonoBehaviour {

	public GameObject shuriken;
	public GameObject smokeParticle;
	public GameObject explosionParticle;
	public AudioClip[] sori;
	int target;
	int obstacle;
	// Use this for initialization
	void Start ()
	{
		Clear();
	}
	void Update ()
	{
		int tc = Input.touchCount;
		for(int i = 0; i < tc; ++i)
		{
			Touch t = Input.GetTouch(i);

			if(t.phase == TouchPhase.Began)
			{
				Vector3 point;
				point = Camera.main.ScreenToWorldPoint(t.position);
				RaycastHit2D hit = Physics2D.Raycast(point, Camera.main.transform.forward);
				if (hit.collider != null)
				{
					if (hit.transform.tag == "Wall")
					{
						Instantiate(shuriken, (Vector3)hit.point + (Vector3.forward * (hit.transform.position.z - 0.1f)), Quaternion.identity);
					}
					if(hit.transform.tag == "Target")
					{
						++target;
						Destroy(hit.transform.gameObject);
						GameObject g = Instantiate(smokeParticle, (Vector3)hit.point + (Vector3.forward * (hit.transform.position.z - 0.1f)), Quaternion.identity);
						Destroy(g, 3f);
					}
					if(hit.transform.tag == "Obstacle")
					{
						++obstacle;
						Destroy(hit.transform.gameObject);
						GameObject g = Instantiate(explosionParticle, (Vector3)hit.point + (Vector3.forward * (hit.transform.position.z - 0.1f)), Quaternion.identity);
						Destroy(g, 3f);
					}
					GetComponent<AudioSource>().clip = sori[Random.Range(0, sori.Length)];
					GetComponent<AudioSource>().Play();
				}
			}
		}

		//마우스 테스트
		//if (Input.GetMouseButtonDown(0))
		//{
		//	Vector3 point;
		//	point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	RaycastHit2D hit = Physics2D.Raycast(point, Camera.main.transform.forward);
		//	if (hit.collider != null)
		//	{
		//		if (hit.transform.tag == "Wall")
		//		{
		//			Instantiate(shuriken, (Vector3)hit.point + (Vector3.forward * (hit.transform.position.z - 0.1f)), Quaternion.identity);
		//		}
		//		if (hit.transform.tag == "Target")
		//		{
		//			++target;
		//			Destroy(hit.transform.gameObject);
		//			GameObject g = Instantiate(smokeParticle, (Vector3)hit.point + (Vector3.forward * (hit.transform.position.z - 0.1f)), Quaternion.identity);
		//			Destroy(g, 3f);
		//		}
		//		if (hit.transform.tag == "Obstacle")
		//		{
		//			++obstacle;
		//			Destroy(hit.transform.gameObject);
		//			GameObject g = Instantiate(explosionParticle, (Vector3)hit.point + (Vector3.forward * (hit.transform.position.z - 0.1f)), Quaternion.identity);
		//			Destroy(g, 3f);
		//		}
		//		GetComponent<AudioSource>().clip = sori[Random.Range(0, sori.Length)];
		//		GetComponent<AudioSource>().Play();
		//	}
		//}
	}
	public void Clear()
	{
		target = 0;
		obstacle = 0;
	}
	public int[] Result()
	{
		return new int[] { target, obstacle };
	}
}
