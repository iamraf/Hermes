using Hermes.Model.Models;

namespace Hermes.View.upload
{
    interface IUploadView
    {
        string name { get; }
        float price { get; }
        Location location { get; }
        string description { get; }
        SubCategory subcategory { get; }
        public string GetImagePath { get; }
    }
}
