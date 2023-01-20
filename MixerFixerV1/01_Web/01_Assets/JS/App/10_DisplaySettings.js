
function _App_DisplaySettingsShow(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}

function _App_DisplaySettings_ManagedChange(P_MessageType, P_DisplayId, P_DisplayName)
{
    var L_Comm =
    {
        CommType: P_MessageType,
        Data: [{ Id: P_DisplayId, Value: P_DisplayName }]
    }
    _Comm_Send(L_Comm);
}

function _App_DisplaySettings_PowerChange(P_MessageType, P_DisplayId, P_DisplayName)
{
    var L_Comm =
    {
        CommType: P_MessageType,
        Data: [{ Id: P_DisplayId, Value: P_DisplayName }]
    }
    _Comm_Send(L_Comm);
}

