using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Service;

public class RoomService : IRoomService
{
    private readonly HogwartsContext _context;

    public RoomService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task AddRoom(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }

    public Task<Room> GetRoom(long roomId)
    {
        return _context.Rooms.Include(r => r.Residents).FirstOrDefaultAsync(room => room.ID == roomId);
    }

    public Task<List<Room>> GetAllRooms()
    {
        return _context.Rooms.Include(r => r.Residents).ToListAsync();
    }

    public async Task UpdateRoom(Room room)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRoom(long id)
    {
        var roomToDelete = await GetRoom(id);
        if (roomToDelete != null)
        {
            _context.Rooms.Remove(roomToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public Task<List<Room>> GetRoomsForRatOwners()
    {
        return _context.Rooms.Include(r => r.Residents)
            .Where(room => room.Residents.Any(student => student.PetType != PetType.Cat || student.PetType != PetType.Owl))
            .ToListAsync();
    }
}