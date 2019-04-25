namespace Assets.Code.Classes.Command
{
    public interface ICommand
    {
        void Execute();

        void Unexecute();
    }
}
