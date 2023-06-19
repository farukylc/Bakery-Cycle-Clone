using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Animator playerAnim;

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButton(0))
        {
            PathFollower.pathFollower.speed = 20;
            playerAnim.SetBool("isStarted", true);

        }
        else
        {
            PathFollower.pathFollower.speed = 0;
            playerAnim.SetBool("isStarted", false);
        }
    }

    
}
