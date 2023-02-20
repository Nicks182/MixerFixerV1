
function _App_QRCodeShow(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}

function _App_QRCode_OpenInLocalBrowser(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}