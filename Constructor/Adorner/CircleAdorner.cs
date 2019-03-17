using System;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

namespace Constructor.Adorner
{
    class CircleAdorner : System.Windows.Documents.Adorner
    {
        public CircleAdorner(UIElement adornedElement): base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            double renderRadius = 5.0;

            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
        }
    }
}
