

var G_VolumeChangeTimeOut = null;

function _App_VolumeChange(P_UniqueId, P_Value)
{
    clearTimeout(G_VolumeChangeTimeOut);

    G_VolumeChangeTimeOut = setTimeout(_App_VolumeChange_Handler(P_UniqueId, P_Value), 15);
}

function _App_VolumeChange_Handler(P_UniqueId, P_Value)
{
    return function()
    {
        var L_Comm =
        {
            CommType: "Volume_Change",
            Data: [{ Id: P_UniqueId, Value: P_Value }]
        }
        _Comm_Send(L_Comm);
    }
}


//function _App_VolumeChange(P_UniqueId, P_Value)
//{
//    var L_Comm =
//    {
//        CommType: "Volume_Change",
//        Data: [{ Id: P_UniqueId, Value: P_Value }]
//    }
//    //_Comm_Send(L_Comm);
//}


function _App_MouseVolumeChange(P_Event, P_Control, P_UniqueId)
{
    console.log("IsCTRL: " + P_Event.ctrlKey);
    if (P_Event.ctrlKey == true)
    {
        
        if (P_Event.deltaY < 0)
        {
            P_Control.valueAsNumber += 1;
        }
        else
        {
            P_Control.valueAsNumber -= 1;
        }

        _App_VolumeChange(P_UniqueId, P_Control.value)
        P_Event.preventDefault();
        P_Event.stopPropagation();
        return false;
    }
}

function _App_VolumeModal_Show(P_UniqueId, P_CommType)
{
    var L_Comm =
    {
        CommType: P_CommType,
        Data: [{ Id: "Id", Value: P_UniqueId }]
    }
    _Comm_Send(L_Comm);
}