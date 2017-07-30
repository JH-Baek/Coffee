using Coffee.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<NotCoffeeModel> notCoffeeTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://notcoffee.azurewebsites.net");
            this.notCoffeeTable = this.client.GetTable<NotCoffeeModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<NotCoffeeModel>> GetCoffeeInformation()
        {
            return await this.notCoffeeTable.ToListAsync();
        }

        public async Task PostCoffeeInformation(NotCoffeeModel notCoffeeModel)
        {
            await this.notCoffeeTable.InsertAsync(notCoffeeModel);
        }
    }
}
