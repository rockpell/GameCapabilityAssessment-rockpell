using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public partial class YukgackManager : MonoBehaviour
{

    private List<Action> actionList;
    //private IEnumerator[] routines;

    [SerializeField]
    private GameObject yukgack; // 화면에 나타나는 육각이 오브젝트

    private RectTransform yukgackRect;

    [SerializeField]
    private GameObject wordBubble;

    [SerializeField]
    private GameObject dialogText;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private Vector3 endPos;

    [SerializeField]
    private int moveSpeed;

    private bool killAction;

    private bool end { get; set; }

    private int numOfplay;

    private ResultCode result;

    private FinalResultCode finalResult;

    List<ResultCode> possibleDialogue;

    List<TotalResultCode> totalDialogue;

    private void Start()
    {
        actionList = new List<Action>();

        //yukgackRect = yukgack.GetComponent<RectTransform>();

        //yukgackRect.anchoredPosition3D = startPos;

        end = false;

        numOfplay = NewGameManager.Instance.GetMaxEvalationTime();

        possibleDialogue = new List<ResultCode>();

        totalDialogue = new List<TotalResultCode>();

        killAction = false;

        //test();
    }

    [ContextMenu("test")]
    private void test()
    {
        Subject subject = Subject.Aiming;

        List<GameResult>[] gameResults = new List<GameResult>[5];

        List<GameResult> aimList = new List<GameResult>();

        aimList.Add(new GameResult("SomeBodyHelpMe", Result.Successful));
        aimList.Add(new GameResult("WhiteFlag", Result.Successful));
        aimList.Add(new GameResult("SomeBodyHelpMe", Result.Fail));
        aimList.Add(new GameResult("WhiteFlag", Result.Fail));
        aimList.Add(new GameResult("SomeBodyHelpMe", Result.Successful));
        aimList.Add(new GameResult("WhiteFlag", Result.Fail));
        aimList.Add(new GameResult("SomeBodyHelpMe", Result.Successful));
        aimList.Add(new GameResult("WhiteFlag", Result.Successful));

        gameResults[0] = aimList;

        Act(Subject.Aiming, gameResults);

    }

    private void test2()
    {
        totalDialogue.Add(TotalResultCode.TOT_A3);
        totalDialogue.Add(TotalResultCode.TOT_B2);

        chooseAction(totalDialogue);

        actionList.Sort(delegate (Action a, Action b) // 내림차순 정렬
        {
            if (a.priority < b.priority) return -1;
            else if (a.priority > b.priority) return 1;
            return 0;
        });

        StartCoroutine(MultipleTask(actionList));
    }

    public string[] EvalationMente(Subject subject, List<GameResult>[] gameResults)
    {
        List<string> output = new List<string>();
        
        if (Subject.None != subject)
        {
            if (Subject.Aiming == subject)
            {
                AimingAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.Concentration == subject)
            {
                ConcentrationAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.Quickness == subject)
            {
                QuicknessAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.RhythmicSense == subject)
            {
                RhythmicSenseAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.Thinking == subject)
            {
                ThinkingAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }

            result = choosePriorityResult(possibleDialogue);

            output.Add(resultCodeToString(result));
            output.Add(finalResultCodeToString(finalResult));

        }
        else // if (Subject.None == subject)
        {
            TotalAct(gameResults);

            foreach (TotalResultCode i in totalDialogue)
            {
                output.Add(totalResultCodeToString(i));
            }

        }

        return output.ToArray();
    }

    public void Act(Subject subject, List<GameResult>[] gameResults)
    {
        if (Subject.None != subject)
        {
            if (Subject.Aiming == subject)
            {
                AimingAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.Concentration == subject)
            {
                ConcentrationAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.Quickness == subject)
            {
                QuicknessAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.RhythmicSense == subject)
            {
                RhythmicSenseAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }
            else if (Subject.Thinking == subject)
            {
                ThinkingAct(gameResults[NewGameManager.Instance.GetSubjectIndex(subject)]);
            }


            result = choosePriorityResult(possibleDialogue);

            chooseAction(result, finalResult);

            actionList.Sort(delegate (Action a, Action b) // 내림차순 정렬
            {
                if (a.priority < b.priority) return -1;
                else if (a.priority > b.priority) return 1;
                return 0;
            });

            StartCoroutine(MultipleTask(actionList));
        }
        else // if (Subject.None == subject)
        {
            TotalAct(gameResults);

            chooseAction(totalDialogue);

            actionList.Sort(delegate (Action a, Action b) // 내림차순 정렬
            {
                if (a.priority < b.priority) return -1;
                else if (a.priority > b.priority) return 1;
                return 0;
            });

            StartCoroutine(MultipleTask(actionList));
        }
    }

    //같은 부류의 멘트은 else if로 묶을 것 (예 : if(1번 성공) else if(2번 성공) else if(3번 성공)...)

    private IEnumerator AimingAct(List<GameResult> gameResults)
    {
        ResultAnalysis analysis = Analyze(gameResults);

        finalResult = chooseFinalResult(analysis.finalScore, Subject.Aiming);

        generalAnalyze(gameResults);

        if (analysis.totalSuccess == 0)
        {
            possibleDialogue.Add(ResultCode.AIM_1);
        }
        else if (analysis.totalSuccess == 1)
        {
            possibleDialogue.Add(ResultCode.AIM_2);
        }
        else if (analysis.totalSuccess == 2)
        {
            possibleDialogue.Add(ResultCode.AIM_3);
        }
        else if (analysis.totalSuccess == 3)
        {
            possibleDialogue.Add(ResultCode.AIM_4);
        }
        else if (analysis.totalSuccess == 4)
        {
            possibleDialogue.Add(ResultCode.AIM_5);
        }
        else if (analysis.totalSuccess == 5)
        {
            possibleDialogue.Add(ResultCode.AIM_6);
        }
        else if (analysis.totalSuccess == 6)
        {
            possibleDialogue.Add(ResultCode.AIM_7);
        }
        else if (analysis.totalSuccess == 7)
        {
            possibleDialogue.Add(ResultCode.AIM_8);
        }
        else if (analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.AIM_9);
        }


        return null;
    }

    private IEnumerator ConcentrationAct(List<GameResult> gameResults)
    {
        ResultAnalysis analysis = Analyze(gameResults);

        finalResult = chooseFinalResult(analysis.finalScore, Subject.Concentration);

        generalAnalyze(gameResults);

        if (analysis.totalSuccess == 0)
        {
            possibleDialogue.Add(ResultCode.CON_1);
        }
        else if (analysis.totalSuccess == 1)
        {
            possibleDialogue.Add(ResultCode.CON_2);
        }
        else if (analysis.totalSuccess == 2)
        {
            possibleDialogue.Add(ResultCode.CON_3);
        }
        else if (analysis.totalSuccess == 3)
        {
            possibleDialogue.Add(ResultCode.CON_4);
        }
        else if (analysis.totalSuccess == 4)
        {
            possibleDialogue.Add(ResultCode.CON_5);
        }
        else if (analysis.totalSuccess == 5)
        {
            possibleDialogue.Add(ResultCode.CON_6);
        }
        else if (analysis.totalSuccess == 6)
        {
            possibleDialogue.Add(ResultCode.CON_7);
        }
        else if (analysis.totalSuccess == 7)
        {
            possibleDialogue.Add(ResultCode.CON_8);
        }
        else if (analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.CON_9);
        }

        if (analysis.grList[analysis.grList.Count - 1].gameName.Equals("Cooking") && analysis.grList[analysis.grList.Count - 1].result == true)
        {
            possibleDialogue.Add(ResultCode.CON_12);
        }


        if (analysis.grList[analysis.grList.Count - 1].gameName.Equals("BoxingGame") && analysis.finalScore == 15)
        {
            possibleDialogue.Add(ResultCode.CON_13);
        }


        if (analysis.resultList[5] == false && analysis.resultList[6] == false && analysis.resultList[7] == false)
        {
            possibleDialogue.Add(ResultCode.CON_14);
        }



        return null;
    }

    private IEnumerator QuicknessAct(List<GameResult> gameResults)
    {
        ResultAnalysis analysis = Analyze(gameResults);

        finalResult = chooseFinalResult(analysis.finalScore, Subject.Quickness);

        generalAnalyze(gameResults);

        if (analysis.totalSuccess == 0)
        {
            possibleDialogue.Add(ResultCode.QUK_1);
        }
        else if (analysis.totalSuccess == 1)
        {
            possibleDialogue.Add(ResultCode.QUK_2);
        }
        else if (analysis.totalSuccess == 2)
        {
            possibleDialogue.Add(ResultCode.QUK_3);
        }
        else if (analysis.totalSuccess == 3)
        {
            possibleDialogue.Add(ResultCode.QUK_4);
        }
        else if (analysis.totalSuccess == 4)
        {
            possibleDialogue.Add(ResultCode.QUK_5);
        }
        else if (analysis.totalSuccess == 5)
        {
            possibleDialogue.Add(ResultCode.QUK_6);
        }
        else if (analysis.totalSuccess == 6)
        {
            possibleDialogue.Add(ResultCode.QUK_7);
        }
        else if (analysis.totalSuccess == 7)
        {
            possibleDialogue.Add(ResultCode.QUK_8);
        }
        else if (analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.QUK_9);
        }

        if (analysis.grList[analysis.grList.Count - 1].gameName.Equals("NEmain"))
        {
            possibleDialogue.Add(ResultCode.QUK_36);
        }

        return null;
    }

    private IEnumerator RhythmicSenseAct(List<GameResult> gameResults)
    {
        ResultAnalysis analysis = Analyze(gameResults);

        finalResult = chooseFinalResult(analysis.finalScore, Subject.RhythmicSense);

        generalAnalyze(gameResults);

        if (analysis.totalSuccess == 0)
        {
            possibleDialogue.Add(ResultCode.RHY_1);
        }
        else if (analysis.totalSuccess == 1)
        {
            possibleDialogue.Add(ResultCode.RHY_2);
        }
        else if (analysis.totalSuccess == 2)
        {
            possibleDialogue.Add(ResultCode.RHY_3);
        }
        else if (analysis.totalSuccess == 3)
        {
            possibleDialogue.Add(ResultCode.RHY_4);
        }
        else if (analysis.totalSuccess == 4)
        {
            possibleDialogue.Add(ResultCode.RHY_5);
        }
        else if (analysis.totalSuccess == 5)
        {
            possibleDialogue.Add(ResultCode.RHY_6);
        }
        else if (analysis.totalSuccess == 6)
        {
            possibleDialogue.Add(ResultCode.RHY_7);
        }
        else if (analysis.totalSuccess == 7)
        {
            possibleDialogue.Add(ResultCode.RHY_8);
        }
        else if (analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.RHY_9);
        }

        return null;
    }

    private IEnumerator ThinkingAct(List<GameResult> gameResults)
    {
        ResultAnalysis analysis = Analyze(gameResults);

        finalResult = chooseFinalResult(analysis.finalScore, Subject.Thinking);

        generalAnalyze(gameResults);

        if (analysis.totalSuccess == 0)
        {
            possibleDialogue.Add(ResultCode.TNK_1);
        }
        else if (analysis.totalSuccess == 1)
        {
            possibleDialogue.Add(ResultCode.TNK_2);
        }
        else if (analysis.totalSuccess == 2)
        {
            possibleDialogue.Add(ResultCode.TNK_3);
        }
        else if (analysis.totalSuccess == 3)
        {
            possibleDialogue.Add(ResultCode.TNK_4);
        }
        else if (analysis.totalSuccess == 4)
        {
            possibleDialogue.Add(ResultCode.TNK_5);
        }
        else if (analysis.totalSuccess == 5)
        {
            possibleDialogue.Add(ResultCode.TNK_6);
        }
        else if (analysis.totalSuccess == 6)
        {
            possibleDialogue.Add(ResultCode.TNK_7);
        }
        else if (analysis.totalSuccess == 7)
        {
            possibleDialogue.Add(ResultCode.TNK_8);
        }
        else if (analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.TNK_9);
        }

        if ((analysis.resultList[0] == true && analysis.resultList[1] == false && analysis.resultList[2] == true && analysis.resultList[3] == false && analysis.resultList[4] == true &&
            analysis.resultList[5] == false && analysis.resultList[6] == true && analysis.resultList[7] == false && analysis.resultList[8] == true && analysis.resultList[9] == false)
            ||
            (analysis.resultList[0] == false && analysis.resultList[1] == true && analysis.resultList[2] == false && analysis.resultList[3] == true && analysis.resultList[4] == false &&
            analysis.resultList[5] == true && analysis.resultList[6] == false && analysis.resultList[7] == true && analysis.resultList[8] == false && analysis.resultList[9] == true))
        {
            possibleDialogue.Add(ResultCode.TNK_12);
        }


        return null;
    }

    private IEnumerator TotalAct(List<GameResult>[] gameResults)
    {
        TotalAnalysis totalAnalysis = totalAnalyze(gameResults);

        if (0 <= totalAnalysis.average && totalAnalysis.average <= 3)
        {
            totalDialogue.Add(TotalResultCode.TOT_A1);
        }
        else if (4 <= totalAnalysis.average && totalAnalysis.average <= 6)
        {
            totalDialogue.Add(TotalResultCode.TOT_A2);
        }
        else if (7 <= totalAnalysis.average && totalAnalysis.average <= 11)
        {
            totalDialogue.Add(TotalResultCode.TOT_A3);
        }
        else if (12 <= totalAnalysis.average)
        {
            totalDialogue.Add(TotalResultCode.TOT_A4);
        }

        if (0 <= totalAnalysis.range && totalAnalysis.range <= 3)
        {
            totalDialogue.Add(TotalResultCode.TOT_B1);
        }
        else if (4 <= totalAnalysis.range && totalAnalysis.range <= 8)
        {
            totalDialogue.Add(TotalResultCode.TOT_B2);
        }
        else if (9 <= totalAnalysis.range)
        {
            totalDialogue.Add(TotalResultCode.TOT_B3);
        }


        if (totalAnalysis.highestSubject == Subject.Aiming && totalAnalysis.highestScore >= 11)
        {
            totalDialogue.Add(TotalResultCode.TOT_C1);
        }
        else if (totalAnalysis.highestSubject == Subject.Concentration && totalAnalysis.highestScore >= 11)
        {
            totalDialogue.Add(TotalResultCode.TOT_C2);
        }
        else if (totalAnalysis.highestSubject == Subject.Quickness && totalAnalysis.highestScore >= 11)
        {
            totalDialogue.Add(TotalResultCode.TOT_C3);
        }
        else if (totalAnalysis.highestSubject == Subject.RhythmicSense && totalAnalysis.highestScore >= 11)
        {
            totalDialogue.Add(TotalResultCode.TOT_C4);
        }
        else if (totalAnalysis.highestSubject == Subject.Thinking && totalAnalysis.highestScore >= 11)
        {
            totalDialogue.Add(TotalResultCode.TOT_C5);
        }

        if (totalAnalysis.lowestSubject == Subject.Aiming && totalAnalysis.lowestScore <= 5)
        {
            totalDialogue.Add(TotalResultCode.TOT_D1);
        }
        else if (totalAnalysis.lowestSubject == Subject.Concentration && totalAnalysis.lowestScore <= 5)
        {
            totalDialogue.Add(TotalResultCode.TOT_D2);
        }
        else if (totalAnalysis.lowestSubject == Subject.Quickness && totalAnalysis.lowestScore <= 5)
        {
            totalDialogue.Add(TotalResultCode.TOT_D3);
        }
        else if (totalAnalysis.lowestSubject == Subject.RhythmicSense && totalAnalysis.lowestScore <= 5)
        {
            totalDialogue.Add(TotalResultCode.TOT_D4);
        }
        else if (totalAnalysis.lowestSubject == Subject.Thinking && totalAnalysis.lowestScore <= 5)
        {
            totalDialogue.Add(TotalResultCode.TOT_D5);
        }

        return null;
    }

    private void generalAnalyze(List<GameResult> gameResults)
    {
        ResultAnalysis analysis = Analyze(gameResults);

        if (analysis.totalSuccess == 0)
        {
            possibleDialogue.Add(ResultCode.GEN_1);
        }
        else if (analysis.totalSuccess == 1)
        {
            possibleDialogue.Add(ResultCode.GEN_2);
        }
        else if (analysis.totalSuccess == 2)
        {
            possibleDialogue.Add(ResultCode.GEN_3);
        }
        else if (analysis.totalSuccess == 3)
        {
            possibleDialogue.Add(ResultCode.GEN_4);
        }
        else if (analysis.totalSuccess == 4)
        {
            possibleDialogue.Add(ResultCode.GEN_5);
        }
        else if (analysis.totalSuccess == 5)
        {
            possibleDialogue.Add(ResultCode.GEN_6);
        }
        else if (analysis.totalSuccess == 6)
        {
            possibleDialogue.Add(ResultCode.GEN_7);
        }
        else if (analysis.totalSuccess == 7)
        {
            possibleDialogue.Add(ResultCode.GEN_8);
        }
        else if (analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.GEN_9);
        }


        if (analysis.gaList.Exists(x => x.chain == 3) && analysis.gaList.Find(x => x.chain == 3).success == 0)
        {
            possibleDialogue.Add(ResultCode.GEN_12);
        }

        if (analysis.resultList[analysis.resultList.Count - 1] == false)
        {
            possibleDialogue.Add(ResultCode.GEN_36);
        }

        if (analysis.resultList[analysis.resultList.Count - 1] == false && analysis.totalSuccess == 8)
        {
            possibleDialogue.Add(ResultCode.GEN_36);
        }

    }

    private ResultCode choosePriorityResult(List<ResultCode> input)
    {
        if (input.Count == 0)
        {
            return ResultCode.NULL;
        }

        List<ResultPriority> resultPriorities = new List<ResultPriority>();

        List<ResultPriority> biggistPriorities = new List<ResultPriority>();

        for (int i = 0; i < input.Count; i++)
        {
            resultPriorities.Add(new ResultPriority(input[i], priorityCheck(input[i])));
        }

        resultPriorities.Sort(delegate (ResultPriority a, ResultPriority b) // 내림차순 정렬
        {
            if (a.priority < b.priority) return -1;
            else if (a.priority > b.priority) return 1;
            return 0;
        });

        for (int i = 0; i < resultPriorities.Count; i++)
        {
            if (resultPriorities[i].priority == resultPriorities[0].priority)
            {
                biggistPriorities.Add(resultPriorities[i]);
            }
        }

        return biggistPriorities[Random.Range(0, biggistPriorities.Count)].result;
    }

    private FinalResultCode chooseFinalResult(int finalResult, Subject subject)
    {
        // 범용이 선택됨
        if (Random.Range(0, 2) == 1)
        {
            return (FinalResultCode)finalResult;
        }

        // 범용이 선택 안됨
        else
        {
            if (Subject.Aiming == subject)
            {
                return (FinalResultCode)(100 + finalResult);
            }
            else if (Subject.Concentration == subject)
            {
                return (FinalResultCode)(200 + finalResult);
            }
            else if (Subject.Quickness == subject)
            {
                return (FinalResultCode)(300 + finalResult);
            }
            else if (Subject.RhythmicSense == subject)
            {
                return (FinalResultCode)(400 + finalResult);
            }
            else if (Subject.Thinking == subject)
            {
                return (FinalResultCode)(500 + finalResult);
            }
            else // if (Subject.None == subject)
            {
                return FinalResultCode.NULL;
            }
        }

    }

    private int CalculateEndValue(int startValue, List<GameResult> gameResult) // 마지막 게임 결과를 반환
    {
        int _result = startValue;
        foreach (GameResult result in gameResult)
        {
            _result += (int)result.resultValue;
        }
        return _result;
    }

    private IEnumerator MultipleTask(params IEnumerator[] routines)
    {
        foreach (IEnumerator item in routines)
        {
            yield return StartCoroutine(item);
        }

        yield break;
    }

    private IEnumerator MultipleTask(List<Action> routines)
    {
        foreach (Action item in routines)
        {
            yield return StartCoroutine(item.act);
        }

        yield break;
    }

    private IEnumerator Move(Vector3 position)
    {
        float dist = Vector3.Distance(yukgackRect.anchoredPosition3D, position);

        //Debug.Log(yukgackRect.anchoredPosition3D);

        //Debug.Log(position);

        //Debug.Log(dist);

        float curr = dist;

        while (curr > 0.0001 && !killAction)
        {
            yukgackRect.anchoredPosition3D = Vector3.MoveTowards(yukgackRect.anchoredPosition3D, position, Time.deltaTime * moveSpeed * ((dist + curr * 1) / (dist * 2)));

            curr = Vector3.Distance(yukgackRect.anchoredPosition3D, position);

            yield return null;
        }

        yukgackRect.anchoredPosition3D = endPos;
        killAction = false;


        yield return null;
    }

    private IEnumerator NonStopTalk(string text)
    {
        Text dialogue = dialogText.GetComponent<Text>();

        //int nTrigger = 0;

        dialogue.text = "";
        if (!wordBubble.activeSelf)
        {
            wordBubble.SetActive(true);
        }

        text = StringVariablePaser(text);

        foreach (char letter in text.ToCharArray())
        {
            dialogue.text += letter;

            yield return new WaitForSeconds(0.05f);

            if (killAction)
            {
                dialogue.text = text;

                break;
            }
        }



        killAction = false;

        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator endDialogue()
    {
        end = true;

        yield return null;
    }

    private string StringVariablePaser(string text)
    {
        //string txt = "%%test%%?잠이 %%test2%% 오네요 이런 %%test3%% 젠장";
        string txt = text;
        Regex rgPattern = new Regex(@"\%%(\s*\w+\s*\w*)\%%");
        MatchCollection result = rgPattern.Matches(txt);
        int resizeNum = 0;

        foreach (Match mm in result)
        {
            Group g = mm.Groups[1];
            txt = txt.Remove(g.Index - 2 - resizeNum, g.Value.Length + 4);
            resizeNum -= InsertVariable(ref txt, g.Value, g.Index - 2 - resizeNum);
            resizeNum += g.Value.Length + 4;
        }
        //Debug.Log(txt);

        return txt;
    }

    private int InsertVariable(ref string text, string variableText, int startIndex)
    {
        string target = "";

        switch (variableText)
        {
            case "test":
                target = "테스트";
                break;
            case "test2":
                target = "테스트2";
                break;
            case "test3":
                target = "테스트3";
                break;
        }

        text = text.Insert(startIndex, target);

        return target.Length;
    }

    private ResultAnalysis Analyze(List<GameResult> gameResults)
    {
        ResultAnalysis output = new ResultAnalysis();


        for (int i = 0; gameResults.Count > i; i++)
        {
            //게임별 GameAnalysis 생성
            GameAnalysis temp = null;

            if (output.gaList.Exists(x => x.gameName.Equals(gameResults[i].name)))//이미 게임이 있음
            {
                temp = output.gaList.Find(x => x.gameName.Equals(gameResults[i].name));

                temp.frequency++;

                //성공 수
                if (gameResults[i].resultValue != Result.Fail)
                {
                    temp.success++;
                }
            }
            else// 없음
            {
                temp = new GameAnalysis(gameResults[i].name, gameResults[i].resultValue);
                output.gaList.Add(temp);
            }

            //총 성공횟수 계산 & 성공 실패 순서
            //각 게임별 Game_Result 생성
            if (gameResults[i].resultValue != Result.Fail)
            {
                output.totalSuccess += 1;

                output.resultList.Add(true);

                output.grList.Add(new Game_Result(gameResults[i].name, true));
            }
            else
            {
                output.resultList.Add(false);

                output.grList.Add(new Game_Result(gameResults[i].name, false));
            }
        }

        //게임 별 연속 수 계산
        for (int i = 0; output.gaList.Count > i; i++)
        {
            int highest = 0;

            int now = 0;

            for (int j = 0; gameResults.Count > j; j++)
            {
                if (output.gaList[i].gameName.Equals(gameResults[j].name))
                {
                    now++;
                }

                else
                {
                    if (highest < now)
                    {
                        highest = now;

                        now = 0;
                    }
                }
            }

            if (highest < now)
            {
                highest = now;

                now = 0;
            }

            output.gaList[i].chain = highest;
        }

        output.finalScore = CalculateEndValue(5, gameResults); // 마지막 결과 계산

        // 최종게임 결과
        if (gameResults[numOfplay - 1].resultValue != Result.Fail)
        {
            output.finalGameResult = true;
        }

        return output;
    }

    private TotalAnalysis totalAnalyze(List<GameResult>[] gameResults)
    {
        TotalAnalysis output = new TotalAnalysis();

        List<ResultAnalysis> analysises = new List<ResultAnalysis>();

        int high = -1;

        int low = 17;

        analysises.Add(Analyze(gameResults[NewGameManager.Instance.GetSubjectIndex(Subject.Aiming)]));
        analysises.Add(Analyze(gameResults[NewGameManager.Instance.GetSubjectIndex(Subject.Concentration)]));
        analysises.Add(Analyze(gameResults[NewGameManager.Instance.GetSubjectIndex(Subject.Quickness)]));
        analysises.Add(Analyze(gameResults[NewGameManager.Instance.GetSubjectIndex(Subject.RhythmicSense)]));
        analysises.Add(Analyze(gameResults[NewGameManager.Instance.GetSubjectIndex(Subject.Thinking)]));

        output.average = (analysises[0].finalScore + analysises[1].finalScore + analysises[2].finalScore + analysises[3].finalScore + analysises[4].finalScore) / 5;

        //for(int i = 0; analysises.Count > i; i++)
        //{
        //    if (analysises[i].finalScore > high)
        //    {
        //        high = analysises[i].finalScore;

        //        output.highestSubject = (Subject)(i + 1);
        //    }

        //    if (analysises[i].finalScore < low)
        //    {
        //        low = analysises[i].finalScore;

        //        output.lowestSubject = (Subject)(i + 1);
        //    }
        //}

        // 최고 최소 난이도 계산
        for(int i = 0; i < gameResults.Length ;++i)
        {
            int tmp = NewGameManager.Instance.CalculateResultValue(gameResults[i]);

            if(high < tmp)
            {
                high = tmp;
            }
            if(low > tmp)
            {
                low = tmp;
            }
        }

        output.highestScore = high;

        output.lowestScore = low;

        output.range = high - low;

        return output;
    }

    public void skip()
    {
        Debug.Log("aycaramba");

        killAction = true;
    }

    public class Action
    {
        public int priority;
        public IEnumerator act;

        public Action(int priority, IEnumerator act)
        {
            this.priority = priority;
            this.act = act;
        }
    }

    // 고려해야할 것들
    // 1. 게임별 총 등장 횟수
    // 2. 게임별 성공 횟수
    // 3. 게임별 연속 등장 횟수
    // 4. 최종 결과
    // 5. 마지막 게임과 결과
    // 6. 각각 게임별 결과
    // 7. 총 성공횟수
    // 8. 성공 실패 순서
    // * 성공 대성공은 같은 걸로 취급
    class ResultAnalysis
    {
        public List<GameAnalysis> gaList { get; set; }

        public int finalScore { get; set; }

        public bool finalGameResult { get; set; }

        public List<Game_Result> grList { get; set; }

        public int totalSuccess { get; set; }

        public List<bool> resultList { get; set; }

        public ResultAnalysis()
        {
            gaList = new List<GameAnalysis>();

            finalScore = 0;

            finalGameResult = false;

            grList = new List<Game_Result>();

            totalSuccess = 0;

            resultList = new List<bool>();
        }
    }

    class GameAnalysis //결과 전체에서
    {
        public string gameName { get; set; } // 게임 이름

        public int frequency { get; set; } // 총 등장 수

        public int success { get; set; } // 성공 횟수

        public int chain { get; set; } // 연속 등장 횟수

        public GameAnalysis()
        {
            gameName = "";

            frequency = 0;

            success = 0;

            chain = 0;
        }

        public GameAnalysis(string name, Result result)
        {
            gameName = name;

            frequency = 1;

            if (result != Result.Fail)
            {
                success = 1;
            }
            else
            {
                success = 0;
            }

            chain = 1;
        }
    }

    public class Game_Result // 각 게임에서
    {
        public string gameName { get; set; }

        public bool result { get; set; }

        public Game_Result()
        {
            gameName = "";

            result = false;
        }

        public Game_Result(string gameName, bool result)
        {
            this.gameName = gameName;

            this.result = result;
        }
    }

    class ResultPriority
    {
        public ResultCode result;

        public int priority;

        public ResultPriority(ResultCode result, int priority)
        {
            this.result = result;

            this.priority = priority;
        }
    }

    /////

    class TotalAnalysis
    {
        public int average { get; set; }

        public int highestScore { get; set; }

        public int lowestScore { get; set; }

        public int range { get; set; } //최대값 - 최소값

        public Subject highestSubject { get; set; }

        public Subject lowestSubject { get; set; }

        public TotalAnalysis()
        {
            average = 0;

            highestScore = 0;

            lowestScore = 0;

            range = 0;

            highestSubject = Subject.None;

            lowestSubject = Subject.None;
        }

    }

}