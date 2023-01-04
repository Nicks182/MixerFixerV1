
function _Message_Receive(P_CommObject)
{
    try
    {
        if (P_CommObject.CommType != "DataUpdate")
        {
            console.log(P_CommObject);
        }

        _SetAllHTMLs(P_CommObject.HTMLs);
        _Modal_SetState(P_CommObject.ModalInfo);

        switch (P_CommObject.CommType)
        {
            case "Init":
                _App_Init(P_CommObject)
                break;

            case "DataUpdate":
                _App_DataUpdate_Receive(P_CommObject)
                break;

            case "ShowMessage":
                alert('bla');
                break;

            case "_Log":
                _LogToConsole(P_CommObject);
                break;

        }

    }
    catch (ex)
    {
        _Log(ex);
    }
}

function _LogToConsole(P_CommObject)
{
    for (var i = 0; i < P_CommObject.Data.length; i++)
    {
        console.log(P_CommObject.Data[i].Id + " - " + P_CommObject.Data[i].Value)
    }
}