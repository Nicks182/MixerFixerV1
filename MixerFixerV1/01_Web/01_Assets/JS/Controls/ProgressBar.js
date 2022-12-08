
function _ProgressBar_SetValue(P_Id, P_Value)
{
    $("#" + P_Id + " > div").width(P_Value);
}
