using Constructor.ViewModel.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
        public string nameTemplate = "Шаблон";

        public MainVM()
        {
            Template = new TemplateVM();
        }

        public string NameTemplate
        {
            get
            {
                return nameTemplate;
            }
            set
            {
                nameTemplate = value;
                if (Template != null)
                {
                    Template.NameTemplate = value;
                }
                OnPropertyChanged("NameTemplate");
            }
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
                OnPropertyChanged("Template");
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

        public void Select(Point location)
        {
            foreach (var table in Template.Table)
            {
                if (location.X >= table.XPoint && location.Y >= table.YPoint
                    && location.X <= (table.XPoint + table.Width)
                    && location.Y <= (table.YPoint + table.Height))
                {
                    Template.SelectTable = table;
                    return;
                }
            }
        }

        public void CreateTextBox(Point newLocation)
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
        }

        public void CreateImage(Point newLocation)
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
        }

        public void CreateTable(Point newLocation, int countColumn, int countRow)
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
        }

        public void CreateTable(Point newLocation, int countColumn, int countRow, IEnumerable<double> list)
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
            table.UsingTableApi(list);
            Template.Table.Add(table);
            Template.State = State.normally;
            Template.SelectTable = table;
        }

        public void AddSimpleAPI(string nameAttribute)
        {
            Template.SelectTable.SelectCell.Content += "%|"+nameAttribute+"|%";
            Template.SelectTable.IsUsedApi = true;
            Template.SelectTable.SelectCell.CellHaveApi = true;
        }

        public void AddImageInSelectCell(string url)
        {
            Template.SelectTable.SelectCell.Url = url;
        }


        #region Serialize/Deserialize

        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TemplateVM));


        public bool Seriliz()
        {
            if (File.Exists("Template\\" + NameTemplate + ".json"))
            {
                if (MessageBox.Show("Файл с таким название уже существует. Заменить?", "Ошибка",
                                            MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    File.Delete("Template\\" + NameTemplate + ".json");
                    using (FileStream fs = new FileStream("Template\\" + NameTemplate + ".json", FileMode.OpenOrCreate))
                    {
                        jsonFormatter.WriteObject(fs, Template);
                    }
                    Template = null;
                    return true;
                }
            }
            else
            {
                using (FileStream fs = new FileStream("Template\\" + NameTemplate + ".json", FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, Template);
                }
                Template = null;
                return true;
            }
            return false;
        }

        public void Deseriliz(string path)
        {
            Template = null;
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Template = (TemplateVM)jsonFormatter.ReadObject(fs);
                }
            }
            else
            {
                MessageBox.Show("Файл с таким не найдено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
    }
}
