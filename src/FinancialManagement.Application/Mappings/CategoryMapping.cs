namespace FinancialManagement.Application.Mappings;

public static class CategoryMapping
{
    public static Category ToEntity(this CategoryDTO categoryDTO) => new(categoryDTO.Id, categoryDTO.Name);

    public static CategoryDTO ToDTO(this Category category) => new(category.Id, category.Name!);

    public static Category ToEntity(this CategoryPostDTO categoryPostDTO) => new(categoryPostDTO.Name);

    public static IEnumerable<CategoryDTO> ToListDTO(this IEnumerable<Category> categories) => categories.Select(x => x.ToDTO());

    public static Category ToEntity(this CategoryPutDTO categoryDTO) => new(categoryDTO.Id, categoryDTO.Name);
}
