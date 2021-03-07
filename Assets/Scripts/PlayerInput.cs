using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check for input here
        if (Input.GetMouseButtonDown(0))
        {
            //Make raycast here
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            //Check if raycast hit collider
            if(hit.collider != null)
            {
                if(hit.collider.tag == "Enemy")
                {
                    //Code to kill enemy
                    gameObject.GetComponent<GameManager>().killEnemy();
                }
            }
        }
    }
}
