using System.Collections.Generic;

namespace Ventus.Models
{
    public class Orden : Paged
    {
        public List<BE.Orden> Data { get; set; }
    }
}