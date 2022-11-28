using MagicVillaAPi.Models.DTO;

namespace MagicVillaAPi.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>()
        {
            new VillaDto()
            {
                Id = 1, Name = "Pool view"
            },
            new VillaDto()
            {
                Id = 2, Name = "Beach view"
            },
        };
    }
}
