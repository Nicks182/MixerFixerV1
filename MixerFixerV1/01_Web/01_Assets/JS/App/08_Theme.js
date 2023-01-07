

function _App_ThemeShow(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}

function _App_Theme_OnChangeColor(P_Id, P_MessageType, P_Value)
{
    var L_Comm =
    {
        CommType: P_MessageType,
        Data: [
            { Id: "Id", Value: P_Id },
            { Id: "Value", Value: P_Value }
        ]
    }
    _Comm_Send(L_Comm);
}

function _App_Theme_OnChangeColor_MouseWheel(P_Event, P_Id, P_MessageType, P_Value)
{
    _App_Theme_OnChangeColor(P_Id, P_MessageType, P_Value);
}


function _App_Theme_ColorChanged(P_CommObject)
{
    console.log(P_CommObject);
    _App_DataUpdate_Receive(P_CommObject);
}

function _App_ThemeReset(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}