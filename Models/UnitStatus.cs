using WorkMate.Views;
using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class UnitStatus : INotifyPropertyChanged, IEditableObject
    {
        public UnitStatus backupCopy;
        private bool inEdit;

        public short UnitNo { get; set; }


        ushort _ProcessCode;
        public string Process { get => MainView.DicKeyValues[ProcessCode].iValue; }
        public ushort ProcessCode
        {
            get => _ProcessCode;
            set
            {
                if (_ProcessCode != value)
                {
                    _ProcessCode = value;
                    OnPropertyChanged(nameof(ProcessCode));
                }
            }
        }

        ushort _SideType;
        public string Side { get => MainView.DicKeyValues[SideType].iValue; }
        public ushort SideType
        {
            get => _SideType;
            set
            {
                if (_SideType != value)
                {
                    _SideType = value;
                    OnPropertyChanged(nameof(SideType));
                }
            }
        }

        public string Unit {  get; set; }

        int _InvestNo;
        public int InvestNo
        {
            get => _InvestNo;
            set
            {
                if (_InvestNo != value)
                {
                    _InvestNo = value;
                    OnPropertyChanged(nameof(InvestNo));
                }
            }
        }

        ushort _ProcessLine;
        public string Line { get => MainView.DicKeyValues[ProcessLine].iValue; }
        public ushort ProcessLine
        {
            get => _ProcessLine;
            set
            {
                if (_ProcessLine != value)
                {
                    _ProcessLine = value;
                    OnPropertyChanged(nameof(ProcessLine));
                }
            }
        }

        string _Installation;
        public string Installation
        {
            get => _Installation;
            set
            {
                if (_Installation != value)
                {
                    _Installation = value;
                    OnPropertyChanged(nameof(Installation));
                }
            }
        }

        string _IsRTSO;
        public string IsRTSO
        {
            get => _IsRTSO;
            set
            {
                if (_IsRTSO != value)
                {
                    _IsRTSO = value;
                    OnPropertyChanged(nameof(IsRTSO));
                }
            }
        }

        string _IsPower;
        public string IsPower
        {
            get => _IsPower;
            set
            {
                if (_IsPower != value)
                {
                    _IsPower = value;
                    OnPropertyChanged(nameof(IsPower));
                }
            }
        }

        string _IsYTSO;
        public string IsYTSO
        {
            get => _IsYTSO;
            set
            {
                if (_IsYTSO != value)
                {
                    _IsYTSO = value;
                    OnPropertyChanged(nameof(IsYTSO));
                }
            }
        }

        string _SetupSingle;
        public string SetupSingle
        {
            get => _SetupSingle;
            set
            {
                if (_SetupSingle != value)
                {
                    _SetupSingle = value;
                    OnPropertyChanged(nameof(SetupSingle));
                }
            }
        }

        string _SetupIntegrated;
        public string SetupIntegrated
        {
            get => _SetupIntegrated;
            set
            {
                if (_SetupIntegrated != value)
                {
                    _SetupIntegrated = value;
                    OnPropertyChanged(nameof(SetupIntegrated));
                }
            }
        }

        string _Consistency;
        public string Consistency
        {
            get => _Consistency;
            set
            {
                if (_Consistency != value)
                {
                    _Consistency = value;
                    OnPropertyChanged(nameof(Consistency));
                }
            }
        }

        string _Memo;
        public string Memo
        {
            get => _Memo;
            set
            {
                if (_Memo != value)
                {
                    _Memo = value;
                    OnPropertyChanged(nameof(Memo));
                }
            }
        }

        string _ProductNote;
        public string ProductNote
        {
            get => _ProductNote;
            set
            {
                if (_ProductNote != value)
                {
                    _ProductNote = value;
                    OnPropertyChanged(nameof(ProductNote));
                }
            }
        }

        DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get => _UpdateDate;
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    OnPropertyChanged(nameof(UpdateDate));
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

         // IEditableObject
        public void BeginEdit()
        {
            if (!inEdit)
            {
                backupCopy = new UnitStatus { 
                    UnitNo          = this.UnitNo,
                    ProcessCode     = this.ProcessCode,
                    SideType        = this.SideType,
                    Unit            = this.Unit,
                    InvestNo        = this.InvestNo,
                    ProcessLine     = this.ProcessLine,
                    Installation    = this.Installation,
                    IsRTSO          = this.IsRTSO,
                    IsPower         = this.IsPower,
                    IsYTSO          = this.IsYTSO,
                    SetupSingle     = this.SetupSingle,
                    SetupIntegrated = this.SetupIntegrated,
                    Consistency     = this.Consistency,
                    Memo            = this.Memo,
                    ProductNote     = this.ProductNote,
                };
                inEdit = true;
            }
        }

        public void CancelEdit()
        {
            if (inEdit)
            {
                this.UnitNo         = backupCopy.UnitNo;
                this.ProcessCode    = backupCopy.ProcessCode;
                this.SideType       = backupCopy.SideType;
                this.Unit           = backupCopy.Unit;
                this.InvestNo       = backupCopy.InvestNo;
                this.ProcessLine    = backupCopy.ProcessLine;
                this.Installation   = backupCopy.Installation;
                this.IsRTSO         = backupCopy.IsRTSO;
                this.IsPower        = backupCopy.IsPower;
                this.IsYTSO         = backupCopy.IsYTSO;
                this.SetupSingle    = backupCopy.SetupSingle;
                this.SetupIntegrated = backupCopy.SetupIntegrated;
                this.Consistency    = backupCopy.Consistency;
                this.Memo           = backupCopy.Memo;
                this.ProductNote    = backupCopy.ProductNote;
                inEdit              = false;
            }
        }

        public void EndEdit()
        {
            if (inEdit)
            {
                backupCopy  = null;
                inEdit      = false;
            }
        }
    }
}