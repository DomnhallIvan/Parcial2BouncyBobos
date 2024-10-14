using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Referencias")]
    public Rigidbody rb;
    //private GameManager gameManager;

    [Header("Datos")]
    private int porteriaLayer;
    private int playerLayer;
    private int bolaLayer;
    private Vector3 lastVelocity;
    [SerializeField] private int NumOfBounces = 20;
    private int curBounces = 0;

    private void Awake()
    {
        porteriaLayer = LayerMask.NameToLayer("Porteria");
        playerLayer = LayerMask.NameToLayer("Player");
        bolaLayer = LayerMask.NameToLayer("BolaLoca");
        //gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        GameManager.instance.onReset += ReturnToPool;
    }

    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.layer==porteriaLayer)
        {
            ScoreZone scoreZone = collision.gameObject.GetComponent<ScoreZone>();
            if (scoreZone)
                GameManager.instance.OnScoreZoneReached(scoreZone.id);

            ReturnToPool();
        }
        if(collision.gameObject.layer==playerLayer|| collision.gameObject.layer == bolaLayer)
        {            
           
            curBounces++;
            if (curBounces <= NumOfBounces) return;
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ObjectPool pool = FindObjectOfType<ObjectPool>();
        if (pool != null)
        {
            curBounces = 0;
            pool.ReturnToPool(gameObject);
        }
    }
}
