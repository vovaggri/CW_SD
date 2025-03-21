namespace CW1.Domain.CommandAndDecorator;

public class TimingCommandDecorator : ICommand
{
    private readonly ICommand _innerCommand;

    public TimingCommandDecorator(ICommand innerCommand)
    {
        _innerCommand = innerCommand;
    }
    
    public void Execute()
    {
        var time = System.Diagnostics.Stopwatch.StartNew();
        _innerCommand.Execute();
        time.Stop();
        Console.WriteLine($"Time in milliseconds: {time.ElapsedMilliseconds} ms");
    }
}