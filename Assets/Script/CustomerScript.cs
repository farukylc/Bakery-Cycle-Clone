using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using TMPro;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    [SerializeField] private GameObject customerWP;
    [SerializeField] private GameObject customer;
  

    private float distanceTravelled;
    private float customerSpeed = 5;
    public bool canMove = false;

    
    
    

    public float delay;
    // [SerializeField] private GameObject customerSpawn;

    private void Start()
    {
        StartCoroutine(triggerStart(delay));
    }

    private void Update()
    {
        if (canMove)
        {
            distanceTravelled += customerSpeed * Time.deltaTime;
            transform.position = GameManager.GM.customerPathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = GameManager.GM.customerPathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
            
            

    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        switch (other.tag)
        {
            case "BuyArea":

                GameObject soldBread = Instantiate(GameManager.GM.bread);
                soldBread.transform.position = new Vector3(customerWP.transform.position.x, 
                    customerWP.transform.position.y,
                    customerWP.transform.position.z);
                
                soldBread.transform.SetParent(gameObject.transform);

                
                Destroy(GameManager.GM.toSellBread[^1]);
                GameManager.GM.toSellBread.Remove(GameManager.GM.toSellBread[^1]);
                
                break;
            
            case "DestroyCustomer":
                
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator triggerStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
}
