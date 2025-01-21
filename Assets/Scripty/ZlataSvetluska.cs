using UnityEngine;

public class ZlataSvetluska : MonoBehaviour
{
    public int nasobekBodu = 5;

    private void OnMouseDown()
    {
        if (ScoreManager.instance != null)
        {
            int zakladniBody = ScoreManager.instance.ZiskejZakladniBody();
            ScoreManager.instance.PrictiBod(zakladniBody * nasobekBodu);
        }
        Destroy(gameObject); 
    }
}