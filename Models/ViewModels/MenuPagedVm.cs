namespace BepNha.Web.Models.ViewModels
{
    public class MenuPagedVm
    {
        public List<MenuItemVm> Items { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<string> Categories { get; set; } = new();
    }
}
