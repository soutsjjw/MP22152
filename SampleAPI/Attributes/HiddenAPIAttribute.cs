namespace SampleAPI.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class HiddenAPIAttribute : System.Attribute
{
}
