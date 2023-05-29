using TransactionalAPIMaddiApp.Helpers.Sql;
using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Headquarters
{
    public class RepositoryHeadquarters : IRepositoryHeadquarters
    {
        private readonly ISqlHelper _sqlhelper;

        public RepositoryHeadquarters(ISqlHelper sqlhelper)
        {
            _sqlhelper = sqlhelper;
        }

        public async Task<dynamic> GetHeadquartersByRestaurant(GetHeadquartersByRestaurantViewModel model)
        {
            var query = @"exec sp_GetHeadquartersByRestaurant @User_Id, @Restaurant_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> GetHeadquarterById(GetHeadquarterByIdViewModel model)
        {
            var query = @"exec sp_GetHeadquarterById @User_Id, @Headquarter_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Headquarter_Id = model.Headquarter_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> DeleteHeadquarter(DeleteHeadquarterViewModel model)
        {
            var query = @"exec sp_DeleteHeadquarter @User_Id, @Headquarter_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Headquarter_Id = model.Headquarter_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> UpdateHeadquarter(UpdateHeadquarterViewModel model)
        {
            var query = @"exec sp_UpdateHeadquarter @User_Id, @Headquarter_Id, @StrName, @StrAddress, @DtStart, @DtEnd, @BiActiveNew, @BiActiveTableBooking, @BiActiveOrderFromTheTable, @BiActiveDelivery, @BiActiveAccounting, @BiActiveRemarks, @BiActiveChatBot, @BiActiveCustomThemes";
            var parameters = new
            {
                User_Id = model.User_Id,
                Headquarter_Id = model.Headquarter_Id,
                StrName = model.Name,
                StrAddress = model.Address,
                DtStart = model.DtStart,
                DtEnd = model.DtEnd,
                BiActiveNew = model.BiActive,
                BiActiveTableBooking = model.BiActiveTableBooking,
                BiActiveOrderFromTheTable = model.BiActiveOrderFromTheTable,
                BiActiveDelivery = model.BiActiveDelivery,
                BiActiveAccounting = model.BiActiveAccounting,
                BiActiveRemarks = model.BiActiveRemarks,
                BiActiveChatBot = model.BiActiveChatBot,
                BiActiveCustomThemes = model.BiActiveCustomThemes
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> CreateHeadquarter(CreateHeadquarterViewModel model)
        {
            var query = @"exec sp_CreateHeadquarter @User_Id, @Restaurant_Id, @StrName, @StrAddress, @DtStart, @DtEnd, @BiActiveNew, @BiActiveTableBooking, @BiActiveOrderFromTheTable, @BiActiveDelivery, @BiActiveAccounting, @BiActiveRemarks, @BiActiveChatBot, @BiActiveCustomThemes";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id,
                StrName = model.Name,
                StrAddress = model.Address,
                DtStart = model.DtStart,
                DtEnd = model.DtEnd,
                BiActiveNew = model.BiActive,
                BiActiveTableBooking = model.BiActiveTableBooking,
                BiActiveOrderFromTheTable = model.BiActiveOrderFromTheTable,
                BiActiveDelivery = model.BiActiveDelivery,
                BiActiveAccounting = model.BiActiveAccounting,
                BiActiveRemarks = model.BiActiveRemarks,
                BiActiveChatBot = model.BiActiveChatBot,
                BiActiveCustomThemes = model.BiActiveCustomThemes
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }
    }
}