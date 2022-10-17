using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


/* adds tab functionality to inputfields so you can easily scroll through all options */
public static class TabInputField
{
    public static List<TMP_InputField> inputs = new List<TMP_InputField>();
    public static Button generateButton;

    private static int count = 0;

    public static void Reset()
    {
        count = 0;
    }

    public static void SelectNext()
    {
        /* If count is bigger than input.count Select generate button and reset the count so next tab push will select first inputfield */
        if (count >= inputs.Count)
        {
            generateButton.Select();
            count = -1;
        }
        else
        {
            /* Selects the next inputfield */
            inputs[count].Select();

            if (count == inputs.Count)
            {
                generateButton.onClick.Invoke();
                count = -1;
            }
        }

        count++;
    }
}
