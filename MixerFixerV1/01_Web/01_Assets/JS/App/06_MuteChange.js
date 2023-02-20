

function _App_MuteChange(P_Event, P_UniqueId)
{
    var L_Comm =
    {
        CommType: "Mute_Change",
        Data: [{ Id: P_UniqueId }]
    }
    _Comm_Send(L_Comm);

    P_Event.preventDefault();
    return false;
}