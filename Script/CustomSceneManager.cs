using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour {
    public enum UIScenes { Evaluation, SeletScene, TrainingScene, MiniGameTest, ResultScene, Tutorial, NewEvaluation};

    [SerializeField] private SceneField[] aimingSceneNames;
    [SerializeField] private SceneField[] concentrationSceneNames;
    [SerializeField] private SceneField[] quicknessSceneNames;
    [SerializeField] private SceneField[] rhythmicSenseSceneNames;
    [SerializeField] private SceneField[] thinkingSceneNames;
    [SerializeField] private SceneField evaluationScene;
    [SerializeField] private SceneField selectScene;
    [SerializeField] private SceneField trainingScene;
    [SerializeField] private SceneField miniGameTest;
    [SerializeField] private SceneField resultScene;
    [SerializeField] private SceneField tutorialScene;
    [SerializeField] private SceneField newEvaluation;

    private Subject nowGameAttribue;
    private int nowGameIndex;

    private static CustomSceneManager instance;

    public static CustomSceneManager Instance{
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = GetComponent<CustomSceneManager>();
        }
    }

    public void LoadGame(Subject subject, int index)
    {
        switch (subject)
        {
            case Subject.Aiming:
                if(aimingSceneNames[index] != null)
                    SceneManager.LoadScene(aimingSceneNames[index]);
                break;
            case Subject.Concentration:
                if(concentrationSceneNames[index] != null)
                    SceneManager.LoadScene(concentrationSceneNames[index]);
                break;
            case Subject.Quickness:
                if(quicknessSceneNames[index] != null)
                    SceneManager.LoadScene(quicknessSceneNames[index]);
                break;
            case Subject.RhythmicSense:
                if(rhythmicSenseSceneNames[index] != null)
                    SceneManager.LoadScene(rhythmicSenseSceneNames[index]);
                break;
            case Subject.Thinking:
                if(thinkingSceneNames[index] != null)
                    SceneManager.LoadScene(thinkingSceneNames[index]);
                break;
        }
    }

    public void LoadUIScene(UIScenes uiScene)
    {
        switch (uiScene)
        {
            case UIScenes.Evaluation:
                if (evaluationScene != null)
                    SceneManager.LoadScene(evaluationScene);
                break;
            case UIScenes.SeletScene:
                if (selectScene != null)
                    SceneManager.LoadScene(selectScene);
                break;
            case UIScenes.TrainingScene:
                if (trainingScene != null)
                    SceneManager.LoadScene(trainingScene);
                break;
            case UIScenes.MiniGameTest:
                if (miniGameTest != null)
                    SceneManager.LoadScene(miniGameTest);
                break;
            case UIScenes.ResultScene:
                if (resultScene != null)
                    SceneManager.LoadScene(resultScene);
                break;
            case UIScenes.Tutorial:
                if(tutorialScene != null)
                    SceneManager.LoadScene(tutorialScene);
                break;
            case UIScenes.NewEvaluation:
                if (newEvaluation != null)
                    SceneManager.LoadScene(newEvaluation);
                break;
        }
    }

    public void LoadUIScene(UIScenes uiScene, float delayTime)
    {
        StartCoroutine(DelayLoadUIScene(uiScene, delayTime));
    }

    private IEnumerator DelayLoadUIScene(UIScenes uiScene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        LoadUIScene(uiScene);
    }

    public SceneField[] GetSceneNames(Subject subject)
    {
        switch (subject)
        {
            case Subject.Aiming:
                return aimingSceneNames;
            case Subject.Concentration:
                return concentrationSceneNames;
            case Subject.Quickness:
                return quicknessSceneNames;
            case Subject.RhythmicSense:
                return rhythmicSenseSceneNames;
            case Subject.Thinking:
                return thinkingSceneNames;
        }
        return null;
    }
    
    public Subject GetGameSubject(string gameName)
    {
        Subject _result = Subject.None;

        for(int i = 1; i <= 5; i++)
        {
            SceneField[] _names = GetSceneNames((Subject)i);
            for (int p = 0; p < _names.Length; p++)
            {
                if (_names[p] == gameName)
                {
                    _result = (Subject)i;
                    break;
                }
            }
            if (_result != Subject.None) break;
        }

        return _result;
    }

    public void MoveSceneLobby()
    {
        LoadUIScene(UIScenes.SeletScene);
    }

    public string GetNowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public string GetNewEvaluation()
    {
        return newEvaluation;
    }
}
