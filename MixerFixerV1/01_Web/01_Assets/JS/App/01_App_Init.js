

function _Init()
{
    var L_Comm =
    {
        CommType: "Init"
    }

    _Comm_Send(L_Comm);
}


function _App_Init(P_CommObject)
{
    
    _App_DataUpdate_Send();
}