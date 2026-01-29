using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
   [Header("Configurações")]
    public float interactRange = 1.5f;
    public LayerMask interactLayer;

    void OnInteract(InputValue value)
    {   
        Debug.Log("Interagir");
        if (value.isPressed)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Collider2D foundObject = Physics2D.OverlapCircle(transform.position,interactRange,interactLayer);

        if(foundObject != null)
        {
            IIteractable interactable = foundObject.GetComponent<IIteractable>();

            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,interactRange);
    }


}
