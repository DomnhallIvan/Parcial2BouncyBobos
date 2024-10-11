using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Transform _firePoint;
    private ObjectPool bulletPool;

    [Space]
    [SerializeField] private float FireRate = 5;
    [SerializeField] private float BulletForce = 40;

    private void Awake()
    {
        bulletPool = FindObjectOfType<ObjectPool>();
    }

    void Start()
    {
        FIREBALL(_firePoint.position, _firePoint.forward);         
    }

    private void FIREBALL(Vector3 firePointPosition, Vector3 fireDirection)
    {
        GameObject fire = bulletPool.GetFromPool();
        fire.transform.position = firePointPosition;
        fire.transform.rotation = Quaternion.identity;

        Rigidbody bulletRB = fire.GetComponent<Rigidbody>();
        if (bulletRB != null)
        {
            bulletRB.velocity = Vector3.zero;
            bulletRB.AddForce(fireDirection * BulletForce);
        }
    }


}
