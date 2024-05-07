using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace SignalRWebUI.Controllers
{
    public class QRController : Controller
    {


        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            // Bellek üzerinde işlem yapmak için bir MemoryStream oluşturuluyor.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // QR kodu oluşturmak için bir QRCodeGenerator nesnesi oluşturuluyor.
                QRCodeGenerator createQRCode = new QRCodeGenerator();

                // Verilen değerden bir QR kodu oluşturuluyor. ECC seviyesi Q olarak ayarlanmış.
                QRCodeGenerator.QRCode squareCode = createQRCode.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
                // ECC seviyesi, QR kodunun hata düzeltme yeteneğini belirtir.
                // L (Low), M (Medium), Q (Quartile) ve H (High) olmak üzere dört seviyede belirtilir. 
                // Her seviye, hata düzeltme kodlarının miktarını ve karmaşıklığını belirler.
                // QR kodunu bir Bitmap nesnesi olarak almak için GetGraphic metodu kullanılıyor.
                using (Bitmap image = squareCode.GetGraphic(10))
                {
                    // Oluşturulan QR kodu MemoryStream'e kaydediliyor.
                    image.Save(memoryStream, ImageFormat.Png);

                    // QR kodunun base64 formatına dönüştürülüp ViewBag'e ekleniyor.
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            // Index view'ını geri döndürülüyor.
            return View();
        }

    }
}
