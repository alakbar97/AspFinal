using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vizew.WebUI.Models.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime? CreationDate { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DeletedDate { get; set; }
        [ScaffoldColumn(false)]
        public int? DeletedId { get; set; }
    }
}