using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D arrowRb;
    [SerializeField] private float lifeTime = 10.0f;

    [SerializeField] private GameObject fire;
    public bool inFire
    {
        set { fire.SetActive(value); }
    }
    bool inAir = true;

    void Update()
    {
        if (inAir) InAir();
        else Destroy(this.gameObject, lifeTime);
    }

    void InAir()
    {
        float angle = Mathf.Atan2(arrowRb.velocity.y, arrowRb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Projectile")
        {
            inAir = false;
            arrowRb.gravityScale = 0;
            arrowRb.velocity = Vector3.zero;
        }
    }
}
