using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleClickButton : MonoBehaviour {

    [SerializeField] int index;
    private EvaluationManager evaluationManager;
	// Use this for initialization
	void Start () {
        evaluationManager = GameObject.Find("EvaluationManager").GetComponent<EvaluationManager>();
        StartCoroutine(WaitEvaluationSettingEnd());
    }
	
    private IEnumerator WaitEvaluationSettingEnd()
    {
        yield return new WaitUntil(() => evaluationManager.IsSettingProcedure);

        if (GameManager.Instance.EvaluationSubject != null && GameManager.Instance.EvaluationSubject.Contains(index))
        {
            if(GameManager.Instance.EvaluationSubject.Count - 1 == GameManager.Instance.EvaluationSubject.IndexOf(index))
            {
                yield return new WaitUntil(() => evaluationManager.IsMovingProcedure);
                ColorChangeDark();
            }
            else
            {
                ColorChangeDark();
            }
            
        }
    }

    public void ClickCapsule()
    {
        evaluationManager.ClickCapsule(index);
    }

    private void ColorChangeDark()
    {
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.6f);
    }
}
