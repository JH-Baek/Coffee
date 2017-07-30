using Coffee.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Coffee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AzureTable : ContentPage
    {
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;

        Geocoder geoCoder;

        public AzureTable()
        {
            InitializeComponent();

            geoCoder = new Geocoder();

        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            loading.IsRunning = true;
            List<NotCoffeeModel> notCoffeeInformation = await AzureManager.AzureManagerInstance.GetCoffeeInformation();

            foreach (NotCoffeeModel model in notCoffeeInformation)
            {
                var position = new Position(model.Latitude, model.Longitude);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                foreach (var address in possibleAddresses)
                    model.City = address;
            }

            CoffeeList.ItemsSource = notCoffeeInformation;
            loading.IsRunning = false;
        }
    }
}