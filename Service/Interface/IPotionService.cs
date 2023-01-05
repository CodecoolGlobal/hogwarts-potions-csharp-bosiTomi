using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Service.Interface;

public interface IPotionService
{
    Task AddPotion(Potion potion);
    Task<Potion> GetPotion(long? potionId);
    Task<List<Potion>> GetAllPotions();
    Task UpdatePotion(Potion potion);
    Task DeletePotion(long id);
    Task<Potion> CreateAPotion(Potion potion, Student student);
    void Update(Potion potion);
    void SaveChanges();
    void RemovePotion(Potion potion);
}