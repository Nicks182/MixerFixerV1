

function _App_DataUpdate_Send()
{
    var L_Comm =
    {
        CommType: 2
    }
    _Comm_Send(L_Comm);
}



function _App_DataUpdate_Receive(P_CommObject)
{
    for (var i = 0; i < P_CommObject.Data.length; i++)
    {
        switch (P_CommObject.Data[i].DataType)
        {
            case "Progress":
                _App_DataUpdate_Progress(P_CommObject.Data[i]);
                break;

            case "Text":
                _App_DataUpdate_Text(P_CommObject.Data[i]);
                break;

            case "Slider":
                _App_DataUpdate_Slider(P_CommObject.Data[i]);
                break;

            case "ButtonText":
                _App_DataUpdate_ButtonText(P_CommObject.Data[i]);
                break;

            case "Toggle":
                _App_DataUpdate_Toggle(P_CommObject.Data[i]);
                break;
        }

    }
    //setTimeout(_App_DataUpdate_Send, 100);
}

function _App_DataUpdate_Progress(P_Data)
{
    $("#" + P_Data.Id + "").width(P_Data.Value);
}

function _App_DataUpdate_Text(P_Data)
{
    $("#" + P_Data.Id + "").val(P_Data.Value);
}

function _App_DataUpdate_Slider(P_Data)
{
    
    $("#" + P_Data.Id + "").val(P_Data.Value);
}

function _App_DataUpdate_ButtonText(P_Data)
{
    $("#" + P_Data.Id + "").text(P_Data.Value);
}

function _App_DataUpdate_Toggle(P_Data)
{
    _Toggle_Set_State(P_Data.Id, P_Data.Value);
}