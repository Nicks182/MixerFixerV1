

function _SetAllHTMLs(P_HTMLs)
{
    for (var i = 0; i < P_HTMLs.length; i++)
    {
        if (P_HTMLs[i].IsAppend == true)
        {
            $(P_HTMLs[i].ContainerId).append(P_HTMLs[i].HTML);
        }
        else
        {
            $(P_HTMLs[i].ContainerId).html(P_HTMLs[i].HTML);
        }
    }
}


