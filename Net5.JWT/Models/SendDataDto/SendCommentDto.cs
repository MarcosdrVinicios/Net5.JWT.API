using Net5.JWT.Models.GetDataDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Net5.JWT.Models.SendDataDto
{
    public class SendCommentDto
    {
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        public string User { get; set; }

        public SendCommentDto(Comments c, string user)
        {
            Id = c.Id;
            Date = c.Date.ToString();
            Comment = c.Comment;
            User = user;
        }
    }
}
