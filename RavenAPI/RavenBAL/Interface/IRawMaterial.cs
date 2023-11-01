using RavenDAL.DTO;
using RavenDAL.Models;

namespace RavenBAL.Interface
{
    public interface IRawMaterialService
    {
        RawMaterial CreateRawMaterialDrum(CreateRawMaterialDTO material);
        RawMaterialDTO SampleRawMaterialDrum(RawMaterialDTO material);
        IEnumerable<RawMaterialDTO> ApprovedRawMaterial(int materialNumber);
    }
}