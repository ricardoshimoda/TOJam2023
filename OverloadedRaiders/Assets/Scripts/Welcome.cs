using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    [SerializeField] GameObject homePanel;
    public void onClick() {
        homePanel.SetActive(false);
    }
}
