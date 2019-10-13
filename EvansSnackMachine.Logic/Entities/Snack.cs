namespace EvansSnackMachine.Logic.Entities
{
    public class Snack : AggregateRoot
    {
        public string Name { get; private set; }

        public Snack(string name)
        {
            Name = name;
        }
    }
}
