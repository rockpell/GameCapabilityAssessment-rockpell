using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private Image tutorialImage;

    [SerializeField] private Sprite[] aimingTutorialImages;
    [SerializeField] private Sprite[] concentrationTutorialImages;
    [SerializeField] private Sprite[] quicknessTutorialImages;
    [SerializeField] private Sprite[] rhythmicSenseTutorialImages;
    [SerializeField] private Sprite[] thinkingTutorialImages;

    private CheckTraining checkTraining; // 트레이닝을 시작한 항목과 게임 번호 저장되어 있음

    // Use this for initialization
    void Start () {
        if (!GameManager.Instance.IsEvaluationStart)
        {
            checkTraining = GameManager.Instance.CheckTraining;
            tutorialImage.sprite = GetNowTutorialImage()[checkTraining.MinigameIndex];
        }
        else
        {
            //tutorialImage.sprite = GetNowTutorialImage()[GameManager.Instance.GetNowMiniGame()];
        }
    }
	
	public void StartGame()
    {
        if (GameManager.Instance.IsEvaluationStart)
        {
            Subject tempSubject = GameManager.Instance.NowSubject;
            int tempIndex = GameManager.Instance.GetChoicedMiniGame();
            CustomSceneManager.Instance.LoadGame(tempSubject, tempIndex);
        }
        else
        {
            CustomSceneManager.Instance.LoadGame((Subject)checkTraining.SubjectIndex, checkTraining.MinigameIndex);
        }
    }

    private Sprite[] GetNowTutorialImage()
    {
        int _tempSubjectIndex;
        if (GameManager.Instance.IsEvaluationStart)
        {
            _tempSubjectIndex = (int)GameManager.Instance.NowSubject;

        } else
        {
            _tempSubjectIndex = checkTraining.SubjectIndex;
        }

        if (_tempSubjectIndex == 0) return aimingTutorialImages;
        else if (_tempSubjectIndex == 1) return concentrationTutorialImages;
        else if (_tempSubjectIndex == 2) return quicknessTutorialImages;
        else if (_tempSubjectIndex == 3) return rhythmicSenseTutorialImages;
        else if (_tempSubjectIndex == 4) return thinkingTutorialImages;

        return null;
    }
}
