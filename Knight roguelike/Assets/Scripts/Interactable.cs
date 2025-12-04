using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public bool useEvents;
    public string promptMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BaseInteract()
    {
        if(useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }


        Interact();
    }

    protected virtual void Interact()
    {
        //Aici se da overwritten
    }
}
