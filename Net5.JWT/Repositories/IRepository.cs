using Net5.JWT.Models;
using Net5.JWT.Models.SendDataDto;
using Net5.JWT.Models.GetDataDto;
using System.Collections.Generic;

namespace Net5.JWT.Repositories
{
    public interface IRepository
    {

        User Create(User user);
        User Get(string email);

        User GetById(int id);

        List<UserListDto> GetUsers();

        List<User_Role> GetRoles();

        bool DeleteUser(int id);

        bool ChangeRole(int id, ChangeRoleDto change);

        bool AddAnimal(Found_Animal animal);
        List<SendAnimalDto> AllAnimals();
        SendAnimalDto GetAnimal(int id);

        List<SendAnimalDto> GetAnimalsByShelter(int id);

        List<SendAnimalDto> GetAnimalsByUser(int id);

        List<SendAnimalDto> GetAnimalsAtHome();
                                                        
        bool CheckFound(int userId, int id);

        bool EditAnimal(EditAnimalDto dto);

        bool DeleteAnimal(int id);
        List<SendSheltersDto> AllShelters();

        void AddShelter(Shelter shelter);

        Shelter GetShelter(int id);

        bool EditShelter(Shelter shelter);

        bool DeleteShelter(int id);

        Comments AddComment(GetCommentDto dto, int userId);

        List<SendCommentDto> AllComments(int id);

        bool DeleteComment(int id);

        bool EditComment(int id, GetEditCommentDto cmt);

        List<GetShelterSelectDto> GetSheltersNameAndId();

    }
}
