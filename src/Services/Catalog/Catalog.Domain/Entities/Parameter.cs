using Restmium.ERP.BuildingBlocks.Common.Entities;

namespace Restmium.ERP.Services.Catalog.Domain.Entities
{
    public class Parameter : DatabaseEntity
    {
        public long Id { get; set; }
        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Value of the parameter
        /// </summary>
        public string Value { get; set; }

        public long ParametersGroupId { get; set; }
        public virtual ParametersGroup ParametersGroup { get; set; }
    }
}