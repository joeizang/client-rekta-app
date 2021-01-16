using System.Text.Json.Serialization;

namespace RektaRetailApp.Domain.DomainModels
{
    [JsonConverter(typeof(CustomerStatus))]
    public enum CustomerStatus
    {
        CashPaying,
        Creditor
    }
}