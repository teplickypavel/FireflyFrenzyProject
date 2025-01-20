using UnityEngine;

public class SitkaChytani : MonoBehaviour
{
    public float casOtoceni = 0.2f;  // Jak dlouho trv� oto�en�
    public float rychlostZmizeni = 2.0f; // Rychlost zmizen� (alpha efekt)
    public float cilovyUhel = -45f;  // Kam se m� s�ka oto�it (zm��, pokud chce� jin� �hel)

    private bool aktivni = false;
    private float casSpusteni;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.rotation = Quaternion.Euler(0, 0, 58.165f);

        casSpusteni = Time.time;
    }

    private void Update()
    {
        if (aktivni)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, cilovyUhel), (Time.time - casSpusteni) / casOtoceni);

            Color barva = spriteRenderer.color;
            barva.a -= Time.deltaTime * rychlostZmizeni;
            spriteRenderer.color = barva;

            if (barva.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AktivujZmizeni()
    {
        if (!aktivni)
        {
            aktivni = true;
            casSpusteni = Time.time;
        }
    }
}