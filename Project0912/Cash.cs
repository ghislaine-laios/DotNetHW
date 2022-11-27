namespace Project0912;

internal interface ICash
{
    long Id { get; }
    int Denomination { get; }
}

internal struct Cash100 : ICash
{
    public long Id { get; init; }
    public int Denomination => 100;
}