
function _MenuBtn_Click(P_DeviceId, P_CommType)
{
    var L_Comm =
    {
        CommType: P_CommType,
        Data: []
    }

    L_Comm.Data.push(
        {
            Id: "deviceid",
            Value: P_DeviceId,
            DataType: 1
        });

    console.log(L_Comm);
    _Comm_Send(L_Comm);
}

