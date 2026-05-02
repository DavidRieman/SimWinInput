# SimWinKeyboard

## Usage
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
