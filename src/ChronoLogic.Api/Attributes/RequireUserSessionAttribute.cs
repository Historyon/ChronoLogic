namespace ChronoLogic.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class RequireUserSessionAttribute(bool abortOnFailure = true) : Attribute
{
    public bool AbortOnFailure { get; } = abortOnFailure;
}