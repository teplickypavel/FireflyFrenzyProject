using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CervenaSvetluska : MonoBehaviour
{
    public float radiusZniceni = 2.0f;

    private void OnMouseDown()
    {
        int celkoveBody = ZnicitOkoli();
        Destroy(gameObject);

        if (SvetluskaManager.instance != null)
        {
            for (int i = 0; i < celkoveBody; i++) 
            {
                SvetluskaManager.instance.SpawnNovaSvetluska();
            }
        }
    }

    private int ZnicitOkoli()
    {
        Collider2D[] objekty = Physics2D.OverlapCircleAll(transform.position, radiusZniceni);
        int celkoveBody = 0;

        foreach (Collider2D objekt in objekty)
        {
            if (objekt.gameObject == gameObject) continue; 

            if (objekt.GetComponent<ZlataSvetluska>() != null)
            {
                celkoveBody += 5;
            }
            else if (objekt.GetComponent<Svetluska>() != null)
            {
                celkoveBody += 1;
            }

            Destroy(objekt.gameObject);
        }

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.PrictiBod(celkoveBody);
        }

        return objekty.Length - 1; 
    }
}
