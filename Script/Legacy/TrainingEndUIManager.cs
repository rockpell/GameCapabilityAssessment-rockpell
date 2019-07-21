using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingEndUIManager : MonoBehaviour {

    [SerializeField] private Text[] charTexts;

	// Use this for initialization
	void Start () {

	}

    public void CharUp(int index)
    {
        char target = charTexts[index].text.ToCharArray()[0];
        target = (char)(target - 1);
        target = CheckChar(target);
        charTexts[index].text = target.ToString();
    }

    public void CharDown(int index)
    {
        char target = charTexts[index].text.ToCharArray()[0];
        target = (char)(target + 1);
        target = CheckChar(target);
        charTexts[index].text = target.ToString();
    }
     // A :65,  Z : 90
    private char CheckChar(char value)
    {
        char result = value;
        if(value < 65)
        {
            result = 'Z';
        } else if(value > 90)
        {
            result = 'A';
        }
        return result;
    }

    public void Submit()
    {
        TrainingManager _trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        string _gameName = _trainingManager.SubjectIndex.ToString() + _trainingManager.MinigameIndex.ToString();
        string _result = charTexts[0].text + charTexts[1].text + charTexts[2].text;

        //_trainingManager.EndTraining();
        //GameManager.Instance.gameObject.GetComponent<LocalRankingManager>().SaveRecord(_gameName, _result, GameManager.Instance.DifficultyLevel);
        GameManager.Instance.gameObject.GetComponent<RankingManager>().SaveRecord(
            _gameName, _result, GameManager.Instance.DifficultyLevel, () =>
            {
                _trainingManager.SetMyRankingText();
                _trainingManager.CloseEndPanel();
                _trainingManager.ShowRankPanel();
            }
            );
    }

    public void EndMyRank()
    {
        TrainingManager _trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        _trainingManager.EndTraining();
        GameManager.Instance.InitTrigger();
    }
}
