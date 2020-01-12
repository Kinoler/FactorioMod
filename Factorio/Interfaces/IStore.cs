using Terraria;

namespace FactorioMod.Factorio.Interfaces
{
    public interface IStore
    {
        Item[] GetItems();

        bool SetItem(int index, Item item);
    }
}
