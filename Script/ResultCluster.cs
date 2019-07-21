using System.Collections.Generic;

public partial class YukgackManager
{
    private void chooseAction(ResultCode result, FinalResultCode finalResult)
    {
        switch (result)
        {
            case ResultCode.NULL:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_1:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_2:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_3:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_4:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_5:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_6:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_7:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_8:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_9:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_10:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_11:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_12:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.GEN_36:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_1:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_2:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_3:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_4:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_5:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_6:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_7:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_8:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_9:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_10:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_11:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_12:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_36:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_37:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.AIM_38:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_1:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_2:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_3:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_4:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_5:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_6:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_7:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_8:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_9:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_10:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.CON_11:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_1:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_2:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_3:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_4:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_5:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_6:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_7:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_8:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_9:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_10:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_11:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.QUK_36:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_1:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_2:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_3:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_4:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_5:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_6:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_7:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_8:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_9:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_10:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.RHY_11:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_1:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_2:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_3:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_4:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_5:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_6:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_7:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_8:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_9:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_10:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_11:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_12:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            case ResultCode.TNK_36:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    //actionList.Add(new Action(1, NonStopTalk(resultCodeToString(result))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }

            default:
                {
                    actionList.Add(new Action(0, Move(endPos)));
                    actionList.Add(new Action(1, NonStopTalk(resultCodeToString(ResultCode.NULL))));
                    actionList.Add(new Action(2, NonStopTalk(finalResultCodeToString(finalResult))));
                    actionList.Add(new Action(999, endDialogue()));
                    break;
                }
        }
    }

    private void chooseAction(List<TotalResultCode> input)
    {
        actionList.Add(new Action(-1, Move(endPos)));

        for(int i = 0; input.Count > i; i++)
        {
            actionList.Add(new Action(i,NonStopTalk(totalResultCodeToString(input[i]))));
        }

        actionList.Add(new Action(999, endDialogue()));

    }

    private string resultCodeToString(ResultCode input)
    {
        switch (input)
        {
            case ResultCode.NULL:
                {
                    return "음…뭐라 할말이 없군요.";
                }

            case ResultCode.GEN_1:
                {
                    return "많이 피곤하신가봐요. 빨리 주무시러 가셔야겠어요.";
                }

            case ResultCode.GEN_2:
                {
                    return "많이 피곤하신가봐요. 빨리 주무시러 가셔야겠어요.";
                }

            case ResultCode.GEN_3:
                {
                    return "많이 피곤하신가봐요. 빨리 주무시러 가셔야겠어요.";
                }

            case ResultCode.GEN_4:
                {
                    return "못해놓고 시스템탓 하는거 아니죠?";
                }

            case ResultCode.GEN_5:
                {
                    return "못해놓고 시스템탓 하는거 아니죠?";
                }

            case ResultCode.GEN_6:
                {
                    return "못해놓고 시스템탓 하는거 아니죠?";
                }

            case ResultCode.GEN_7:
                {
                    return "생각보다 꽤 하시네요.";
                }

            case ResultCode.GEN_8:
                {
                    return "당신에게는 단 하나의 부족함을 채워줄 오즈의 마법사 같은 사람이 필요하군요.";
                }

            case ResultCode.GEN_9:
                {
                    return "인정하긴 싫지만 완벽하군요.";
                }

            case ResultCode.GEN_10:
                {
                    return "범용 성공 멘트 10";
                }

            case ResultCode.GEN_11:
                {
                    return "범용 성공 멘트 11";
                }

            case ResultCode.GEN_12:
                {
                    return "3번 연속으로 쓸줄은 모르셨죠? 저도 3번 연속으로 당할줄은 몰랐어요. 3번 연속으로 쓸줄은 모르셨죠? 저도 3번 연속으로 당할줄은 몰랐어요.";
                }

            case ResultCode.GEN_36:
                {
                    return "뭐… 세상 모든 일이 바텐더의 나비넥타이 같이 단정한 끝이 있는건 아닙니다.";
                }

            case ResultCode.GEN_37:
                {
                    return "마지막 한 순간이 결과를 결정하는 법!";
                }

            case ResultCode.AIM_1:
                {
                    return "* 조준력 성공수 멘트 1";
                }

            case ResultCode.AIM_2:
                {
                    return "* 조준력 성공수 멘트 2";
                }

            case ResultCode.AIM_3:
                {
                    return "* 조준력 성공수 멘트 3";
                }

            case ResultCode.AIM_4:
                {
                    return "* 조준력 성공수 멘트 4";
                }

            case ResultCode.AIM_5:
                {
                    return "* 조준력 성공수 멘트 5";
                }

            case ResultCode.AIM_6:
                {
                    return "* 조준력 성공수 멘트 6";
                }

            case ResultCode.AIM_7:
                {
                    return "* 조준력 성공수 멘트 7";
                }

            case ResultCode.AIM_8:
                {
                    return "* 조준력 성공수 멘트 8";
                }

            case ResultCode.AIM_9:
                {
                    return "* 조준력 성공수 멘트 9";
                }

            case ResultCode.AIM_10:
                {
                    return "* 조준력 성공수 멘트 10";
                }

            case ResultCode.AIM_11:
                {
                    return "* 조준력 성공수 멘트 11";
                }


            case ResultCode.CON_1:
                {
                    return "* 집중력 성공수 멘트 1";
                }

            case ResultCode.CON_2:
                {
                    return "* 집중력 성공수 멘트 2";
                }

            case ResultCode.CON_3:
                {
                    return "* 집중력 성공수 멘트 3";
                }

            case ResultCode.CON_4:
                {
                    return "* 집중력 성공수 멘트 4";
                }

            case ResultCode.CON_5:
                {
                    return "* 집중력 성공수 멘트 5";
                }

            case ResultCode.CON_6:
                {
                    return "* 집중력 성공수 멘트 6";
                }

            case ResultCode.CON_7:
                {
                    return "* 집중력 성공수 멘트 7";
                }

            case ResultCode.CON_8:
                {
                    return "* 집중력 성공수 멘트 8";
                }

            case ResultCode.CON_9:
                {
                    return "* 집중력 성공수 멘트 9";
                }

            case ResultCode.CON_10:
                {
                    return "* 집중력 성공수 멘트 10";
                }

            case ResultCode.CON_11:
                {
                    return "* 집중력 성공수 멘트 11";
                }

            case ResultCode.CON_12:
                {
                    return "레시피중에 케이크도 있는거 아시나요? 사실 거짓말이에요.";             
                }

            case ResultCode.CON_13:
                {
                    return "빠밤바~(록키 전주곡)";
                }

            case ResultCode.CON_14:
                {
                    return "끝까지 집중하긴 어려운 법이죠.";
                }

            case ResultCode.QUK_1:
                {
                    return "* 순발력 성공수 멘트 1";
                }

            case ResultCode.QUK_2:
                {
                    return "* 순발력 성공수 멘트 2";
                }

            case ResultCode.QUK_3:
                {
                    return "* 순발력 성공수 멘트 3";
                }

            case ResultCode.QUK_4:
                {
                    return "* 순발력 성공수 멘트 4";
                }

            case ResultCode.QUK_5:
                {
                    return "* 순발력 성공수 멘트 5";
                }

            case ResultCode.QUK_6:
                {
                    return "* 순발력 성공수 멘트 6";
                }

            case ResultCode.QUK_7:
                {
                    return "* 순발력 성공수 멘트 7";
                }

            case ResultCode.QUK_8:
                {
                    return "* 순발력 성공수 멘트 8";
                }

            case ResultCode.QUK_9:
                {
                    return "* 순발력 성공수 멘트 9";
                }

            case ResultCode.QUK_10:
                {
                    return "* 순발력 성공수 멘트 10";
                }

            case ResultCode.QUK_11:
                {
                    return "* 순발력 성공수 멘트 11";
                }

            case ResultCode.QUK_36:
                {
                    return "슉! 슉! 슉!";
                }

            case ResultCode.RHY_1:
                {
                    return "엄마 방금 점수를 죽였어요. 머리에 버튼을 대고 눌러버렸어요.";
                }

            case ResultCode.RHY_2:
                {
                    return "* 리듬감 성공수 멘트 2";
                }

            case ResultCode.RHY_3:
                {
                    return "* 리듬감 성공수 멘트 3";
                }

            case ResultCode.RHY_4:
                {
                    return "* 리듬감 성공수 멘트 4";
                }

            case ResultCode.RHY_5:
                {
                    return "* 리듬감 성공수 멘트 5";
                }

            case ResultCode.RHY_6:
                {
                    return "* 리듬감 성공수 멘트 6";
                }

            case ResultCode.RHY_7:
                {
                    return "* 리듬감 성공수 멘트 7";
                }

            case ResultCode.RHY_8:
                {
                    return "* 리듬감 성공수 멘트 8";
                }

            case ResultCode.RHY_9:
                {
                    return "* 리듬감 성공수 멘트 9";
                }

            case ResultCode.RHY_10:
                {
                    return "* 리듬감 성공수 멘트 10";
                }

            case ResultCode.RHY_11:
                {
                    return "* 리듬감 성공수 멘트 11";
                }

            case ResultCode.TNK_1:
                {
                    return "CONSOLE: ERROR! IMPOSSIBLE RESULT!";
                }

            case ResultCode.TNK_2:
                {
                    return "* 사고력 성공수 멘트 2";
                }

            case ResultCode.TNK_3:
                {
                    return "고장난 시계도 하루에 2번은 맞는다죠. 당신하고 비슷하네요.";
                }

            case ResultCode.TNK_4:
                {
                    return "* 사고력 성공수 멘트 4";
                }

            case ResultCode.TNK_5:
                {
                    return "* 사고력 성공수 멘트 5";
                }

            case ResultCode.TNK_6:
                {
                    return "* 사고력 성공수 멘트 6";
                }

            case ResultCode.TNK_7:
                {
                    return "* 사고력 성공수 멘트 7";
                }

            case ResultCode.TNK_8:
                {
                    return "* 사고력 성공수 멘트 8";
                }

            case ResultCode.TNK_9:
                {
                    return "오늘이 며칠이죠? 2월 30일…?";
                }

            case ResultCode.TNK_10:
                {
                    return "* 사고력 성공수 멘트 10";
                }

            case ResultCode.TNK_11:
                {
                    return "* 사고력 성공수 멘트 11";
                }

            case ResultCode.TNK_12:
                {
                    return "실력이 이렇게 일관적인 것도 참 신기하네요.";
                }

            case ResultCode.TNK_36:
                {
                    return "잊으세요. 반추는 우울함만 증폭 시킬 뿐입니다.";
                }

            default:
                {
                    return "결과를 표출합니다.";
                }
        }
    }

    private string finalResultCodeToString(FinalResultCode input)
    {
        switch (input)
        {
            case FinalResultCode.NULL:
                {
                    return "결과를 출력합니다.";
                }

            case FinalResultCode.GEN_F1: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.GEN_F2: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.GEN_F3: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.GEN_F4: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.GEN_F5: { return "평균 보다 약간 모자라군요. 좀 더 열심히 해보세요."; }
            case FinalResultCode.GEN_F6: { return "평균 보다 약간 모자라군요. 좀 더 열심히 해보세요."; }
            case FinalResultCode.GEN_F7: { return "테스트 결과가 나쁘지 않군요. 게임에 대한 이해를 하고 계시네요."; }
            case FinalResultCode.GEN_F8: { return "테스트 결과가 나쁘지 않군요. 게임에 대한 이해를 하고 계시네요."; }
            case FinalResultCode.GEN_F9: { return "테스트 결과가 나쁘지 않군요. 게임에 대한 이해를 하고 계시네요."; }
            case FinalResultCode.GEN_F10: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.GEN_F11: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.GEN_F12: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.GEN_F13: { return "테스트 결과가 상당히 높으시네요. 하지만, 완벽하다고 말해 드리기에는 조금 모자라군요."; }
            case FinalResultCode.GEN_F14: { return "테스트 결과가 상당히 높으시네요. 그 어떤 사람 보다도 잘 수행했습니다. 물론 예산 문제로 입증되지는 않았습니다."; }
            case FinalResultCode.GEN_F15: { return "매우 훌륭한 결과 입니다. 여기에 재능이 있는 모양이군요."; }
            case FinalResultCode.AIM_F1: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.AIM_F2: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.AIM_F3: { return "테스트 결과가 조금 낮으시네요. 스크린에 묻은 이물질은 입력이 부정확하게 할 수 있습니다. 스크린을 닦고 다시 해보세요. "; }
            case FinalResultCode.AIM_F4: { return "테스트 결과가 조금 낮으시네요. 스크린에 묻은 이물질은 입력이 부정확하게 할 수 있습니다. 스크린을 닦고 다시 해보세요. "; }
            case FinalResultCode.AIM_F5: { return "평균 보다 약간 모자라군요. 가만히 있는 사과를 맞출 수 있는 실력입니다."; }
            case FinalResultCode.AIM_F6: { return "평균 보다 약간 모자라군요. 가만히 있는 사과를 맞출 수 있는 실력입니다."; }
            case FinalResultCode.AIM_F7: { return "테스트 결과가 나쁘지 않군요. 능력있는 사수입니다."; }
            case FinalResultCode.AIM_F8: { return "테스트 결과가 나쁘지 않군요. 능력있는 사수입니다."; }
            case FinalResultCode.AIM_F9: { return "테스트 결과가 나쁘지 않군요. 움직이는 사과를 맞출 수 있는 실력입니다."; }
            case FinalResultCode.AIM_F10: { return "테스트 결과가 괜찮군요. 비유하자면 \"바늘에 실을 한번에 넣을 정도\"의 조준력입니다."; }
            case FinalResultCode.AIM_F11: { return "테스트 결과가 괜찮군요. 비유하자면 \"바늘에 실을 한번에 넣을 정도\"의 조준력입니다."; }
            case FinalResultCode.AIM_F12: { return "테스트 결과가 괜찮군요. 비유하자면 \"바늘에 실을 한번에 넣을 정도\"의 조준력입니다."; }
            case FinalResultCode.AIM_F13: { return "테스트 결과가 상당히 높으시네요. 하지만, 완벽하다고 말해 드리기에는 조금 모자라군요."; }
            case FinalResultCode.AIM_F14: { return "테스트 결과가 상당히 높으시네요. 그 어떤 사람 보다도 잘 수행했습니다. 물론 예산 문제로 입증되지는 않았습니다."; }
            case FinalResultCode.AIM_F15: { return "매우 훌륭한 결과 입니다. 여기에 재능이 있는 모양이군요."; }
            case FinalResultCode.CON_F1: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.CON_F2: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.CON_F3: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.CON_F4: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.CON_F5: { return "평균 보다 약간 모자라군요. 좀 더 열심히 해보세요."; }
            case FinalResultCode.CON_F6: { return "평균 보다 약간 모자라군요. 좀 더 열심히 해보세요."; }
            case FinalResultCode.CON_F7: { return "테스트 결과가 나쁘지 않군요. 게임과 과자를 동시에 즐길 수 있는 집중력입니다."; }
            case FinalResultCode.CON_F8: { return "테스트 결과가 나쁘지 않군요. 게임과 과자를 동시에 즐길 수 있는 집중력입니다."; }
            case FinalResultCode.CON_F9: { return "테스트 결과가 나쁘지 않군요. 게임과 과자, 음료까지 동시에 즐길 수 있는 집중력입니다."; }
            case FinalResultCode.CON_F10: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.CON_F11: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.CON_F12: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.CON_F13: { return "테스트 결과가 상당히 높으시네요. 하지만, 완벽하다고 말해 드리기에는 조금 모자라군요."; }
            case FinalResultCode.CON_F14: { return "테스트 결과가 상당히 높으시네요. 그 어떤 사람 보다도 잘 수행했습니다. 물론 예산 문제로 입증되지는 않았습니다."; }
            case FinalResultCode.CON_F15: { return "매우 훌륭한 결과 입니다. 여기에 재능이 있는 모양이군요."; }
            case FinalResultCode.QUK_F1: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.QUK_F2: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.QUK_F3: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.QUK_F4: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.QUK_F5: { return "평균 보다 약간 모자라군요. 손에 뭔가 무거운 걸 차고있다면 벗어두시길 추천합니다."; }
            case FinalResultCode.QUK_F6: { return "평균 보다 약간 모자라군요. 손에 뭔가 무거운 걸 차고있다면 벗어두시길 추천합니다."; }
            case FinalResultCode.QUK_F7: { return "테스트 결과가 나쁘지 않군요. 모기를 손으로 잡을 수 있을 정도입니다."; }
            case FinalResultCode.QUK_F8: { return "테스트 결과가 나쁘지 않군요. 모기를 손으로 잡을 수 있을 정도입니다."; }
            case FinalResultCode.QUK_F9: { return "테스트 결과가 나쁘지 않군요. 날고 있는 모기를 손으로 잡을 수 있을 정도입니다."; }
            case FinalResultCode.QUK_F10: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.QUK_F11: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.QUK_F12: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.QUK_F13: { return "테스트 결과가 상당히 높으시네요. 하지만, 완벽하다고 말해 드리기에는 조금 모자라군요."; }
            case FinalResultCode.QUK_F14: { return "테스트 결과가 상당히 높으시네요. 그 어떤 사람 보다도 잘 수행했습니다. 물론 예산 문제로 입증되지는 않았습니다."; }
            case FinalResultCode.QUK_F15: { return "매우 훌륭한 결과 입니다. 여기에 재능이 있는 모양이군요."; }
            case FinalResultCode.RHY_F1: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.RHY_F2: { return "본 평가는 음향장비를 필요로 합니다. 음향장비가 잘 작동하는지 점검이 필요합니다."; }
            case FinalResultCode.RHY_F3: { return "본 평가는 음향장비를 필요로 합니다. 음향장비가 잘 작동하는지 점검이 필요합니다."; }
            case FinalResultCode.RHY_F4: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.RHY_F5: { return "평균 보다 약간 모자라군요. 좀 더 열심히 해보세요."; }
            case FinalResultCode.RHY_F6: { return "평균 보다 약간 모자라군요. 좀 더 열심히 해보세요."; }
            case FinalResultCode.RHY_F7: { return "테스트 결과가 나쁘지 않군요. 노래방에 가시면 81점 정도 받으실거에요."; }
            case FinalResultCode.RHY_F8: { return "테스트 결과가 나쁘지 않군요. 노래방에 가시면 86점 정도 받으실거에요."; }
            case FinalResultCode.RHY_F9: { return "테스트 결과가 나쁘지 않군요. 노래방에 가시면 91점 정도 받으실거에요."; }
            case FinalResultCode.RHY_F10: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.RHY_F11: { return "테스트 결과가 괜찮군요. 메트로놈도 필요없을 정도의 리듬감을 가지셨군요."; }
            case FinalResultCode.RHY_F12: { return "테스트 결과가 괜찮군요. 메트로놈도 필요없을 정도의 리듬감을 가지셨군요."; }
            case FinalResultCode.RHY_F13: { return "테스트 결과가 상당히 높으시네요. 하지만, 완벽하다고 말해 드리기에는 조금 모자라군요."; }
            case FinalResultCode.RHY_F14: { return "테스트 결과가 상당히 높으시네요. 그 어떤 사람 보다도 잘 수행했습니다. 물론 예산 문제로 입증되지는 않았습니다."; }
            case FinalResultCode.RHY_F15: { return "매우 훌륭한 결과 입니다. 여기에 재능이 있는 모양이군요."; }
            case FinalResultCode.TNK_F1: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.TNK_F2: { return "테스트 결과가 매우 낮군요. 손이 있으면 위로라도 해드렸을텐데 아쉽네요."; }
            case FinalResultCode.TNK_F3: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.TNK_F4: { return "테스트 결과가 조금 낮으시네요. 그렇다고 너무 기죽지마세요. 잘하는 사람이 있으면 못하는 사람도 있는거죠."; }
            case FinalResultCode.TNK_F5: { return "평균 보다 약간 모자라군요. CPU를 더 좋은 모델로 업그레이드 하면 도움이 될겁니다. 네? 불가능하다구요?"; }
            case FinalResultCode.TNK_F6: { return "평균 보다 약간 모자라군요. CPU를 더 좋은 모델로 업그레이드 하면 도움이 될겁니다. 네? 불가능하다구요?"; }
            case FinalResultCode.TNK_F7: { return "테스트 결과가 나쁘지 않군요. 사고력 평가의 평균에 뒤쳐지지 않는 결과입니다."; }
            case FinalResultCode.TNK_F8: { return "테스트 결과가 나쁘지 않군요. 사고력 평가의 평균에 뒤쳐지지 않는 결과입니다."; }
            case FinalResultCode.TNK_F9: { return "테스트 결과가 나쁘지 않군요. 사고력 평가의 평균에 뒤쳐지지 않는 결과입니다."; }
            case FinalResultCode.TNK_F10: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.TNK_F11: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.TNK_F12: { return "호오... 테스트 결과가 괜찮군요. 하지만 갈길이 멀답니다."; }
            case FinalResultCode.TNK_F13: { return "테스트 결과가 상당히 높으시네요. 하지만, 완벽하다고 말해 드리기에는 조금 모자라군요."; }
            case FinalResultCode.TNK_F14: { return "테스트 결과가 상당히 높으시네요. 그 어떤 사람 보다도 잘 수행했습니다. 물론 예산 문제로 입증되지는 않았습니다."; }
            case FinalResultCode.TNK_F15: { return "매우 훌륭한 결과 입니다. 여기에 재능이 있는 모양이군요."; }

            default:
                {
                    return "결과를 출력합니다.";
                }
        }
    }

    private string totalResultCodeToString(TotalResultCode input)
    {
        switch (input)
        {
            case TotalResultCode.NULL:
                {
                    return "최종결과를 출력합니다.";
                }

            case TotalResultCode.TOT_A1:
                {
                    return "평가가 부정확 할 수 있습니다. 다시 한번 해보세요.";
                }

            case TotalResultCode.TOT_A2:
                {
                    return "평균 적으로 보통보다 조금 낮습니다.";
                }

            case TotalResultCode.TOT_A3:
                {
                    return "평균적인 실력을 가지고 있습니다.";
                }

            case TotalResultCode.TOT_A4:
                {
                    return "매우 뛰어난 실력을 가지고 있습니다.";
                }

            case TotalResultCode.TOT_B1:
                {
                    return "모든 능력이 고르게 분포 하고있습니다.";
                }

            case TotalResultCode.TOT_B2:
                {
                    return "능력간의 편차가 조금 있는 편이 군요.";
                }

            case TotalResultCode.TOT_B3:
                {
                    return "능력간의 편차가 심한 편입니다. 부족한 부분을 더 연습해 보세요.";
                }

            case TotalResultCode.TOT_C1:
                {
                    return "조준력이 상당히 좋군요.";
                }

            case TotalResultCode.TOT_C2:
                {
                    return "집중을 잘하시는 편이군요. ";
                }

            case TotalResultCode.TOT_C3:
                {
                    return "손가락이 상당히 빠르시군요.";
                }

            case TotalResultCode.TOT_C4:
                {
                    return "안정적인 박자감이 돋보입니다.";
                }

            case TotalResultCode.TOT_C5:
                {
                    return "중앙처리장치가 꽤 괜찮군요.";
                }

            case TotalResultCode.TOT_D1:
                {
                    return "조준력은 FPS 봇전 같은 연습을 하면 좋아질 수 있습니다.";
                }

            case TotalResultCode.TOT_D2:
                {
                    return "집중력을 기르는데는 독서가 효과적입니다.";
                }

            case TotalResultCode.TOT_D3:
                {
                    return "날아다니는 곤충을 잡아보세요. 순발력을 기를 수 있습니다.";
                }

            case TotalResultCode.TOT_D4:
                {
                    return "리듬감을 기르기 위해 악기를 다뤄보는 것도 좋습니다.";
                }

            case TotalResultCode.TOT_D5:
                {
                    return "퍼즐게임을 여러가지 해보세요. 사고력이 올라갑니다.";
                }

            default:
                {
                    return "최종결과를 출력합니다.";
                }
        }
    }

    private int priorityCheck(ResultCode input)
    {
        //특수한 경우 먼저 따짐
        if (input == ResultCode.NULL)
        {
            return 999;
        }

        int temp = ((int)input) % 100;

        if (1 <= temp && temp <= 11)
        {
            return 5;
        }
        else if (12 <= temp && temp <= 25)
        {
            return 1;
        }
        else if (26 <= temp && temp <= 35)
        {
            return 2;
        }
        else if (36 <= temp && temp <= 50)
        {
            return 3;
        }
        else if (51 <= temp && temp <= 65)
        {
            return 4;
        }
        else
        {
            return 999;
        }
    }

    //결과에 코드 부여
    //0XX: 무관, 1XX : 조준력, 2XX : 집중력, 3XX : 순발력, 4XX : 리듬감, 5XX : 사고력
    private enum ResultCode
    {
        NULL,
        GEN_1 = 001, GEN_2, GEN_3, GEN_4, GEN_5, GEN_6, GEN_7, GEN_8, GEN_9, GEN_10, GEN_11, GEN_12, GEN_13, GEN_14, GEN_15, GEN_16, GEN_17, GEN_18, GEN_19, GEN_20, GEN_21, GEN_22, GEN_23, GEN_24, GEN_25, GEN_26, GEN_27, GEN_28, GEN_29, GEN_30, GEN_31, GEN_32, GEN_33, GEN_34, GEN_35, GEN_36, GEN_37, GEN_38, GEN_39, GEN_40, GEN_41, GEN_42, GEN_43, GEN_44, GEN_45, GEN_46, GEN_47, GEN_48, GEN_49, GEN_50, GEN_51, GEN_52, GEN_53, GEN_54, GEN_55, GEN_56, GEN_57, GEN_58, GEN_59, GEN_60, GEN_61, GEN_62, GEN_63, GEN_64, GEN_65,
        AIM_1 = 101, AIM_2, AIM_3, AIM_4, AIM_5, AIM_6, AIM_7, AIM_8, AIM_9, AIM_10, AIM_11, AIM_12, AIM_13, AIM_14, AIM_15, AIM_16, AIM_17, AIM_18, AIM_19, AIM_20, AIM_21, AIM_22, AIM_23, AIM_24, AIM_25, AIM_26, AIM_27, AIM_28, AIM_29, AIM_30, AIM_31, AIM_32, AIM_33, AIM_34, AIM_35, AIM_36, AIM_37, AIM_38, AIM_39, AIM_40, AIM_41, AIM_42, AIM_43, AIM_44, AIM_45, AIM_46, AIM_47, AIM_48, AIM_49, AIM_50, AIM_51, AIM_52, AIM_53, AIM_54, AIM_55, AIM_56, AIM_57, AIM_58, AIM_59, AIM_60, AIM_61, AIM_62, AIM_63, AIM_64, AIM_65,
        CON_1 = 201, CON_2, CON_3, CON_4, CON_5, CON_6, CON_7, CON_8, CON_9, CON_10, CON_11, CON_12, CON_13, CON_14, CON_15, CON_16, CON_17, CON_18, CON_19, CON_20, CON_21, CON_22, CON_23, CON_24, CON_25, CON_26, CON_27, CON_28, CON_29, CON_30, CON_31, CON_32, CON_33, CON_34, CON_35, CON_36, CON_37, CON_38, CON_39, CON_40, CON_41, CON_42, CON_43, CON_44, CON_45, CON_46, CON_47, CON_48, CON_49, CON_50, CON_51, CON_52, CON_53, CON_54, CON_55, CON_56, CON_57, CON_58, CON_59, CON_60, CON_61, CON_62, CON_63, CON_64, CON_65,
        QUK_1 = 301, QUK_2, QUK_3, QUK_4, QUK_5, QUK_6, QUK_7, QUK_8, QUK_9, QUK_10, QUK_11, QUK_12, QUK_13, QUK_14, QUK_15, QUK_16, QUK_17, QUK_18, QUK_19, QUK_20, QUK_21, QUK_22, QUK_23, QUK_24, QUK_25, QUK_26, QUK_27, QUK_28, QUK_29, QUK_30, QUK_31, QUK_32, QUK_33, QUK_34, QUK_35, QUK_36, QUK_37, QUK_38, QUK_39, QUK_40, QUK_41, QUK_42, QUK_43, QUK_44, QUK_45, QUK_46, QUK_47, QUK_48, QUK_49, QUK_50, QUK_51, QUK_52, QUK_53, QUK_54, QUK_55, QUK_56, QUK_57, QUK_58, QUK_59, QUK_60, QUK_61, QUK_62, QUK_63, QUK_64, QUK_65,
        RHY_1 = 401, RHY_2, RHY_3, RHY_4, RHY_5, RHY_6, RHY_7, RHY_8, RHY_9, RHY_10, RHY_11, RHY_12, RHY_13, RHY_14, RHY_15, RHY_16, RHY_17, RHY_18, RHY_19, RHY_20, RHY_21, RHY_22, RHY_23, RHY_24, RHY_25, RHY_26, RHY_27, RHY_28, RHY_29, RHY_30, RHY_31, RHY_32, RHY_33, RHY_34, RHY_35, RHY_36, RHY_37, RHY_38, RHY_39, RHY_40, RHY_41, RHY_42, RHY_43, RHY_44, RHY_45, RHY_46, RHY_47, RHY_48, RHY_49, RHY_50, RHY_51, RHY_52, RHY_53, RHY_54, RHY_55, RHY_56, RHY_57, RHY_58, RHY_59, RHY_60, RHY_61, RHY_62, RHY_63, RHY_64, RHY_65,
        TNK_1 = 501, TNK_2, TNK_3, TNK_4, TNK_5, TNK_6, TNK_7, TNK_8, TNK_9, TNK_10, TNK_11, TNK_12, TNK_13, TNK_14, TNK_15, TNK_16, TNK_17, TNK_18, TNK_19, TNK_20, TNK_21, TNK_22, TNK_23, TNK_24, TNK_25, TNK_26, TNK_27, TNK_28, TNK_29, TNK_30, TNK_31, TNK_32, TNK_33, TNK_34, TNK_35, TNK_36, TNK_37, TNK_38, TNK_39, TNK_40, TNK_41, TNK_42, TNK_43, TNK_44, TNK_45, TNK_46, TNK_47, TNK_48, TNK_49, TNK_50, TNK_51, TNK_52, TNK_53, TNK_54, TNK_55, TNK_56, TNK_57, TNK_58, TNK_59, TNK_60, TNK_61, TNK_62, TNK_63, TNK_64, TNK_65
    }

    //각 능력 최종결과를 나타내는 코드
    private enum FinalResultCode
    {
        NULL,
        GEN_F1 = 001, GEN_F2, GEN_F3, GEN_F4, GEN_F5, GEN_F6, GEN_F7, GEN_F8, GEN_F9, GEN_F10, GEN_F11, GEN_F12, GEN_F13, GEN_F14, GEN_F15,
        AIM_F1 = 101, AIM_F2, AIM_F3, AIM_F4, AIM_F5, AIM_F6, AIM_F7, AIM_F8, AIM_F9, AIM_F10, AIM_F11, AIM_F12, AIM_F13, AIM_F14, AIM_F15,
        CON_F1 = 201, CON_F2, CON_F3, CON_F4, CON_F5, CON_F6, CON_F7, CON_F8, CON_F9, CON_F10, CON_F11, CON_F12, CON_F13, CON_F14, CON_F15,
        QUK_F1 = 301, QUK_F2, QUK_F3, QUK_F4, QUK_F5, QUK_F6, QUK_F7, QUK_F8, QUK_F9, QUK_F10, QUK_F11, QUK_F12, QUK_F13, QUK_F14, QUK_F15,
        RHY_F1 = 401, RHY_F2, RHY_F3, RHY_F4, RHY_F5, RHY_F6, RHY_F7, RHY_F8, RHY_F9, RHY_F10, RHY_F11, RHY_F12, RHY_F13, RHY_F14, RHY_F15,
        TNK_F1 = 501, TNK_F2, TNK_F3, TNK_F4, TNK_F5, TNK_F6, TNK_F7, TNK_F8, TNK_F9, TNK_F10, TNK_F11, TNK_F12, TNK_F13, TNK_F14, TNK_F15
    }

    //종합 결과를 나타내는 코드
    private enum TotalResultCode
    {
        NULL,
        TOT_A1, TOT_A2, TOT_A3, TOT_A4,
        TOT_B1, TOT_B2, TOT_B3,
        TOT_C1, TOT_C2, TOT_C3, TOT_C4, TOT_C5,
        TOT_D1, TOT_D2, TOT_D3, TOT_D4, TOT_D5
    }
}
