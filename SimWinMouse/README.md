# SimWinMouse

NOTE: The modern .NET `System.Windows.Forms.Cursor` provides an effective means to control the mouse cursor position by setting the `Cursor.Position` property.
If mouse _positioning_ is all that you need, try `Cursor.Position` first. However, `SimWinMouse` will help you accomplish additional scenarios, such as clicking.

## Usage
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
