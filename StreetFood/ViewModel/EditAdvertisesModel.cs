using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.ViewModel
{
    public class EditAdvertisesModel
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public IFormFile ImgParth { get; set; }
    }
}
