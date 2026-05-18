using UnityEngine;

public class TrashZone : MonoBehaviour
{
    //1 beluga / zone
    public bool isBelugaTrappedInside = false;
    public Beluga currentBelugaTrapped;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.GetComponent<CircleCollider2D>().radius);
    }
}
