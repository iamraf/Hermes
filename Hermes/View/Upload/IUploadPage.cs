using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.Upload
{
    interface IUploadPage
    {
        string name { get; }
        float price { get; }
        Location location { get; }
        string description { get; }
        SubCategory subcategory { get; }
    }
}
