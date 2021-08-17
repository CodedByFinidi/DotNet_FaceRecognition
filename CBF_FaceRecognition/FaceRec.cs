using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CBF_FaceRecognition.Classes;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace CBF_FaceRecognition
{
    public partial class FaceRec : Form
    {
        private static readonly string TrainedImagesPath = Directory.GetCurrentDirectory() + @"\TrainedImages";
        private static readonly string TrainedFilePath = Directory.GetCurrentDirectory() + @"\TrainedFile";
        private static bool _isTrained;
        private readonly CascadeClassifier _cascadeClassifier =
            new(Application.StartupPath + @"Haarcascades\haarcascade_frontalface_default.xml");
        private readonly List<FaceData> _facesData = new();
        private readonly Mat _frame = new();
        private Image<Bgr, byte> _currentFrame;
        private bool _enableSaveImage;
        private bool _faceDetectionEnabled;
        private EigenFaceRecognizer _recognizer;
        private VideoCapture _videoCapture;

        public FaceRec()
        {
            InitializeComponent();

            if (!Directory.Exists(TrainedImagesPath)) Directory.CreateDirectory(TrainedImagesPath);
            if (!Directory.Exists(TrainedFilePath)) Directory.CreateDirectory(TrainedFilePath);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            //Dispose of Capture if it was created before
            _videoCapture?.Dispose();
            _videoCapture = new VideoCapture {FlipHorizontal = true};
            _videoCapture.ImageGrabbed += ProcessFrame;
            _videoCapture.Start();
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            //Step 1: Video Capture
            if (_videoCapture == null || _videoCapture.Ptr == IntPtr.Zero) return;
            _videoCapture.Retrieve(_frame);
            _currentFrame = _frame.ToImage<Bgr, byte>()
                .Resize(picCapture.Width, picCapture.Height, Inter.Cubic);

            if (_faceDetectionEnabled)
            {
                var grayImage = new Mat();
                CvInvoke.CvtColor(_currentFrame, grayImage, ColorConversion.Bgr2Gray);
                //Enhance the image to get better result
                CvInvoke.EqualizeHist(grayImage, grayImage);
                var faces =
                    _cascadeClassifier.DetectMultiScale(grayImage, 1.1, 3, Size.Empty, Size.Empty);

                if (faces.Length > 0)
                    foreach (var face in faces)
                    {
                        var resultImage = _currentFrame.Convert<Bgr, byte>();
                        CvInvoke.Rectangle(_currentFrame, face, new Bgr(Color.Red).MCvScalar);
                        _currentFrame.ROI = Rectangle.Empty;
                        resultImage.ROI = face;
                        picDetected.SizeMode = PictureBoxSizeMode.StretchImage;
                        picDetected.Image = resultImage.ToBitmap();
                        if (_enableSaveImage & (txtPersonName.Text != ""))
                            //we will save 10 images with delay a second for each image 
                            //to avoid hang GUI we will create a new task
                            Task.Factory.StartNew(() =>
                            {
                                for (var i = 0; i < 10; i++)
                                {
                                    resultImage.Resize(200, 200, Inter.Cubic).Save(TrainedImagesPath + @"\" +
                                        txtPersonName.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
                                        ".jpg");
                                    Thread.Sleep(500);
                                }
                            });
                        _enableSaveImage = false;
                        if (btnAddPerson.InvokeRequired)
                            btnAddPerson.Invoke(new ThreadStart(delegate { btnAddPerson.Enabled = true; }));
                        // Step 5: Recognize the face 
                        if (_isTrained)
                        {
                            var grayFaceResult = resultImage.Convert<Gray, byte>().Resize(200, 200, Inter.Cubic);
                            CvInvoke.EqualizeHist(grayFaceResult, grayFaceResult);
                            var result = _recognizer.Predict(grayFaceResult);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox1.Image = grayFaceResult.ToBitmap();

                            Debug.WriteLine(result.Label + ". " + result.Distance);
                            //Here results found known faces
                            if (result.Label != -1)
                            {
                                pictureBox2.Image = _facesData[result.Label].FaceImage.ToBitmap();
                                CvInvoke.PutText(_currentFrame, _facesData[result.Label].PersonName,
                                    new Point(face.X - 2, face.Y - 2),
                                    FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                CvInvoke.Rectangle(_currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                            }
                            //here results did not found any know faces
                            else
                            {
                                CvInvoke.PutText(_currentFrame, "Unknown", new Point(face.X - 2, face.Y - 2),
                                    FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                CvInvoke.Rectangle(_currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);
                            }
                        }
                    }
            }

            picCapture.Image = _currentFrame.ToBitmap();
            if (_currentFrame != null) _currentFrame.Dispose();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            _faceDetectionEnabled = !_faceDetectionEnabled;
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            btnAddPerson.Enabled = false;
            _enableSaveImage = true;
        }

        private void TrainRecognizer()
        {
            var personsLabels = new VectorOfInt();
            var mats = new VectorOfMat();
            var contador = 0;
            personsLabels.Clear();
            _facesData.Clear();
            mats.Clear();
            try
            {
                var files = Directory.GetFiles(TrainedImagesPath, "*.jpg", SearchOption.AllDirectories);
                var imagesCount = new int[files.Length];
                foreach (var file in files)
                {
                    var name = file.Split('\\').Last().Split('_')[0];
                    var createDate = File.GetCreationTime(file);
                    imagesCount[contador] = contador;
                    var trainedImage = new Image<Gray, byte>(file).Resize(200, 200, Inter.Cubic);
                    CvInvoke.EqualizeHist(trainedImage, trainedImage);
                    var faceData = new FaceData
                    {
                        FaceId = contador,
                        FaceImage = trainedImage,
                        PersonName = name,
                        CreateDate = createDate,
                        FileName = file
                    };
                    mats.Push(trainedImage.Mat);
                    contador++;
                    _facesData.Add(faceData);
                }

                personsLabels.Push(imagesCount);
                if (_facesData.Any())
                {
                    _recognizer = new EigenFaceRecognizer(imagesCount.Length);
                    _recognizer.Train(mats, personsLabels);
                    _recognizer.Write(TrainedFilePath + @"\trained.cbf");
                    _isTrained = true;
                    return;
                }

                _isTrained = false;
            }
            catch (Exception ex)
            {
                _isTrained = false;
                MessageBox.Show(@"Error in Train Images: " + ex.Message);
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            TrainRecognizer();
        }
    }
}