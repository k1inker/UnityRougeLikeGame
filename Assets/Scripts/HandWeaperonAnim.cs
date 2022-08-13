using UnityEngine;

public class HandWeaperonAnim : MonoBehaviour
{
    [SerializeField] private InteractObject _interactObject;

    public void PressButtonAnimation()
    {
        _interactObject.EquipObject();
    }
}
