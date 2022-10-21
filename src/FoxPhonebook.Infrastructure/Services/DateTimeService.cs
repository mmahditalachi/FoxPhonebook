using FoxPhonebook.Application.Common.Interfaces;

namespace FoxPhonebook.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}