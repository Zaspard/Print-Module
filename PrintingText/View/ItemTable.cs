using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PrintingText.View;

namespace PrintingText.View
{
    public class ItemTable : ListBoxItem
    {
        public static readonly DependencyProperty XProperty =
             DependencyProperty.Register("X", typeof(double), typeof(ItemTable),
                 new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(ItemTable),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ZIndexProperty =
            DependencyProperty.Register("ZIndex", typeof(int), typeof(ItemTable),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        internal static readonly DependencyProperty ParentTempateViewProperty =
            DependencyProperty.Register("ParentTemplateView", typeof(TableView), typeof(ItemTable),
                new FrameworkPropertyMetadata(ParentTemplateView_PropertyChanged));

        internal static readonly RoutedEvent TableDragStartedEvent =
            EventManager.RegisterRoutedEvent("TableDragStarted", RoutingStrategy.Bubble, typeof(TableDragStartedEventHandler), typeof(ItemTable));

        internal static readonly RoutedEvent TableDraggingEvent =
            EventManager.RegisterRoutedEvent("TableDragging", RoutingStrategy.Bubble, typeof(TableDraggingEventHandler), typeof(ItemTable));

        internal static readonly RoutedEvent TableDragCompletedEvent =
            EventManager.RegisterRoutedEvent("TableDragCompleted", RoutingStrategy.Bubble, typeof(TableDragCompletedEventHandler), typeof(ItemTable));

        public ItemTable()
        {
            Focusable = false;
        }

        public double X
        {
            get
            {
                return (double)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
            }
        }

        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
            }
        }

        public int ZIndex
        {
            get
            {
                return (int)GetValue(ZIndexProperty);
            }
            set
            {
                SetValue(ZIndexProperty, value);
            }
        }

        internal TableView ParentTemplateView
        {
            get
            {
                return (TableView)GetValue(ParentTempateViewProperty);
            }
            set
            {
                SetValue(ParentTempateViewProperty, value);
            }
        }

        private Point lastMousePoint;
        private bool isLeftMouseDown = false;
        private bool isLeftMouseAndControlDown = false;
        private bool isDragging = false;
        private static readonly double DragThreshold = 5;

        static ItemTable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemTable), new FrameworkPropertyMetadata(typeof(ItemTable)));
        }

        internal void BringToFront()
        {
            if (ParentTemplateView == null)
            {
                return;
            }

            int maxZ = ParentTemplateView.FindMaxZIndex();
            ZIndex = maxZ + 1;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            BringToFront();

            if (ParentTemplateView != null)
            {
                ParentTemplateView.Focus();
            }
            if (e.ChangedButton == MouseButton.Left && ParentTemplateView != null)
            {
                lastMousePoint = e.GetPosition(ParentTemplateView);

                isLeftMouseDown = true;

                LeftMouseDownSelectionLogic();

                e.Handled = true;
            }
        }

        internal void LeftMouseDownSelectionLogic()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                isLeftMouseAndControlDown = true;
            }
            else
            {
                isLeftMouseAndControlDown = false;

                if (ParentTemplateView.SelectedTables.Count == 0)
                {
                    IsSelected = true;
                }
                else if (ParentTemplateView.SelectedTables.Contains(this) ||
                         ParentTemplateView.SelectedTables.Contains(DataContext))
                {
                }
                else
                {
                    ParentTemplateView.SelectedTables.Clear();
                    IsSelected = true;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDragging)
            {
                Point curMousePoint = e.GetPosition(ParentTemplateView);
                object item = this;
                if (DataContext != null)
                {
                    item = DataContext;
                }
                Vector offset = curMousePoint - lastMousePoint;
                if (offset.X != 0.0 ||
                    offset.Y != 0.0)
                {
                    lastMousePoint = curMousePoint;
                    RaiseEvent(new TableDraggingEventArgs(TableDraggingEvent, this, new object[] { item }, offset.X, offset.Y));
                }
            }
            else if (isLeftMouseDown && ParentTemplateView.EnableTableDragging)
            {
                Point curMousePoint = e.GetPosition(ParentTemplateView);
                var dragDelta = curMousePoint - lastMousePoint;
                double dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    TableDragStartedEventArgs eventArgs = new TableDragStartedEventArgs(TableDragStartedEvent, this, new ItemTable[] { this });
                    RaiseEvent(eventArgs);

                    if (eventArgs.Cancel)
                    {
                        isLeftMouseDown = false;
                        isLeftMouseAndControlDown = false;
                        return;
                    }
                    isDragging = true;
                    CaptureMouse();
                    e.Handled = true;
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (isLeftMouseDown)
            {
                if (isDragging)
                {
                    RaiseEvent(new TableDragCompletedEventArgs(TableDragCompletedEvent, this, new ItemTable[] { this }));
                    ReleaseMouseCapture();
                    isDragging = false;
                }
                else
                {
                    LeftMouseUpSelectionLogic();
                }
                isLeftMouseDown = false;
                isLeftMouseAndControlDown = false;
                e.Handled = true;
            }
        }

        internal void LeftMouseUpSelectionLogic()
        {
            if (isLeftMouseAndControlDown)
            {
                IsSelected = !IsSelected;
            }
            else
            {
                if (ParentTemplateView.SelectedTables.Count == 1 &&
                    (ParentTemplateView.SelectedTable == this ||
                     ParentTemplateView.SelectedTable == DataContext))
                {
                }
                else
                {
                    ParentTemplateView.SelectedTables.Clear();
                    IsSelected = true;
                }
            }

            isLeftMouseAndControlDown = false;
        }

        private static void ParentTemplateView_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var itemTable = (ItemTable)o;
            itemTable.BringToFront();
        }
    }
}
