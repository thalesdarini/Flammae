using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;

    GameObject target;
    Animator animatorReference;
    AudioSource soundEffect;
    bool shotFired;
    bool shotHit;
    bool destructionCommanded;

    public GameObject Target { set => target = value; }
    public float Damage { get => damage; }
    public float Speed { get => speed; }

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        animatorReference = transform.Find("Animation").GetComponent<Animator>();
        soundEffect = GetComponent<AudioSource>();
        shotFired = false;
        shotHit = false;
        destructionCommanded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !shotHit)
        {
            if (!shotFired)
            {
                shotFired = true;
                soundEffect.Stop();
                animatorReference.SetTrigger("Shoot");
                soundEffect.PlayOneShot(SoundManager.fireball, 5);
            }
            transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
            transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(Vector2.up, target.transform.position - transform.position);
        }

        if(destructionCommanded && ( (!shotHit) || (shotHit && animatorReference.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) ))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D objectThatEntered)
    {
        if (!objectThatEntered.isTrigger && objectThatEntered.gameObject == target && !shotHit)
        {
            shotHit = true;
            animatorReference.SetTrigger("Hit");
            soundEffect.PlayOneShot(SoundManager.explosion, 0.2f);
            objectThatEntered.GetComponent<EnemyHealth>().TakeDamage(damage);
            CommandDestruction();
        }
    }

    public void CommandDestruction()
    {
        destructionCommanded = true;
    }
}
