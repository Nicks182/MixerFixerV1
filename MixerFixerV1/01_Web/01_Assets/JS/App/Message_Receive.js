
function _Message_Receive(P_CommObject)
{
    try
    {
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
        }

    }
    catch (ex)
    {
        _Log(ex);
    }
}