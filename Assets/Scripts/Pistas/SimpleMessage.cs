using UnityEngine;

public class SimpleMessage : MonoBehaviour, IIteractable
{
    public string message = "Isso é uma pista!";

    public void Interact()
    {
        Debug.Log("O jogador LEU: " + message);
    }
}
