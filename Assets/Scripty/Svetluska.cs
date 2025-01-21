using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Svetluska : MonoBehaviour
{
    public SvetluskaManager manager;
    private Vector3 cilovyBodStred;
    private Vector3 cilovyBodKonec;
    private bool letiDoStredu = true;

    private void Start()
    {
        cilovyBodStred = VygenerujNahodnyBodVRadiusu();
        cilovyBodKonec = VypocitejCilKonec();
    }

    private void Update()
    {
        float krok = manager.rychlostPohybu * Time.deltaTime;

        if (letiDoStredu)
        {
            transform.position = Vector3.MoveTowards(transform.position, cilovyBodStred, krok);

            if (Vector3.Distance(transform.position, cilovyBodStred) < 0.1f)
                letiDoStredu = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cilovyBodKonec, krok);

            if (Vector3.Distance(transform.position, cilovyBodKonec) < 0.1f)
            {
                manager.SvetluskaOdletela(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        manager.SvetluskaChycena(gameObject);
    }

    private Vector3 VygenerujNahodnyBodVRadiusu()
    {
        Vector2 nahodnyVektor = Random.insideUnitCircle * manager.radiusStred;
        return new Vector3(nahodnyVektor.x, nahodnyVektor.y, 0);
    }

    private Vector3 VypocitejCilKonec()
    {
        float x = transform.position.x > 0 ? -manager.spawnOkrajOffset.x : manager.spawnOkrajOffset.x;
        float y = transform.position.y > 0 ? -manager.spawnOkrajOffset.y : manager.spawnOkrajOffset.y;
        return new Vector3(x, y, 0);
    }
}
