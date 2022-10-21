using FoxPhonebook.Application.Common.Models;

namespace FoxPhonebook.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
        => (queryable.ProjectTo<TDestination>(configuration).ToListAsync());
}
