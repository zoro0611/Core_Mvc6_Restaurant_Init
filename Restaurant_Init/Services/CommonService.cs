using Restaurant_Init.Models;
using Restaurant_Init.Models.DBModels;
using Restaurant_Init.Repos;

namespace Restaurant_Init.Services
{
    public class CommonService
    {
        private readonly IRepository<User> _userRepository;

        public CommonService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUser()
        {
            return _userRepository.GetAllReadOnly().ToList();
        }
    }
}
