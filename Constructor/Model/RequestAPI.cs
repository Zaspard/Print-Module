using Constructor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model
{
    public class RequestAPI: BaseVM
    {
        private API selectItemApi;

        public List<API> List { get; set; } = new List<API>()
        {
            new API() { Id=0, Name="Наименование компании", Content="АО Геотрансгаз" },
            new API() { Id=1, Name="Наименование страны", Content="Российская Федерация" },
            new API() { Id=2, Name="Наименование скважины", Content="540" },
            new API() { Id=3, Name="Наименование месторождения", Content="Береговое" },
            new API() { Id=4, Name="Наименование регион", Content="ЯНАО" },
        };

        public API SelectItemApi
        {
            get { return selectItemApi; }
            set
            {
                selectItemApi = value;
                OnPropertyChanged("SelectItemApi");
            }
        }
    }
}
