using Restaurant_Init.Models.DBModels;
using Restaurant_Init.Repos;

namespace Restaurant_Init.Services
{
    public class MenuService
    {
        private readonly IRepository<Menu> _menuRepos;

        public MenuService(IRepository<Menu> menuRepos)
        {
            _menuRepos = menuRepos;
        }
        public List<Menu> GetAllMenu()
        {
            List<Menu> menus = _menuRepos.GetAll().ToList();
            return menus;
        }
    }
}
