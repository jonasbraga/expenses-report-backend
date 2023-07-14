using System.Text.Json.Serialization;

namespace AccountOrganizationService.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        FieldStaff = 1,
        Manager = 2,
        Accountant = 3,
    }
}