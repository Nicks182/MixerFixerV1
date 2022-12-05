

function _App_ManagedChange(P_UniqueId)
{
    var L_Comm =
    {
        CommType: "Managed_Change",
        Data: [{ Id: P_UniqueId }]
    }
    _Comm_Send(L_Comm);
}