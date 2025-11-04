using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    public TMP_InputField passwordField;

    public void TogglePasswordVisibility()
    {
        if (passwordField.contentType == TMP_InputField.ContentType.Password)
        {
            passwordField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            passwordField.contentType = TMP_InputField.ContentType.Password;
        }

        passwordField.ForceLabelUpdate();
    }
}
