using Constructor.ViewModel.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Constructor.ViewModel
{
    public class MainVM : BaseVM
    {
        public TemplateVM template = null;

        public MainVM()
        {
            Template = new TemplateVM();
        }

        public TemplateVM Template
        {
            get
            {
                return template;
            }
            set
            {
                template = value;
                OnPropertyChanged("TemplateVM");
            }
        }

        public void DeleteSelectedTable()
        {
            var tempTable = Template.Table.ToArray();
            foreach (var table in tempTable)
            {
                if (table == Template.SelectTable)
                {
                    Template.Table.Remove(table);
                    Template.SelectTable = null;
                    return;
                }
            }
        }

        public ITable Select(Point location)
        {
            foreach (var table in Template.Table)
            {
                if (location.X >= table.XPoint && location.Y >= table.YPoint
                    && location.X <= (table.XPoint + table.Width)
                    && location.Y <= (table.YPoint + table.Height))
                {
                    Template.SelectTable = table;
                    return table;
                }
            }
            return null;
        }

        public ITable CreateTextBox(Point newLocation)
        {
            var table = new TableWithTextOrImageVM()
            {
                Columns = 1,
                Rows = 1,
                XPoint = newLocation.X,
                YPoint = newLocation.Y,
                Height = 50,
                Width = 80,
                IsBorder = true,
                ZPoint = 1
            };
            table.CreateTextBox();
            Template.Table.Add(table);
            Template.State = State.normally;
            Template.SelectTable = table;
            return table;
        }

        public ITable CreateImage(Point newLocation)
        {
            var table = new TableWithTextOrImageVM()
            {
                Columns = 1,
                Rows = 1,
                XPoint = newLocation.X,
                YPoint = newLocation.Y,
                Height = 50,
                Width = 80,
                IsBorder = true,
                ZPoint = 1
            };
            table.CreateImage();
            Template.Table.Add(table);
            Template.State = State.normally;
            Template.SelectTable = table;
            return table;
        }

        public ITable CreateTable(Point newLocation, int countColumn, int countRow)
        {
            var table = new TableWithArrayVM()
            {
                Columns = 1,
                Rows = 1,
                XPoint = newLocation.X,
                YPoint = newLocation.Y,
                Height = 20,
                Width = 50,
                IsBorder = true,
                ZPoint = 1
            };
            table.CreateUserTable();
            table.Rows = countRow;
            table.Columns = countColumn;
            Template.Table.Add(table);
            Template.State = State.normally;
            Template.SelectTable = table;
            return table;
        }

        public void AddSimpleAPI(string nameAttribute)
        {
            Template.SelectTable.SelectCell.Content += "%"+nameAttribute+"%";
            Template.SelectTable.IsUsedApi = true;
            Template.SelectTable.SelectCell.CellHaveApi = true;
        }
    }
}
