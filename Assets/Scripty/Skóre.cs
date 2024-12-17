using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    public Text scoreText;
    private int skore;

    void Awake()
    {
        if (instance == null) instance = this; 
        else Destroy(gameObject); 
    }

    public void PrictiBod() => scoreText.text = (++skore).ToString();
}
