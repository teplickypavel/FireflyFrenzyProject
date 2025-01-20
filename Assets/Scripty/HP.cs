    using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public Text livesText;
    private int zivoty = 1;

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
