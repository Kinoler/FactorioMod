using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FactorioMod.Factorio.Interfaces;
using Terraria;

namespace FactorioMod.Factorio.Models
{
    public class AssemblingInventory : IEnumerable<Item>
    {
        public Item[] Items { get; set; }
        public int Count => Items.Length;

        public void CloneFrom(Item[] items)
        {
            Items = new Item[items.Length];
            for (int i = 0; i < Count; i++)
            {
                Items[i] = items[i].Clone();
                Items[i].stack = 0;
            }
        }

        public Item this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        public void Clear()
        {
            Items = new Item[0];
        }

        public IEnumerator<Item> GetEnumerator() => Items.ToList<Item>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
