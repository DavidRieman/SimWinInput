# SimWinInput
Simulate mouse and keyboard (and eventually gamepad) events on Windows.
These provide convenient, small, easy-to-use APIs so you don't have to muck around in messy interop directly.

Purpose: Accessibility, automation, augmented gaming, and more.


## Simulate Mouse
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
