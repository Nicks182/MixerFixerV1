

function _App_SettingsShow(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}

function _App_Settings_VolumeInput_Show(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);
}


function _App_Settings_PriorityItemMove(P_DeviceId, P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType,
        Data: [{ Id: "DeviceId", Value: P_DeviceId }]
    }
    _Comm_Send(L_Comm);

    _App_SettingsShowSaved();
}

function _App_Settings_UseDefaultVolume_Change(P_MessageType)
{
    var L_Comm =
    {
        CommType: P_MessageType
    }
    _Comm_Send(L_Comm);

    _App_SettingsShowSaved();
}


function _App_SettingsShowSaved()
{
    $(".MF_Modal_Content_Footer_Notification").attr("IsSaved", "1");
    setTimeout(function ()
    {
        $(".MF_Modal_Content_Footer_Notification").attr("IsSaved", "0");
    }, 1100);
}