using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Crafting.AssemblingMachine;
using FactorioMod.Factorio.Crafting.Furnace;
using FactorioMod.Tiles.Placeable.CraftingMachine;
using FactorioMod.Tiles.Placeable.CraftingMachine.Furnace;
using FactorioMod.Tiles.Placeable.CraftingMachine.AssemblingMachine;

namespace FactorioMod.Factorio.Helpers
{
    public static class FactoryHelper
    {
        public static CraftingMachineState CraftingMachineStateFactory(CraftingMachine machine)
        {
            switch (machine)
            {
                case AssemblingMachine _:
                    return new AssemblingMachineState(machine.BaseCraftingSpeed);
                case Furnace _:
                    return new FurnaceState(machine.BaseCraftingSpeed);
                default:
                    return null;
            }
        }
    }
}
