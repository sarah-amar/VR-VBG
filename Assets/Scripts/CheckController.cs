using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ButtonInteractionHandler : MonoBehaviour
{
    public ActionBasedController leftController;
    public ActionBasedController rightController;

    public InputActionProperty startActionLeft;
    public InputActionProperty xButtonLeft;
    public InputActionProperty menuButtonLeft;

    public PauseManager pauseManager;

    private Dictionary<string, System.Action<InputAction.CallbackContext>> actionCallbacks;

    private void OnEnable()
    {
        InitializeActionCallbacks();

        if (leftController != null)
        {
            RegisterActions(leftController, true);
            RegisterLeftSpecificActions();
        }
        else
        {
            Debug.LogError("Left controller is not assigned!");
        }

        if (rightController != null)
        {
            RegisterActions(rightController, false);
        }
        else
        {
            Debug.LogError("Right controller is not assigned!");
        }
    }

    private void OnDisable()
    {
        if (leftController != null)
        {
            UnregisterActions(leftController, true);
            UnregisterLeftSpecificActions();
        }

        if (rightController != null)
        {
            UnregisterActions(rightController, false);
        }
    }

    private void InitializeActionCallbacks()
    {
        actionCallbacks = new Dictionary<string, System.Action<InputAction.CallbackContext>>
        {
            { "OnSelectButtonPressed", OnSelectButtonPressed },
            { "OnActivateButtonPressed", OnActivateButtonPressed },
            { "OnUIPressButtonPressed", OnUIPressButtonPressed },
            { "OnGripButtonPressed", OnGripButtonPressed },
            { "OnPrimaryButtonPressed", OnPrimaryButtonPressed },
            { "OnSecondaryButtonPressed", OnSecondaryButtonPressed },
            { "OnMenuButtonPressed", OnMenuButtonPressed }
        };
    }

    private void RegisterActions(ActionBasedController controller, bool isLeftController)
    {
        controller.selectAction.action.performed += actionCallbacks["OnSelectButtonPressed"];
        controller.activateAction.action.performed += actionCallbacks["OnActivateButtonPressed"];
        controller.uiPressAction.action.performed += actionCallbacks["OnUIPressButtonPressed"];
        controller.selectActionValue.action.performed += actionCallbacks["OnGripButtonPressed"];
        controller.rotateAnchorAction.action.performed += actionCallbacks["OnPrimaryButtonPressed"];
        controller.translateAnchorAction.action.performed += actionCallbacks["OnSecondaryButtonPressed"];
        controller.scaleToggleAction.action.performed += actionCallbacks["OnMenuButtonPressed"];

        if (isLeftController)
        {
            if (startActionLeft != null && startActionLeft.action != null)
            {
                startActionLeft.action.performed += OnStartButtonPressed;
            }

            if (xButtonLeft != null && xButtonLeft.action != null)
            {
                xButtonLeft.action.performed += OnXButtonPressed;
            }

            if (menuButtonLeft != null && menuButtonLeft.action != null)
            {
                menuButtonLeft.action.performed += OnMenuButtonPressed;
            }
        }
    }

    private void UnregisterActions(ActionBasedController controller, bool isLeftController)
    {
        controller.selectAction.action.performed -= actionCallbacks["OnSelectButtonPressed"];
        controller.activateAction.action.performed -= actionCallbacks["OnActivateButtonPressed"];
        controller.uiPressAction.action.performed -= actionCallbacks["OnUIPressButtonPressed"];
        controller.selectActionValue.action.performed -= actionCallbacks["OnGripButtonPressed"];
        controller.rotateAnchorAction.action.performed -= actionCallbacks["OnPrimaryButtonPressed"];
        controller.translateAnchorAction.action.performed -= actionCallbacks["OnSecondaryButtonPressed"];
        controller.scaleToggleAction.action.performed -= actionCallbacks["OnMenuButtonPressed"];

        if (isLeftController)
        {
            if (startActionLeft != null && startActionLeft.action != null)
            {
                startActionLeft.action.performed -= OnStartButtonPressed;
            }

            if (xButtonLeft != null && xButtonLeft.action != null)
            {
                xButtonLeft.action.performed -= OnXButtonPressed;
            }

            if (menuButtonLeft != null && menuButtonLeft.action != null)
            {
                menuButtonLeft.action.performed -= OnMenuButtonPressed;
            }
        }
    }

    private void RegisterLeftSpecificActions()
    {
        if (startActionLeft != null && startActionLeft.action != null)
        {
            startActionLeft.action.performed += OnStartButtonPressed;
        }

        if (xButtonLeft != null && xButtonLeft.action != null)
        {
            xButtonLeft.action.performed += OnXButtonPressed;
        }

        if (menuButtonLeft != null && menuButtonLeft.action != null)
        {
            menuButtonLeft.action.performed += OnMenuButtonPressed;
        }
    }

    private void UnregisterLeftSpecificActions()
    {
        if (startActionLeft != null && startActionLeft.action != null)
        {
            startActionLeft.action.performed -= OnStartButtonPressed;
        }

        if (xButtonLeft != null && xButtonLeft.action != null)
        {
            xButtonLeft.action.performed -= OnXButtonPressed;
        }

        if (menuButtonLeft != null && menuButtonLeft.action != null)
        {
            menuButtonLeft.action.performed -= OnMenuButtonPressed;
        }
    }

    private void OnSelectButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Select button clicked!");
    }

    private void OnActivateButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Activate button clicked!");
    }

    private void OnUIPressButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("UI Press button clicked!");
    }

    private void OnGripButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Grip button clicked!");

        if (pauseManager != null)
        {
            pauseManager.TogglePause();
        }
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Primary button clicked!");
    }

    private void OnSecondaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Secondary button clicked!");
    }

    private void OnMenuButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Menu button clicked!");
    }

    private void OnStartButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Start button on left controller clicked!");
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("X button on left controller clicked!");

        if (pauseManager != null)
        {
            pauseManager.TogglePause();
        }
    }
}
