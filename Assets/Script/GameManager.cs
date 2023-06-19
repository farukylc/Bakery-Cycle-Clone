using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using PathCreation;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;


    [SerializeField] public PathCreator customerPathCreator;
    
    
    public int hamurAmount = 0;
    [SerializeField] private TextMeshPro scoreText;
    public UnityEngine.UI.Image cooldown;
    public int waitTime = 1;
    
    [SerializeField] public List<GameObject> collectedHamur= new List<GameObject>();
    //[SerializeField] public List<GameObject> breadList= new List<GameObject>();
    [SerializeField] public List<GameObject> fırınList= new List<GameObject>();
    [SerializeField] public List<GameObject> cookedList = new List<GameObject>();
    [SerializeField] public List<GameObject> collectedBread = new List<GameObject>();
    [SerializeField] public List<GameObject> toSellBread = new List<GameObject>();

    [SerializeField] private GameObject hamur;
    [SerializeField] public GameObject bread;

    [SerializeField] private GameObject SpawnerWP;
    [SerializeField] private GameObject mikserWP;

    [SerializeField] private GameObject HamurHolder;

    private void Start()
    {
        //InvokeRepeating("BreadSpawner",0,1);

        GM= this;
    }

    private void Update()
    {
       

    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Hamur":
                 Hamur();
                 break;
            
            case "Sell":
                 Sell();
                break;   
            
            case "FırınArea":
                hamurAmount = 0;
                scoreText.text = hamurAmount.ToString() + "/5";
                Fırın();
                
                
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Hamur":
                cooldown.fillAmount = 0;
                SpawnerWP.transform.position += new Vector3(0, (-collectedHamur.Count), 0);
                break;
            
            case "Take":

                if (bread.transform.position !=  new Vector3(0,6.1f,0))
                {
                    SpawnerWP.transform.position += new Vector3(0, (-collectedBread.Count), 0);
                    breadSpawner.transform.position -= new Vector3(0, collectedBread.Count, 0);
                }
                
                break;
            
            case "FırınArea":
                fırınCooldown.fillAmount = 0;
                break;
            
            case "Sell":
                test =false;
                sellCooldown.fillAmount = 0;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Take":
                Take();
                    break;
        }
    }


    private void Hamur()
    {
        if (hamurAmount <= 4 && collectedBread.Count == 0)
        {
            cooldown.fillAmount += 1f * Time.deltaTime;
        }

        if (cooldown.fillAmount == 1 && collectedBread.Count == 0)
        {
            hamurAmount++;
            cooldown.fillAmount = 0;
            scoreText.text = hamurAmount.ToString() + "/5";


            GameObject newHamur = Instantiate(hamur);

            newHamur.transform.position = new Vector3(mikserWP.transform.position.x, mikserWP.transform.position.y,
                    mikserWP.transform.position.z);
            
            
            newHamur.transform.DOMove(SpawnerWP.transform.position, .1f);
            SpawnerWP.transform.position += new Vector3(0, 1f, 0);
            
            newHamur.gameObject.transform.SetParent(HamurHolder.transform);
            
             
            collectedHamur.Add(newHamur);
        }

        if (hamurAmount == 5)
        {
            cooldown.fillAmount = 0;
        }

        
    }
    
    
    public UnityEngine.UI.Image fırınCooldown;
    [SerializeField] private GameObject fırınWP;
    [SerializeField] private GameObject breadSpawner;
    
    public int breadAmount = 0;
    private void Fırın()
    {
        if (collectedHamur.Count != 0)
        {
            fırınCooldown.fillAmount += 2f * Time.deltaTime;
        }
        

        if (fırınCooldown.fillAmount == 1 && collectedHamur.Count != 0)
        {
            fırınCooldown.fillAmount = 0;
            foreach (var item in collectedHamur)
            {
                item.transform.SetParent(fırınWP.transform);
                item.transform.DOMove(fırınWP.transform.position, 2);
                breadAmount = breadAmount + 1;
                fırınList.Add(item);
            }

            StartCoroutine(BreadSpawner());
            collectedHamur.Clear();
        }
    }

   
    
    IEnumerator BreadSpawner()
    {
        if (0 < fırınList.Count && fırınList.Count <= 5 )
        {
            int a = fırınList.Count;
            for (int i = 0; i < a; i++)
            {

                GameObject newBread = Instantiate(bread);

                newBread.transform.position = new Vector3(breadSpawner.transform.position.x, breadSpawner.transform.position.y,
                    breadSpawner.transform.position.z);
            
                breadSpawner.transform.position += new Vector3(0, 1f, 0);

                fırınList.Remove(fırınList[^1]);
                cookedList.Add(newBread);
                yield return new WaitForSeconds(1);
            }
            
            
        }

    }

    private void Take()
    {

        foreach (var cooked in cookedList)
        {
            
            collectedBread.Add(cooked);
            cooked.transform.SetParent(HamurHolder.transform);
            cooked.transform.DOMove(SpawnerWP.transform.position, .03f);
            SpawnerWP.transform.position += new Vector3(0, 1f, 0);

        }
        cookedList.Clear();
        
    }
    
    
    
    
    

    [SerializeField] private List<Transform> sellWP = new List<Transform>();
    public UnityEngine.UI.Image sellCooldown;
    private bool test = false;
    private void Sell()
    
    {
        if (test == false)
        {
            sellCooldown.fillAmount += 1f * Time.deltaTime;
        }
            

        if (sellCooldown.fillAmount == 1)
        {
                int a = toSellBread.Count;
                int b = 0;
                int currentIndex = collectedBread.Count-1;
                for (int i =a; i <= a+currentIndex; i++)
                {
                    collectedBread[b].transform.parent = null;
                    collectedBread[b].transform.position = sellWP[i].position;
                    toSellBread.Add(collectedBread[b]);
                    // currentIndex--;
                    // toSellBread.Add(collectedBread[i]);
                    b++;
                }
                
                collectedBread.Clear();
                test = true;
        }
    }
}



