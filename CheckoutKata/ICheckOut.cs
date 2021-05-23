namespace CheckoutKata
{
    public interface ICheckOut
    {
         int GetTotalPrice();
         ICheckOut Scan(string scan);
         string ScannedProducts { get; }
    }
}
