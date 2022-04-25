using System;
using Yubico.Core.Iso7816;
using Yubico.YubiKey;
using Yubico.YubiKey.Piv;
using Yubico.YubiKey.Piv.Commands;

class TestClass
{
    static void Main(string[] args)
    {
        IYubiKeyDevice? yk = SelectYubiKey();
        if (yk == null)
        {
            System.Environment.Exit(1);
        }
        Console.WriteLine (yk);
        using (var piv = new PivSession(yk))
        {
            byte[] data = { 0xde, 0xad, 0xbe, 0xef };
            PutDataCommand putDataCommand =
                new PutDataCommand(0, data);
            PutDataResponse putDataResponse =
                piv.Connection.SendCommand(putDataCommand);

            if (putDataResponse.StatusWord != SWConstants.Success)
            {
                Console.WriteLine("error putting data");
            }
        }
    }

    static IYubiKeyDevice? SelectYubiKey()
    {
        IEnumerable<IYubiKeyDevice> list = YubiKeyDevice.FindAll();
        return list.First();
    }
}
