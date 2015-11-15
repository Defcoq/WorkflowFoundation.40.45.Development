using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Packpub.Northwind.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",GetDataDirectory());
            base.OnStartup(e);
        }
        private string GetDataDirectory()
        {
            string solutionDirectory
                = Directory.GetParent(
                            Directory.GetCurrentDirectory())
                                .Parent.Parent.FullName;
            string dataDirectory
                = Path.Combine(solutionDirectory,
                    "MVVM.Packpub.Northwind.Data");
            if (!Directory.Exists(dataDirectory))
                throw new InvalidOperationException(
                    "Unable to locate data directory.");
            return dataDirectory;
        }
    }
}
