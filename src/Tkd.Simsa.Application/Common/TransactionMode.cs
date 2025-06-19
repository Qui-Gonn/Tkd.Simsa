namespace Tkd.Simsa.Application.Common;

public class TransactionMode
{
    public static readonly TransactionMode Auto = new ("Auto");

    public static readonly TransactionMode Manual = new ("Manual");

    private TransactionMode(string mode)
    {
        this.Mode = mode;
    }

    public bool IsAuto => this == Auto;

    public bool IsManual => this == Manual;

    private string Mode { get; }
}