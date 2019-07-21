using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VSRhythmSample
{
    public AudioClip beat;
    public AudioClip startBeat;
}

public class VSSampleRhythm : MonoBehaviour {

    public VSRhythmSample[] beatList;
    public float rhythm;
    public int count;
    public int repeatCount;
    public float curTime;
    public float endTime;
    private List<float> rhythmList;
    private new AudioSource audio;
    private int index;
    private int beatIndex;
    private int level;

    [SerializeField]
    private GameObject _backGround;
    [SerializeField]
    private GameObject _time;
    [SerializeField]
    private GameObject _navigator;

    void Awake ()
    {
        audio = GetComponent<AudioSource>();
        level = NewGameManager.Instance.StartGame();
        if (GameObject.Find("TutorialCanvas") != null) level = 1;
        beatIndex = (int)((level - 1) / 5);
        audio.clip = beatList[beatIndex].beat;
        count = beatIndex*2 + level + 1;
        if (level > 10)
            count -= 6;
        //레벨에 따라 오디오 클립 교체, Index 0 = 1-5난이도, 1 = 6-10난이도, 2 = 11-15난이도
        rhythm = (float)System.Math.Round(audio.clip.length, 3);
        rhythmList = new List<float>();
        MakeRhythm();

        _backGround.GetComponent<AudioSource>().clip = this.audio.clip;
        _backGround.GetComponent<AudioSource>().Play();
        StartCoroutine(_navigator.GetComponent<VSNavigationBar>().StartNavigator());
    }
	
    void MakeRhythm()
    {
        while (rhythmList.Count < count)
        {
            float addValue = Random.Range(2, (int)(endTime/rhythm) + 1) * rhythm;
            if (!rhythmList.Contains(addValue))
                rhythmList.Add(addValue);
        }
        rhythmList.Sort();
    }

	void Update ()
    {
        curTime += Time.deltaTime;
        if(repeatCount == 1)
        {
            _time.GetComponent<UnityEngine.UI.Text>().text = ((int)endTime - Mathf.Round(curTime)).ToString();
        }

        if ((rhythmList.Count > index) && (rhythmList[index] < curTime) && (repeatCount < 2))
        {
            audio.Play();
            index++;
        }
        if ((endTime < curTime) && (repeatCount < 2))
        {
            curTime = 0;
            index = 0;
            repeatCount++;
        }
        else if(repeatCount == 2)
        {
            repeatCount++;
            _time.GetComponent<UnityEngine.UI.Text>().text = "Start!";
            StartCoroutine(TimeFieldInit());
        }
        else if(repeatCount > 1)
        {
            if (!audio.isPlaying)
                GameObject.Find("HandKnife").GetComponent<VSHandMoving>()._gameStart = true;
                
        }
    }

    IEnumerator TimeFieldInit()
    {
        yield return new WaitForSeconds(0.5f);
        _time.GetComponent<UnityEngine.UI.Text>().text = "";
    }
    public List<float> GetList()
    { return rhythmList; }
}
