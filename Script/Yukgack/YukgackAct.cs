using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class YukgackAct : MonoBehaviour
{
	[Header("Dialog Value Setting")]
	[SerializeField]
	private float dialogWidth;
	[SerializeField]
	private float dialogHeight;
	[SerializeField]
	private float textBoxWidth;
	[SerializeField]
	private float textBoxHeightGenerally;
	[SerializeField]
	private float textBoxHeightWhenAsk;

	[Header("Emotion")]
	[SerializeField]
	private FaceMap[] emotion;

	[Header("Defendency Setting")]
	[SerializeField]
	private GameObject yukgack; // 화면에 나타나는 육각이 오브젝트
	[SerializeField]
	private GameObject yukgackBody; // 화면에 나타나는 육각이 오브젝트 중의 몸통만
	[SerializeField]
	private GameObject wordBubble; // 말풍선
	[SerializeField]
	private Text dialogText; // 말풍선의 말
	[SerializeField]
	private GameObject buttonParent; // 버튼 부모 오브젝트
	[SerializeField]
	private GameObject buttonProto; // 버튼 프리팹
	[SerializeField]
	private GameObject talkEndFlag; // 대사 끝에 나오는 화살표


	private RoutineStream tmp = new RoutineStream();
	[ContextMenu("Movetest")]
	void MoveTest()
	{
		StartCoroutine(DialogMoveTo(Vector3.left * 100, 1));
	}
	[ContextMenu("Rotatetest")]
	void RotateTest()
	{
		StartCoroutine(RotateTo(Vector3.forward * 100, 1));
	}
	[ContextMenu("test")]
	void test()
	{
		if (wordBubble.activeSelf)
			StartCoroutine(SetDialogDeactive());
		else
			StartCoroutine(SetDialogActive());
		//StartCoroutine(Talk("안녕하세요,ㄴaㅈasㄹ13ㅎ2ㅎㄷ3gk이", new RoutineStream(), 0.1f));
		//StartCoroutine(Ask("질문을 하나 할게요!", new string[3] { "어", "아니", "그래" }, tmp));
	}
	/// <summary>
	/// Routine을 동시에 실행하고 싶을때 사용한다.
	/// </summary>
	/// <param name="routines">동시에 실행할 Routine함수를 적는다</param>
	/// <returns>모든 루틴이 끝나면 반환된다.</returns>
	public IEnumerator MultipleTask(params IEnumerator[] routines)
	{
		int length = routines.Length;
		Coroutine[] isDone = new Coroutine[length];

		for (int i = 0; i < length; ++i)
		{
			isDone[i] = StartCoroutine(routines[i]);
		}

		for (int i = 0; i < length; ++i)
		{
			yield return isDone[i];
		}
	}
	/// <summary>
	/// Routine을 동시에 실행하고 싶을때 사용한다.
	/// </summary>
	/// <param name="routines">동시에 실행할 Routine함수 리스트</param>
	/// <returns>모든 루틴이 끝나면 반환된다.</returns>
	public IEnumerator MultipleTask(List<IEnumerator> routines)
	{
		int length = routines.Count;
		Coroutine[] isDone = new Coroutine[length];

		for (int i = 0; i < length; ++i)
		{
			isDone[i] = StartCoroutine(routines[i]);
		}

		for (int i = 0; i < length; ++i)
		{
			yield return isDone[i];
		}
	}
	/// <summary>
	/// 현재 위치에서 dest까지 이동한다.
	/// </summary>
	/// <param name="dest">이동할 목적지 position</param>
	/// <param name="time">이동할 때까지 걸리는 시간</param>
	/// <returns>루틴이 끝나면 반환돤다.</returns>
	public IEnumerator MoveTo(Vector3 dest, float time)
	{
		RectTransform yukgackRect = yukgack.GetComponent<RectTransform>();
		Vector3 startPos = yukgackRect.anchoredPosition3D;
		yield return StartCoroutine(Move(startPos, dest, yukgackRect, time));
	}
	/// <summary>
	/// 현재 위치에서 vector만큼 이동한다.
	/// </summary>
	/// <param name="vector">이동하고 싶은 거리와 방향</param>
	/// <param name="time">도착까지 걸리는 시간</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator MoveToward(Vector3 vector, float time)
	{
		Vector3 startPos = yukgack.GetComponent<RectTransform>().anchoredPosition3D;
		Vector3 dest = startPos + vector;
		yield return StartCoroutine(Move(startPos, dest, yukgack.GetComponent<RectTransform>(), time));
	}
	/// <summary>
	/// 말풍선을 현재위치에서 dest까지 이동한다.
	/// 원점 기준은 육각이이다.
	/// </summary>
	/// <param name="dest">이동할 목적지 position</param>
	/// <param name="time">이동할 때까지 걸리는 시간</param>
	/// <returns>루틴이 끝나면 반환돤다.</returns>
	public IEnumerator DialogMoveTo(Vector3 dest, float time)
	{
		RectTransform target = wordBubble.GetComponent<RectTransform>();
		Vector3 startPos = target.anchoredPosition3D;
		yield return StartCoroutine(Move(startPos, dest, target, time));
	}
	/// <summary>
	/// 말풍선이 현재 위치에서 vector만큼 이동한다.
	/// </summary>
	/// <param name="vector">이동하고 싶은 거리와 방향</param>
	/// <param name="time">착까지 걸리는 시간</param>
	/// <returns>루틴이 끝나면 반환돤다.</returns>
	public IEnumerator DialogMoveToward(Vector3 vector, float time)
	{
		RectTransform target = wordBubble.GetComponent<RectTransform>();
		Vector3 startPos = target.anchoredPosition3D;
		Vector3 dest = startPos + vector;
		yield return StartCoroutine(Move(startPos, dest, target, time));
	}
	/// <summary>
	/// 현재 rotation에서 angle까지 회전한다.
	/// </summary>
	/// <param name="angle">원하는 회전값</param>
	/// <param name="time">수행에 걸리는 시간</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator RotateTo(Vector3 angle, float time)
	{
		Vector3 endAngle = Formalization(angle);
		Vector3 startAngle = Formalization(yukgackBody.GetComponent<RectTransform>().eulerAngles);
		float x = 0;
		float y = 0;
		float z = 0;

		//이동이 적은방향 계산
		if (Mathf.Abs(startAngle.x - endAngle.x) > 180)
		{
			if(startAngle.x > endAngle.x)
				startAngle -= (Vector3.right * 360);
			else
				endAngle -= (Vector3.right * 360);
		}
		if (Mathf.Abs(startAngle.y - endAngle.y) > 180)
		{
			if(startAngle.y > endAngle.y)
				startAngle -= (Vector3.up * 360);
			else
				endAngle -= (Vector3.up * 360);
		}
		if (Mathf.Abs(startAngle.z - endAngle.z) > 180)
		{
			if (startAngle.z > endAngle.z)
				startAngle -= (Vector3.forward * 360);
			else
				endAngle -= (Vector3.forward * 360);
		}

		yield return StartCoroutine(Rotate(startAngle, endAngle, time));
	}
	/// <summary>
	/// 현재 rotation에서 amount만큼 추가한다.
	/// </summary>
	/// <param name="amount">추가한 회전값</param>
	/// <param name="time">수행에 걸리는 시간</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator RotateToward(Vector3 amount, float time)
	{
		Vector3 startAngle = yukgackBody.GetComponent<RectTransform>().eulerAngles;
		Vector3 endAngle = yukgackBody.GetComponent<RectTransform>().eulerAngles + amount;
		yield return StartCoroutine(Rotate(startAngle, endAngle, time));
	}
	/// <summary>
	/// 다이얼로그에 대사가 나온다.
	/// </summary>
	/// <param name="text">적고 싶은 텍스트</param>
	/// <param name="clickFlag">대사를 스킵하거나 다음으로 넘어갈때 확인하는 값</param>
	/// <param name="oneCharTime">한 글자가 나오는데 소요되는 시간</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator Talk(string text, RoutineStream clickFlag, float oneCharTime = 0.04f)
	{
		SetTalkEndFlag(false);
		dialogText.GetComponent<RectTransform>().sizeDelta = new Vector2(textBoxWidth, textBoxHeightGenerally);

		yield return StartCoroutine(WriteToDialog(text, clickFlag, oneCharTime));

		dialogText.text = text;
		SetTalkEndFlag(true);
		yield return new WaitUntil(() => (bool)clickFlag.flag);
		SetTalkEndFlag(false);
	}
	/// <summary>
	/// 다이얼로그에 대사와 선택지가 뜨고, 선택을 하면 반환된다.
	/// </summary>
	/// <param name="text">선택지에 대한 대사</param>
	/// <param name="choice">선택지 문구 배열</param>
	/// <param name="answer">대사를 스킵하거나, 질의의 값을 기록한다.</param>
	/// <param name="oneCharTime">한 글자가 나오는데 소요되는 시간</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator Ask(string text, string[] choice, RoutineStream answer, float oneCharTime = 0.04f)
	{
		// 말하기 ( 묻기 )
		dialogText.GetComponent<RectTransform>().sizeDelta = new Vector2(textBoxWidth, textBoxHeightWhenAsk);
		yield return StartCoroutine(WriteToDialog(text, answer, oneCharTime));

		// 버튼 만들 준비
		GameObject[] buttonList = new GameObject[choice.Length];
		buttonParent.GetComponent<HorizontalLayoutGroup>().spacing = 100f / choice.Length;

		// 버튼 만들기
		RoutineStream trigger = new RoutineStream();
		trigger.flag = false;
		for(int i = 0; i < choice.Length; ++i)
		{
			buttonList[i] = Instantiate(buttonProto, buttonParent.transform);
			buttonList[i].GetComponentInChildren<Text>().text = choice[i];
			//choice[i]를 사용하면 참조로 접근하기 때문에 문제가 생김
			//이를 해결하기 위해 변수에 값을 복사에 사용
			string item = choice[i];
			buttonList[i].GetComponent<Button>().onClick.AddListener(() => { trigger.flag = true; answer.result = item; });
		}
		// 버튼 활성화 ( 보이게하기 )
		buttonParent.SetActive(true);

		// 버튼 누를때까지 기다리기
		yield return new WaitUntil(() => (bool)trigger.flag);

		// 버튼 비활성화 및 제거
		buttonParent.SetActive(false);
		foreach(GameObject item in buttonList)
		{
			Destroy(item);
		}
	}
	/// <summary>
	/// 다이얼로그의 활성화 상태를 설정한다.
	/// </summary>
	/// <param name="isActive">값에 따라 다이얼로그가 활성/비활성 된다.</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator SetDialogActive(bool isActive)
	{
		if (isActive == wordBubble.activeSelf)
			yield break;
		if (isActive)
		{
			yield return StartCoroutine(SetDialogActive());
		}
		else
		{
			yield return StartCoroutine(SetDialogDeactive());
		}
	}
	/// <summary>
	/// 원하는 표정을 나타낸다.
	/// </summary>
	/// <param name="face">원하는 표정 상태</param>
	/// <returns>루틴이 끝나면 반환된다.</returns>
	public IEnumerator SetEmotion(Face face)
	{
		Sprite sprite = GetSprite(face);
		if (sprite != null)
		{
			yukgackBody.GetComponent<Image>().sprite = sprite;
		}
		yield return null;
	}

	private IEnumerator Move(Vector3 startPos, Vector3 destPos, RectTransform target, float time)
	{
		float timer = 0;

		if (time != 0)
		{
			while (timer < time)
			{
				timer += Time.deltaTime;
				if (timer > time)
					timer = time;

				target.anchoredPosition3D = Vector3.Lerp(startPos, destPos, Mathf.SmoothStep(0, 1, timer / time));
				yield return null;
			}
		}

		target.anchoredPosition3D = destPos;
	}
	private IEnumerator Rotate(Vector3 startAngle, Vector3 destAngle, float time)
	{
		RectTransform yukgackRect = yukgackBody.GetComponent<RectTransform>();
		float timer = 0;

		if (time > 0)
		{
			while (timer < time)
			{
				timer += Time.deltaTime;
				if (timer > time)
					timer = time;

				//tmp.eulerAngles = Vector3.Lerp(startAngle, destAngle, Mathf.SmoothStep(0, 1, timer / time));
				yukgackRect.eulerAngles = Vector3.Lerp(startAngle, destAngle, Mathf.SmoothStep(0, 1, timer / time));
				yield return null;
			}
		}

		yukgackRect.eulerAngles = destAngle;
	}
	private IEnumerator WriteToDialog(string text, RoutineStream clickFlag, float oneCharTime)
	{
		yield return StartCoroutine(SetDialogActive(true));
		//text = StringVariablePaser(text);
		dialogText.text = "";

		//0 이하면 그냥 바로 텍스트 완성
		if (oneCharTime > 0)
		{
			foreach (char letter in text.ToCharArray())
			{
				dialogText.text += letter;

				//빠른 넘김
				if (clickFlag.flag is bool && (bool)clickFlag.flag)
				{
					clickFlag.flag = false;
					break;
				}
				yield return new WaitForSeconds(oneCharTime);
			}
		}
		dialogText.text = text;
	}
	private IEnumerator SetDialogActive()
	{
		//초기화
		dialogText.text = "";
		SetTalkEndFlag(false);
		wordBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 0);
		wordBubble.SetActive(true);

		//높이 늘리기
		float height = 0;
		float timer = 0;
		float time = 0.3f;
		while(height < dialogHeight)
		{
			timer += Time.deltaTime;
			if (timer > time)
				timer = time;

			height = Mathf.Lerp(-dialogHeight, dialogHeight, 0.5f + Mathf.SmoothStep(0,0.5f,(timer / time)));
			wordBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(30, height);
			yield return null;
		}

		//너비 늘리기
		float width = 30f;
		timer = 0;
		time = 0.2f;
		while (width < dialogWidth)
		{
			timer += Time.deltaTime;
			if (timer > time)
				timer = time;

			width = Mathf.Lerp(30 - dialogWidth, dialogWidth, 0.5f + Mathf.SmoothStep(0, 0.5f, (timer / time)));
			wordBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			yield return null;
		}

		//대사켜기
		dialogText.gameObject.SetActive(true);
	}
	private IEnumerator SetDialogDeactive()
	{
		//대사 끄기
		dialogText.gameObject.SetActive(false);
		dialogText.text = "";
		SetTalkEndFlag(false);

		//너비 줄이기
		float width = wordBubble.GetComponent<RectTransform>().sizeDelta.x;
		float timer = 0;
		float time = 0.3f;
		while (width > 30)
		{
			timer += Time.deltaTime;
			if (timer > time)
				timer = time;

			width = Mathf.Lerp(2 * dialogWidth - 30, 30, 0.5f + Mathf.SmoothStep(0, 0.5f, timer / time));
			wordBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(width, dialogHeight);
			yield return null;
		}

		//높이 줄이기
		float height = wordBubble.GetComponent<RectTransform>().sizeDelta.y;
		timer = 0;
		time = 0.2f;
		while (height > 0)
		{
			timer += Time.deltaTime;
			if (timer > time)
				timer = time;

			height = Mathf.Lerp(2 * dialogHeight, 0, 0.5f + Mathf.SmoothStep(0, 0.5f, (timer / time)));
			wordBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(30, height);
			yield return null;
		}

		//마지막엔 비활성
		wordBubble.SetActive(false);
	}
	private Vector3 Formalization(Vector3 target)
	{
		return new Vector3(target.x % 360 >= 0 ? target.x % 360 : (target.x % 360) + 360,
			target.y % 360 >= 0 ? target.y % 360 : (target.y % 360) + 360,
			target.z % 360 >= 0 ? target.z % 360 : (target.z % 360) + 360);
	}
	private void SetTalkEndFlag(bool isActivate)
	{
		talkEndFlag.SetActive(isActivate);
	}
	private Sprite GetSprite(Face face)
	{
		Sprite result = null;
		//얼굴에 따른 스프라이트 반화
		foreach(FaceMap item in emotion)
		{
			if (item.face == face)
				result = item.sprite;
		}

		return result;
	}
	
	//private string StringVariablePaser(string text)
 //   {
 //       //string txt = "%%test%%?잠이 %%test2%% 오네요 이런 %%test3%% 젠장";
 //       string txt = text;
 //       Regex rgPattern = new Regex(@"\%%(\s*\w+\s*\w*)\%%");
 //       MatchCollection result = rgPattern.Matches(txt);
 //       int resizeNum = 0;

 //       foreach (Match mm in result)
 //       {
 //           Group g = mm.Groups[1];
 //           txt = txt.Remove(g.Index - 2 - resizeNum, g.Value.Length + 4);
 //           resizeNum -= InsertVariable(ref txt, g.Value, g.Index - 2 - resizeNum);
 //           resizeNum += g.Value.Length + 4;
 //       }
 //       //Debug.Log(txt);

 //       return txt;
 //   }


	//private int InsertVariable(ref string text, string variableText, int startIndex)
	//{
	//	string target = "";

	//	switch (variableText)
	//	{
	//		case "test":
	//			target = "테스트";
	//			break;
	//		case "test2":
	//			target = "테스트2";
	//			break;
	//		case "test3":
	//			target = "테스트3";
	//			break;
	//	}

	//	text = text.Insert(startIndex, target);

	//	return target.Length;
	//}
	//public IEnumerator move(Vector3 startPos, Vector3 endPos)
	//{
	//    RectTransform yukgackRect = yukgack.GetComponent<RectTransform>();

	//    yukgackRect.anchoredPosition3D = startPos;

	//    float dist = Vector3.Distance(yukgackRect.anchoredPosition3D, endPos);

	//    float curr = dist;

	//    Vector3 velocity = Vector3.zero;

	//    while (curr > 0.05 && !killAction)
	//    {
	//        yukgackRect.anchoredPosition3D = Vector3.SmoothDamp(yukgackRect.anchoredPosition3D, endPos, ref velocity, moveTime);

	//        curr = Vector3.Distance(yukgackRect.anchoredPosition3D, endPos);

	//        yield return null;
	//    }

	//    yukgackRect.anchoredPosition3D = endPos;
	//    killAction = false;

	//    yield return null;
	//}

	//public IEnumerator turn(Vector3 startPos, Vector3 endPos)
	//{
	//	RectTransform yukgackRect = yukgackBody.GetComponent<RectTransform>();

	//	yukgackRect.eulerAngles = startPos;

	//	float dist = Mathf.Abs(yukgackRect.eulerAngles.z - endPos.z);

	//	float curr = dist;

	//	Vector3 velocity = Vector3.zero;

	//	while (curr > 0.05 && !killAction)
	//	{
	//		yukgackRect.eulerAngles = Vector3.SmoothDamp(yukgackRect.eulerAngles, endPos, ref velocity, turnTime);

	//		curr = Mathf.Abs(yukgackRect.eulerAngles.z - endPos.z);

	//		yield return null;
	//	}

	//	yukgackRect.eulerAngles = endPos;
	//	killAction = false;

	//	yield return null;
	//}
	//public IEnumerator NonStopTalk(string text)
	//{
	//	SetDialogActive(true);
	//	Text dialogue = dialogText.GetComponent<Text>();

	//	//int nTrigger = 0;

	//	dialogue.text = "";
	//	if (!wordBubble.activeSelf)
	//	{
	//		wordBubble.SetActive(true);
	//	}

	//	text = StringVariablePaser(text);

	//	foreach (char letter in text.ToCharArray())
	//	{
	//		dialogue.text += letter;

	//		yield return new WaitForSeconds(0.05f);

	//		if (killAction)
	//		{
	//			dialogue.text = text;

	//			break;
	//		}
	//	}

	//	killAction = false;

	//	//다음 터치 입력까지 대기
	//	while (killAction == false)
	//	{
	//		yield return null;
	//	}

	//	killAction = false;

	//	yield return new WaitForSeconds(0.2f);
	//}
}

[System.Serializable]
public class FaceMap
{
	public Face face;
	public Sprite sprite;
}

//[CreateAssetMenu(fileName = "YukgackEmotion", menuName = "Yukgack/Emotion")]
//public class YukgackFace : ScriptableObject
//{
//	public FaceMap[] emotions;
//}