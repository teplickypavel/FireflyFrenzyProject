using UnityEngine;

public class DuhovaSvetluska : MonoBehaviour
{
    public float nasobekBodu = 5.0f;
    public float trvaniEfektu = 5.0f;

    private void OnMouseDown()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AktivujNasobeniBodu((int)nasobekBodu, trvaniEfektu);
        }
        Destroy(gameObject);
    }
}
