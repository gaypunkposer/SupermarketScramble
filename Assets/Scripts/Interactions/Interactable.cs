namespace Interactions
{
    public interface IInteractable
    {
        string GetTooltip();
        void StartLookAt();
        void EndLookAt();
        void Interact();
    }
}
