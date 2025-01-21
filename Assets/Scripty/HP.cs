using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public TextMeshProUGUI livesText;
    private int zivoty = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AktualizujText();
    }

    public void OdeberZivot()
    {
        zivoty--;

        if (zivoty <= 0)
        {
            zivoty = 0;
            GameOver();
        }

        AktualizujText();
    }

    private void AktualizujText()
    {
        livesText.text = " " + zivoty;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
