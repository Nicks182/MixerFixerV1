

var G_ConnectionUrl = "/CommandHub";

var G_HubConnection = new signalR.HubConnectionBuilder().withUrl(G_ConnectionUrl).build();

$(document)
    .ready(function ()
    {
        G_HubConnection.start().then(function () 
        {
            _Log("Hub Status: " + G_HubConnection.state);
            _Init();
        });
    });





G_HubConnection.on("ReceiveMessage", function (P_CommObject)
{
    _Message_Receive(P_CommObject);
});

function constructJSONPayload(P_CommObject)
{
    return JSON.stringify(P_CommObject);
}

function _Log(P_Data)
{
    console.log(P_Data);
    $("#Log").append(P_Data + "<br />");
}

