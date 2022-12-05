
function _ToggleClick(e, P_Control)
{
    let L_CheckValue = P_Control.getAttribute("IsChecked");

    if (L_CheckValue == "1")
    {
        P_Control.setAttribute("IsChecked", "0");
    }
    else
    {
        P_Control.setAttribute("IsChecked", "1");
    }

    e.preventDefault();
}

function _Toggle_Set_State(P_Id, P_Value)
{
    $("#" + P_Id + "").attr("IsChecked", P_Value);
    
}