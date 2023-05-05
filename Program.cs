using System.CommandLine;

class Program
{
    static void Main(string[] args)
    {
        var rootCommand = new RootCommand();
        rootCommand.Add( new ListCommand() );
        rootCommand.Add( new MoveWindowCommand() );

        rootCommand.InvokeAsync(args).Wait();
    }
}
