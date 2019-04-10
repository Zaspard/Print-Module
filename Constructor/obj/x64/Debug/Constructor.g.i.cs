#pragma checksum "..\..\..\Constructor.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "579BDDD7F2E68FFA0CADD990319ED3B2CE2FAABB"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Constructor;
using Constructor.UC;
using Constructor.View;
using Constructor.ViewModel;
using Constructor.ViewModel.Table;
using Constructor.ViewModel.Table.Array;
using Constructor.ViewModel.Table.TextOrImage;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Constructor
{


    /// <summary>
    /// Constructor
    /// </summary>
    public partial class MainWindows : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 505 "..\..\..\Constructor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Constructor.View.TableView constructor;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Constructor;component/constructor.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Constructor.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler)
        {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 417 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.ClickButton_CreateTextBox);

#line default
#line hidden
                    return;
                case 2:

#line 420 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.ClickButton_CreateTable);

#line default
#line hidden
                    return;
                case 3:

#line 423 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.ClickButton_CreateImage);

#line default
#line hidden
                    return;
                case 4:

#line 426 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CreateElement_Executed);

#line default
#line hidden
                    return;
                case 5:

#line 429 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Select);

#line default
#line hidden
                    return;
                case 6:

#line 432 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.DeleteSelectedTable);

#line default
#line hidden
                    return;
                case 7:

#line 436 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SimpleRequestByAPI);

#line default
#line hidden
                    return;
                case 8:

#line 440 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CreateOpenDialog);

#line default
#line hidden
                    return;
                case 9:

#line 444 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Serialize);

#line default
#line hidden
                    return;
                case 10:

#line 448 "..\..\..\Constructor.xaml"
                    ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Deserialize);

#line default
#line hidden
                    return;
                case 11:
                    this.constructor = ((Constructor.View.TableView)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

