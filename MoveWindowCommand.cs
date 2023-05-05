using System.CommandLine;
using System.Runtime.InteropServices;
using System.Text;

internal class MoveWindowCommand : Command
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    public MoveWindowCommand() : base("move", "Move a on screen.")
    {
        var windowNameArgument = new Argument<string>("window-name", "The name of the window to move");
        Add( windowNameArgument);

        var rectCoordinatesArgument = new Argument<int[]>("rect", "The rectangle to move the window to.");
        Add( rectCoordinatesArgument);

        this.SetHandler( ( invocationContext ) =>
        {
            var windowName = invocationContext.ParseResult.GetValueForArgument<string>(windowNameArgument);
            var rect = invocationContext.ParseResult.GetValueForArgument<int[]>(rectCoordinatesArgument);

            IntPtr hWnd = FindWindow(null, windowName);
            
            bool result = SetWindowPos(hWnd, HWND_TOPMOST, rect[0], rect[1], rect[2], rect[3], 0);

            if (!result)
            {
                Console.WriteLine("Failed to set window position!");
            }
        });
    }
}