using UnityEngine;

public class ZlataSvetluska : MonoBehaviour
{
    private void OnMouseDown()
    {
        int nasobekBodu = 4;
        int zakladniBody = ScoreManager.instance.ZiskejZakladniBody(); // Získáme základní hodnotu bodù

        ScoreManager.instance.PrictiBod(zakladniBody * nasobekBodu); // Pøièteme pouze násobené body
        Destroy(gameObject); // Znièíme svìtlušku po chycení
    }
}
