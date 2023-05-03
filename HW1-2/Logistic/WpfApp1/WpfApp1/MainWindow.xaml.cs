using Logistic.Core;
using Logistic.DAL;
using Logistic.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		InMemoryRepository<Vehicle> vehicleRepository;
		VehicleService vehicleService ;
		ReportService<Vehicle, int> reportService;

		public MainWindow()
		{

			InitializeComponent();

			foreach(var type in Enum.GetValues(typeof(VehicleType)))
				ComboBoxType.Items.Add(type.ToString());

			vehicleRepository = new InMemoryRepository<Vehicle>();
			vehicleService = new VehicleService(vehicleRepository);
			reportService = new ReportService<Vehicle, int>(new JsonRepository<Vehicle, int>(), new XmlRepository<Vehicle, int>());

			ListViewVehicle.ItemsSource= vehicleService.GetAll();

			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
		}

		static void MyHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = (Exception)args.ExceptionObject;

			if (args.ExceptionObject is ArgumentOutOfRangeException)
				MessageBox.Show("Choose item from ListView first", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			else if(args.ExceptionObject is ArgumentException)
				MessageBox.Show("Cargo loading error (check overflow or ids)", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			else
				MessageBox.Show("Unhandled exception", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

			//я ще не знаю, як обробити, щоб програма не вилітала при ексепшнах
			//просто не давати користувачу можливості (вимикати кнопки) створити ексепшни
		}

		private void LoadCargoButton_Click(object sender, RoutedEventArgs e)
		{

			int previousVehicleId = ((Vehicle)ListViewVehicle.SelectedItems[0]).Id;
			CargoWindow cargoWindow = new CargoWindow(vehicleService, previousVehicleId);

			if ((bool)cargoWindow.ShowDialog())
			{
				ListViewVehicle.ItemsSource = vehicleService.GetAll();
				ListViewVehicle.UnselectAll();
			}

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true) 
			{
				ImportTextBox.Text = openFileDialog.FileName;
			}
		}

		private void ListViewVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ListViewVehicle.SelectedItems.Count > 0)
			{
				LoadCargoButton.IsEnabled = true;
				ButtonDelete.IsEnabled = true;
				ButtonUpdate.IsEnabled = true;

				//після видалення Vehicle автоматично змінюється обраний елементи, а після видалення останнього буде NullReference
				Vehicle selectedVehicle = (Vehicle)ListViewVehicle.SelectedItems[0];
				TextBoxNumber.Text = selectedVehicle.Number;
				TextBoxWeight.Text = selectedVehicle.MaxCargoWeightKg.ToString();
				TextBoxVolume.Text = selectedVehicle.MaxCargoVolume.ToString();
				ComboBoxType.Text = selectedVehicle.Type.ToString();

			}
			else
			{
				LoadCargoButton.IsEnabled = false;
				ButtonDelete.IsEnabled = false;
				ButtonUpdate.IsEnabled = false;
			}
		}
		private Vehicle InputVehicle()
        {
			double maxCargoVolume = Convert.ToDouble(TextBoxVolume.Text);
			int maxCargoWeightKg = Convert.ToInt32(TextBoxWeight.Text);
			string number = TextBoxNumber.Text;

			var isParsed = Enum.TryParse((string)ComboBoxType.SelectedItem, out VehicleType vehicleType) && Enum.IsDefined(typeof(VehicleType), vehicleType);
			
			Vehicle result = new Vehicle(vehicleType, maxCargoWeightKg, maxCargoVolume);
			result.Number = number;

			return result;
		}
		private void ButtonCreate_Click(object sender, RoutedEventArgs e)
		{
			var newVehicle = InputVehicle();

			var serviceGivenId=vehicleService.Create(newVehicle);
			newVehicle.Id = serviceGivenId;

			ListViewVehicle.ItemsSource = vehicleService.GetAll();
		}

		private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
			var selectedVehicle = (Vehicle)ListViewVehicle.SelectedItems[0];
			var newVehicle = InputVehicle();

			newVehicle.Cargos = selectedVehicle.Cargos;

			vehicleService.Delete(selectedVehicle.Id);
			vehicleService.Create(newVehicle);

			ListViewVehicle.ItemsSource = vehicleService.GetAll();

		}

		private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
			var selectedVehicle = (Vehicle)ListViewVehicle.SelectedItems[0];

			vehicleService.Delete(selectedVehicle.Id);
			ListViewVehicle.ItemsSource = vehicleService.GetAll();

		}

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
			var dialog = new OpenFileDialog();

			Assembly asm = Assembly.GetExecutingAssembly();
			string path = System.IO.Path.GetDirectoryName(asm.Location);

			dialog.InitialDirectory = path;

			dialog.Filter = "(*.xml, *.json)|*.xml;*.json|All files (*.*)|*.*"; 

			bool? result = dialog.ShowDialog();

			if (result == true)
			{
				try
				{
					ImportTextBox.Text = dialog.FileName;
					var list = reportService.LoadReport(dialog.FileName);
					ListViewImport.ItemsSource = list;
				}
				catch (Exception)
				{
					MessageBox.Show("File import error", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
		}

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if(ReportTab.IsSelected)
				ListViewExport.ItemsSource = vehicleService.GetAll();

		}

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
			try
			{
				Enum.TryParse(ReportTypeComboBox.Text, out ReportType reportType);
				var entities = vehicleService.GetAll();

				if(entities.Count>0)
					reportService.CreateReport(entities, reportType);
			}
			catch (Exception)
			{
				MessageBox.Show("File export error", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}
    }
}
