using TMPro;

public static class ErrorHandler
{
    public static void ShowErrorMessage(TMP_Text textfield)
    {
        textfield.enabled = true;
    }

    public static void HideErrorMessage(TMP_Text textfield)
    {
        textfield.enabled = false;
    }
}
