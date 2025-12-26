
using UnityEngine;

using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
// Adds a visual label in the Unity Inspector for organization
[Header("Input Action Asset")]
// Allows you to drag and drop your .inputactions file into this slot in the Inspector
[SerializeField] private InputActionAsset playerControls;

```
// Adds a visual label for the action map section
[Header("Action Map Name Reference")]
// The name of the specific Map (like "Player" or "Menu") within your Input Asset
[SerializeField] private string actionMapName = "Player";

// Adds a visual label for the individual action names
[Header("Action Name References")]
// The string ID used to find the "movement" action in your asset
[SerializeField] private string movement = "movement";
// The string ID used to find the "rotation" action in your asset
[SerializeField] private string rotation = "rotation";
// The string ID used to find the "Jump" action in your asset
[SerializeField] private string jump = "Jump";
// The string ID used to find the "Sprint" action in your asset
[SerializeField] private string sprint = "Sprint";

// Internal variable to store the found movement action data
private InputAction movementAction;
// Internal variable to store the found rotation action data
private InputAction rotationAction;
// Internal variable to store the found jump action data
private InputAction jumpAction;
// Internal variable to store the found sprint action data
private InputAction sprintAction;

// A public variable other scripts can read (but not change) to see move direction
public Vector2 MovementInput { get; private set; }

// A public variable other scripts can read to see camera/look direction
public Vector2 RotationInput { get; private set; }

// A true/false value showing if the player is currently jumping
public bool JumpTriggered { get; private set; }

// A true/false value showing if the player is currently sprinting
public bool SprintTriggered { get; private set; }

// Runs when the script instance is first loaded
private void Awake()
{
    // Searches the Input Asset for the specific Map name provided
    InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

    // Finds the "movement" action inside that map and stores it
    movementAction = mapReference.FindAction(movement);
    // Finds the "rotation" action inside that map and stores it
    rotationAction = mapReference.FindAction(rotation);
    // Finds the "jump" action inside that map and stores it
    jumpAction = mapReference.FindAction(jump);
    // Finds the "sprint" action inside that map and stores it
    sprintAction = mapReference.FindAction(sprint);

    // Calls the custom function below to start listening for button presses
    SubscribeActionValuesToInputEvents();
}

// Sets up the "Listeners" that react when keys are pressed or released
private void SubscribeActionValuesToInputEvents()
{
    // When movement keys are held, update MovementInput with the X/Y values
    movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
    // When movement keys are released, set the MovementInput back to zero
    movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

    // When the mouse or stick moves, update the rotation values
    rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
    // When the mouse or stick stops moving, set rotation back to zero
    rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

    // When the jump button is pressed, set JumpTriggered to true
    jumpAction.performed += inputInfo => JumpTriggered = true;
    // When the jump button is released, set JumpTriggered to false
    jumpAction.canceled += inputInfo => JumpTriggered = false;
}

// Runs whenever the script/object is turned on
private void OnEnable()
{
    // Tells the Input System to start actively listening for inputs on this map
    playerControls.FindActionMap(actionMapName).Enable();
}

// Runs whenever the script/object is turned off
private void OnDisable()
{
    // Tells the Input System to stop listening for inputs on this map
    playerControls.FindActionMap(actionMapName).Disable();
}

```

}
