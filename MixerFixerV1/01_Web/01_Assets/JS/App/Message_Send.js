
function _Comm_Send(P_Comm)
{
    G_HubConnection.invoke("SendMessage", constructJSONPayload(P_Comm));
    
}