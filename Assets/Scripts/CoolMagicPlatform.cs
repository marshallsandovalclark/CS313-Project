using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolMagicPlatform : MonoBehaviour
{
    public GameObject platform;
    public string text;
    public TMP_Text display;
    public Image backdrop;
    private bool entered = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "PlayerArmature")
        {
            if (Input.GetKey(KeyCode.F))
            {
                platform.SetActive(true);

                entered = true;

                display.text = text;
                backdrop.enabled = true;

                Invoke(nameof(removeText), 5);
            }
        }
    }

    private void removeText()
    {
        display.text = "";
        backdrop.enabled = false;
    }
}
