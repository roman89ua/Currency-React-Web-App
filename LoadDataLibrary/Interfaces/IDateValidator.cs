namespace LoadDataLibrary.Interfaces;

public interface IDateValidator
{
    public bool IsValidDate(string value, string[] formats);
}