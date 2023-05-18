using Dapper;
using System.Data.SqlClient;
using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Headquarters
{
    public class RepositoryHeadquarters : IRepositoryHeadquarters
    {
        private readonly string connectioString;
        public RepositoryHeadquarters(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> GetHeadquartersByRestaurant(GetHeadquartersByRestaurantViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_GetHeadquartersByRestaurant @User_Id, @Restaurant_Id",
                                                                new
                                                                { User_Id = model.User_Id, Restaurant_Id = model.Restaurant_Id });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> GetHeadquarterById(GetHeadquarterByIdViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_GetHeadquarterById @User_Id, @Headquarter_Id",
                                                                new
                                                                { User_Id = model.User_Id, Headquarter_Id = model.Headquarter_Id });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> DeleteHeadquarter(DeleteHeadquarterViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_DeleteHeadquarter @User_Id, @Headquarter_Id",
                                                                new
                                                                { User_Id = model.User_Id, Headquarter_Id = model.@Headquarter_Id });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> UpdateHeadquarter(UpdateHeadquarterViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_UpdateHeadquarter @User_Id, @Headquarter_Id, @StrName, @StrAddress, @DtStart, @DtEnd, @BiBooking, @BiOrderTable, @BiDelibery, @BiActive",
                                                                new
                                                                { User_Id = model.User_Id, 
                                                                    Headquarter_Id = model.@Headquarter_Id, 
                                                                    StrName = model.Name, 
                                                                    StrAddress = model.Address, 
                                                                    DtStart = model.DtStart, 
                                                                    DtEnd = model.DtEnd, 
                                                                    BiBooking = model.BiBooking,
                                                                    BiOrderTable = model.BiOrderTable, 
                                                                    BiDelibery = model.BiDelibery,
                                                                    BiActive = model.BiActive
                                                                });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> CreateHeadquarter(CreateHeadquarterViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_CreateHeadquarter @User_Id, @Restaurant_Id, @StrName, @StrAddress, @DtStart, @DtEnd, @BiBooking, @BiOrderTable, @BiDelibery, @BiActive",
                                                                new
                                                                {
                                                                    User_Id = model.User_Id,
                                                                    Restaurant_Id = model.Restaurant_Id,
                                                                    StrName = model.Name,
                                                                    StrAddress = model.Address,
                                                                    DtStart = model.DtStart,
                                                                    DtEnd = model.DtEnd,
                                                                    BiBooking = model.BiBooking,
                                                                    BiOrderTable = model.BiOrderTable,
                                                                    BiDelibery = model.BiDelibery,
                                                                    BiActive = model.BiActive
                                                                });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
    }
}
