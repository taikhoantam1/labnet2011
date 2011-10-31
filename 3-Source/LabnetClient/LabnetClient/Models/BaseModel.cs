using LabnetClient.Constant;

namespace LabnetClient.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            ViewMode = ViewMode.Create;
        }
        /// <summary>
        /// Sets or gets type of current view
        /// </summary>
       public ViewMode ViewMode { get; set; }
    }
}