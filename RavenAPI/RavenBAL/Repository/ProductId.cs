using RavenDAL.DTO;
using RavenDAL.Interface;
using RavenBAL.Interface;
using RavenBAL.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Repository
{
    public class ProductId : IProductId
    {
        private readonly IMaterialData<MaterialDataDTO> _materialData;
        private readonly IRawMaterial<RawMaterialDTO> _rawMaterialDrum;

        public ProductId(IMaterialData<MaterialDataDTO> materialData, IRawMaterial<RawMaterialDTO> rawMaterialDrum)
        {
            _materialData = materialData;
            _rawMaterialDrum = rawMaterialDrum;
        }
        public string GetProductId(int materialNumber)
        {
            var material = _materialData.GetById(materialNumber) ?? throw new ArgumentNullException(nameof(materialNumber));

            return GetNextProductId(_rawMaterialDrum.GetAllByMaterialNumber((int)material.MaterialNumber).LastOrDefault().ProductId) ?? GetFirstProductId(material);
        }
        private string GetFirstProductId(MaterialDataDTO material)
        {
            return material.SequenceId + material.MaterialCode;
        }
        private string GetNextProductId(string productId)
        {
            int id;
            string code;

            if (productId.Length == 10 || productId.Length == 6)
            {
                id = int.Parse(productId[..4]) + 1;
                code = productId.Substring(4, 6);
            }
            else
            {
                id = int.Parse(productId[..3]) + 1;
                code = productId.Substring(3, 5);
            }
            return id + code;
        }
    }
}
