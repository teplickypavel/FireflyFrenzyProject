using UnityEngine;

public class ChytaniSvetlusky : MonoBehaviour
{
    public GameObject sitkaPrefab;
    public float posunLeva = -0.5f;
    public float posunDolu = -0.3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 poziceMysi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            poziceMysi.z = 0;

            Vector3 poziceSitky = poziceMysi + new Vector3(posunLeva, posunDolu, 0);

            GameObject sitka = Instantiate(sitkaPrefab, poziceSitky, Quaternion.identity);


            ZkontrolujChytani(sitka);
        }
    }

    private void ZkontrolujChytani(GameObject sitka)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Svetluska svetluska = hit.collider.GetComponent<Svetluska>();
            if (svetluska != null)
            {
                svetluska.manager.SvetluskaChycena(hit.collider.gameObject);
            }
        }

        SitkaChytani sitkaScript = sitka.GetComponent<SitkaChytani>();
        if (sitkaScript != null)
        {
            sitkaScript.AktivujZmizeni();
        }
    }
}
