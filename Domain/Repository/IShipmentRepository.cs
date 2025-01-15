using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Domain.Repositories
{
    public interface IShipmentRepository
    {
        Task<List<ShipmentDto>> GetAllShipmentsAsync();
        Task<ShipmentDto> GetShipmentByIdAsync(int shipmentId);
        Task AddShipmentAsync(ShipmentDto shipment);
        Task UpdateShipmentAsync(ShipmentDto shipment);
        Task DeleteShipmentAsync(int shipmentId);
    }
}