namespace FoxPhonebook.Application.Common.Exceptions;
public class PaymentException : Exception
{
    public PaymentException()
     : base()
    {
    }

    public PaymentException(string message)
       : base(message)
    {
    }

    public PaymentException(string bankErrorCode, string message)
       : base($"payment faild, bank code: {bankErrorCode} bank message: {message}")
    {
    }
}
