using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartiColl : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle hit");
    }
}
