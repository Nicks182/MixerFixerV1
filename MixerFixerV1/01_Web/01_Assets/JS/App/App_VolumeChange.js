

function _App_VolumeChange(P_UniqueId, P_Value)
{
    var L_Comm =
    {
        CommType: 3,
        Data: [{ Id: P_UniqueId, Value: P_Value }]
    }
    _Comm_Send(L_Comm);
}