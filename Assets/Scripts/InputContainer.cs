using UnityEngine;

[CreateAssetMenu(fileName = "InputContainer", menuName = "Custom/InputContainer")]
public class InputContainer : ScriptableObject
{

    private CustomInput _current;
    public CustomInput Current
    {
        get
        {
            if (_current == null)
            {
                _current = new CustomInput();
                _current.Enable();
            }
            return _current;
        }
    }

}