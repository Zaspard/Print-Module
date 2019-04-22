using API;
using Constructor.ViewModel;

namespace PrintingText.ViewModel
{
    class ApiTab : BaseVM, ITab
    {
        private Dossier dossier = new Dossier();
        public Dossier Dossier
        {
            get
            {
                return dossier;
            }
        }
    }
}
