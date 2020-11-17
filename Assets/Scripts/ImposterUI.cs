using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImposterUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void addListenersToPlayer(GameObject player)
    {
        //todo
        return;
    }

    void toggleImposterUI(bool isImposter)
    {
        gameObject.SetActive(isImposter);
    }
}
