using Logistic.Core;
using Logistic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Cargo.xaml
	/// </summary>
	public partial class CargoWindow : Window
	{
		public int selectedVehcileId;
		public Cargo newCargo;
		public VehicleService vehicleService;

		ObservableCollection<Cargo> cargoList ;

		
		public CargoWindow(VehicleService vehicleService, int selectedVehcileId)
		{
			InitializeComponent();
			this.vehicleService=vehicleService;
			this.selectedVehcileId = selectedVehcileId;
			cargoList = new ObservableCollection<Cargo>(vehicleService.GetById(selectedVehcileId).Cargos);
			ListViewCargo.ItemsSource = cargoList;

		}
		public CargoWindow()
		{
			InitializeComponent();
		}
		private void SaveDataButton_Click(object sender, RoutedEventArgs e)
		{
			int weight = Convert.ToInt32(TextBoxVolume.Text);
			int volume = Convert.ToInt32(TextBoxWeight.Text);
			string number = TextBoxNumber.Text;

			newCargo = new Cargo(weight, volume);
			newCargo.Invoice = new Invoice() {
				RecipientAddress = TextBoxRecipientAddress.Text,
				SenderAddress = TextBoxSenderAddress.Text,
				RecipientPhoneNumber = TextBoxRecipientPhone.Text,
				SenderPhoneNumber = TextBoxSenderPhone.Text
			};
			newCargo.Code = number;

			vehicleService.LoadCargo(newCargo, selectedVehcileId);
			this.DialogResult = true;
			Close();
		}

       
        private void ButtonUnloadCargo_Click(object sender, RoutedEventArgs e)
        {
			Cargo selectedCargo = (Cargo)ListViewCargo.SelectedItems[0];
			vehicleService.UnloadCargo(selectedCargo.Id, selectedVehcileId);
			this.DialogResult = true;

			Close();
		}
		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

        private void ListViewCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (ListViewCargo.SelectedItems.Count > 0)
			{
				ButtonUnloadCargo.IsEnabled = true;

				Cargo selectedCargo = (Cargo)ListViewCargo.SelectedItems[0];
				TextBoxNumber.Text = selectedCargo.Code;
				TextBoxWeight.Text = selectedCargo.Weight.ToString();
				TextBoxVolume.Text = selectedCargo.Volume.ToString();

				TextBoxRecipientAddress.Text = selectedCargo.Invoice.RecipientAddress;
				TextBoxSenderAddress.Text = selectedCargo.Invoice.SenderAddress;
				TextBoxRecipientPhone.Text = selectedCargo.Invoice.RecipientPhoneNumber;
				TextBoxSenderPhone.Text = selectedCargo.Invoice.SenderPhoneNumber;


			}
			else
			{
				ButtonUnloadCargo.IsEnabled = false;
			}
		}
	}
}
