using Microsoft.EntityFrameworkCore;
using Net5.JWT.Models;
using Net5.JWT.Models.GetDataDto;
using Net5.JWT.Models.SendDataDto;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Net5.JWT.Repositories
{
    public class Repository : IRepository
    {
        public readonly ApiContext Context;
        public Repository(ApiContext context)
        {
            Context = context;
        }

        public bool AddAnimal(Found_Animal animal)
        {
            if (!Context.Users.Any(u => u.Id == animal.UserRefId))
            {
                return false;
            }
            Context.Found_Animals.Add(animal);
            Context.SaveChanges();
            return true;
        }

        public Comments AddComment(GetCommentDto dto, int userId)
        {
            if (!Context.Found_Animals.Any(u => u.Id == dto.AnimalId))
            {
                return null;
            }
            var comment = new Comments {
                Date = DateTime.Now,
                UserId = userId,
                Comment = dto.Comment,
                Found_AnimalRefId = dto.AnimalId
            };

            Context.Comments.Add(comment);
            Context.SaveChanges();
            return comment;
        }

        public void AddShelter(Shelter shelter)
        {
            Context.Shelters.Add(shelter);
            Context.SaveChanges();
        }

        public List<SendAnimalDto> AllAnimals()
        {
            return Context.Found_Animals.Select(f => new SendAnimalDto(f)).ToList().OrderByDescending(x => x.Id).ToList();
        }

        public List<SendCommentDto> AllComments(int id)
        {
            return Context.Comments.Where(c => c.Found_AnimalRefId == id)
                                   .Select(f => new SendCommentDto(f, Context.Users.FirstOrDefault(u => u.Id == f.UserId).Username))
                                   .ToList();
        }

        public List<SendSheltersDto> AllShelters()
        {
            return Context.Shelters.Select(s => new SendSheltersDto(s)).ToList();
        }

        public bool ChangeRole(int id, ChangeRoleDto change)
        {
            var user = Context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            user.RoleRefId = change.Role;
            Context.Users.Update(user);
            Context.SaveChanges();
            return true;
        }

        public bool CheckFound(int userId, int id)
        {
            return Context.Found_Animals.Where(f => f.UserRefId == userId && f.Id == id).Count() > 0;
        }

        public User Create(User user)
        {
            if (Context.Users.Any(u => u.Email == user.Email)) {
                return null;
            }
            Context.Users.Add(user);
            user.Id = Context.SaveChanges();
            return user;
        }

        public bool DeleteAnimal(int id)
        {
            var animal = Context.Found_Animals.Include(c => c.Comments).FirstOrDefault(f => f.Id == id);
            if (animal == null)
            {
                return false;
            }
            Context.Found_Animals.Remove(animal);
            Context.SaveChanges();
            return true;
        }

        public bool DeleteComment(int id)
        {
            var comment = Context.Comments.FirstOrDefault(f => f.Id == id);
            if (comment == null)
            {
                return false;
            }
            Context.Comments.Remove(comment);
            Context.SaveChanges();
            return true;
        }

        public bool DeleteShelter(int id)
        {
            var shelter = Context.Shelters.Include(s => s.Found_Animals).FirstOrDefault(f => f.Id == id);
            if (shelter == null)
            {
                return false;
            }
            Context.Shelters.Remove(shelter);
            Context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = Context.Users.Include(f => f.Found_Animals).FirstOrDefault(f => f.Id == id);
            if (user == null)
            {
                return false;
            }
            Context.Users.Remove(user);
            Context.SaveChanges();
            return true;
        }

        public bool EditAnimal(EditAnimalDto dto)
        {
            var animal = Context.Found_Animals.Where(f => f.Id == dto.Id).FirstOrDefault();

            if (animal == null)
            {
                return false;
            }

            animal.Title = dto.Title;
            animal.Address = dto.Address;
            animal.Description = dto.Description;
            animal.ImagePath = dto.ImageUrl;
            animal.ShelterRefId = dto.ShelterId == -1 || dto.ShelterId == 0 ? null : dto.ShelterId;

            Context.Found_Animals.Update(animal);
            Context.SaveChanges();
            return true;
        }

        public bool EditComment(int id, GetEditCommentDto cmt)
        {
            var _comment = Context.Comments.FirstOrDefault(c => c.Id == id);
            if (_comment == null)
            {
                return false;
            }
            _comment.Comment = cmt.Comment;
            Context.Comments.Update(_comment);
            Context.SaveChanges();
            return true;
        }

        public bool EditShelter(Shelter shelter)
        {
            if (!Context.Shelters.Any(s => s.Id == shelter.Id))
            {
                return false;
            }
            Context.Shelters.Update(shelter);
            Context.SaveChanges();
            return true;
        }

        public User Get(string email)
        {
            return Context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public SendAnimalDto GetAnimal(int id)
        {
            return Context.Found_Animals.Where(f => f.Id == id).Select(f => new SendAnimalDto(f)).FirstOrDefault();
        }

        public List<SendAnimalDto> GetAnimalsAtHome()
        {
            return Context.Found_Animals.Where(f => f.ShelterRefId == null).OrderByDescending(f => f.Id).Select(f => new SendAnimalDto(f)).ToList();
        }

        public List<SendAnimalDto> GetAnimalsByShelter(int id)
        {
            return  Context.Found_Animals.Where(f => f.ShelterRefId == id).OrderByDescending(f => f.Id).Select(f => new SendAnimalDto(f)).ToList();
        }

        public List<SendAnimalDto> GetAnimalsByUser(int id)
        {
            return Context.Found_Animals.Where(f => f.UserRefId == id).OrderByDescending(f => f.Id).Select(f => new SendAnimalDto(f)).ToList();
        }

        public User GetById(int id)
        {
            return Context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<User_Role> GetRoles()
        {
            return Context.Roles.ToList();
        }

        public Shelter GetShelter(int id)
        {
            return Context.Shelters.FirstOrDefault(s => s.Id == id);
        }

        public List<GetShelterSelectDto> GetSheltersNameAndId()
        {
            return Context.Shelters.Select(s => new GetShelterSelectDto(s.Id, s.ShelterName)).ToList();
        }

        public List<UserListDto> GetUsers()
        {
            return Context.Users.Select(u => new UserListDto(u)).ToList();
        }
    }
}
