using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearingPlatforms : MonoBehaviour
{
    private void OnInit()
    {
        SetActive();
        Invoke(nameof(SetInactive), 5f);
    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
