using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RektaRetailApp.Domain.DomainModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentType
    {
        Cash,
        Credit,
        Electronic,
        USSD,
        Cheque,
        Other
    }
}