
using RavenDAL.DTO;

namespace RavenBAL.Interface
{
    public interface IRawMaterial
    {
        RawMaterialDTO CreateRawMaterialDrum(CreateRawMaterialDTO material);
        RawMaterialDTO SampleRawMaterialDrum(RawMaterialDTO material);
        IEnumerable<RawMaterialDTO> SelectRawMaterials(int materialNumber);
    }
}
