using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SvetluskaManager : MonoBehaviour
{
    public GameObject svetluskaPrefab;   // Normální svìtluška
    public GameObject modraSvetluskaPrefab;
    public GameObject cervenaSvetluskaPrefab;
    public GameObject zlataSvetluskaPrefab;
    public GameObject duhovaSvetluskaPrefab;

    public int pocetSvetlusek = 3;
    public float rychlostPohybu = 5.0f;
    public float radiusStred = 3.0f;
    public Vector2 spawnOkrajOffset = new Vector2(8.0f, 6.0f);

    private void Start()
    {
        for (int i = 0; i < pocetSvetlusek; i++)
        {
            SpawnNovaSvetluska();
        }
    }

    private void SpawnNovaSvetluska()
    {
        Vector3 spawnPozice = VypocitejSpawnPozici();
        GameObject prefab = VyberPrefabSvetlusky();

        GameObject novaSvetluska = Instantiate(prefab, spawnPozice, Quaternion.identity);
        Svetluska svetluskaScript = novaSvetluska.AddComponent<Svetluska>();
        svetluskaScript.manager = this;
    }

    private GameObject VyberPrefabSvetlusky()
    {
        float sance = Random.Range(0f, 100f); // Náhodné èíslo 0 - 100

        if (sance < 0.05f) return duhovaSvetluskaPrefab;    // 0.05% šance na duhovou svìtlušku
        if (sance < 0.5f) return zlataSvetluskaPrefab;     // 0.5% šance na zlatou (0.05% + 0.5%)
        if (sance < 2.55f) return cervenaSvetluskaPrefab;   // 2% šance na èervenou (0.05% + 0.5% + 2%)
        if (sance < 5.05f) return modraSvetluskaPrefab;     // 2.5% šance na modrou (0.05% + 0.5% + 2% + 2.5%)

        return svetluskaPrefab;  // Normální svìtluška (zbylých 94.95%)
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

    private void Start()
    {
        cilovyBodStred = VygenerujNahodnyBodVRadiusu();
        cilovyBodKonec = VypocitejCilKonec();
    }

    private void Update()
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
