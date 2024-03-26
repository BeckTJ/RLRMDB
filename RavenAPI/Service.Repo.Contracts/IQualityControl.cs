using Shared.DTO;

namespace Service.Repo.Contracts
{
    public interface IQualityControl
    {
        Task<IEnumerable<RequliredSampleDTO>> CheckRequiredSample(MaterialVendorDTO material);
        void SubmitSample(string sampleType, long inspectionLotNumber);
        Task<IEnumerable<RequliredSampleDTO>> GetRequiredSample(int parentMaterialNumber,string materialType);

    }
}
