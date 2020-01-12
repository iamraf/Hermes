using Hermes.Model.Models;
/* IUploadView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
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
