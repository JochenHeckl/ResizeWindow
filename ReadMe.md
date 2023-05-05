A little tool to position  windows around your screen from the console.

Example

```
dotnet run -- list
```

will dump the window names to the console.

```
dotnet run -- move "This is a test window" 0, 0, 1920, 1080
```
will move the window with the name "This is a test window" to the top left corner of the screen and make it fit 1080p.