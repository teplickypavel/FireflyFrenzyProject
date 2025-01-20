using UnityEngine;

public class ZlataSvetluska : MonoBehaviour
{
    private void OnMouseDown()
    {
        int nasobekBodu = 4;
        int zakladniBody = ScoreManager.instance.ZiskejZakladniBody(); // Z�sk�me z�kladn� hodnotu bod�

        ScoreManager.instance.PrictiBod(zakladniBody * nasobekBodu); // P�i�teme pouze n�soben� body
        Destroy(gameObject); // Zni��me sv�tlu�ku po chycen�
    }
}
