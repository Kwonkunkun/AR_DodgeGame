using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;


public class PlayerDie : MonoBehaviour
{
    Animator anim;

    public bool isDie{ get; private set;}
    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" && isDie == false)
        {
            Debug.Log("Player Die");
            isDie = true;
            anim.SetTrigger("Die");
            GameManager.instance.PlayerDie();
        }
    }
}
