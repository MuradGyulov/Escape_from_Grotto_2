using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageTranslator : MonoBehaviour
{
    [SerializeField] private string russianMessageVersion;
    [SerializeField] private string englishMessageVersion;

    private Text messageText;

    private void OnEnable()
    {
        messageText = GetComponentInChildren<Text>();


        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            StartCoroutine(TextWrite(russianMessageVersion));
        }
        else
        {
            StartCoroutine(TextWrite(englishMessageVersion));
        }
    }

    private IEnumerator TextWrite(string writeText)
    {
        foreach(char abs in writeText)
        {
            messageText.text += abs;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
