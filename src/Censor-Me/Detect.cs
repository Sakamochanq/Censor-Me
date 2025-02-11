using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;

namespace Censor_Me
{
    internal class Detect
    {
        public Image Face(Image load_image)
        {
            // Image型をMat型に変換
            Mat mat = BitmapConverter.ToMat((Bitmap)load_image);

            // カスケードファイルの読み込み
            using (var cascade = new CascadeClassifier("./assets/haarcascade_frontalface_default.xml"))
            {
                // 顔認識
                var faces = cascade.DetectMultiScale(
                    mat,
                    scaleFactor: 1.2,
                    minNeighbors: 6,
                    minSize: new OpenCvSharp.Size(50, 50)
                );

                // 顔に四角を描画
                foreach (var rect in faces)
                {
                    Cv2.Rectangle(mat, rect, new Scalar(0, 0, 255), 3);
                }

                // Mat型をImage型に変換して返す
                return BitmapConverter.ToBitmap(mat);
            }
        }
    }
}
