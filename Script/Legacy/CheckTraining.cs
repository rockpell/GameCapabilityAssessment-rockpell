using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTraining{
    private int subjectIndex; // 항목 번호
    private int minigameIndex; // 항목 내 미니게임 번호

    public CheckTraining(int subjectIndex, int minigameIndex)
    {
        this.subjectIndex = subjectIndex;
        this.minigameIndex = minigameIndex;
    }

    public int SubjectIndex {
        get { return subjectIndex; }
        set { subjectIndex = value; }
    }
    
    public int MinigameIndex {
        get { return minigameIndex; }
        set { minigameIndex = value; }
    }
}
