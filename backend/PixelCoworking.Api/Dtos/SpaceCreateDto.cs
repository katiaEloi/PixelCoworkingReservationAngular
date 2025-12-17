namespace PixelCoworking.Api.Dtos
{
   public record SpaceCreateDto(string Name, int Type, int Capacity, bool HasPrivateBathroom);

   public record SpaceUpdateDto(string Name, int Type, int Capacity, bool HasPrivateBathroom);
}
