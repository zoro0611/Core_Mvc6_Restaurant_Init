using System;
using System.Collections.Generic;

namespace Restaurant_Init.Models.DBModels
{
    public partial class Menu : BaseEntity
    {
        public int Id { get; set; }
        public string DishName { get; set; } = null!;
        public int Price { get; set; }
        public string Image { get; set; } = null!;
        public string Active { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string CreateId { get; set; } = null!;
        public string CreateName { get; set; } = null!;
    }
}
