using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float _dano;
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        if (other.tag == "corona")
        {   
            other.GetComponent<Covid>()._vida -= _dano;
        }

    }
}
