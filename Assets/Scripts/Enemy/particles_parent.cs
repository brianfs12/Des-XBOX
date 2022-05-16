using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles_parent : MonoBehaviour
{
    private ParticleSystem particles;

    // Start is called before the first frame update
    private void Awake()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        while (transform.parent) {
            transform.parent = transform.parent.transform.parent;
        }
    }

    private void Update()
    {
        if (!particles.isPlaying) {
            Destroy(gameObject);
        }
    }
}
