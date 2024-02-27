using Shared.DTO;

namespace Service.Repo.Contracts
{
    public interface IQualityControl
    {
        Task<RequiredSampleDTO> CheckRequiredSample(MaterialVendorDTO material);
        void SubmitSample(string sampleType, long inspectionLotNumber);

    }
}
