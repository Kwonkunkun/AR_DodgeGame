using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//랜덤한 초마다 bullet을 발사한다.
public class EnemyFire : MonoBehaviour
{
    GameObject target;
    Animator modelAnim;

    public GameObject bulletFactory;
    public float currentTime;
    public float fireTime;

    void Start()
    {
        fireTime = Random.Range(1f, 3f);
        target = GameObject.Find("Player");
        modelAnim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        LookAt();
        Fire();
    }

    private void LookAt()
    {
        transform.LookAt(target.transform);
    }

    private void Fire()
    {
        if (currentTime > fireTime)
        {
            //fire animation
            modelAnim.SetTrigger("Attack 02");

            //fire
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = transform.position;
            bullet.transform.forward = transform.forward;
            fireTime = Random.Range(1f, 3f);
            currentTime = 0f;
        }
        currentTime += Time.deltaTime;
    }
}
