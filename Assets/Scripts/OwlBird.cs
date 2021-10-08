using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    private bool _hasExplode = false;
    [SerializeField] private float _explodeforce = 2000;
    [SerializeField] private float _explodeRadius = 0.5f;
    [SerializeField] private GameObject explosion;

    public void Meledak(Collision2D collision)
    {
        if (State == BirdState.Thrown && !_hasExplode)
        {
            Collider2D[] colliderAround = Physics2D.OverlapCircleAll(transform.position, _explodeRadius);
            foreach (Collider2D obj in colliderAround)
            {
                Vector2 direction = obj.transform.position - transform.position;
                if (obj.GetComponent<Rigidbody2D>() != null)
                {
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * _explodeforce);
                }
            }

            RigidBody.AddForce(Vector2.up * (_explodeforce / 2));
            _hasExplode = true;
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Meledak(collision);
    }
}
