using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    // [SerializeField] private GameObject customerWP;


    private void Update()
    {
       customerSpawner();
    }

    void customerSpawner()
    {
        if (GameManager.GM.toSellBread.Count > 0)
        {

            if (GameObject.FindGameObjectsWithTag("Customer").Length <= GameManager.GM.toSellBread.Count-1)
            {

                GameObject newCustomer = Instantiate(customer);
                newCustomer.transform.position = transform.position;
                newCustomer.GetComponent<CustomerScript>().delay= GameObject.FindGameObjectsWithTag("Customer").Length *1.5f;

            }
        }
    }
    
}
