using System;
using System.Windows.Media.Imaging;
using PrisPilot.Models;
using PrisPilot.Services;

namespace PrisPilot.ViewModels
{
    public class TemplateViewModel : BaseViewModel<Template>
    {
        private ImageService _imageService;
        private Template _template;

        private int _templateID;
        public int TemplateID
        {
            get => _templateID;
            set
            {
                if (_templateID == value) return;
                _templateID = value;
                OnPropertyChanged();
            }
        }

        private string _introduction = string.Empty;
        public string Introduction
        {
            get => _introduction;
            set
            {
                if (_introduction == value) return;
                _introduction = value;
                OnPropertyChanged();
            }
        }

        private string _timeTable = string.Empty;
        public string TimeTable
        {
            get => _timeTable;
            set
            {
                if (_timeTable == value) return;
                _timeTable = value;
                OnPropertyChanged();
            }
        }

        private bool _aboutUs;
        public bool AboutUs
        {
            get => _aboutUs;
            set
            {
                if (_aboutUs == value) return;
                _aboutUs = value;
                OnPropertyChanged();
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _drosselMail = string.Empty;
        public string DrosselMail
        {
            get => _drosselMail;
            set
            {
                if (_drosselMail == value) return;
                _drosselMail = value;
                OnPropertyChanged();
            }
        }

        private string _drosselPhoneNumber = string.Empty;
        public string DrosselPhoneNumber
        {
            get => _drosselPhoneNumber;
            set
            {
                if (_drosselPhoneNumber == value) return;
                _drosselPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        private byte[]? _drosselLogo;
        public byte[]? DrosselLogo
        {
            get => _drosselLogo;
            set
            {
                if (_drosselLogo == value) return;
                _drosselLogo = value;
                OnPropertyChanged();
                UpdatePreviewFromLogo();
            }
        }

        private BitmapImage? _previewImage;
        public BitmapImage? PreviewImage
        {
            get => _previewImage;
            private set
            {
                if (_previewImage == value) return;
                _previewImage = value;
                OnPropertyChanged();
            }
        }

        public TemplateViewModel(Template template) : base(template)
        {
            _template = template;

            TemplateID = template.TemplateID;
            Introduction = template.Introduction ?? string.Empty;
            TimeTable = template.TimeTable ?? string.Empty;
            AboutUs = template.AboutUs;
            Title = template.Title ?? string.Empty;
            DrosselMail = template.DrosselMail ?? string.Empty;
            DrosselPhoneNumber = template.DrosselPhoneNumber ?? string.Empty;
            DrosselLogo = template.DrosselLogo;
        }

        private void UpdatePreviewFromLogo()
        {
            if (_drosselLogo == null)
            {
                PreviewImage = null;
                return;
            }

            _imageService = new();
            PreviewImage = _imageService.ReencodeToBitmap(_drosselLogo);
        }

        public bool IsTemplateValid()
        {
            bool result = true;

            // Title is required
            if (string.IsNullOrWhiteSpace(Title)) result = false;

            // Basic contact info should be present
            if (string.IsNullOrWhiteSpace(DrosselMail)) result = false;
            if (string.IsNullOrWhiteSpace(DrosselPhoneNumber)) result = false;

            return result;
        }

        public Template ToModel()
        {
            _template.TemplateID = TemplateID;
            _template.Introduction = Introduction;
            _template.TimeTable = TimeTable;
            _template.AboutUs = AboutUs;
            _template.Title = Title;
            _template.DrosselMail = DrosselMail;
            _template.DrosselPhoneNumber = DrosselPhoneNumber;
            _template.DrosselLogo = DrosselLogo;

            return _template;
        }
    }
}