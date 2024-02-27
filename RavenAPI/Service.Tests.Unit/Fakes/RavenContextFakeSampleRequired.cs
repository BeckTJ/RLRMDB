using RavenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests.Unit.Fakes
{
    public class RavenContextFakeSampleRequired
    {
        private List<SampleRequired> _beer;
        private List<SampleRequired> _juice;

        public List<SampleRequired> WithSampleRequiredBeer()
        {
            return new List<SampleRequired>
            {
                new SampleRequired()
                {
                    MaterialNumber = 58245,
                    MaterialType = "Finish Product",
                    Vln = "Finished Product",
                    Assay = true,
                    Water = true,
                    Metals = true,
                    Chloride = true,
                    Boron = false,
                    Phosphorus = false,
                },

                new SampleRequired()
                {
                    MaterialNumber = 58245,
                    MaterialType = "Raw Material",
                    Vln = "New",
                    Assay = true,
                    Water = true,
                    Metals = true,
                    Chloride = false,
                    Boron = false,
                    Phosphorus = false,
                },
                new SampleRequired()
                {
                    MaterialNumber = 58245,
                    MaterialType = "Raw Material",
                    Vln = "old",
                    Assay = true,
                    Water = true,
                    Metals = false,
                    Chloride = false,
                    Boron = false,
                    Phosphorus = false,
                },

            };
        }
        public List<SampleRequired> WithSampleRequiredJuice()
        {
            return new List<SampleRequired>
            {
                new SampleRequired()
                {
                    MaterialNumber = 58243,
                    MaterialType = "Raw Material",
                    Vln = "New",
                    Assay = true,
                    Water = true,
                    Metals = true,
                    Chloride = false,
                    Boron = false,
                    Phosphorus = false,
                },
                new SampleRequired()
                {
                    MaterialNumber = 58243,
                    MaterialType = "Finished Product",
                    Vln = "Finished Product",
                    Assay = true,
                    Water = true,
                    Metals = true,
                    Chloride = false,
                    Boron = true,
                    Phosphorus = false,
                },

            };
        }

    }
}
