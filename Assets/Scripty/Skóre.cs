using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    private int skore;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PrictiBod(int pocetBodu = 1)
    {
        skore += pocetBodu;
        scoreText.text = skore.ToString();
    }

    public int ZiskejZakladniBody()
    {
        return 1; // Pokud máš jiný základní poèet bodù, zmìò tuto hodnotu
    }

}
