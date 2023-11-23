using RavenDB.DTO;
using RavenDB.Models;

namespace Service.Contracts
{
    public interface IRawMaterialService
    {
        RawMaterial CreateRawMaterialDrum(CreateRawMaterialDTO material);
        RawMaterialDTO SampleRawMaterialDrum(RawMaterialDTO material);
        IEnumerable<RawMaterial> ApprovedRawMaterial(int materialNumber);
    }
}