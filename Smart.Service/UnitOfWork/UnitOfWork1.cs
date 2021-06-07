
using Smart.Data;
using Smart.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Smart.Service
{
    public partial class UnitOfWork : IDisposable
    {
	
		private DDepartment _DDepartment;
      
        public DDepartment DDepartment
        {
            get
            {
                if (this._DDepartment == null)
                {
                    this._DDepartment = new DDepartment(context);
                }
                return _DDepartment;
            }
        }

		
		private DDoctor _DDoctor;
      
        public DDoctor DDoctor
        {
            get
            {
                if (this._DDoctor == null)
                {
                    this._DDoctor = new DDoctor(context);
                }
                return _DDoctor;
            }
        }

		
		private DOperationImage _DOperationImage;
      
        public DOperationImage DOperationImage
        {
            get
            {
                if (this._DOperationImage == null)
                {
                    this._DOperationImage = new DOperationImage(context);
                }
                return _DOperationImage;
            }
        }

		
		private DOperationRecord _DOperationRecord;
      
        public DOperationRecord DOperationRecord
        {
            get
            {
                if (this._DOperationRecord == null)
                {
                    this._DOperationRecord = new DOperationRecord(context);
                }
                return _DOperationRecord;
            }
        }

		
		private DOperationVideo _DOperationVideo;
      
        public DOperationVideo DOperationVideo
        {
            get
            {
                if (this._DOperationVideo == null)
                {
                    this._DOperationVideo = new DOperationVideo(context);
                }
                return _DOperationVideo;
            }
        }

		
		private DPatient _DPatient;
      
        public DPatient DPatient
        {
            get
            {
                if (this._DPatient == null)
                {
                    this._DPatient = new DPatient(context);
                }
                return _DPatient;
            }
        }

		
		private DSmartContext _DSmartContext;
      
        public DSmartContext DSmartContext
        {
            get
            {
                if (this._DSmartContext == null)
                {
                    this._DSmartContext = new DSmartContext(context);
                }
                return _DSmartContext;
            }
        }

		
		private DUser _DUser;
      
        public DUser DUser
        {
            get
            {
                if (this._DUser == null)
                {
                    this._DUser = new DUser(context);
                }
                return _DUser;
            }
        }

	

	
    }
}