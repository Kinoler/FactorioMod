using System.ComponentModel;
using System.Runtime.CompilerServices;
using FactorioMod.Annotations;
using FactorioMod.Factorio.Interfaces;
using Terraria;

namespace FactorioMod.Factorio
{
    public class MachineInventory : IStore, INotifyPropertyChanged
    {
        public Item[] Items { get; set; }

        public void CloneFrom(Item[] items)
        {
            Items = new Item[items.Length];
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].CloneDefaults(items[i].type);
            }
        }

        public void Clear()
        {
            Items = null;
        }

        public Item[] GetItems()
        {
            return Items;
        }

        public bool SetItem(int index, Item item)
        {
            if (Items[index] != null)
                return false;

            Items[index] = item;
            return true;
        }

        public void UpdateCount(int index, int newStack)
        {
            Items[index].stack = newStack;
            OnPropertyChanged(nameof(Items));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
