using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Livet;
using System.IO;
using Inscribe.Configuration;
using Inscribe.Common;

namespace Inscribe.ViewModels.Dialogs.SettingSub
{
    public class KeyAssignConfigViewModel : ViewModel, IApplyable
    {
        public KeyAssignConfigViewModel()
        {
            KeyAssignIndex = KeyAssignFiles.ToList().IndexOf(Setting.Instance.KeyAssignProperty.KeyAssignFile);
        }

        public String[] KeyAssignFiles
        {
            get
            {
                var keyAssignPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Define.ApplicationName, Define.KeyAssignDirectory);
                return Directory.GetFiles(keyAssignPath, "*.xml")
                    .Select(s => Path.GetFileName(s))
                    .OrderBy(s => s)
                    .ToArray();
            }
        }

        private int _keyAssignIndex;
        public int KeyAssignIndex
        {
            get { return _keyAssignIndex; }
            set
            {
                _keyAssignIndex = value;
                RaisePropertyChanged(() => KeyAssignIndex);
            }
        }

        public void Apply()
        {
            if (KeyAssignIndex >= 0 && KeyAssignIndex < KeyAssignFiles.Length)
                Setting.Instance.KeyAssignProperty.KeyAssignFile = KeyAssignFiles[KeyAssignIndex];
            else
                Setting.Instance.KeyAssignProperty.KeyAssignFile = "default.xml";
        }
    }
}
