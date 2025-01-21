using UnityEngine;

public class ModraSvetluska : MonoBehaviour
{
    public float snizeniRychlosti = 1.0f;
    public float trvaniEfektu = 5.0f;

    private void OnMouseDown()
    {
        if (SvetluskaManager.instance != null)
        {
            SvetluskaManager.instance.AktivujDocasneSnizeniRychlosti(snizeniRychlosti, trvaniEfektu);
        }
        Destroy(gameObject);
    }
}
