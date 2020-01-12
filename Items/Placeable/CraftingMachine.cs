using FactorioMod.Factorio;

namespace FactorioMod.Items.Placeable
{
    public class CraftingMachine : Entity
    {
        private MachineBase machine;

        private string[] crafting_categories = new string[]{};
        private double _craftingSpeed;

        private EnergySource energy_source;
        private Energy energy_usage;

        public CraftingMachine(double craftingSpeed = 1)
        {
            _craftingSpeed = craftingSpeed;
            this.machine = new MachineBase(_craftingSpeed);
        }

        public override bool NewRightClick(int i, int j)
        {

            return true;
        }
    }
}
