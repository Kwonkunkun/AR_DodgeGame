using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float angle;
    public float Speed = 10f;
    public int numOfBump = 0;
    public int maxOfBump = 3;

    private Vector3 direction;
    bool enter;

    void Start()
    {
        direction = transform.forward;
        enter = false;
    }

    void Update()
    {
        //p = p0 + vt
        transform.position += direction * Speed * Time.deltaTime;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enter)
            return;
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("collision Obstacle");

            Vector3 incommingVector = direction;
            //incommingVector = incommingVector.normalized;

            Vector3 myNormal = collision.transform.forward;
            myNormal = myNormal.normalized;

            Vector3 pnVector = incommingVector.magnitude * Mathf.Cos(Vector3.Dot(incommingVector, myNormal)) * myNormal;
            Vector3 rVector = incommingVector + pnVector*2;

            Speed = Random.Range(5.0f, 10.0f);
            direction = rVector.normalized;
            enter = true;

            //3번이상 부딪히면 없애기
            numOfBump++;
            if (numOfBump == maxOfBump)
                Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        enter = false;
    }
}
