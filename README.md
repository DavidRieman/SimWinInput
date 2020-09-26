# SimWinInput
Simulate mouse, keyboard, and GamePad events on Windows.
These provide convenient, small, easy-to-use APIs so you don't have to muck around in messy interop directly.

Purpose: Accessibility, automation, augmented gaming, and more.


## Simulate Mouse
NOTE: The modern .NET `System.Windows.Forms.Cursor` provides an effective means to control the mouse cursor position by setting the `Cursor.Position` property. If mouse _positioning_ is all that you need, try `Cursor.Position` first. However, `SimWinMouse` will help you accomplish additional scenarios, such as clicking.

Install `SimWinMouse` [via NuGet](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package)
 or pull the source and add a project reference.

To immediately move the mouse to the pixel at (100,50) and left-click:
```
SimMouse.Click(MouseButtons.Left, 100, 50);
```

To click and drag from (20, 20) to (50, 50):
```
SimMouse.Act(SimMouse.Action.LeftButtonDown, 20, 20);
Thread.Sleep(10);
SimMouse.Act(SimMouse.Action.LeftButtonUp, 50, 50);
```

### Advanced Mouse Simulation
If you have more advanced scenarios than the simple API above supports, check out the `InteropMouse.mouse_event` DLL import.


## Simulate Keyboard
Install `SimWinKeyboard` [via NuGet](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package)
 or pull the source and add a project reference.

To simulate a 'Q' keystroke:
```
SimKeyboard.Press((byte)'Q');
```

To hold the 'Q' key for a prolonged time until later releasing it:
```
SimKeyboard.KeyDown((byte)'Q');
...
SimKeyboard.KeyUp((byte)'Q');
```

### Advanced Keyboard Simulation
If you have more advanced scenarios than the simple API above supports, check out the `InteropKeyboard.keybd_event` DLL import.


## Simulate GamePad
Install `SimWinGamePad` [via NuGet](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package)
 or pull the source and add a project reference.

Before issuing other commands, call `SimGamePad.Instance.Initialize()`.

Upon first initialization on a given PC, the user may be prompted to accept automatic installation of the [ScpVBus](https://github.com/nefarius/ScpVBus) driver required to simulate Xbox 360 GamePads attached to Windows.
(This is accomplished via the [ScpDriverInterface](https://github.com/DavidRieman/ScpDriverInterface/) installer, which is now an embedded resource in SimWinInput and extracted at runtime as needed. Thus you no longer need to explicitly include the installer executable with your own application/installers.)
Recovery from missing driver requires neither reboot nor app restart, but Initialize will throw an exception if something prevents success (such as the user declining the prompt or UAC elevation for the installer).

There can be up to four simulated GamePads, but they do not start plugged in. To plug one in as the first GamePad:
```
SimGamePad.Instance.PlugIn();
```

Then, to simulate pressing the 'A' button on the GamePad for a moment, before releasing it:
```
SimGamePad.Instance.Use(GamePadControl.A);
```

Even the analog controls can be simulated into maximum state for a moment, before returning them to their default, unheld positions:
```
// Pull and release the left trigger.
SimGamePad.Instance.Use(GamePadControl.LeftTrigger);
// Move the right analog stick into the leftmost position, then return to neutral position.
SimGamePad.Instance.Use(GamePadControl.RightStickLeft);
```

To unplug the virtual GamePad:
```
SimGamePad.Instance.Unplug();
```

When exiting the program, you should always call `SimGamePad.Instance.ShutDown()`.
This will unplug any remaining simulated GamePads and clean up any utilized resources, such as disposing the driver.

### Intermediate GamePad Simulation
Should you need more than one GamePad simulated, each command can optionally specify an index from 0 to 3 for which one to drive. You can also specify hold times (in milliseconds) if the default is not suitable for your needs:
```
SimGamePad.Instance.Use(GamePadControl.LeftTrigger, 2, 500);
```

You can use GamePadControl as flags to designate multiple controls to use at the same time.
You can also simulate controls in a held position until later asking to release them.
This example demonstrates both:
```
SimGamePad.Instance.SetControl(GamePadControl.RightTrigger | GamePadControl.RightBumper);
...
SimGamePad.Instance.ReleaseControl(GamePadControl.RightTrigger | GamePadControl.RightBumper);
```


### Advanced GamePad Simulation
For more advanced scenarios, you can exercise full control over all analog controls and buttons, including the guide button, by modifying and managing state updates manually. For example:
```
// Get a reference to the state of the first GamePad:
var simPad = SimGamePad.Instance;
var state = simPad.State[0];
// Pull the left trigger halfway back:
state.LeftTrigger = 127;
// Move the right analog stick three quarters of the way to the left:
state.RightStickX = short.MinValue * 3 / 4;
// Add the RightBumber to the set of held buttons:
state.Buttons |= GamePadControl.RightShoulder;
// Update the driver's simulated state with the above state changes:
simPad.Update(0);
...
// Reset the GamePad to the natural at-rest state:
state.Reset();
simPad.Update();
```
