
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
    console.log($Modal.attr("NoMask"));
    if ($Modal.attr("NoMask") != "1")
    {
        $Modal.find(".MF_Modal_Mask")
            .width($(document).width())
            .height($(document).height());
    }
    else
    {
        $Modal.find(".MF_Modal_Mask")
            .css({ "width": "", "height": "" });
    }

    $Modal.attr("IsVisible", "1");
}

function _Modal_Hide(P_ModalId)
{
    //$("#" + P_ModalId).attr("IsVisible", "0");
    $("#" + P_ModalId).remove();
}

//function _Modal_ShowHide(P_ModalId)
//{
//    var $Modal = $("#" + P_ModalId);

//    if ($("#" + P_ModalId).attr("IsVisible") == "1")
//    {
//        $("#" + P_ModalId).attr("IsVisible", "0");
//    }
//    else
//    {
//        if ($Modal.attr("NoMask") != "1")
//        {
//            $("#" + P_ModalId + " > .Modal_Mask")
//                .width($(document).width())
//                .height($(document).height());
//        }
//        else
//        {
//            $("#" + P_ModalId + " > .Modal_Mask")
//                .css({ "width": "", "height": "" });
//        }
//        $("#" + P_ModalId).attr("IsVisible", "1");
//    }
//}


