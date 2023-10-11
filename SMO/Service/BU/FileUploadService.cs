using SMO.Core.Entities.BU;
using SMO.Repository.Common;
using SMO.Repository.Implement.AD;
using SMO.Repository.Implement.BU;
using SMO.Service.Class;
using SMO.Service.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class FileUploadService : GenericService<T_BU_FILE_UPLOAD, FileUploadRepo>
    {
        public List<T_BU_LINK_UPLOAD> listLink;
        public List<string> lstLink { get; set; }
        public string ListFileDelete { get; set; }
        public int version { get; set; }
        public FileUploadService() : base()
        {
            listLink = new List<T_BU_LINK_UPLOAD>();
            lstLink = new List<string>();
        }
        public void GetListFiles()
        {
            IUnitOfWork UnitOfWork = new NHUnitOfWork();
            listLink = UnitOfWork.Repository<LinkUploadRepo>().Queryable().Where(x => x.PARENT == ObjDetail.PARENT && x.IS_DELETE != true&&x.VERSION==this.version).ToList();
            lstLink = UnitOfWork.Repository<LinkUploadRepo>().Queryable().Where(x => x.PARENT == ObjDetail.PARENT&&x.IS_DELETE!=true && x.VERSION == this.version).Select(x=>x.LINK).ToList();
            this.ObjList = this.CurrentRepository.Queryable().Where(x => x.PARENT == ObjDetail.PARENT && x.IS_DELETE != true && x.VERSION == this.version).ToList();
        }
        public void UpdateFile(HttpRequestBase Request)
        {
            try
            {
                // lưu file
                var lstFileStream = new List<FILE_STREAM>();
                for (int i = 0; i < Request.Files.AllKeys.Length; i++)
                {
                    var file = Request.Files[i];
                    lstFileStream.Add(new FILE_STREAM()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        FILE_OLD_NAME = file.FileName,
                        FILE_NAME = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                        FILE_EXT = Path.GetExtension(file.FileName),
                        FILE_SIZE = file.ContentLength,
                        FILESTREAM = Request.Files[i]                     
                    });
                }
                FileStreamService.InsertFile(lstFileStream);
                UnitOfWork.BeginTransaction();            
                foreach (var item in lstFileStream)
                {
                    var fileUpload = new T_BU_FILE_UPLOAD()
                    {
                        ID = item.PKID,
                        //CONNECTION_ID = systemConfigService.ObjDetail.CURRENT_CONNECTION,
                        //DATABASE_NAME = systemConfigService.ObjDetail.CURRENT_DATABASE_NAME,
                        FILE_EXT = item.FILE_EXT,
                        FILE_NAME = item.FILE_NAME,
                        FILE_OLD_NAME = item.FILE_OLD_NAME,
                        FILE_SIZE = item.FILE_SIZE,
                        DIRECTORY_PATH = item.DIRECTORY_PATH,
                        FULL_PATH = item.FULL_PATH,
                        PARENT = this.ObjDetail.PARENT,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        IS_DELETE = false,
                    };
                    UnitOfWork.Repository<FileUploadRepo>().Create(fileUpload);
                }
                if(lstLink.Count > 0)
                {
                    foreach (var item in lstLink)
                    {
                        var linkUpload = new T_BU_LINK_UPLOAD()
                        {
                            ID = Guid.NewGuid().ToString(),
                            LINK = item,
                            PARENT = this.ObjDetail.PARENT,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            IS_DELETE = false
                        };
                        UnitOfWork.Repository<LinkUploadRepo>().Create(linkUpload);
                    }

                }

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }
    }
 }