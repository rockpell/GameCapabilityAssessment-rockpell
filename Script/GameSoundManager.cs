using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NewGameManager))]

public class GameSoundManager : MonoBehaviour {
    [Range(0f, 1f)] public static float bgmValue;
    [Range(0f, 1f)] public static float effectValue;
    [Range(0f, 1f)] public static float uiValue;
}
