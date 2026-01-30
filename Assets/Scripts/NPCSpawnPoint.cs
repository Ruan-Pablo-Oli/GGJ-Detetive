using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{
    private void OawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,0.5f);      
    }
}
