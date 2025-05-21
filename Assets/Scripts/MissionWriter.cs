using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MissionWriter : MonoBehaviour
{
    public TMP_InputField NameField, DescriptionField, VerboseField;
    public TMP_Dropdown TypeField;

    public void SubmitPressed()
    {
        if (NameField.text == string.Empty || DescriptionField.text == string.Empty || VerboseField.text == string.Empty) return;

        Mission m = new Mission(true);
        m.Name = NameField.text;
        m.Description = DescriptionField.text;
        m.Verbose = VerboseField.text;
        m.Type = (MissionType)TypeField.value;

        MissionRandomiser.AddToJson(m);

        ResetFields();
    }
    private void ResetFields()
    {
        NameField.text = string.Empty;
        DescriptionField.text = string.Empty;
        VerboseField.text = string.Empty;
        TypeField.value = 0;
    }
}
