using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuiGaugeImageController : GaugeImageController
{
    [SerializeField] private Transform[] startAndEndPos;
    [SerializeField] private SpriteRenderer[] inflags;
    [SerializeField] private GameObject[] outFlags;
    [SerializeField] private Sprite changeFlagImage;
    [SerializeField] private GameObject quiMainObject;
    private float totalMoveDistance;
    private float moveSpeed = 0.25f;
    private float increasePos = 0;
    private bool isPlaySound = true;

    public override IEnumerator ImageActivate(int value)
    {
        //총 점수
        totalMoveDistance = startAndEndPos[1].position.x - startAndEndPos[0].position.x;
        float currentScore = (float)value / 15;
        //도달해야하는 위치
        float moveDistance = totalMoveDistance * currentScore;
        bool[] changFlagImage = new bool[3];
        for (int i = 0; i < changFlagImage.Length; i++)
        {
            changFlagImage[i] = false;
        }
        while (increasePos < 1)
        {
            if (increasePos > 0.993f) increasePos = 1;
            else increasePos = (quiMainObject.transform.localPosition.x - startAndEndPos[0].position.x) / moveDistance;
            quiMainObject.transform.Translate(moveSpeed - moveSpeed * increasePos, 0, 0, Space.Self);
            nowValue = increasePos * value;
            if ((int)nowValue  % 5 == 4)
            {
                if (nowValue > (int)nowValue + 0.5f && !changFlagImage[(int)nowValue / 5])
                {
                    changFlagImage[(int)nowValue / 5] = true;
                    ChangeFlag((int)nowValue / 5);
                }
            }
            yield return null;
        }
        StartCoroutine(GaugeSound(value, 0.001f));
        yield return null;
    }
    private void ChangeFlag(int index)
    {
        int flagIndex = 0;
        flagIndex += index;
        inflags[flagIndex].sprite = changeFlagImage;
        outFlags[flagIndex].SetActive(true);
        //SpecialSound();
    }
}
