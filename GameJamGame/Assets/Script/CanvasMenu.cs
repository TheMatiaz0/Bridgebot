using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviourPlus
{
    public enum MenuType
    {
        UpDown,
        LeftRight
    }


    [Serializable]
    [ShowCyberInspector]
    protected struct ButtonWithEvent
    {
        [field: SerializeField]
        public Text ButtonText { get; private set; }
        [field: SerializeField]
        public UnityEvent OnClick { get; private set; }
    }

    [SerializeField]
    protected ButtonWithEvent[] buttons = null;
    [SerializeField]
    private Color selectColor = Color.yellow;
    [SerializeField]
    private Color nonSelectColor = Color.white;

    [SerializeField]
    private MenuType currentMenuType = MenuType.UpDown;

    private InputActions inputAction;

    private Cint selectId = 0;

    protected new void Awake()
    {
        base.Awake();
        inputAction = new InputActions();
    }

    protected void OnEnable()
    {
        inputAction.Enable();
    }
    protected void OnDisable()
    {
        inputAction.Disable();
    }


    protected virtual void Start()
    {
        foreach (ButtonWithEvent item in buttons)
        {
            Button trueButton = item.ButtonText.GetComponentInChildren<Button>();
            if (trueButton == null)
                continue;
            trueButton.onClick.AddListener(() => item.OnClick.Invoke());
            EventTrigger trigger = item.ButtonText.gameObject.TryGetElseAdd<EventTrigger>();
            trigger.Add(EventTriggerType.PointerEnter, (b) => selectId = (uint)buttons.GetIndex(element => element.ButtonText == item.ButtonText));

        }
    }

    protected virtual void Update()
    {
        if (buttons.Length == 0)
            return;

        switch (currentMenuType)
        {
            case MenuType.UpDown:
                CheckMovementY();
                break;

            case MenuType.LeftRight:
                CheckMovementX();
                break;
        }

        if (selectId >= buttons.Length)
            selectId = (Cint)(uint)(buttons.Length - 1);

        for (int x = 0; x < buttons.Length; x++)
            buttons[x].ButtonText.color = (selectId == x) ? selectColor : nonSelectColor;

        if (inputAction.MainMenu.Click.triggered)
            buttons[selectId].OnClick.Invoke();

    }

    private void CheckMovementX()
    {
        if (inputAction.MainMenu.Left.triggered)
        {
            selectId--;
        }

        else if (inputAction.MainMenu.Right.triggered)
        {
            selectId++;
        }
    }

    private void CheckMovementY ()
    {
        if (inputAction.MainMenu.Up.triggered)
        {
            selectId--;
        }

        else if (inputAction.MainMenu.Down.triggered)
        {
            selectId++;
        }
    }
}