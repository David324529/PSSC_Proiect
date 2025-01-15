using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Domain.Repositories;

namespace Data.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ShoppingContext _context;

        public ShipmentRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task<List<ShipmentDto>> GetAllShipmentsAsync()
        {
            return await _context.Shipments.ToListAsync();
        }

        public async Task<ShipmentDto> GetShipmentByIdAsync(int shipmentId)
        {
            return await _context.Shipments.FindAsync(shipmentId);
        }

        public async Task AddShipmentAsync(ShipmentDto shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShipmentAsync(ShipmentDto shipment)
        {
            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShipmentAsync(int shipmentId)
        {
            var shipment = await _context.Shipments.FindAsync(shipmentId);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}