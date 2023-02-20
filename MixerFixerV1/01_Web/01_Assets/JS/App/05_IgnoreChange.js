

function _App_IgnoreChange(P_Event, P_UniqueId)
{
    var L_Comm =
    {
        CommType: "Ignore_Change",
        Data: [{ Id: P_UniqueId }]
    }
    _Comm_Send(L_Comm);

    P_Event.preventDefault();
    return false;
}