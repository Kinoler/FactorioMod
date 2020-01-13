using System.Collections.Generic;
using Terraria;

namespace FactorioMod.Factorio.Interfaces
{
    public interface IStore : IEnumerable<Item>
    {
        Item[] GetItems();

        bool SetItem(int index, Item item);
    }
}
