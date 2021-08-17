using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace CBF_FaceRecognition.Classes
{
    class FaceData
    {
        public int FaceId { get; set; }
        public string PersonName { get; set; }
        public Image<Gray, byte> FaceImage { get; set; }
        public DateTime CreateDate { get; set; }
        public string FileName { get; set; }
    }
}
