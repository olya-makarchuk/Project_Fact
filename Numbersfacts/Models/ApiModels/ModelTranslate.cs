using System.Collections.Generic;

namespace Numbersfacts.Models.ApiModels
{
    public class ResponseData
    {
        public string translatedText { get; set; }
    }

    public class ModelTranslate
    {
        public ResponseData responseData { get; set; }
    }

}
