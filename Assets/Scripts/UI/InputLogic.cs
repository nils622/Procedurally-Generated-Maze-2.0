using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputLogic : MonoBehaviour
{
    public List<string> lastValueInputs = new List<string>();

    [SerializeField] private List<TMP_InputField> inputs = new List<TMP_InputField>();
    [SerializeField] private Button generateButton;

    [SerializeField] private TMP_Text errorTextObject;

    private void Awake()
    {
        /* Set values of TabInputFields */
        TabInputField.Reset();
        TabInputField.inputs = inputs;
        TabInputField.generateButton = generateButton;

        /* Fill the lastValueInput spots */
        for (int i = 0; i < inputs.Count; i++)
        {
            lastValueInputs.Add("");
        }
    }

    private void Update()
    {
        /* Tab key to switch between inputs Code */
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabInputField.SelectNext();
        }

        /* Input validation Code */
        for (int i = 0; i < inputs.Count; i++)
        {
            /* Store old and new value */
            string inputText = inputs[i].text;
            string oldInputText = lastValueInputs[i];

            /* if values have changed */
            if (oldInputText != inputText)
            {
                /* Input Text Output number */
                int validatedInputValue = InputValidation.GetValidatedInput(inputText);

                /* Set validated output in variable and UI */
                lastValueInputs[i] = validatedInputValue.ToString();

                /*  If value is lower than 10 dont change UI only change the variable to 10 */
                if (validatedInputValue < 10)
                {
                    lastValueInputs[i] = 10.ToString();
                    continue;
                }

                inputs[i].text = validatedInputValue.ToString();
            }
        }

        /* Error message */
        for (int i = 0; i < inputs.Count; i++)
        {
            /* Gets input */
            string inputText = inputs[i].text;
            int validatedInputValue = InputValidation.GetValidatedInput(inputText);

            /* If value is lower than 10 show error message and return so other fields cant turn the error message off */
            if (validatedInputValue < 10)
            {
                ErrorHandler.ShowErrorMessage(errorTextObject);
                return;
            }

            ErrorHandler.HideErrorMessage(errorTextObject);
        }
    }
}