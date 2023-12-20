using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers.Domain.Models
{
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        public string? TagName { get; set; }
        public string? TextColor { get; set; }

        public string? BackgroundColor { get; set; }

        //public Tag(string _tagName, Color _color)
        //{
        //    TagName = _tagName;
        //    Color = _color;
        //}
    }
}
