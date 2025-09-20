using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action that modifies the state of a flag in response to game events.
    /// </summary>
    /// <remarks>This class provides methods to handle success and failure scenarios by triggering
    /// corresponding game events  and updating the visual state of the flag. It is intended to be used as a base class
    /// for flag-related behaviors.</remarks>
    public class FlagAction : MonoBehaviour
    {
        protected void PassAction()
        {
            GameEvents.CorrectPass();
            GetComponent<Renderer>().material.color = Color.green;
        }

        protected void FailAction()
        {
            GameEvents.IncorrectPass();
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
