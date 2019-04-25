namespace com.paintpuzzle
{
    public interface ICommand
    {
        void Execute();

        void Unexecute();
    }
}
