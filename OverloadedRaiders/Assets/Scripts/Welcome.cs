using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    [SerializeField] GameController controller;
    public void onClick() {
        controller.changeState(1);

    }
}
