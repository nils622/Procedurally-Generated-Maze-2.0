public static class InputValidation
{
    /* Returns int instead of string so you cant put zeros infront of number */
    public static int GetValidatedInput(string inputText)
    {
        /* If empty */
        if (string.IsNullOrEmpty(inputText))
        {
            inputText = "10";
            return int.Parse(inputText);
        }

        /* Is set again here because inputText cant be null now */
        int inputValue = int.Parse(inputText);

        /* Below minimum */
        if (inputValue <= 10)
        {
            return inputValue;
        }

        /* Above maximum */
        if (inputValue > 250)
        {
            inputValue = 250;
            return inputValue;
        }

        return inputValue;
    }
}