using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public delegate void NextFunc(List<Record> l);
    public delegate void NormalNextFunc();

    public List<Record> resultList;
    private Record tempMyRecord;
    private string tempGameName;
    private int tempMyRanking;
    // Use this for initialization
    void Start()
    {

    }

    public int TempMyRanking {
        get { return tempMyRanking; }
    }

    public void SaveRecord(string game, string name, long score)
    {
        StartCoroutine(SaveToTimeRecord(game, name, score));
    }

    public void SaveRecord(string game, string name, long score, NormalNextFunc nnf)
    {
        tempGameName = game;
        StartCoroutine(SaveToTimeRecord(game, name, score, nnf));
    }

    public int GetMyRank(List<Record> rank, string name, long score)
    {
        int count = rank.Count;
        int result = -1;
        for (int i = 0; i < count; ++i)
        {
            if (rank[i].name == name && rank[i].score == score)
                result = i + 1;
        }
        return result;
    }

    public void GetMyRank(NormalNextFunc nnf)
    {
        StartCoroutine(GetToPHPFindMyRank(tempGameName, nnf));
    }

    private IEnumerator FindMyRank(List<Record> records, NormalNextFunc nnf)
    {
        for (int i = 0; i < records.Count; i++)
        {
            Debug.Log(records[i].name + " , " + records[i].score + " , " + records[i].time);
            if (records[i].IsEqual(tempMyRecord))
            {
                tempMyRanking = i + 1;
                break;
            }
        }

        yield return null;

        nnf();
    }

    IEnumerator SaveToTimeRecord(string game, string name, long score)
    {
        WWW www = new WWW("http://35.221.70.194/time.php");

        yield return www;

        string text = "game_num=" + game + "&name=" + name + "&score=" + score.ToString() + "&date=" + www.text.Substring(0, 12);

        WWW wwwTager = new WWW("http://35.221.70.194/gca/insertrank.php?" + text);
    }

    IEnumerator SaveToTimeRecord(string game, string name, long score, NormalNextFunc nnf)
    {
        WWW www = new WWW("http://35.221.70.194/time.php");

        yield return www;

        string text = "game_num=" + game + "&name=" + name + "&score=" + score.ToString() + "&date=" + www.text.Substring(0, 12);
        tempMyRecord = new Record(name, score, www.text.Substring(2, 10));
        WWW wwwTager = new WWW("http://35.221.70.194/gca/insertrank.php?" + text);

        yield return wwwTager;

        nnf();
    }

    Coroutine tmp;
    public void GetRanking(string game, NextFunc next)
    {
        if (tmp != null)
            StopCoroutine(tmp);
        tmp = StartCoroutine(GetToPHP(game, next));
    }

    IEnumerator GetToPHP(string game, NextFunc next)
    {
        resultList = new List<Record>();

        WWW www = new WWW("http://35.221.70.194/gca/getrank.php?game_num=" + game);

        yield return www;
        if (www.text.Length == 0) yield break;
        string[] result = www.text.Split('%');
        int amount = int.Parse(result[0]);

        for (int i = 0; i < amount; ++i)
        {
            string[] row = result[i + 1].Split(' ');
            resultList.Add(new Record(row[0], int.Parse(row[1]), row[2].Substring(2, 10)));
        }
        next(resultList);
    }

    IEnumerator GetToPHPFindMyRank(string game, NormalNextFunc nnf)
    {
        resultList = new List<Record>();

        WWW www = new WWW("http://35.221.70.194/gca/getrank.php?game_num=" + game);

        yield return www;
        if (www.text.Length == 0) yield break;
        string[] result = www.text.Split('%');
        int amount = int.Parse(result[0]);

        for (int i = 0; i < amount; ++i)
        {
            string[] row = result[i + 1].Split(' ');
            resultList.Add(new Record(row[0], int.Parse(row[1]), row[2].Substring(2, 10)));
        }
        //next(resultList);
        StartCoroutine(FindMyRank(resultList, nnf));
    }
}

public class Record
{
    public string name;
    public long score;
    public string time;

    public Record(string name, long score, string time)
    {
        this.name = name;
        this.score = score;
        this.time = time;
    }

    public bool IsEqual(Record r)
    {
        if(r.name == this.name && r.score == this.score && r.time == this.time)
        {
            return true;
        }
        return false;
    }
}