using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace Censor_Me
{
    internal class Detect
    {
        //顔が検出されたかどうかのフラグ
        public bool face = false;

        public Image Face(Image load_image, bool apply)
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

                face = faces.Length > 0;

                if (!face)
                {
                    // 元の画像をそのまま返す
                    return load_image;
                }

                // 顔に四角を描画
                foreach (var rect in faces)
                {
                    //Cv2.Rectangle(mat, rect, new Scalar(0, 0, 255), 3);

                    // モザイク処理（ブラーに変更）
                    if (apply == true)
                    {
                        var roi = mat[rect];

                        // ガウシアンブラーを適用（カーネルサイズ (int, int)、標準偏差 int）
                        Cv2.GaussianBlur(roi, roi, new OpenCvSharp.Size(125, 125), 0);

                        // ぼかしたROIを元の画像に適用
                        roi.CopyTo(mat[rect]);
                    }
                }

                // Mat型をImage型に変換して返す
                return BitmapConverter.ToBitmap(mat);
            }
        }
    }
}
