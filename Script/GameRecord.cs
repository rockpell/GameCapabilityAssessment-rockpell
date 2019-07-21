using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameRecord {
    public string time; // 내림차순으로 정렬하면 최근 순서순으로 될거임
    public List<GameResult>[] gameResults;
	
    public GameRecord(string time, List<GameResult>[] gameResults)
    {
        this.time = time;
        this.gameResults = gameResults;
    }
}
