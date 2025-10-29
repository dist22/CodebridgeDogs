using CodebridgeDogs.Enums;

namespace CodebridgeDogs.Dto_s;

public record GetDogsDto(DogSortAttributes SortAttribute,int PageNumber = 1, int PageSize = 10,bool Desc = false);