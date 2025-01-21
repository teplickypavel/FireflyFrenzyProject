using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SvetluskaManager : MonoBehaviour
{
    public static SvetluskaManager instance;
    public GameObject svetluskaPrefab;
    public GameObject modraSvetluskaPrefab;
    public GameObject cervenaSvetluskaPrefab;
    public GameObject zlataSvetluskaPrefab;
    public GameObject duhovaSvetluskaPrefab;

    public int pocetSvetlusek = 3;
    public float rychlostPohybu = 5.0f;
    public float radiusStred = 3.0f;
    public Vector2 spawnOkrajOffset = new Vector2(8.0f, 6.0f);
    private Coroutine snizeniRychlostiCoroutine;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < pocetSvetlusek; i++)
        {
            SpawnNovaSvetluska();
        }
    }

    public void SpawnNovaSvetluska()
    {
        Vector3 spawnPozice = VypocitejSpawnPozici();
        GameObject prefab = VyberPrefabSvetlusky();
        GameObject novaSvetluska = Instantiate(prefab, spawnPozice, Quaternion.identity);
        Svetluska svetluskaScript = novaSvetluska.AddComponent<Svetluska>();
        svetluskaScript.manager = this;
    }

    private GameObject VyberPrefabSvetlusky()
    {
        float sance = Random.Range(0f, 100f);
        if (sance < 0.05f) return duhovaSvetluskaPrefab;
        if (sance < 0.5f) return zlataSvetluskaPrefab;
        if (sance < 2.55f) return cervenaSvetluskaPrefab;
        if (sance < 5.05f) return modraSvetluskaPrefab;
        return svetluskaPrefab;
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
        SpawnNovaSvetluska();
        LifeManager.instance?.OdeberZivot();

    }

    public void AktivujDocasneSnizeniRychlosti(float snizeni, float trvani)
    {
        if (snizeniRychlostiCoroutine != null) StopCoroutine(snizeniRychlostiCoroutine);
        snizeniRychlostiCoroutine = StartCoroutine(DocasneSnizeniRychlosti(snizeni, trvani));
    }

    private IEnumerator DocasneSnizeniRychlosti(float snizeni, float trvani)
    {
        rychlostPohybu -= snizeni;
        yield return new WaitForSeconds(trvani);
        rychlostPohybu += snizeni;
    }
}
