

function _SetAllHTMLs(P_HTMLs)
{
    try
    {
        
        for (var i = 0; i < P_HTMLs.length; i++)
        {
            $(P_HTMLs[i].ContainerId).html(P_HTMLs[i].HTML);
        }
    }
    catch (ex)
    {
        _Log(ex);
        
    }
}