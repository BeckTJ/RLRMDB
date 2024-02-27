using Shared.DTO;

namespace Service.Repo.Contracts
{
    public interface IRawMaterialDrum
    {
        Task<RawMaterialDrumDTO> CreateRawMaterialDrum(CreateRawMaterialDTO rawMaterial);
        void SubmitRawMaterialSample(RawMaterialDrumDTO drum);
    }
}
