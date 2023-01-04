using System.Security.Cryptography;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Boom : MonoBehaviour
{
    public float ExplosiveRadius = 5f;
    public float Power = 200f;


    public new AudioSource audio;
    public ParticleSystem ParticalExplosive;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G)) 
        { 
            Invoke(nameof(Explose), 0.3f);
        }
        
    }
    private void Explose()
    {
        ExploseForce();
        
        audio.Play();


        Instantiate(ParticalExplosive, transform.position, Quaternion.identity);

        DestroyImmediate(gameObject);

    }
    private void ExploseForce()
    {
        var col = Physics.OverlapSphere(transform.position, ExplosiveRadius);
        foreach(Collider hit in col)
        {
            var body = GetComponent<Rigidbody>();
            if(body)
                body.AddExplosionForce(Power, transform.position, ExplosiveRadius, 2f);
        }
    }
}
