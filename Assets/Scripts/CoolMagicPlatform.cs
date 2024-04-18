using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolMagicPlatform : MonoBehaviour
{
    public GameObject platform;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "PlayerArmature")
        {
            if (Input.GetKey(KeyCode.F))
            {
                platform.SetActive(true);
            }
        }
    }
}
