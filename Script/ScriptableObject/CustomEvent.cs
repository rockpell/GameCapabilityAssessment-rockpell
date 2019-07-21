using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType { TALK, NONSTOPTALK, ACT, WAIT, WAITCLICK, CLOSEDIALOG, BRANCH, JUMP, LABEL }
// TALK : 클릭해야 다음으로 넘어감(대사)
// NONSTOPTALK : 클릭이 없어도 다음으로 넘어감(대사)
// ACT : 어떠한 행동(함수 이름)
// WAIT : 기다리는 시간(float 시간)
// WAITCLICK : 클릭을 기다림
// CLOSEDIALOG : 대화창 닫기
// BRANCH : 선택지 이벤트(두개의 LABEL을 입력 ex) yes1,no1)
// JUMP : 이벤트 다음 실행 순서를 등록된 LABEL로 변경(임의의 LABEL을 입력)
// LABEL : BRANCH나 JUMP의 순서 변경 목적지

[System.Serializable]
public class CustomAction
{
    public ActionType aType;
    public string value;
}

[CreateAssetMenu (fileName = "NewEvent", menuName = "CustomEvent")]
public class CustomEvent : ScriptableObject
{
    public bool isImportant;
    public CustomAction[] actionList;
}
