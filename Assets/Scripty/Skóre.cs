using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int skore = 0;
    private int nasobitelBodu = 1;
    private Coroutine nasobeniCoroutine;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PrictiBod(int pocetBodu = 1)
    {
        skore += pocetBodu * nasobitelBodu;
        scoreText.text = skore.ToString();
    }

    public int ZiskejZakladniBody()
    {
        return 1;
    }

    public void AktivujNasobeniBodu(int nasobek, float trvani)
    {
        if (nasobeniCoroutine != null) StopCoroutine(nasobeniCoroutine);
        nasobeniCoroutine = StartCoroutine(NasobeniBoduCoroutine(nasobek, trvani));
    }

    private IEnumerator NasobeniBoduCoroutine(int nasobek, float trvani)
    {
        nasobitelBodu = nasobek;
        yield return new WaitForSeconds(trvani);
        nasobitelBodu = 1;
    }

    public void AktivujDocasneSnizeniRychlosti(float snizeni, float trvani)
    {
        StartCoroutine(DocasneSnizeniRychlosti(snizeni, trvani));
    }

    private IEnumerator DocasneSnizeniRychlosti(float snizeni, float trvani)
    {
        nasobitelBodu -= (int)snizeni;
        yield return new WaitForSeconds(trvani);
        nasobitelBodu += (int)snizeni;
    }
}
