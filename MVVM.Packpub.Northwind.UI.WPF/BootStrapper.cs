using MVVM.Packpub.Northwind.ViewModel;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.UI.WPF
{
   public  class BootStrapper
    {
        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return ObjectFactory.GetInstance<MainWindowViewModel>();
            }
        }
        public BootStrapper()
        {
            ObjectFactory.Initialize(
            o => o.Scan(
            a =>
            {
               // a.TheCallingAssembly();
                a.WithDefaultConventions();
                //a.Exclude(x => x.FullName.Contains("System.Action"));
                a.AssembliesFromApplicationBaseDirectory(d => d.FullName.StartsWith("MVVM.Packpub.Northwind"));
                a.LookForRegistries();
               //a.Assembly("MVVM.Packpub.Northwind.Model");
               // a.Assembly("MVVM.Packpub.Northwind.ViewModel");
               // a.Assembly("MVVM.Packpub.Northwind.UI.WPF");
               //a.Assembly("MVVM.Packpub.Northwind.Application");
                //a.ExcludeNamespaceContainingType<IContainer>(); 
                //a.AssembliesFromPath(AppDomain.CurrentDomain.BaseDirectory); 
               
            }));
        }

    }
}
