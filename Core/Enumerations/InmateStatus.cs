using System.Runtime.Serialization;

namespace Core.Enumerations
{
    public enum InmateStatus
    {
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "Left")]
        Left,
        [EnumMember(Value = "Away")]
        Away
    }
}