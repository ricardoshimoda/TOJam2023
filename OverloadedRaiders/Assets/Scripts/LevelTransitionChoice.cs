using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionChoice : MonoBehaviour
{
    [SerializeField] GameObject firstTransitionPanel;
    [SerializeField] GameObject secondTransitionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFirstTransition() {
        secondTransitionPanel.SetActive(true);
        firstTransitionPanel.SetActive(false);
    }
}
