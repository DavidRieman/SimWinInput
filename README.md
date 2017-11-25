# SimWinMouse
Simulate mouse events on Windows;  Easily move mouse cursor and click via .NET code.
This small library may be useful for accessibility, gaming, automation, and more.

## Usage
Add a reference to SimWinMouse.

To immediately move the mouse to the pixel at (100,50) and left-click:
```
SimMouse.SimulateClick(100, 50, MouseButtons.Left);
```

To click and drag from (20, 20) to (50, 50):
```
SimMouse.Simulate(20, 20, SimMouse.Action.LeftButtonDown);
Thread.Sleep(10);
SimMouse.Simulate(50, 50, SimMouse.Action.LeftButtonUp);
```
