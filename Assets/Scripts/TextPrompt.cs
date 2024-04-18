using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextPrompt : MonoBehaviour
{
    public string text;
    public TMP_Text display;
    public Image backdrop;
    private bool entered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerArmature" && entered == false)
        {
            entered = true;

            display.text = text;
            backdrop.enabled = true;

            Invoke(nameof(removeText), 5);
        }
    }

    private void removeText()
    {
        display.text = "";
        backdrop.enabled = false;
    }
}
