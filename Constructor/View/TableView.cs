using Constructor.Model;
using Constructor.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using View;

namespace Constructor.View 
{
    public partial class TableView : Control
    {
        private static readonly DependencyPropertyKey TablesPropertyKey =
           DependencyProperty.RegisterReadOnly("Tables", typeof(ObservableCollection<object>), typeof(TableView),
               new FrameworkPropertyMetadata());
        public static readonly DependencyProperty TablesProperty = TablesPropertyKey.DependencyProperty;

        public static readonly DependencyProperty TablesSourceProperty =
            DependencyProperty.Register("TablesSource", typeof(IEnumerable), typeof(TableView),
                new FrameworkPropertyMetadata(TablesSource_PropertyChanged));

        public static readonly DependencyProperty IsClearSelectionOnEmptySpaceClickEnabledProperty =
            DependencyProperty.Register("IsClearSelectionOnEmptySpaceClickEnabled", typeof(bool), typeof(TableView),
                new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty EnableTableDraggingProperty =
            DependencyProperty.Register("EnableTableDragging", typeof(bool), typeof(TableView),
                new FrameworkPropertyMetadata(true));

        private static readonly DependencyPropertyKey IsDraggingTablePropertyKey =
            DependencyProperty.RegisterReadOnly("IsDraggingTable", typeof(bool), typeof(TableView),
                new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsDraggingTableProperty = IsDraggingTablePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey IsNotDraggingTablePropertyKey =
            DependencyProperty.RegisterReadOnly("IsNotDraggingTable", typeof(bool), typeof(TableView),
                new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty IsNotDraggingTableProperty = IsDraggingTablePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey IsDraggingPropertyKey =
            DependencyProperty.RegisterReadOnly("IsDragging", typeof(bool), typeof(TableView),
                new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey IsNotDraggingPropertyKey =
            DependencyProperty.RegisterReadOnly("IsNotDragging", typeof(bool), typeof(TableView),
                new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty IsNotDraggingProperty = IsNotDraggingPropertyKey.DependencyProperty;

        public static readonly DependencyProperty TableProperty =
            DependencyProperty.Register("TableTemplate", typeof(DataTemplate), typeof(TableView));

        public static readonly DependencyProperty TableSelectorProperty =
            DependencyProperty.Register("TableSelector", typeof(DataTemplateSelector), typeof(TableView));

        public static readonly DependencyProperty TableStyleProperty =
            DependencyProperty.Register("TableStyle", typeof(Style), typeof(TableView));

        public static readonly RoutedEvent TableDragStartedEvent =
            EventManager.RegisterRoutedEvent("TableDragStarted", RoutingStrategy.Bubble, typeof(TableDragStartedEventHandler), typeof(TableView));

        public static readonly RoutedEvent TableDraggingEvent =
            EventManager.RegisterRoutedEvent("TableDragging", RoutingStrategy.Bubble, typeof(TableDraggingEventHandler), typeof(TableView));

        public static readonly RoutedEvent TableDragCompletedEvent =
            EventManager.RegisterRoutedEvent("TableDragCompleted", RoutingStrategy.Bubble, typeof(TableDragCompletedEventHandler), typeof(TableView));

        public static readonly RoutedCommand SelectAllCommand = null;
        public static readonly RoutedCommand SelectNoneCommand = null;
        public static readonly RoutedCommand InvertSelectionCommand = null;
        public static readonly RoutedCommand CancelConnectionDraggingCommand = null;


        private ElementControl TableControl = null;

        private List<object> initialSelectedTable = null;

        public TableView()
        {
            Table = new ObservableCollection<object>();
            Background = Brushes.White;

            AddHandler(ItemTable.TableDragStartedEvent, new TableDragStartedEventHandler(Table_DragStarted));
            AddHandler(ItemTable.TableDraggingEvent, new TableDraggingEventHandler(Table_Dragging));
            AddHandler(ItemTable.TableDragCompletedEvent, new TableDragCompletedEventHandler(Table_DragCompleted));
        }

        public event TableDragStartedEventHandler TableDragStarted
        {
            add { AddHandler(TableDragStartedEvent, value); }
            remove { RemoveHandler(TableDragStartedEvent, value); }
        }

        public event TableDraggingEventHandler TableDragging
        {
            add { AddHandler(TableDraggingEvent, value); }
            remove { RemoveHandler(TableDraggingEvent, value); }
        }

        public event TableDragCompletedEventHandler TableDragCompleted
        {
            add { AddHandler(TableDragCompletedEvent, value); }
            remove { RemoveHandler(TableDragCompletedEvent, value); }
        }

        public ObservableCollection<object> Table
        {
            get
            {
                return (ObservableCollection<object>)GetValue(TablesProperty);
            }
            private set
            {
                SetValue(TablesPropertyKey, value);
            }
        }

        public IEnumerable TablesSource
        {
            get
            {
                return (IEnumerable)GetValue(TablesSourceProperty);
            }
            set
            {
                SetValue(TablesSourceProperty, value);
            }
        }

        public bool IsClearSelectionOnEmptySpaceClickEnabled
        {
            get
            {
                return (bool)GetValue(IsClearSelectionOnEmptySpaceClickEnabledProperty);
            }
            set
            {
                SetValue(IsClearSelectionOnEmptySpaceClickEnabledProperty, value);
            }
        }

        public bool EnableTableDragging
        {
            get
            {
                return (bool)GetValue(EnableTableDraggingProperty);
            }
            set
            {
                SetValue(EnableTableDraggingProperty, value);
            }
        }

        public bool IsDraggingTable
        {
            get
            {
                return (bool)GetValue(IsDraggingTableProperty);
            }
            private set
            {
                SetValue(IsDraggingTablePropertyKey, value);
            }
        }


        public bool IsNotDraggingTable
        {
            get
            {
                return (bool)GetValue(IsNotDraggingTableProperty);
            }
            private set
            {
                SetValue(IsNotDraggingTablePropertyKey, value);
            }
        }

        public bool IsDragging
        {
            get
            {
                return (bool)GetValue(IsDraggingProperty);
            }
            private set
            {
                SetValue(IsDraggingPropertyKey, value);
            }
        }

        public bool IsNotDragging
        {
            get
            {
                return (bool)GetValue(IsNotDraggingProperty);
            }
            private set
            {
                SetValue(IsNotDraggingPropertyKey, value);
            }
        }

        public DataTemplate TableTemplate
        {
            get
            {
                return (DataTemplate)GetValue(TableProperty);
            }
            set
            {
                SetValue(TableProperty, value);
            }
        }

        public DataTemplateSelector TableSelector
        {
            get
            {
                return (DataTemplateSelector)GetValue(TableSelectorProperty);
            }
            set
            {
                SetValue(TableSelectorProperty, value);
            }
        }

        public Style TableStyle
        {
            get
            {
                return (Style)GetValue(TableStyleProperty);
            }
            set
            {
                SetValue(TableStyleProperty, value);
            }
        }

        public object SelectedTable
        {
            get
            {
                if (TableControl != null)
                {
                    return TableControl.SelectedItem;
                }
                else
                {
                    if (initialSelectedTable == null)
                    {
                        return null;
                    }

                    if (initialSelectedTable.Count != 1)
                    {
                        return null;
                    }

                    return initialSelectedTable[0];
                }
            }
            set
            {
                if (TableControl != null)
                {
                    TableControl.SelectedItem = value;
                }
                else
                {
                    if (initialSelectedTable == null)
                    {
                        initialSelectedTable = new List<object>();
                    }

                    initialSelectedTable.Clear();
                    initialSelectedTable.Add(value);
                }
            }
        }

        public IList SelectedTables
        {
            get
            {
                if (TableControl != null)
                {
                    return TableControl.SelectedItems;
                }
                else
                {
                    if (initialSelectedTable == null)
                    {
                        initialSelectedTable = new List<object>();
                    }

                    return initialSelectedTable;
                }
            }
        }

        public event SelectionChangedEventHandler SelectionChanged;
        public void BringSelectedTablesIntoView()
        {
            BringTablesIntoView(SelectedTables);
        }

        public void BringTablesIntoView(ICollection Tables)
        {
            if (Tables == null)
            {
                throw new ArgumentNullException("'Tables' argument shouldn't be null.");
            }

            if (Tables.Count == 0)
            {
                return;
            }

            Rect rect = Rect.Empty;

            foreach (var item in Tables)
            {
                ItemTable table = FindAssociatedTable(item);
                Rect tableRect = new Rect(table.X, table.Y, table.ActualWidth, table.ActualHeight);

                if (rect == Rect.Empty)
                {
                    rect = tableRect;
                }
                else
                {
                    rect.Intersect(tableRect);
                }
            }

            this.BringIntoView(rect);
        }

        public void SelectTable()
        {
            this.SelectedTables.Clear();
        }

        public void SelectAll()
        {
            if (this.SelectedTables.Count != this.Table.Count)
            {
                this.SelectedTables.Clear();
                foreach (var node in this.Table)
                {
                    this.SelectedTables.Add(node);
                }
            }
        }

        public void InvertSelection()
        {
            var selectedTablesCopy = new ArrayList(this.SelectedTables);
            this.SelectedTables.Clear();

            foreach (var table in Table)
            {
                if (!selectedTablesCopy.Contains(table))
                {
                    this.SelectedTables.Add(table);
                }
            }
        }

        static TableView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TableView), new FrameworkPropertyMetadata(typeof(TableView)));

            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            SelectAllCommand = new RoutedCommand("SelectAll", typeof(TableView), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Escape));
            SelectNoneCommand = new RoutedCommand("SelectTable", typeof(TableView), inputs);

            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            InvertSelectionCommand = new RoutedCommand("InvertSelection", typeof(TableView), inputs);

            CommandBinding binding = new CommandBinding();
            binding.Command = SelectAllCommand;
            binding.Executed += new ExecutedRoutedEventHandler(SelectAll_Executed);
            CommandManager.RegisterClassCommandBinding(typeof(TableView), binding);

            binding = new CommandBinding();
            binding.Command = SelectNoneCommand;
            binding.Executed += new ExecutedRoutedEventHandler(SelectTable_Executed);
            CommandManager.RegisterClassCommandBinding(typeof(TableView), binding);

            binding = new CommandBinding();
            binding.Command = InvertSelectionCommand;
            binding.Executed += new ExecutedRoutedEventHandler(InvertSelection_Executed);
            CommandManager.RegisterClassCommandBinding(typeof(TableView), binding);
        }

        private static void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TableView c = (TableView)sender;
            c.SelectAll();
        }

        private static void SelectTable_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TableView c = (TableView)sender;
            c.SelectTable();
        }

        private static void InvertSelection_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TableView c = (TableView)sender;
            c.InvertSelection();
        }

        private static void TablesSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TableView c = (TableView)d;
            c.Table.Clear();

            if (e.OldValue != null)
            {
                var notifyCollectionChanged = e.OldValue as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                {
                    notifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(c.TablesSource_CollectionChanged);
                }
            }

            if (e.NewValue != null)
            {
                var enumerable = e.NewValue as IEnumerable;
                if (enumerable != null)
                {
                    foreach (object obj in enumerable)
                    {
                        c.Table.Add(obj);
                    }
                }

                var notifyCollectionChanged = e.NewValue as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                {
                    notifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(c.TablesSource_CollectionChanged);
                }
            }
        }

        private void TablesSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Table.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (object obj in e.OldItems)
                    {
                        Table.Remove(obj);
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (object obj in e.NewItems)
                    {
                        Table.Add(obj);
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TableControl = (ElementControl)Template.FindName("PART_TableControl", this);
            if (TableControl == null)
            {
                throw new ApplicationException("Failed to find 'PART_TableControl' in the visual tree for 'TableView'.");
            }

            if (initialSelectedTable != null && initialSelectedTable.Count > 0)
            {
                foreach (var node in initialSelectedTable)
                {
                    TableControl.SelectedItems.Add(node);
                }
            }
            initialSelectedTable = null;

            TableControl.SelectionChanged += new SelectionChangedEventHandler(TableControl_SelectionChanged);

            dragSelectionCanvas = (FrameworkElement)Template.FindName("PART_DragSelectionCanvas", this);
            if (dragSelectionCanvas == null)
            {
                throw new ApplicationException("Failed to find 'PART_DragSelectionCanvas' in the visual tree for 'TableView'.");
            }

            dragSelectionBorder = (FrameworkElement)Template.FindName("PART_DragSelectionBorder", this);
            if (dragSelectionBorder == null)
            {
                throw new ApplicationException("Failed to find 'PART_dragSelectionBorder' in the visual tree for 'TableView'.");
            }
        }

        private void TableControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(ListBox.SelectionChangedEvent, e.RemovedItems, e.AddedItems));
        }

        internal int FindMaxZIndex()
        {
            if (TableControl == null)
            {
                return 0;
            }
            int maxZ = 0;

            for (int tableIndex = 0; ; ++tableIndex)
            {
                ItemTable table = (ItemTable)TableControl.ItemContainerGenerator.ContainerFromIndex(tableIndex);
                if (table == null)
                {
                    break;
                }

                if (table.ZIndex > maxZ)
                {
                    maxZ = table.ZIndex;
                }
            }
            return maxZ;
        }

        internal ItemTable FindAssociatedTable(object table)
        {
            ItemTable tabl = table as ItemTable;
            if (tabl == null)
            {
                tabl = TableControl.FindAssociatedTable(table);
            }
            return tabl;
        }

        private bool isControlAndLeftMouseButtonDown = false;
        private bool isDraggingSelectionRect = false;
        private Point origMouseDownPoint;
        private FrameworkElement dragSelectionCanvas = null;
        private FrameworkElement dragSelectionBorder = null;
        private List<ItemTable> cachedSelectedTables = null;
        private static readonly double DragThreshold = 5;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (e.ChangedButton == MouseButton.Left &&
               (Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                SelectedTables.Clear();
                isControlAndLeftMouseButtonDown = true;
                origMouseDownPoint = e.GetPosition(this);
                CaptureMouse();
                e.Handled = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.ChangedButton == MouseButton.Left)
            {
                bool wasDragSelectionApplied = false;
                if (isDraggingSelectionRect)
                {
                    isDraggingSelectionRect = false;
                    ApplyDragSelectionRect();
                    e.Handled = true;
                    wasDragSelectionApplied = true;
                }
                if (isControlAndLeftMouseButtonDown)
                {
                    isControlAndLeftMouseButtonDown = false;
                    ReleaseMouseCapture();
                    e.Handled = true;
                }
                if (!wasDragSelectionApplied && IsClearSelectionOnEmptySpaceClickEnabled)
                {
                    SelectedTables.Clear();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDraggingSelectionRect)
            {
                Point curMouseDownPoint = e.GetPosition(this);
                UpdateDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
                e.Handled = true;
            }
            else if (isControlAndLeftMouseButtonDown)
            {
                Point curMouseDownPoint = e.GetPosition(this);
                var dragDelta = curMouseDownPoint - origMouseDownPoint;
                double dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    isDraggingSelectionRect = true;
                    InitDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
                }
                e.Handled = true;
            }
        }
        private void InitDragSelectionRect(Point pt1, Point pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);
            dragSelectionCanvas.Visibility = Visibility.Visible;
        }

        private void UpdateDragSelectionRect(Point pt1, Point pt2)
        {
            double x, y, width, height;
            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }
            Canvas.SetLeft(dragSelectionBorder, x);
            Canvas.SetTop(dragSelectionBorder, y);
            dragSelectionBorder.Width = width;
            dragSelectionBorder.Height = height;
        }

        private void ApplyDragSelectionRect()
        {
            dragSelectionCanvas.Visibility = Visibility.Collapsed;
            double x = Canvas.GetLeft(dragSelectionBorder);
            double y = Canvas.GetTop(dragSelectionBorder);
            double width = dragSelectionBorder.Width;
            double height = dragSelectionBorder.Height;
            Rect dragRect = new Rect(x, y, width, height);
            dragRect.Inflate(width / 10, height / 10);
            TableControl.SelectedItems.Clear();
            for (int tableIndex = 0; tableIndex < Table.Count; ++tableIndex)
            {
                var table = (ItemTable)TableControl.ItemContainerGenerator.ContainerFromIndex(tableIndex);
                var transformToAncestor = table.TransformToAncestor(this);
                Point itemPt1 = transformToAncestor.Transform(new Point(0, 0));
                Point itemPt2 = transformToAncestor.Transform(new Point(table.ActualWidth, table.ActualHeight));
                Rect itemRect = new Rect(itemPt1, itemPt2);
                if (dragRect.Contains(itemRect))
                {
                    table.IsSelected = true;
                }
            }
        }

        private void Table_DragStarted(object source, TableDragStartedEventArgs e)
        {
            e.Handled = true;
            IsDragging = true;
            IsNotDragging = false;
            IsDraggingTable = true;
            IsNotDraggingTable = false;
            var eventArgs = new TableDragStartedEventArgs(TableDragStartedEvent, this, SelectedTables);
            RaiseEvent(eventArgs);
            e.Cancel = eventArgs.Cancel;
        }

        private void Table_Dragging(object source, TableDraggingEventArgs e)
        {
            e.Handled = true;
            if (cachedSelectedTables == null)
            {
                cachedSelectedTables = new List<ItemTable>();
                foreach (var selectedTable in SelectedTables)
                {
                    ItemTable table = FindAssociatedTable(selectedTable);
                    if (table == null)
                    {
                        throw new ApplicationException("Unexpected code path!");
                    }
                    cachedSelectedTables.Add(table);
                }
            }
            foreach (var table in cachedSelectedTables)
            {
                table.X += e.HorizontalChange;
                table.Y += e.VerticalChange;
            }

            var eventArgs = new TableDraggingEventArgs(TableDraggingEvent, this, SelectedTables, e.HorizontalChange, e.VerticalChange);
            RaiseEvent(eventArgs);
        }
        private void Table_DragCompleted(object source, TableDragCompletedEventArgs e)
        {
            e.Handled = true;
            var eventArgs = new TableDragCompletedEventArgs(TableDragCompletedEvent, this, SelectedTables);
            RaiseEvent(eventArgs);
            if (cachedSelectedTables != null)
            {
                cachedSelectedTables = null;
            }
            IsDragging = false;
            IsNotDragging = true;
            IsDraggingTable = false;
            IsNotDraggingTable = true;
        }
    }
}
