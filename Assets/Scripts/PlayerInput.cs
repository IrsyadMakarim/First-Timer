using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip[] smashSounds;
    AudioSource audioSource;
    public GameObject bloodEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                    audioSource.PlayOneShot(smashSounds[Random.Range(0, smashSounds.Length)], 0.1f);
                    gameObject.GetComponent<GameManager>().killEnemy();
                    Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                    DisplayBloodEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }
    }
    private void DisplayBloodEffect(Vector2 pos)
    {
        bloodEffect.transform.position = pos;
        bloodEffect.GetComponent<Animator>().SetTrigger("Smashed");
    }
}
