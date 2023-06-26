using data_list.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_list.DAL.Repositories
{
    interface IDataRepository
    {
        List<DataPerson> GetlistData();
        void SetlistData(List<DataPerson> listData);
        List<DataPerson> SearchlistData(SearchWord wordSearch);
    }

    public class DataRepository : IDataRepository
    {
        private List<DataPerson> listData;

        //public DataRepository(List<DataPerson> listData)
        //{
        //    this.listData = listData;
        //}
        public List<DataPerson> GetlistData()
        {
            return listData;
        }

        public void SetlistData(List<DataPerson> listData)
        {
            this.listData = listData;
        }
        public List<DataPerson> SearchlistData(SearchWord searchWord)
        {
            try {
                List<DataPerson> filteredData = listData.Where(data =>
                data.nameAddres.Contains(searchWord.searchWord) ||
                data.areaAddres.Contains(searchWord.searchWord) ||
                data.cityAddres.Contains(searchWord.searchWord)
                 ).ToList();
                return filteredData;

            }
            catch (Exception)
            {
                return null;

                throw;
            }
    }
}
}