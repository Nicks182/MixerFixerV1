
function _Message_Receive(P_CommObject)
{
    try
    {
        if (P_CommObject.CommType != "DataUpdate")
        {
            console.log(P_CommObject);
        }

        _SetAllHTMLs(P_CommObject.HTMLs);

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
        }

    }
    catch (ex)
    {
        console.log(ex);
    }
}