using EvansSnackMachine.Logic.Entities;

namespace EvansSnackMachine.Persistence.Models
{
    public class SnackDBO
    {
        public string Name { get; set; }

        public SnackDBO(Snack snack)
        {
            this.Name = snack.Name;
        }
    }
}
