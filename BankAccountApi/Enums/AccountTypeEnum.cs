using System.Text.Json.Serialization;

namespace BankAccountApi.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountTypeText
    {
        Current,
        Savings
    }
}
