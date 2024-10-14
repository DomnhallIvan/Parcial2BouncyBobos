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
    private float timeLastSpeedChange = 0f, minTimeSpeedChange = 3f;
    private bool StartGame;
    private void Awake()
    {
        bulletPool = FindObjectOfType<ObjectPool>();
       
    }

    private void LateUpdate()
    {
        if(StartGame)
        {
            timeLastSpeedChange += Time.deltaTime;
            if (timeLastSpeedChange >= Random.Range(1f, 750f))
            {
                FIREBALL(_firePoint.position, _firePoint.forward);
                timeLastSpeedChange = 0f;
            }
        }
        
    }

    void Start()
    {
        GameManager.instance.gameUI.onStartGame += GameStart;
        GameManager.instance.onReset += GameEnd;
    }

    private void GameStart()
    {
        StartGame = true;
    }

    private void GameEnd()
    {
        StartGame=false;
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
