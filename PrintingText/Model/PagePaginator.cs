using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace PrintingText.ViewModel
{/*
    class PagePaginator : DocumentPaginator
    {
        private DocumentPaginator flowDocumentPaginator;

        public PagePaginator(FlowDocument document)
        {
            flowDocumentPaginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
        }

        public override bool IsPageCountValid
        {
            get { return flowDocumentPaginator.IsPageCountValid; }
        }

        public override int PageCount
        {
            get { return flowDocumentPaginator.PageCount; }
        }

        public override Size PageSize
        {
            get { return flowDocumentPaginator.PageSize; }
            set { flowDocumentPaginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return flowDocumentPaginator.Source; }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            DocumentPage page = flowDocumentPaginator.GetPage(pageNumber);
            ContainerVisual newVisual = new ContainerVisual();
            newVisual.Children.Add(page.Visual);
            DrawingVisual header = new DrawingVisual();
            using (DrawingContext dc = header.RenderOpen())
            {
                Typeface typeface = new Typeface("Times New Roman");
                FormattedText text = new FormattedText("Страница " + (pageNumber + 1).ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight, typeface, 14, Brushes.Black);
                dc.DrawText(text, new Point(96 * 0.25, 96 * 0.25));
            }
            newVisual.Children.Add(header);
            DocumentPage newPage = new DocumentPage(newVisual);
            return newPage;
        }
    }*/
}
