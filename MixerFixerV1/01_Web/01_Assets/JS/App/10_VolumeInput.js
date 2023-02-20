

function _App_VolumeInput_Save(P_MessageType, P_ObjectId, P_Txt_InputId)
{
    var L_Comm =
    {
        CommType: P_MessageType,
        Data: [{ Id: P_ObjectId, Value: $("#" + P_Txt_InputId).val() }]
    }
    _Comm_Send(L_Comm);
}

function _App_VolumeInput_OnKeyDown(P_Event, P_MessageType, P_ObjectId, P_Txt_InputId)
{
    if (P_Event.keyCode == 13)
    {
        _App_VolumeInput_Save(P_MessageType, P_ObjectId, P_Txt_InputId);
        P_Event.preventDefault();
        return;
    }
    //if (48 > P_Event.which || P_Event.which > 57)
    //{
    //    if (P_Event.key.length === 1)
    //    {
    //        P_Event.preventDefault();
    //    }
    //}
}

