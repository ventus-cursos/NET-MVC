using System.Collections.Generic;

namespace Ventus.Models
{
    public class Ciudad : Paged
    {
        public List<BE.Ciudad> Data { get; set; }
    }
}