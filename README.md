# SimWinMouse
Simulate mouse events on Windows;  Easily move mouse cursor and click via .NET code.
This small library may be useful for accessibility, gaming, automation, and more.

## Usage
Add a reference to SimWinMouse.

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
