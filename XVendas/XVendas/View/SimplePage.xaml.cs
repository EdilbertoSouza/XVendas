using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamvvm;

namespace XVendas
{
    public partial class SimplePage : ContentPage, IBasePage<SimplePageModel>
    {
        public SimplePage()
        {
            InitializeComponent();
        }

        public void FlowScrollTo(object item)
        {
            flowListView.FlowScrollTo(item, ScrollToPosition.MakeVisible, true);
        }
    }
}