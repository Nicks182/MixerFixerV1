
function _Modal_SetState(P_ModalInfo)
{
    if (P_ModalInfo)
    {
        if (P_ModalInfo.State == 1)
        {
            _Modal_Show(P_ModalInfo.Id);
        }
        else
        {
            _Modal_Hide(P_ModalInfo.Id);
        }
    }
}


function _Modal_Show(P_ModalId)
{
    var $Modal = $("#" + P_ModalId);
    
    $Modal.attr("IsVisible", "1");
}


function _Modal_Hide(P_ModalId)
{
    $("#" + P_ModalId).remove();
}



