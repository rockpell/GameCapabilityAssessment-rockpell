using System;

[Serializable]
public class IntroTrigger{
    public bool isNeedIntro;
    public bool isNeedMenu;
    public bool isNeedSelectSubject;
    public bool isNeedDetailSubejct;
    public bool isNeedSubjectResultAfter;
    public bool isNeedEvalationEnd;

    public IntroTrigger()
    {
        this.isNeedIntro = true;
        this.isNeedMenu = true;
        this.isNeedSelectSubject = true;
        this.isNeedDetailSubejct = true;
        this.isNeedSubjectResultAfter = true;
        this.isNeedEvalationEnd = true;
    }
}
