using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private  int hitPoints = 1;
    [SerializeField] private ParticleSystem destroyBrickParticles;
    
    public static event Action<Brick> OnBrickDestroyed;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.AddComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.hitPoints--;

        if (this.hitPoints <= 0)
        {
            OnBrickDestroyed?.Invoke(this);
            BeginParticlesOnDestroy();
            Destroy(this.gameObject);
        }
        else
        {
            //
        }
    }

    private void BeginParticlesOnDestroy()
    {
        Vector3 brickPosition = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPosition.x, brickPosition.y, brickPosition.z);
        GameObject effect = Instantiate(destroyBrickParticles.gameObject, spawnPosition, Quaternion.identity);
        //destroyBrickParticles.GetComponent<ParticleSystem> ().Play();
    }
}
