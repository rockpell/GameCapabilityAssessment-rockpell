public enum Subject { None, Aiming, Concentration, Quickness, RhythmicSense, Thinking  };
// 조준 : 빨강, 집중 : 파랑, 순발 : 노랑, 리듬 : 보라, 사고 : 초록
public enum Result { Fail = -1, Successful = 1, BigSuccessful = 2 };
public enum Face { Idle, LookRight, LookLeft };
/*보류
//모든게임에 코드 부여
//1X : 조준력, 2X : 집중력, 3X : 순발력, 4X : 리듬감, 5X : 사고력
public enum GameCode
{
    NULL,
    AIM_SHM = 10, AIM_WHF,
    CON_COF = 20, CON_COK, CON_BOX,
    QUK_CHI = 30, QUK_NIN,
    RHY_COK = 40,
    TNK_MGK = 50, TNK_SNT, TNK_CVN
}
*/
public class RoutineStream
{
	public object flag;
	public object result;
	public RoutineStream()
	{
		flag = false;
	}
}