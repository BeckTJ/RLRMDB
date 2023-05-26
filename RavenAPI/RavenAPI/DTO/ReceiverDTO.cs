using RavenAPI.Models;

namespace RavenAPI.DTO
{
    public class ReceiverDTO
    {
        public int? MaterialNumber { get; set; }
        public string? ReceiverName { get; set; }
        public int? MaxLevel { get; set; }

        public static List<ReceiverDTO> SetReceivers(int materialNumber)
        {
            RavenDBContext ctx = new RavenDBContext();
            return ctx.SystemReceivers
                .Where(sr => sr.MaterialNumber == materialNumber)
                .Select(sr => new ReceiverDTO
                {
                    MaterialNumber = sr.MaterialNumber,
                    ReceiverName = sr.ReceiverName,
                    MaxLevel = sr.MaxReceiverLevel
                }).ToList();
        }
        /*
        static List<ReceiverDTO> Receivers { get; } = ctx.SystemReceivers
            .Select(sr => new ReceiverDTO
            {
                MaterialNumber = sr.MaterialNumber,
                ReceiverName = sr.ReceiverName,
                MaxLevel = sr.MaxReceiverLevel
            }).ToList();
        */
    }
}
