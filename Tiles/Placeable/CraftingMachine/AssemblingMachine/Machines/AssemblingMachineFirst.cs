namespace FactorioMod.Tiles.Placeable.CraftingMachine.AssemblingMachine.Machines
{
    public class AssemblingMachineFirst : AssemblingMachine
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            BaseCraftingSpeed = 2;
            IngridientCountLimit = 1;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            base.Autoload(ref name, ref texture);
            return true;
        }
    }
}
