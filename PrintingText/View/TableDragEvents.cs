using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections;

namespace PrintingText.View
{
    public class TableDragEventArgs : RoutedEventArgs
    {
        public ICollection table = null;
        protected TableDragEventArgs(RoutedEvent routedEvent, object source, ICollection tables) :
            base(routedEvent, source)
        {
            table = tables;
        }
        public ICollection Tables
        {
            get
            {
                return table;
            }
        }
    }

    public delegate void TableDragEventHandler(object sender, TableDragEventArgs e);
    public class TableDragStartedEventArgs : TableDragEventArgs
    {
        internal TableDragStartedEventArgs(RoutedEvent routedEvent, object source, ICollection tables) :
            base(routedEvent, source, tables)
        {
        }
        public bool Cancel { get; set; } = false;
    }

    public delegate void TableDragStartedEventHandler(object sender, TableDragStartedEventArgs e);
    public class TableDraggingEventArgs : TableDragEventArgs
    {
        public double horizontalChange = 0;
        public double verticalChange = 0;
        internal TableDraggingEventArgs(RoutedEvent routedEvent, object source, ICollection tables, double horizontalChange, double verticalChange) :
            base(routedEvent, source, tables)
        {
            this.horizontalChange = horizontalChange;
            this.verticalChange = verticalChange;
        }
        public double HorizontalChange
        {
            get
            {
                return horizontalChange;
            }
        }
        public double VerticalChange
        {
            get
            {
                return verticalChange;
            }
        }
    }

    public delegate void TableDraggingEventHandler(object sender, TableDraggingEventArgs e);
    public class TableDragCompletedEventArgs : TableDragEventArgs
    {
        public TableDragCompletedEventArgs(RoutedEvent routedEvent, object source, ICollection tables) :
            base(routedEvent, source, tables)
        {
        }
    }
    public delegate void TableDragCompletedEventHandler(object sender, TableDragCompletedEventArgs e);
}
