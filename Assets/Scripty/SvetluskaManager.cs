using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SvetluskaManager : MonoBehaviour
{
    public GameObject svetluskaPrefab; 
    public int pocetSvetlusek = 3;     
    public float rychlostPohybu = 5.0f; 
    public float radiusStred = 3.0f;    
    public Vector2 spawnOkrajOffset = new Vector2(8.0f, 6.0f); 

    void Start()
    {
       
        for (int i = 0; i < pocetSvetlusek; i++)
        {
            SpawnNovaSvetluska();
        }
    }

    void SpawnNovaSvetluska()
    {
        Vector3 spawnPozice = VypocitejSpawnPozici();
        GameObject novaSvetluska = Instantiate(svetluskaPrefab, spawnPozice, Quaternion.identity);
        Svetluska svetluskaScript = novaSvetluska.AddComponent<Svetluska>();
        svetluskaScript.manager = this;
    }

    private Vector3 VypocitejSpawnPozici()
    {
        int strana = Random.Range(0, 4);
        switch (strana)
        {
            case 0: return new Vector3(Random.Range(-spawnOkrajOffset.x, spawnOkrajOffset.x), spawnOkrajOffset.y, 0);
            case 1: return new Vector3(Random.Range(-spawnOkrajOffset.x, spawnOkrajOffset.x), -spawnOkrajOffset.y, 0);
            case 2: return new Vector3(-spawnOkrajOffset.x, Random.Range(-spawnOkrajOffset.y, spawnOkrajOffset.y), 0);
            case 3: return new Vector3(spawnOkrajOffset.x, Random.Range(-spawnOkrajOffset.y, spawnOkrajOffset.y), 0);
        }
        return Vector3.zero;
    }

    public void SvetluskaChycena(GameObject svetluska)
    {
        Destroy(svetluska);
        ScoreManager.instance?.PrictiBod();
        SpawnNovaSvetluska();
    }

    public void SvetluskaOdletela(GameObject svetluska)
    {
        Destroy(svetluska);
        LifeManager.instance?.OdeberZivot();
        SpawnNovaSvetluska();
    }
}

public class Svetluska : MonoBehaviour
{
    public SvetluskaManager manager; 
    private Vector3 cilovyBodStred;
    private Vector3 cilovyBodKonec;
    private bool letiDoStredu = true;
    private float rychlostPohybu = 5.0f;

    void Start()
    {
        cilovyBodStred = VygenerujNahodnyBodVRadiusu();
        cilovyBodKonec = VypocitejCilKonec();
    }

    void Update()
    {
        if (letiDoStredu)
        {
            transform.position = Vector3.MoveTowards(transform.position, cilovyBodStred, rychlostPohybu * Time.deltaTime);

            if (Vector3.Distance(transform.position, cilovyBodStred) < 0.1f)
                letiDoStredu = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cilovyBodKonec, rychlostPohybu * Time.deltaTime);

            if (Vector3.Distance(transform.position, cilovyBodKonec) < 0.1f)
            {
                manager.SvetluskaOdletela(gameObject);
            }
        }
    }

    void OnMouseDown()
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
