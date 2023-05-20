using UnityEngine;
using UnityEngine.InputSystem;

public class GetActionInputExample : MonoBehaviour
{
    public InputActionReference testInput;
    // Start is called before the first frame update

    void Start()
    {
        testInput.action.performed += printPerformed;
        testInput.action.canceled += printCanceled;
    }

    private void printPerformed(InputAction.CallbackContext obj){
        print("listen to button" + obj);
    }

    private void printCanceled(InputAction.CallbackContext obj){
        print("canceled button" + obj);
    }
}
