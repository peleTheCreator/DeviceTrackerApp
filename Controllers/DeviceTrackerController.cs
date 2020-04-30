using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace DeviceTracker.Controllers
{
    public class DeviceTrackerController : ApiController
    {
        [Route("api/withinRegion")]
        [HttpPost]
        public object withinRegion(HttpRequestMessage request)
        {
            string body = request.Content.ReadAsStringAsync().Result;
            var AdroidDeviceLog = new AndriodDevice.AndroidDeviceLog();
            string json1;
            json1 = AdroidDeviceLog.UpdateDeviceTrackingregion(body);
            var JavaScriptSerializer = new JavaScriptSerializer();
            var Json = JavaScriptSerializer.Deserialize(json1, typeof(object));
            return Json;
        }

        [Route("api/UploadData")]
        [HttpPost]
        public object UploadData(HttpRequestMessage request)
        {
            string body = request.Content.ReadAsStringAsync().Result;
            var AdroidDeviceLog = new AndriodDevice.AndroidDeviceLog();
            string json1;
            json1 = AdroidDeviceLog.UploadDeviceLocation(body);
            var JavaScriptSerializer = new JavaScriptSerializer();
            var Json = JavaScriptSerializer.Deserialize(json1, typeof(object));
            return Json;
        }
    }
}