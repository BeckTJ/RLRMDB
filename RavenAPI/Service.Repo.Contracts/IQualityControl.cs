using Shared.DTO;

namespace Service.Repo.Contracts
{
    public interface IQualityControl
    {
        Task<IEnumerable<RequiredSampleDTO>> CheckRequiredSample(MaterialVendorDTO material);
        void SubmitSample(string sampleType, long inspectionLotNumber);
        Task<IEnumerable<RequiredSampleDTO>> GetRequiredSample(int parentMaterialNumber,string materialType);

    }
}
