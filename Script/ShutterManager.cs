
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterManager : MonoBehaviour {
    [SerializeField]
    private float closeSpeed; // 문 닫는 속도 기본 40으로 설정
    [SerializeField]
    private float openSpeed; // 문 여는 속도 기본 40으로 설정
    [SerializeField]
    private float shakePower; // 문 흔들림 강도 기본 50에서 설정
    [SerializeField]
    private float stampSpeed; // 도장 찍는 속도 기본 0.01
    [SerializeField]
    private float closePosition;
    [SerializeField]
    private int defaultScale;
    [SerializeField]
    private List<Sprite> sagakSprite;
    [SerializeField]
    private List<Sprite> clearStampIcon;
    [SerializeField]
    private List<AudioClip> shutterSounds;

    private AudioSource audioSource;
    private bool isPlay;
    private bool isSceneChange;
    private float startYPosition;

    private int sequenceCount;

    private static ShutterManager instance;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject); // 씬전환과 관련
    }

    void Start () {
        //ShutterSequence(Result.Fail, "NewEvaluation", true);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public GameObject FindChild(GameObject canvas, string name)
    {
        GameObject target = null;
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject.name == name)
            {
                target = child.gameObject;
            }
        }
        return target;
    }

    public void ShutterSequence(Result result, string sceneName, bool isStamp) // 문닫고 도장찍고 씬전환하고 문열기
    {
        isSceneChange = false;
        if (!isPlay)
        {
            sequenceCount = 0;
            GameObject shutter = FindChild(this.gameObject, "ShutterForm");
            shutter.SetActive(true);
            FindChild(this.gameObject, "Sagak").SetActive(true);
            FindChild(this.gameObject, "Sagak").GetComponent<UnityEngine.UI.Image>().sprite = sagakSprite[Random.Range(0, sagakSprite.Count)];
            audioSource = FindChild(this.gameObject, "ShutterImage").GetComponent<AudioSource>();

            StartCoroutine(ShutterDown(shutter, isPlay));
            StartCoroutine(Stamp(shutter, FindChild(this.gameObject, "Stamp"), isStamp, isPlay, result));
            StartCoroutine(ChangeSceneCheck(sceneName, isPlay));
        }
        else if(sequenceCount > 0)
        {
            StartCoroutine(CheckCoroutine(result, sceneName, isStamp));
        }
        sequenceCount++;
        isPlay = true;
    }
    // ShutterSequence 작동중에는 다른 인터페이스(public 함수들)가 호출되어도 작동하지 않는다.
    // isStamp 값에 따라 도장 찍을지 말지 결정
    // isSceneChange true일 경우 씬전환

    IEnumerator CheckCoroutine(Result result, string sceneName, bool isStamp)
    {
        while (isPlay)
        {
            yield return null;
        }
        sequenceCount = 0;
        GameObject shutter = FindChild(this.gameObject, "ShutterForm");
        shutter.SetActive(true);
        FindChild(this.gameObject, "Sagak").SetActive(true);
        FindChild(this.gameObject, "Sagak").GetComponent<UnityEngine.UI.Image>().sprite = sagakSprite[Random.Range(0, sagakSprite.Count)];
        audioSource = FindChild(this.gameObject, "ShutterImage").GetComponent<AudioSource>();

        StartCoroutine(ShutterDown(shutter, isPlay));
        StartCoroutine(Stamp(shutter, FindChild(this.gameObject, "Stamp"), isStamp, isPlay, result));
        StartCoroutine(ChangeSceneCheck(sceneName, isPlay));
    }

    private void ChangeScene(string sceneName) // 씬전환은 이 함수를 호출하면 됨
    {
        if(CustomSceneManager.Instance != null)
            CustomSceneManager.Instance.ChangeScene(sceneName);
    }

    IEnumerator ShutterDown(GameObject shutter, bool isPlaying)
    {
        if (!isPlaying)
        {
            int flip = 1;
            float cSpeed = closeSpeed;
            float sPower = shakePower;
            float endPoint = closePosition;

            RectTransform shutterRect = shutter.GetComponent<RectTransform>();
            Vector3 startPosition = FindChild(this.gameObject, "StartPosition").GetComponent<RectTransform>().transform.localPosition;

            if (audioSource != null)
            {
                audioSource.clip = shutterSounds[0];
                audioSource.loop = false;
                audioSource.Play();
            }
            //셔터 내림
            while (shutterRect.localPosition.y > endPoint)
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.clip = shutterSounds[1];
                    audioSource.loop = true;
                    audioSource.Play();
                }
                flip *= -1;
                shutterRect.localPosition = new Vector3(shutterRect.localPosition.x + Random.Range(-1.0f, 1.0f) * flip * Time.deltaTime * 60, shutterRect.localPosition.y - cSpeed*Time.deltaTime*60);
                yield return null;
            }
            //셔터 흔들림
            audioSource.loop = false;
            audioSource.clip = shutterSounds[2];
            audioSource.Play();
			//for (int count = 0; count < 20; count++)
			//{
			//    flip *= -1;
			//    shutterRect.position = new Vector3(shutterRect.position.x + Random.Range(0.0f, sPower) * flip, shutterRect.position.y);
			//    yield return null;
			//}
			//shutterRect.position = new Vector3(startPosition.x, shutterRect.position.y);
			//yield return StartCoroutine(Shaking(shutter, 10, 0.3f));
        }
    }

    IEnumerator ChangeSceneCheck(string sceneName, bool isPlaying)
    {
        if (!isPlaying)
        {
            float shutterYPosition = FindChild(this.gameObject, "ShutterForm").GetComponent<RectTransform>().localPosition.y;
            float endPosition = closePosition;

            while (shutterYPosition > endPosition)
            {
                isSceneChange = false;
                shutterYPosition = FindChild(this.gameObject, "ShutterForm").GetComponent<RectTransform>().localPosition.y;
                yield return null;
            }

            if ((shutterYPosition <= endPosition) && !isSceneChange)
            {
                ChangeScene(sceneName);
                if ((sceneName != "NewEvaluation") && (sceneName != "MenuScene"))
                    NewGameManager.Instance.StopBGM();
                else
                    NewGameManager.Instance.PlayBGM(0.5f);
                isSceneChange = true;
                NewGameManager.Instance.SetTouchDisablePanel(false);
            }
        }
    }

    IEnumerator Stamp(GameObject shutter, GameObject stamp, bool isStamp, bool isPlaying, Result result)
    {
        if (!isPlaying)
        {
            float sSpeed = stampSpeed;

            RectTransform shutterRect = shutter.GetComponent<RectTransform>();
            RectTransform stampRect = stamp.GetComponent<RectTransform>();
            Vector3 stampStartPosition = stampRect.localPosition;
            Vector3 startPosition = FindChild(this.gameObject, "StartPosition").GetComponent<RectTransform>().transform.localPosition;

            while (!isSceneChange)
            {
                stampStartPosition = stampRect.localPosition;
                yield return null;
            }

            if (isStamp)
            {
                FindChild(this.gameObject, "Stamp").SetActive(true);
                Subject nowSubject = NewGameManager.Instance.GetNowSubject();

                Sprite resultIcon = null;
                switch(result)
                {
                    case Result.BigSuccessful:
                        resultIcon = clearStampIcon[0];
                        break;
                    case Result.Successful:
                        resultIcon = clearStampIcon[1];
                        break;
                    case Result.Fail:
                        resultIcon = clearStampIcon[2];
                        break;
                }
                stamp.GetComponent<UnityEngine.UI.Image>().sprite = resultIcon;
                //이미지를 자신이 가지고 있도록 수정 필요
                audioSource.clip = shutterSounds[3];
                audioSource.loop = true;
                audioSource.Play();
                while ((stampRect.localScale.x > 4.0f) && (stampRect.localScale.y > 4.0f))
                {
                    stampRect.localScale = new Vector3(stampRect.localScale.x - sSpeed * Time.deltaTime * 60, stampRect.localScale.y - sSpeed * Time.deltaTime * 60);
                    //stampRect.position = stampStartPosition;
                    yield return null;
                }
                audioSource.clip = shutterSounds[4];
                audioSource.loop = false;
                audioSource.Play();
				//for (int count = 0; count < 10; count++)
				//{
				//    flip *= -1;
				//    shutterRect.position = new Vector3(shutterRect.position.x + Random.Range(0.0f, 10.0f) * flip, shutterRect.position.y);
				//    yield return null;
				//}
				//shutterRect.position = new Vector3(startPosition.x, shutterRect.position.y);
				yield return StartCoroutine(Shaking(shutter, 10, 0.4f));
				//yield return new WaitForSeconds(0.5f);
            }
            StartCoroutine(ShutterUp(FindChild(this.gameObject, "ShutterForm"), isPlaying));
        }
        //도장 찍고 흔들림
    }

    IEnumerator ShutterUp(GameObject shutter, bool isPlaying)
    {
        if (!isPlaying)
        {
            float oSpeed = openSpeed;
            RectTransform shutterRect = shutter.GetComponent<RectTransform>();
            int flip = 1;
            FindChild(this.gameObject, "Sagak").SetActive(false);
            if(audioSource.isPlaying == false)
            {
                audioSource.loop = true;
                audioSource.clip = shutterSounds[1];
                audioSource.Play();
            }
            Vector3 startPosition = FindChild(this.gameObject, "StartPosition").GetComponent<RectTransform>().transform.localPosition;
            while (shutterRect.localPosition.y < startPosition.y)
            {
                flip *= -1;
                shutterRect.localPosition = new Vector3(shutterRect.localPosition.x + Random.Range(-1.0f, 1.0f) * flip * Time.deltaTime * 60,
                    shutterRect.localPosition.y + oSpeed * Time.deltaTime * 60);
                yield return null;
            }
            FindChild(this.gameObject, "ShutterForm").SetActive(false);

            FindChild(this.gameObject, "Stamp").SetActive(false);
            FindChild(this.gameObject, "Stamp").GetComponent<RectTransform>().localScale = new Vector3(defaultScale, defaultScale, 1);
            //FindChild(this.gameObject, "ShutterForm").GetComponent<RectTransform>().position = new Vector3(shutterRect.position.x, startYPosition);
        }
        isPlay = false;
    }

	//Caution 다른 position 조정함수랑 같이 쓰면 문제
	IEnumerator Shaking(GameObject shutter, float power, float time)
	{
		RectTransform shutterRect = shutter.GetComponent<RectTransform>();
		Vector3 pivot = shutterRect.position;

		float timer = 0;
		for (; timer < time; timer += Time.deltaTime)
		{
			float angle = Random.Range(0, 360 * Mathf.Deg2Rad);
			shutterRect.position = pivot + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * power * (1 - timer / time));
			yield return null;
		}
		shutterRect.position = pivot;
	}

    public float GetCloseSpeed() { return closeSpeed; }
    public void SetCloseSpeed(float speed)
    {
        if (isPlay)
            return;
        else
            closeSpeed = speed;
    }

    public float GetOpenSpeed() { return openSpeed; }
    public void SetOpenSpeed(float speed)
    {
        if (isPlay)
            return;
        else
            openSpeed = speed;
    }

    public float GetShakePower() { return shakePower; }
    public void SetShakePower(float power)
    {
        if (isPlay)
            return;
        else
            shakePower = power;
    }

    public static ShutterManager Instance
    {
        get { return instance; }
    }
}
