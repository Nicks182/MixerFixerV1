

function _SetAllHTMLs(P_HTMLs)
{
    for (var i = 0; i < P_HTMLs.length; i++)
    {
        $(P_HTMLs[i].ContainerId).html(P_HTMLs[i].HTML);
    }
}


