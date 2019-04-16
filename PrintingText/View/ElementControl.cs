using PrintingText.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrintingText.View
{
    internal class ElementControl : ListBox
    {
        public ElementControl()
        {
            Focusable = false;
        }

        internal ItemTable FindAssociatedTable(object TableDataContext)
        {
            return (ItemTable)this.ItemContainerGenerator.ContainerFromItem(TableDataContext);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ItemTable();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ItemTable;
        }
    }
}