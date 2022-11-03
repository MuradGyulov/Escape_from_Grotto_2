using UnityEngine;
using UnityEngine.UI;

public class Text_Translator : MonoBehaviour
{
    [SerializeField] private string russianMessageVersion;
    [SerializeField] private string englishMessageVersion;

    private Text messageText;

    private void OnEnable()
    {
        messageText = GetComponent<Text>();

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            messageText.text = russianMessageVersion;
        }
        else
        {
            messageText.text = englishMessageVersion;
        }
    }
}
