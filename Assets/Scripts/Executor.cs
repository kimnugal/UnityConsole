using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Executor : MonoBehaviour
{
    [SerializeField]
    InputField input;

    public bool InputReady { get; private set; }
    string str;
    public string String
    {
        get
        {
            InputReady = false;
            return str;
        }
        set
        {
            str = value;
            InputReady = true;
        }
    }

    void Update()
    {
        if (!input.isFocused)
        {
            EventSystem.current.SetSelectedGameObject(input.gameObject, null);
            input.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            Enter();
    }
    public void Enter()
    {
        String = input.text;
        input.text = "";
    }
}