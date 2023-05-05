using System.CommandLine;
using System.Runtime.InteropServices;
using System.Text;

internal class ListCommand : Command
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

    [DllImport("user32.dll")]
    static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public ListCommand() : base("list", "List all top-level window names")
    {
        this.SetHandler( (InvokationContext) =>
        {
            List<string> windowNames = new List<string>();

            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                StringBuilder sb = new StringBuilder(256);
                GetWindowText(hWnd, sb, sb.Capacity);
                string windowName = sb.ToString();
                if (!string.IsNullOrEmpty(windowName))
                {
                    windowNames.Add(windowName);
                }
                return true;
            }, IntPtr.Zero);

            foreach (string windowName in windowNames)
            {
                Console.WriteLine(windowName);
            }
        });
    }
}