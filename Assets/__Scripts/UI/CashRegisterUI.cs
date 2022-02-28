using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegisterUI : MonoBehaviour
{
    [SerializeField] GameObject CashRegister;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CashRegister.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CashRegister.SetActive(false);

        }
    }
}
