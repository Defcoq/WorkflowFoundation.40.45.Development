using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation.Metadata;
using PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH15.Activities;
using System.ComponentModel;

namespace PROWF40.CH08.Host.Com.BookMark.ActivityLibrary.CH15.Activities_designer
{

    public class Metadata : IRegisterMetadata
    {
        public void Register()
        {
            AttributeTableBuilder atb = new AttributeTableBuilder();
            atb.AddCustomAttributes(typeof(CalcShipping),new DesignerAttribute(typeof(CalcShippingDesigner)));
            MetadataStore.AddAttributeTable(atb.CreateTable());
        }
    }

}
