using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Activities;
using Microsoft.Win32;
using System.Activities.DynamicUpdate;
using System.IO;
using System.Xaml;
using System.Activities.XamlIntegration;
using System.Runtime.Serialization;

using System.Xml;
using System.Activities.DurableInstancing;
using System.Threading;
using System.Activities.Statements;

namespace PROWF45.CH06.Version.Update.MoviesRental.WPF.Host
{
    /// <summary>
    /// Interaction logic for MainWindow_UpdateWF.xaml
    /// </summary>
    public partial class MainWindow_UpdateWF : Window
    { ActivityBuilder OriginalWF = null;
        ActivityBuilder DeltaWF = null;
        DynamicUpdateMap updateMap = null;

        public MainWindow_UpdateWF()
        {
            InitializeComponent();
        }

        private void cmdSearchWorkflow_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"Workflow files (*.xaml, *.xamlx)|*.xaml;*.xamlx";
            fileDialog.Multiselect = false;
            bool? close = fileDialog.ShowDialog(this);
            if (close.HasValue && close.Value)
            {
                txtWorkflowFile.Text = fileDialog.FileName;
            }
        }

        private void cmdSetMapPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"Map files (*.map)|*.map";
            fileDialog.Multiselect = false;
            bool? close = fileDialog.ShowDialog(this);
            if (close.HasValue && close.Value)
            {
                txtUpdateMapFile.Text = fileDialog.FileName;
            }
        }

        private void cmdSaveUpdateMap_Click(object sender, RoutedEventArgs e)
        {
            DeltaWF = LoadActivityBuilder(txtWorkflowSnapshot.Text);
            updateMap = DynamicUpdateServices.CreateUpdateMap(DeltaWF);

            SaveUpdateMap(updateMap, txtWorkflowSnapshot.Text);

        }

        private void cmdWorkflowUpdate_Click(object sender, RoutedEventArgs e)
        {
            OriginalWF = LoadActivityBuilder(txtWorkflowFile.Text); //Load original workflow to update
            DynamicUpdateServices.PrepareForUpdate(OriginalWF); //Prepare the workflow for update
            SaveActivityBuilder(OriginalWF, txtWorkflowFile.Text); //Save the prepared workflow
        }

        public void SaveActivityBuilder(ActivityBuilder builder, string path)
        {
            var actualpath = System.IO.Path.GetDirectoryName(path) + "\\wfReadytoUpdate.xaml";
            txtWorkflowSnapshot.Text = actualpath;
            using (var writer = File.CreateText(actualpath))
            {
                var xmlWriter = new XamlXmlWriter(writer, new XamlSchemaContext());
                using (var xamlWriter = ActivityXamlServices.CreateBuilderWriter(xmlWriter))
                {
                    XamlServices.Save(xamlWriter, builder);
                }
            }
        }


        public ActivityBuilder LoadActivityBuilder(string fileName)
        {
            ActivityBuilder builder;
            using (var xamlReader = new XamlXmlReader(fileName))
            {
                var builderReader = ActivityXamlServices.CreateBuilderReader(xamlReader);
                builder = (ActivityBuilder)XamlServices.Load(builderReader);
                xamlReader.Close();
            }
            return builder;
        }


        public void SaveUpdateMap(DynamicUpdateMap map, string fileName)
        {
            var path = System.IO.Path.ChangeExtension(fileName, "map");
            DataContractSerializer serialize = new DataContractSerializer(typeof(DynamicUpdateMap));
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                serialize.WriteObject(fs, map);
            }

            txtUpdateMapFile.Text = path;
        }

        private void cmdUpdateInstance_Click(object sender, RoutedEventArgs e)
        {

            SqlWorkflowInstanceStore instanceStore = new SqlWorkflowInstanceStore();

            instanceStore.ConnectionString =
                @"Data Source=(LocalDB)\v11.0;Initial Catalog=WFPersist;Integrated Security=True";

            WorkflowApplicationInstance wfInstance =
                WorkflowApplication.GetInstance(new Guid(txtUpdateInstance.Text), instanceStore);

            DataContractSerializer s = new DataContractSerializer(typeof(DynamicUpdateMap));
            using (FileStream fs = File.Open(txtUpdateMapFile.Text, FileMode.Open))
            {
                updateMap = s.ReadObject(fs) as DynamicUpdateMap;
            }

            var wfApp =
                new WorkflowApplication(new MovieRentalProcess(),
                     new WorkflowIdentity
                     {
                         Name = "v2MovieRentalProcess",
                         Version = new System.Version(2, 0, 0, 0)
                     });

            IList<ActivityBlockingUpdate> act;
            if (wfInstance.CanApplyUpdate(updateMap, out act))
            {
                wfApp.Load(wfInstance, updateMap);
                wfApp.Unload();
            }
        }

        private void cmdSetSnapshot_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"Map files (*.xaml)|*.xaml";
            fileDialog.Multiselect = false;
            bool? close = fileDialog.ShowDialog(this);
            if (close.HasValue && close.Value)
            {
                txtWorkflowSnapshot.Text = fileDialog.FileName;
            }
        }
    }
}
