using RavenAPI.DTO;

interface IRawMaterial
{
    List<RawMaterialDTO> RawMaterialSelection(int materialNumber, string vendor);
}