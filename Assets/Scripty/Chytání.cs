using UnityEngine;

public class ChytaniSvetlusky : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ZkontrolujChytani();
        }
    }

    private void ZkontrolujChytani()
    {
        Vector3 poziceMysi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        poziceMysi.z = 0;

        RaycastHit2D zasah = Physics2D.Raycast(poziceMysi, Vector2.zero);

        if (zasah.collider != null)
        {
            Svetluska svetluska = zasah.collider.GetComponent<Svetluska>();
            if (svetluska != null)
            {
                svetluska.manager.SvetluskaChycena(zasah.collider.gameObject);
            }
        }
    }
}
