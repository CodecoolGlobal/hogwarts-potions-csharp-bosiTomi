using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomService.GetAllRooms();
        }

        [HttpPost]
        public async Task AddRoom([FromBody] Room room)
        {
            await _roomService.AddRoom(room);
        }

        [HttpGet("/{Id:long}")]
        public async Task<Room> GetRoomById(long id)
        {
            return await _roomService.GetRoom(id);
        }

        [HttpPut("/{Id}")]
        public async void UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
            updatedRoom.ID = id;
            await _roomService.UpdateRoom(updatedRoom);
        }

        [HttpDelete("/{Id:long}")]
        public async Task DeleteRoomById(long id)
        {
            await _roomService.DeleteRoom(id);
        }

        [HttpGet("/rat-owners")]
        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _roomService.GetRoomsForRatOwners();
        }
    }
}
