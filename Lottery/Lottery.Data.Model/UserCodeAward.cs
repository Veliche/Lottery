using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Data.Model
{
    [Table("dbo.UserCodeAwards")]

    public class UserCodeAward : IEntity
    {
        [Key]
        [Column("UserCodeAwardID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("UserCodeId")]
        public int UserCodeId { get; set; }
        public UserCode UserCode { get; set; }
        [Column("AwardID")]
        public int AwardID { get; set; }
        public Award Award { get; set; }
        public DateTime WonAt { get; set; }
    }
}
