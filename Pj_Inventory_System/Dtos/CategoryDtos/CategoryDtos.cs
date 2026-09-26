namespace Pj_Inventory_System.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
    }

    public class UpdateCategoryDto : CreateCategoryDto
    {
        public int CategoryID { get; set; }
        public string UID { get; set; }   // ← مهم جداً للفيو والكنترولر
    }

    public class CategoryDto : UpdateCategoryDto
    {
    }
}
