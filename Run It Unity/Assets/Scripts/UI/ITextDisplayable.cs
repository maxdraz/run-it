namespace RunIt.UI
{
    public interface ITextDisplayable
    {
        string GetDisplayText();

        delegate void ValueChangedHandler();

        event ValueChangedHandler ValueChanged;
    }
}